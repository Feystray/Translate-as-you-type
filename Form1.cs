using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoTrans
{
    public partial class Form1 : Form
    {
        private InterceptKeys interceptKeys;
        private bool keyarelogging = false;
        private bool startsentence = false;
        private bool tempstop = false;
        private WinEventProc proc = new WinEventProc(WinEventCallback);
        private static string activewindowName = "";
        private static string desiredwindowname = "";
        private static List<string> waitingkeys = [];
        private static Label keybinderbutton;


        //change
        public string startkey = "Enter";
        public string endkey = "`";
        public string clearkey = "None";
        public string pausekey = "None";
        public static bool viaclipboard;
        public static string Language = "English";
        public static string inputLanguage = "English";
        public static string model = "LLM";
        public static bool deletestartkey = false;
        public static bool pauseing = false;



        public Form1()
        {
            InitializeComponent();
            interceptKeys = new InterceptKeys();
            label5.Text += "";
            comboBox1.SelectedItem = "English";
            //comboBox3.SelectedItem = "Auto-Detect";
            //comboBox2.SelectedItem = "Gemini";
            //textBox1.Text = "note";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (keyarelogging)
            {
                interceptKeys.keypressed -= UpdateLog;
                interceptKeys.Stop();
                UnhookWinEvent(proc);
                keyarelogging = false;
                button4.Text = "\u25b6";
            }
            else if (!keyarelogging)
            {
                desiredwindowname = textBox1.Text.ToLower();
                IntPtr hookId = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, proc, 0, 0, 0);
                interceptKeys.keypressed += UpdateLog;
                viaclipboard = checkBox1.Checked;
                interceptKeys.Start();
                keyarelogging = true;
                button4.Text = "\u25fc";
            }
        }

        private void UpdateLog(string key)
        {
            if (!tempstop)
            {
                if (activewindowName.Contains(desiredwindowname))
                {
                    if (key == "Back")
                    {
                        if (label5.Text?.Length > 0 && startsentence)
                        {
                            int cursorpos = label5.SelectionStart;
                            label5.Text = label5.Text.Remove(cursorpos - 1, 1);
                            label5.SelectionStart = cursorpos - 1;
                        }
                    }
                    else if (key == endkey)
                    {
                        if (label5.Text != "")
                        {
                            tempstop = true;
                            Thread.Sleep(500);
                            label5.Text += "`";
                            if (activewindowName.Contains(desiredwindowname))
                            {
                                SendKeys.Send((deletestartkey ? "{BACKSPACE}" : "") + string.Concat(label5.Text.Select(c => "{BACKSPACE}")));
                                SendKeys.Send("...");
                                string translatedtext = TranslateLLM(label5.Text.Replace("`", "")).Result;
                                SendKeys.SendWait("\b\b\b");
                                if (!viaclipboard)
                                {
                                    SendKeys.Send(translatedtext + (translatedtext.Contains("Sorry, Translation Error (Please contact Developer) :") ? "" : "\n"));
                                }
                                else
                                {
                                    Clipboard.SetText(translatedtext);
                                    SendKeys.SendWait("^{v}" + (translatedtext.Contains("Sorry, Translation Error (Please contact Developer) :") ? "" : "\n"));
                                }
                                label5.Text = translatedtext;
                            }
                            else if (!activewindowName.Contains(desiredwindowname))
                            {
                                waitingkeys.Append(label5.Text);
                                string translatedtext = TranslateLLM(label5.Text.Replace("`", "")).Result;
                                if (!translatedtext.Contains("Sorry, Translation Error :"))
                                {
                                    waitingkeys.Append(translatedtext);
                                }
                                else
                                {
                                    waitingkeys = [];
                                }

                            }
                            startsentence = false;
                            tempstop = false;
                        }
                    }
                    else if (key == startkey)
                    {
                        if(!pauseing) label5.Text = "";
                        startsentence = true;
                    }
                    else if (key == clearkey)
                    {
                        label5.Text = "";
                        pauseing = false;
                        startsentence = false;
                    }
                    else if (key == pausekey && startsentence) { 
                        startsentence= false;
                        pauseing = true;
                    }
                    else if (key == "Right" && startsentence)
                    {
                        label5.SelectionStart = Math.Min(label5.SelectionStart + 1, label5.TextLength);
                    }
                    else if (key == "Left" && startsentence)
                    {
                        label5.SelectionStart = Math.Max(label5.SelectionStart - 1, 0);
                    }
                    else if (key == "Home" && startsentence)
                    {
                        label5.SelectionStart = 0;
                    }
                    else if (key == "End" && startsentence)
                    {
                        label5.SelectionStart = label5.TextLength;
                    }
                    else if (key == "Delete" && startsentence)
                    {
                        int cursorpos = label5.SelectionStart;
                        if (cursorpos != label5.TextLength)
                        {
                            label5.Text = label5.Text.Remove(cursorpos, 1);
                            label5.SelectionStart = cursorpos;
                        }
                    }
                    else
                    {
                        if (startsentence) label5.SelectedText = $"{key}";
                    }
                }
            }
        }

        //Sending Text to App
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        //Active window watcher
        [DllImport("user32")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, uint idProcess, uint idThread, uint dwflags);

        [DllImport("user32")]
        static extern bool UnhookWinEvent(WinEventProc lpfnWinEventProc);

        delegate void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        const uint EVENT_SYSTEM_FOREGROUND = 3;


        private static void WinEventCallback(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                activewindowName = Buff.ToString().ToLower();
                if (waitingkeys.Count != 0)
                {
                    if (activewindowName.Contains(desiredwindowname))
                    {
                        SendKeys.Send(string.Concat(waitingkeys[0].Select(c => "{BACKSPACE}")));
                        SendKeys.Send("...");
                        SendKeys.SendWait("\b\b\b");
                        if (!viaclipboard)
                        {
                            SendKeys.Send(waitingkeys[1] + "\n");
                        }
                        else
                        {
                            Clipboard.SetText(waitingkeys[1] + "\n");
                            SendKeys.SendWait("^{v}");
                        }
                        waitingkeys = [];
                    }
                }
            }
        }
        //end


        private static async Task<string> TranslateLLM(string inputtext)
        {
            string translatedText = "Sorry, Translation Error (Please contact Developer) : ";
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
                httpclient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                httpclient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                httpclient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                httpclient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                httpclient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                if (model == "LLM")
                {
                    string url = "https://sysapi.wordvice.ai/tools/non-member/fetch-llm-result";
                    string typedlang = inputLanguage;
                    if (inputLanguage == "Auto-Detect")
                    {
                        try
                        {
                            string urlt = "https://api.machinetranslation.com/v1/detect/language";
                            string postobj = $"{{\"text\":\"{inputtext}\"}}";
                            Task<HttpResponseMessage> resp = httpclient.PostAsync(urlt, new StringContent(postobj, encoding: System.Text.Encoding.UTF8, "application/json"));
                            resp.Result.EnsureSuccessStatusCode();
                            typedlang = await resp.Result.Content.ReadAsStringAsync();
                            typedlang = typedlang.Split("\"name\":\"")[1];
                            typedlang = typedlang.Split('\"')[0];
                        }
                        catch (Exception ex)
                        {
                            typedlang = "English";
                        }
                    }
                    string json = $"{{\"prompt\": \"Translate the following {typedlang} text into {Language}.\", \"text\": \"{inputtext}\",\"tool\": \"translate\"}}";
                    Task<HttpResponseMessage> response = httpclient.PostAsync(url, new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json"));
                    try
                    {
                        response.Result.EnsureSuccessStatusCode();
                        translatedText = await response.Result.Content.ReadAsStringAsync();
                        translatedText = Regex.Unescape(translatedText.Split("\"result\":")[1].Split("\"")[3]);
                    }
                    catch (Exception ex)
                    {
                        translatedText += inputtext + " : " + response.Result.Content.ReadAsStringAsync();
                    }
                }
                else if (model == "Gemini" || model == "Chatgpt")
                {
                    string url = "https://api.machinetranslation.com/v1/detect/language";
                    string typedlang = inputLanguage switch
                    {
                        "English" => "en",
                        "Hindi" => "hi",
                        "Thai" => "th",
                        "Japanese" => "ja",
                        "Spanish" => "es",
                        _ => "Auto-Detect"
                    };
                    if (inputLanguage == "Auto-Detect")
                    {
                        try
                        {
                            string postobj = $"{{\"text\":\"{inputtext}\"}}";
                            Task<HttpResponseMessage> resp = httpclient.PostAsync(url, new StringContent(postobj, encoding: System.Text.Encoding.UTF8, "application/json"));
                            resp.Result.EnsureSuccessStatusCode();
                            typedlang = await resp.Result.Content.ReadAsStringAsync();
                            typedlang = typedlang.Split("\"code\":\"")[1];
                            typedlang = typedlang.Split('\"')[0];
                        }
                        catch (Exception ex)
                        {
                            typedlang = "en";
                        }
                    }

                    string tolang = Language switch
                    {
                        "English" => "en",
                        "Hindi" => "hi",
                        "Thai" => "th",
                        "Japanese" => "ja",
                        "Spanish" => "es"
                    };

                    url = "https://api.machinetranslation.com/v1/translation/share-id";
                    string json = $"{{\"text\":\"{inputtext}\",\"source_language_code\":\"{typedlang}\",\"target_language_code\":\"{tolang}\",\"s3_file_path\": null,\"total_words\":null}}";
                    Task<HttpResponseMessage> response = httpclient.PostAsync(url, new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json"));
                    try
                    {
                        response.Result.EnsureSuccessStatusCode();
                        string shareid = await response.Result.Content.ReadAsStringAsync();
                        shareid = shareid.Split("\"")[3];
                        url = $"https://api.machinetranslation.com/v1/translation/{model switch { "Gemini" => "gemini", "Chatgpt" => "chat-gpt" }}";
                        json = $"{{\"text\":\"{inputtext}\",\"source_language_code\":\"{typedlang}\",\"target_language_code\":\"{tolang}\",\"share_id\":\"{shareid}\"}}";
                        response = httpclient.PostAsync(url, new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json"));
                        response.Result.EnsureSuccessStatusCode();
                        translatedText = await response.Result.Content.ReadAsStringAsync();
                        translatedText = translatedText.Split("translated_text\":\"")[1];
                        translatedText = translatedText.Split($"{model switch { "Gemini" => "\\n", "Chatgpt" => "" }}\",\"meta\":")[0];
                    }
                    catch (Exception ex) { translatedText += " : " + ex.Message; }
                }
            }

            return translatedText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label buttonobject = (Label)sender;
            buttonobject.Focus();
            buttonobject.Text = "Press a key to bind";
            keybinderbutton = buttonobject;
            interceptKeys.keypressed += setkeybind;
            interceptKeys.Start();
        }

        private void setkeybind(string key)
        {
            keybinderbutton.Text = key;
            if (keybinderbutton.Name == "label9")
            {
                startkey = key;
            }
            else if (keybinderbutton.Name == "label11") { clearkey = key; }
            else if (keybinderbutton.Name == "label13") { pausekey = key;  }
            else
            {
                endkey = key;
            }
            interceptKeys.keypressed -= setkeybind;
            interceptKeys.Stop();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Language = comboBox1.SelectedItem.ToString();
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            inputLanguage = comboBox3.SelectedItem.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            model = comboBox2.SelectedItem.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            deletestartkey = checkBox2.Checked;
        }
    }
}
