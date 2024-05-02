namespace SeminarskaPraksa
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var task1 = new Threading1_Tasks();

            var task2 = Task.Run(task1.SimpleTaskAsyncMethod);

            //what if we want to waith for a task?
            await task1.SimpleTaskAsyncMethod();

            while (true)
            {
                await Task.Delay(500);
                Console.WriteLine("delay");
            }
        }
    }
}
