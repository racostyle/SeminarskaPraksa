using System.Diagnostics;

namespace SeminarskaPraksa.DotnetProcess
{
    public static class Dotnet
    {
        #region EXAMPLES
        public async static Task Example_Run_LaunchExternalProcess(string dllLocation, bool hideWindow = false, Action<string>? outputCallback = null)
        {
            await Console.Out.WriteLineAsync("Process started");
            var savePath = Path.Combine(GlobalFolder.Location, "processInfo.txt");

            Run_LaunchExternalProcess(dllLocation, hideWindow, true, savePath, outputCallback);
            await Task.Delay(4000);
            ProcessHandler.KillProcess_ByPIDAndTimeCreated(savePath);
        }

        public async static Task Example_Run_ExecuteAssemblyAsync(string dllLocation)
        {
            await Console.Out.WriteLineAsync("Process started");
            await Run_ExecuteAssemblyAsync(dllLocation, callback: () =>
            {
                Console.WriteLine("Task completed");
            });
        }

        public async static Task Example_Run_LaunchAssembly(string dllLocation)
        {
            await Console.Out.WriteLineAsync("Process started");
            var process = Run_LaunchAssembly(dllLocation);

            await Task.Delay(4000);
            try
            {
                process.Kill();
                process.Dispose();
                await Console.Out.WriteLineAsync("Process killed");
            }
            catch { }
            await Task.Delay(2000);
        }
        #endregion EXAMPLES

        #region EXTERNAL PROCESS
        /// <summary>
        /// Initiates a .NET assembly as a separate process with configurable window visibility and administrator privileges, returns process 
        /// ID and start time, supports asynchronous output/error stream capture, and ensures automatic process cleanup.
        /// </summary>
        /// <param name="assemblyPath">The path to the .NET assembly (.dll or .exe) to be executed.</param>
        /// <param name="hideWindow">Determines whether the window of the separate process should be hidden. Set to <c>false</c> to show the window and <c>true</c> to hide it, enabling asynchronous capture of output and error streams.</param>
        /// <param name="runAsAdmin">Specifies whether the process should be run with administrator privileges. Set to <c>true</c> to enable administrator mode.</param>
        /// <param name="infoSaveLocation">Optional. The file path where the process information (e.g., process ID and start time) will be saved. If left empty, the information is not saved.</param>
        /// <param name="outputCallback">Optional. An action delegate that receives output from the process's standard output and error streams. This parameter is only effective when <paramref name="hideWindow"/> is set to <c>true</c>.</param>
        /// <returns>An array containing the process ID and start time as strings. The first element is the process ID, and the second element is the start time.</returns>
        public static string[] Run_LaunchExternalProcess(
            string assemblyPath,
            bool hideWindow = false,
            bool runAsAdmin = false,
            string infoSaveLocation = "",
            Action<string>? outputCallback = null)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = assemblyPath,
                UseShellExecute = !hideWindow, //if hideWindow = true it must be false to redirect output
                CreateNoWindow = hideWindow,
                RedirectStandardOutput = hideWindow,
                RedirectStandardError = hideWindow,
                Verb = runAsAdmin ? "runAs" : string.Empty,
            };

            var process = new Process();
            process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;

            if (outputCallback != null)
                SubscribeToOutputStream(process, outputCallback);

            process.Exited += (sender, e) =>
            {
                process.Dispose();
            };

            process.Start();
            ProcessHandler.SaveProcessInfo(process, infoSaveLocation);

            if (hideWindow)
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            return new string[] { process.Id.ToString(), process.StartTime.ToString() };
        }
        #endregion

        #region INTERNAL PROCESS
        /// <summary>
        /// Asynchronously executes a .NET assembly, automatically manages the process lifecycle, captures output and error streams, and invokes 
        /// a callback upon completion.
        /// </summary>
        /// <param name="assemblyPath">The file path of the .NET assembly to execute.</param>
        /// <param name="args">Optional arguments to pass to the assembly. Defaults to an empty string if no arguments are provided.</param>
        /// <param name="callback">An optional callback action that is invoked when the process exits. Can be <c>null</c>, in which case no action is taken on</param>
        /// <returns>A task that represents the asynchronous operation of running the .NET assembly and awaiting its completion.</returns>
        public static async Task Run_ExecuteAssemblyAsync(string assemblyPath, string args = "", Action? callback = null)
        {
            string result = string.Empty;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet", // Use the dotnet runtime
                Arguments = $"{assemblyPath} {args}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runAs"
            };

            var tcs = new TaskCompletionSource<object>();

            using (var process = new Process())
            {
                process.StartInfo = startInfo;
                process.EnableRaisingEvents = true;
                SubscribeToOutputStream(process, (value) => Console.WriteLine("Error: " + value));

                process.Exited += (sender, e) =>
                {
                    callback?.Invoke();
                    process.Dispose();
                    tcs.SetResult(null);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for the process to exit
                await tcs.Task;
            }
        }

        /// <summary>
        /// Starts a .NET assembly as an ongoing process, returns the Process object for external management, captures output and error streams, 
        /// and allows for an optional callback on process exit.
        /// </summary>
        /// <param name="assemblyPath">The path to the .NET assembly (.dll or .exe) to be executed.</param>
        /// <param name="args">Optional arguments to pass to the assembly. Defaults to an empty string if no arguments are provided.</param>
        /// <param name="callback">An optional callback action that is invoked when the process exits. Can be <c>null</c>, in which case no action is taken on 
        /// process exit.</param>
        /// <returns>The started <see cref="Process"/> object, allowing the caller to control and monitor the process's execution and lifecycle.</returns>
        public static Process Run_LaunchAssembly(string assemblyPath, string args = "", Action? callback = null)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet", // Use the dotnet runtime
                Arguments = $"{assemblyPath} {args}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Verb = "runAs"
            };

            var process = new Process();
            process.StartInfo = startInfo;
            process.EnableRaisingEvents = true;

            SubscribeToOutputStream(process, (value) => Console.WriteLine("Error: " + value));

            var tcs = new TaskCompletionSource<object>();

            process.Exited += (sender, e) =>
            {
                callback?.Invoke();
                // Do not close or dispose of the process here, as we want to return it to the caller.
                tcs.SetResult(null);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // This task completes when the process exits, but we do not await it here to allow the caller to manage the process.
            // We just want to aknowledge the task
            var ignored = tcs.Task;

            return process;
        }
        #endregion

        #region AUXILARY
        private static void SubscribeToOutputStream(Process process, Action<string> writer)
        {
            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    writer(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    writer(e.Data);
                }
            };
        }
        #endregion
    }
}
