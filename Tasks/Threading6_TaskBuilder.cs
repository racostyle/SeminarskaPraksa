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
            var builtTasks = taskBuilder.BuildTasks(tasks, timeout);

            await Task.Delay(3000);

            List<Task> runningTasks = new List<Task>();
            foreach (var task in builtTasks)
            {
                runningTasks.Add(task.Value);
            }

            await Task.WhenAll(runningTasks);

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
        internal List<Lazy<Task>> BuildTasks(List<IAsyncTask> tasks, int timeout)
        {
            List<Lazy<Task>> createdTasks = new List<Lazy<Task>>();
            foreach (IAsyncTask task in tasks)
                createdTasks.Add(CreateTask(task, timeout));
            return createdTasks;
        }

        private Lazy<Task> CreateTask(IAsyncTask task, int timeoutInSeconds)
        {
            return new Lazy<Task>(async () =>
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
            });
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
