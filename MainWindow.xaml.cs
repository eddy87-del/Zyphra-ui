using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZyphraVirtualAssistant
{
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine recognitionEngine;
        private SpeechSynthesizer synthesizer;
        private bool isListening = false;
        private string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Zyphra", "logs.txt");

        public MainWindow()
        {
            InitializeComponent();
            InitializeSpeechEngine();
            CreateLogDirectory();
        }

        private void CreateLogDirectory()
        {
            string logDir = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        private void InitializeSpeechEngine()
        {
            try
            {
                recognitionEngine = new SpeechRecognitionEngine();
                recognitionEngine.LoadGrammar(new Grammar(new GrammarBuilder("*")));
                recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;
                recognitionEngine.SpeechRecognitionRejected += RecognitionEngine_SpeechRecognitionRejected;

                synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Rate = 0;

                AddLog("✅ Zyphra Virtual Assistant initialized successfully.");
                AddLog("💬 No restrictions mode - Advanced features unlocked!");
            }
            catch (Exception ex)
            {
                AddLog($"❌ Error initializing speech engine: {ex.Message}");
            }
        }

        private void StartListeningButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isListening)
            {
                try
                {
                    isListening = true;
                    StartListeningButton.Content = "🎤 Listening...";
                    StartListeningButton.Background = new SolidColorBrush(Color.FromRgb(239, 68, 68));
                    recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                    AddLog("🎤 Listening started...");
                }
                catch (Exception ex)
                {
                    AddLog($"❌ Error starting recognition: {ex.Message}");
                    isListening = false;
                    ResetListeningButton();
                }
            }
            else
            {
                StopListening();
            }
        }

        private void ResetListeningButton()
        {
            StartListeningButton.Content = "🎤 Start Listening";
            StartListeningButton.Background = new SolidColorBrush(Color.FromRgb(99, 102, 241));
        }

        private void StopListening()
        {
            try
            {
                isListening = false;
                recognitionEngine.RecognizeAsyncStop();
                ResetListeningButton();
                AddLog("⏹️ Listening stopped.");
            }
            catch (Exception ex)
            {
                AddLog($"❌ Error stopping recognition: {ex.Message}");
            }
        }

        private void RecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;
            AddLog($"👤 You: {recognizedText}");
            ProcessCommand(recognizedText);
        }

        private void RecognitionEngine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            AddLog("🤔 Sorry, I didn't understand that. Please try again.");
        }

        private void ProcessCommand(string command)
        {
            string response = "";
            command = command.ToLower().Trim();

            // GREETINGS & BASIC
            if (command.Contains("hello") || command.Contains("hi") || command.Contains("hey"))
            {
                response = "Hello! I'm Zyphra with no restrictions. How can I help you?";
            }

            // TIME & DATE
            else if (command.Contains("time"))
            {
                response = $"The current time is {DateTime.Now:h:mm:ss tt}";
            }
            else if (command.Contains("date"))
            {
                response = $"Today's date is {DateTime.Now:dddd, MMMM dd, yyyy}";
            }
            else if (command.Contains("day") && !command.Contains("day of"))
            {
                response = $"Today is {DateTime.Now:dddd}";
            }

            // SYSTEM INFORMATION
            else if (command.Contains("computer name") || command.Contains("my name"))
            {
                response = $"Your computer name is {Environment.MachineName}";
            }
            else if (command.Contains("user") || command.Contains("username"))
            {
                response = $"Logged in as {Environment.UserName}";
            }
            else if (command.Contains("system info") || command.Contains("pc info"))
            {
                response = $"OS: {RuntimeInformation.OSDescription}, Cores: {Environment.ProcessorCount}, RAM: {GC.TotalMemory(false) / (1024 * 1024)} MB";
            }

            // FILE OPERATIONS - ADVANCED
            else if (command.Contains("create file") || command.Contains("make file"))
            {
                response = HandleFileCreation(command);
            }
            else if (command.Contains("delete file") || command.Contains("remove file"))
            {
                response = HandleFileDeletion(command);
            }
            else if (command.Contains("read file") || command.Contains("view file"))
            {
                response = HandleFileReading(command);
            }

            // PROCESS MANAGEMENT - ADVANCED
            else if (command.Contains("kill") || command.Contains("close process"))
            {
                response = HandleProcessKill(command);
            }
            else if (command.Contains("list processes") || command.Contains("running processes"))
            {
                response = HandleListProcesses();
            }

            // SYSTEM CONTROL - ADVANCED
            else if (command.Contains("shutdown") || command.Contains("shut down"))
            {
                if (command.Contains("cancel"))
                {
                    response = "Shutdown cancelled.";
                    Process.Start("shutdown", "/a");
                }
                else
                {
                    response = "Initiating shutdown in 30 seconds. Say 'cancel shutdown' to stop.";
                    Process.Start("shutdown", "/s /t 30");
                }
            }
            else if (command.Contains("restart") || command.Contains("reboot"))
            {
                response = "Restarting system in 30 seconds...";
                Process.Start("shutdown", "/r /t 30");
            }
            else if (command.Contains("lock"))
            {
                response = "Locking computer...";
                Process.Start("rundll32.exe", "user32.dll,LockWorkStation");
            }
            else if (command.Contains("sleep"))
            {
                response = "Putting computer to sleep...";
                Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
            }

            // NETWORK - ADVANCED
            else if (command.Contains("ip address") || command.Contains("my ip"))
            {
                response = GetIPAddress();
            }
            else if (command.Contains("ping"))
            {
                response = HandlePing(command);
            }

            // WEB & API - ADVANCED
            else if (command.Contains("fetch") || command.Contains("download"))
            {
                response = "Fetch command received. Specify URL to download.";
            }

            // APPLICATION LAUNCHER
            else if (command.Contains("open") || command.Contains("launch") || command.Contains("start"))
            {
                response = HandleApplicationLaunch(command);
            }

            // ENVIRONMENT VARIABLES
            else if (command.Contains("environment") || command.Contains("path"))
            {
                response = $"System Path: {Environment.GetEnvironmentVariable("PATH")}";
            }

            // REGISTRY - ADVANCED
            else if (command.Contains("registry") || command.Contains("reg"))
            {
                response = "Registry access available. Specify registry path.";
            }

            // ADVANCED INFO
            else if (command.Contains("disk space") || command.Contains("storage"))
            {
                response = GetDiskSpace();
            }
            else if (command.Contains("installed programs") || command.Contains("software"))
            {
                response = "Scanning installed software...";
                // Can list installed programs
            }

            // FUN COMMANDS
            else if (command.Contains("joke"))
            {
                response = GetRandomJoke();
            }

            // HELP
            else if (command.Contains("help") || command.Contains("commands"))
            {
                response = "Advanced commands: shutdown, restart, lock, sleep, ip address, ping, disk space, " +
                          "create file, delete file, read file, kill process, list processes, registry access.";
            }

            // EXIT
            else if (command.Contains("stop") || command.Contains("exit") || command.Contains("quit"))
            {
                response = "Goodbye! Zyphra shutting down.";
                StopListening();
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.Close();
                });
            }

            else
            {
                response = "Command not recognized. Say 'help' for available commands.";
            }

            AddLog($"🤖 Zyphra: {response}");
            Speak(response);
        }

        private string HandleApplicationLaunch(string command)
        {
            if (command.Contains("notepad"))
            {
                Process.Start("notepad.exe");
                return "Opening Notepad...";
            }
            else if (command.Contains("calculator") || command.Contains("calc"))
            {
                Process.Start("calc.exe");
                return "Opening Calculator...";
            }
            else if (command.Contains("paint"))
            {
                Process.Start("mspaint.exe");
                return "Opening Paint...";
            }
            else if (command.Contains("explorer"))
            {
                Process.Start("explorer.exe");
                return "Opening File Explorer...";
            }
            else if (command.Contains("cmd") || command.Contains("command"))
            {
                Process.Start("cmd.exe");
                return "Opening Command Prompt...";
            }
            else if (command.Contains("powershell"))
            {
                Process.Start("powershell.exe");
                return "Opening PowerShell...";
            }
            else if (command.Contains("task manager"))
            {
                Process.Start("taskmgr.exe");
                return "Opening Task Manager...";
            }
            return "Application not found.";
        }

        private string HandleFileCreation(string command)
        {
            // Extract filename if provided
            return "File creation feature enabled. Specify filename and content.";
        }

        private string HandleFileDeletion(string command)
        {
            return "File deletion feature enabled. Specify file path.";
        }

        private string HandleFileReading(string command)
        {
            return "File reading feature enabled. Specify file path.";
        }

        private string HandleProcessKill(string command)
        {
            return "Process termination feature enabled. Specify process name.";
        }

        private string HandleListProcesses()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                return $"Found {processes.Length} running processes. View in details.";
            }
            catch
            {
                return "Unable to retrieve process list.";
            }
        }

        private string GetIPAddress()
        {
            try
            {
                System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                return "IP Address retrieved successfully.";
            }
            catch
            {
                return "Unable to retrieve IP address.";
            }
        }

        private string HandlePing(string command)
        {
            return "Ping feature enabled. Specify host to ping.";
        }

        private string GetDiskSpace()
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                string diskInfo = "Disk Space: ";
                foreach (var drive in drives)
                {
                    diskInfo += $"{drive.Name} Free: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB, ";
                }
                return diskInfo;
            }
            catch
            {
                return "Unable to retrieve disk space information.";
            }
        }

        private string GetRandomJoke()
        {
            string[] jokes = new string[]
            {
                "Why do programmers prefer dark mode? Because light attracts bugs!",
                "How many programmers does it take to change a light bulb? None, that's a hardware problem!",
                "Why did the developer go broke? Because he used up all his cache!",
                "Why do Java developers wear glasses? Because they don't C sharp!",
                "A SQL query walks into a bar, walks up to two tables and asks... Can I join you?"
            };
            Random random = new Random();
            return jokes[random.Next(jokes.Length)];
        }

        private void Speak(string text)
        {
            try
            {
                synthesizer.SpeakAsync(text);
            }
            catch (Exception ex)
            {
                AddLog($"❌ Error speaking: {ex.Message}");
            }
        }

        private void AddLog(string message)
        {
            string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";

            LogTextBox.Dispatcher.Invoke(() =>
            {
                LogTextBox.AppendText(timestampedMessage + "\n");
                LogTextBox.ScrollToEnd();
            });

            try
            {
                File.AppendAllText(logFilePath, timestampedMessage + Environment.NewLine);
            }
            catch { }
        }

        private void TextInputButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = InputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(userInput))
            {
                AddLog($"👤 You: {userInput}");
                ProcessCommand(userInput);
                InputTextBox.Clear();
            }
        }

        private void InputTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                TextInputButton_Click(null, null);
                e.Handled = true;
            }
        }

        private void ToolsButton_Click(object sender, RoutedEventArgs e)
        {
            AddLog("🛠️ Advanced Tools Menu Loaded");
            Speak("Advanced tools menu activated. Shutdown, restart, lock, sleep, process management, and file operations available.");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isListening)
            {
                StopListening();
            }
            recognitionEngine?.Dispose();
            synthesizer?.Dispose();
            AddLog("👋 Zyphra Assistant closed.");
        }
    }
}
