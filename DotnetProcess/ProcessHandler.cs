using System.Diagnostics;
using System.Text;

namespace SeminarskaPraksa.DotnetProcess
{
    internal class ProcessHandler
    {
        internal static readonly int PID = 0;
        internal static readonly int CREATON_TIME = 1;

        #region KILL PROCESS BY PID AND CRATE TIME
        /// <summary>
        /// Will search trough all opened procesess and kill a process with PID and TimeCreated saved in <paramref name="fileLocation"/> if processs has same 
        /// PID and TimeCreated
        /// </summary>
        public static void KillProcess_ByPIDAndTimeCreated(string fileLocation)
        {
            if (!File.Exists(fileLocation))
                return;

            var info = LoadProcessInfo(fileLocation);

            foreach (var process in Process.GetProcesses())
            {
                if (process.Id.ToString() == info[PID])
                {
                    // Check if the process has the same metadata
                    if (process.StartTime.ToString() == info[CREATON_TIME])
                    {
                        try
                        {
                            if (!process.HasExited)
                            {
                                process.Kill();
                                Console.WriteLine("Process terminated!");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Failed to kill the process: {e.Message}");
                        }
                        finally
                        {
                            process.Dispose();
                        }
                        File.Delete(fileLocation);
                        break;
                    }
                }
            }
        }

        public static void SaveProcessInfo(Process process, string infoSaveLocation)
        {
            if (string.IsNullOrEmpty(infoSaveLocation))
                return;

            var startTime = process.StartTime.ToString();
            File.WriteAllText(infoSaveLocation, $"{process.Id}|{startTime}");
        }

        private static string[] LoadProcessInfo(string fileLocation)
        {
            try
            {
                return File.ReadAllText(fileLocation).Split("|");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem with reading the file: {e}");
            }
            return new string[] { };
        }
        #endregion

        #region KILL PROCESS BY NAME
        /// <summary>
        /// Generates a PowerShell command to forcefully terminate processes by name, allowing for inclusion (name matches) and exclusion (name does not match prefixed with "!") criteria. 
        /// After Runs a powershell script and executes said command. It will kill all proceses which respect naming conditions
        /// <para>
        /// Example of arguments: ("test", "!production") will generate a script that kills all processes that contain "test" but do not contain "production"
        /// </para>
        /// </summary> 
        public static async Task KillProcess_ByName(params string[] args)
        {
            var command = GenerateDeleteProcesesByNameCommand(args);
            await ExecuteInBackgroundAsync(command, true);
        }

        private static async Task ExecuteInBackgroundAsync(string command, bool asAdmin)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = command,
                Verb = asAdmin ? "runAs" : string.Empty,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = info;
                await Task.Run(() =>
                {
                    process.Start();
                    process.WaitForExit();
                });
            }
        }

        /// <summary>
        /// Generates a PowerShell command to forcefully terminate processes by name, allowing for inclusion (name matches) and exclusion (name does not match prefixed with "!") criteria.
        /// <para>
        /// Example of arguments: ("test", "!production") will generate a script that kills all processes that contain "test" but do not contain "production"
        /// </para>
        /// </summary> 
        private static string GenerateDeleteProcesesByNameCommand(params string[] args)
        {
            var like = args.Where(x => !x.StartsWith("!")).Distinct().ToArray();
            var notLike = args.Where(x => x.StartsWith("!")).Distinct().ToArray();

            var builder = new StringBuilder();
            builder.Append("Get-Process | Where-Object { (");
            for (int i = 0; i < like.Length; i++)
            {
                builder.Append($"$_.ProcessName -like '*{like[i]}*'");
                if (i < like.Length - 1)
                    builder.Append(" -or ");
            }
            if (notLike.Any())
                builder.Append(") -and (");
            for (int i = 0; i < notLike.Length; i++)
            {

                builder.Append($"$_.ProcessName -notlike '*{notLike[i].Replace("!", string.Empty)}*'");
                if (i < notLike.Length - 1)
                    builder.Append(" -or ");
            }

            builder.Append(") } | Stop-Process -Force\r\n");
            return builder.ToString();
        }

        //Get-Process | Where-Object { ($_.ProcessName -like 'notepad' -or $_.ProcessName -like '*winword*') -and $_.ProcessName -notlike '*wordpad*' } | Stop-Process -Force
        #endregion

        #region KILL PROCESS BY WINDOW STYLE
        internal async static Task FindProcesesByWindowStyle()
        {
            var command = "Get-Process | Where-Object { $_.MainWindowTitle -ne \"\" } | Select-Object Id, Name, MainWindowTitle";
            await ExecuteInBackgroundAsync(command, true);
        }
        #endregion
    }
}
