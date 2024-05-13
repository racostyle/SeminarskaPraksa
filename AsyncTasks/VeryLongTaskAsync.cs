namespace SeminarskaPraksa.AsyncTasks
{
    internal class VeryLongTaskAsync : IAsyncTask
    {
        public async Task<string> RunAsync(string input = "")
        {
            await Task.Delay(10000);
            return string.Empty;
        }
    }
}
