namespace SeminarskaPraksa.AsyncTasks
{
    internal class VeryLongTaskAsync : ITasks
    {
        public async Task<string> RunAsync()
        {
            await Task.Delay(10000);
            return string.Empty;
        }
    }
}
