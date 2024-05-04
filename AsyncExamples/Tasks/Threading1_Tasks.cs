namespace AsyncExamples.Tasks
{
    internal class Threading1_Tasks
    {
        private Action<string> _printInTextbox;
        private bool isActive = true;

        public Threading1_Tasks(Action<string> printInTextbox)
        {
            _printInTextbox = printInTextbox;

            SimpleCounter(250, 1500);
            _printInTextbox("Lambda primer");
            LambdaExample();
            _printInTextbox("SimpleTask start");
            SimpleTask(1000);
            _printInTextbox("SimpleTaskAsync start");
            SimpleTaskAsync(1000);
        }
        #region TIMER
        private void SimpleCounter(int timerTick, int timerLimit)
        {
            Task.Run(async () =>
            {
                while (isActive)
                {
                    _printInTextbox(".");
                    await Task.Delay(timerTick);
                }
            });

            Task.Run(() =>
            {
                Task.Delay(timerLimit).Wait();
                isActive = false;
                _printInTextbox("Threading1_Tasks Stopped");
            });
        }
        #endregion

        #region LAMBDA EXAMPLE
        private void LambdaExample()
        {
            //1
            WriteToConsole("1");
            //2
            Action<string> action = WriteToConsole;
            action("2");
            //3
            action?.Invoke("3");
            //4
            Action<string> action2 = (message) =>
            {
                _printInTextbox(message);
            };
            action2.Invoke("4");
        }

        private void WriteToConsole(string message)
        {
            _printInTextbox(message);
        }
        #endregion

        #region SIMPLE TASK
        //this will wait untill task is completed. Napiš zakaj to ni najbolši dizajn. Brez async je bol primerno za task, ki ga ne čakamo
        private void SimpleTask(int time)
        {
            Task.Run(() =>
            {
                Task.Delay(time).Wait();
                _printInTextbox("Simple task completed");
            });
        }
        #endregion

        #region ASYNC TASK
        //this will create a new thread and will run async
        private void SimpleTaskAsync(int time)
        {
            Task.Run(async () =>
            {
                await Task.Delay(time);
                _printInTextbox("Async task completed");
            });
        }
        #endregion

        #region ASYNC TASK
        //this will create a new thread and will run async
        internal async Task SimpleTaskAsyncMethod(int time)
        {
            await Task.Delay(time);
            _printInTextbox("Async method completed");
        }
        #endregion
    }
}
