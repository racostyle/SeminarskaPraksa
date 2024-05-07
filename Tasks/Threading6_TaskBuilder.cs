using SeminarskaPraksa.AsyncTasks;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading6_TaskBuilder
    {
        private readonly Action<string> _writer;

        public Threading6_TaskBuilder(Action<string> writer)
        {
            _writer = writer;
        }

        internal async Task StartTasks(List<IAsyncTask> tasks, int timeout)
        {
            _writer("Začetek");
            var taskBuilder = new TaskBuilder(_writer);
            List<Task> builtTasks = new List<Task>();
            foreach (IAsyncTask task in tasks)
                builtTasks.Add(taskBuilder.BuildTask(task, timeout));

            await Task.WhenAll(builtTasks);
            _writer("Konec");
        }
    }

    internal class TaskBuilder
    {
        private Action<string> _writer;

        public TaskBuilder(Action<string> writer)
        {
            _writer = writer;
        }

        internal async Task BuildTask(IAsyncTask task, int timeoutInSeconds)
        {
            var tcs = new TaskCompletionSource<string>();
            var cts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                cts.CancelAfter(TimeSpan.FromSeconds(timeoutInSeconds));

                var registration = cts.Token.Register(() =>
                {
                    if (!tcs.Task.IsCompleted)
                    {
                        tcs.TrySetResult(string.Empty);
                       _writer("Timeout!");
                    }
                });

                try
                {
                    var result = await MainTaskAsync(task, tcs);
                    _writer(result);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex); 
                }
                finally
                {
                    registration.Dispose();
                    cts.Dispose();
                }
            });

            await tcs.Task;
        }

        private async Task<string> MainTaskAsync(IAsyncTask task, TaskCompletionSource<string> tcs)
        {
            var result = await task.RunAsync();
            if (!string.IsNullOrEmpty(result)) 
                tcs.TrySetResult(result);
            return result;
        }
    }
}
