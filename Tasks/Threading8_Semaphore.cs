using SeminarskaPraksa.Utilities;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading8_Semaphore
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly TextBoxLogger _logger;

        public Threading8_Semaphore(TextBoxLogger logger, int numberOfCuncurentTasks)
        {
            _logger = logger;
            _semaphore = new SemaphoreSlim(numberOfCuncurentTasks);
        }

        internal async Task Start(int noOfTasks)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfTasks; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(async () => await AccessDatabase(taskId))); 
            }

            await Task.WhenAll(tasks);
            _logger.Log("All tasks completed.");
        }

        private async Task AccessDatabase(int taskId)
        {
            _logger.Log($"Task {taskId} is requesting access to the database.");

            // Request access by waiting to enter the semaphore.
            _semaphore.Wait();
            try
            {
                _logger.Log($"* Task {taskId} has entered the database.");
                await Task.Delay(1000);
            }
            finally
            {
                // Release the semaphore.
                _logger.Log($"Task {taskId} is leaving the database.");
                _semaphore.Release();
            }
        }
    }
}
