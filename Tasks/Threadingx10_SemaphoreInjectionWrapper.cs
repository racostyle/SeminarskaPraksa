using SeminarskaPraksa.Utilities;

namespace SeminarskaPraksa.Tasks
{
    internal class Threadingx10_SemaphoreInjectionWrapper
    {
        TextBoxLogger _textBoxLogger;

        public Threadingx10_SemaphoreInjectionWrapper(TextBoxLogger textBoxLogger)
        {
            _textBoxLogger = textBoxLogger;

        }

        internal async Task ExecuteAsync(int semaphoreLimit)
        {
            using (var cts = new CancellationTokenSource())
            {
                var token = cts.Token;
                var semaphore = new SemaphoreSlim(6);

                var executionTasks = new List<TaskWrapper>();
                var builder = new WrappedTaskBuilder();

                var skupineZaIzvajanje = new List<VerificationGroupExecutor>();
                for (int i = 0; i < semaphoreLimit * 3; i++)
                    skupineZaIzvajanje.Add(new VerificationGroupExecutor(_textBoxLogger, $"skupina {i}"));

                foreach (var skupina in skupineZaIzvajanje)
                {
                    var task = builder.CreateWrappedTask(skupina, semaphore, token);
                        executionTasks.Add(task);
                }

                foreach (var group in executionTasks)
                    await group.RunAsync();
            }
        }
    }

    internal class WrappedTaskBuilder
    {
        public TaskWrapper CreateWrappedTask(
        VerificationGroupExecutor groupExecutor,
        SemaphoreSlim semaphore,
        CancellationToken token)
        {
            var task = new Task(async () => {
                try
                {
                    if (token.IsCancellationRequested)
                        return;
                    await groupExecutor.ExecuteAsync(token);
                }
                catch (Exception) { }
            }, token);

            return new TaskWrapper(task, semaphore);
        }
    }

    //Ta razred je samo simulacija. V realni kodi ta razred vesebuje več nalog z dostopom do baz (drop in restore),
    //potem dostop do api klicev in na koncu šeen dostp do baz (drop)
    internal class VerificationGroupExecutor
    {
        private List<Task> _tasks;

        public VerificationGroupExecutor(TextBoxLogger logger, string groupName)
        {
            var random = new Random();
            _tasks = new List<Task>();

            for (int i = 1; i < 3; i++)
            {
                int id = i;
                _tasks.Add(new Task(async () => 
                {
                    int localId = id;
                    await Task.Delay(random.Next(1,4) * 1000);
                    logger.Log($"{groupName}: naloga {localId} končana");
                } ));
            }
        }

        //tukaj je zgolj demonstracija žetona. V resnem programiranju bi zadeve integrirali precej boljše
        public async Task ExecuteAsync(CancellationToken token) 
        {
            foreach (var task in _tasks)
            {
                if (token.IsCancellationRequested)
                    break;
                task.Start();
                await task;
            }
        }
    }

    internal class TaskWrapper
    {
        private Task _task;
        private SemaphoreSlim _semaphore;

        public TaskWrapper(Task task, SemaphoreSlim semaphore)
        {
            _task = task;
            _semaphore = semaphore;
        }

        internal async Task RunAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                _task.Start();
                await _task;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

}
