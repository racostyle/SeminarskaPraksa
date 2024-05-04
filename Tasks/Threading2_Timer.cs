namespace SeminarskaPraksa.Tasks
{
    internal class Threading2_Timer : IDisposable
    {
        private bool _isActive;
        private List<Task> _tasks;

        public Threading2_Timer(Action<string> printInTextbox, int timerTick, int timerDuration)
        {
            _isActive = true;

            var task1 = Task.Run(async () =>
            {
                while (_isActive)
                {
                    printInTextbox(".");
                    await Task.Delay(timerTick);
                }
            });

            var task2 = Task.Run(async () =>
            {
                await Task.Delay(timerDuration);
                _isActive = false;
                printInTextbox("Threading2_Tmer Stopped");
            });

            _tasks = new List<Task>()
            {
                task1,
                task2
            };
        }

        public async Task WaitForComplete()
        {
            await Task.WhenAll(_tasks);
        }

        public void Dispose()
        {

        }
    }
}
