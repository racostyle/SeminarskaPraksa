namespace SeminarskaPraksa.AsyncTasks
{
    internal class HttpAsyncTask : ITasks
    {
        public async Task<string> RunAsync()
        {
            using (var client = new HttpClient())
            {
                string url = "https://jsonplaceholder.typicode.com/posts/1";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
