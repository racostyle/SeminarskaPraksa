namespace SeminarskaPraksa
{
    internal class Threading1_Tasks
    {
        public Threading1_Tasks()
        {
            LambdaExample();
            SimpleTask();
            SimpleTaskAsync();
        }

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
                Console.WriteLine(message);
            };
            action2.Invoke("4");
        }

        private void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
        #endregion

        #region SIMPLE TASK
        //this will wait untill task is completed
        private void SimpleTask()
        {
            Task.Run(() =>
            {
                Task.Delay(2000);
                Console.WriteLine("Simple task completed");
            });
        }
        #endregion

        #region ASYNC TASK
        //this will create a new thread and will run async
        private void SimpleTaskAsync()
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Async task completed");
            });
        }
        #endregion

        #region ASYNC TASK
        //this will create a new thread and will run async
        internal async Task SimpleTaskAsyncMethod()
        {
            await Task.Delay(2000);
            Console.WriteLine("Async method completed");
        }
        #endregion
    }
}
