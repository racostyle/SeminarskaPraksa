namespace SeminarskaPraksa.AsyncTasks
{
    internal interface IAsyncTask
    {
        Task<string> RunAsync(string input = "");
    }
}