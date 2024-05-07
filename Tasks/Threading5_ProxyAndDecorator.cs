using SeminarskaPraksa.AsyncTasks;
using SeminarskaPraksa.Utilities;
using System.Text;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading5_ProxyAndDecorator
    {
        private List<IAsyncTask> _asyncTasks;
        private readonly Action<string> _writer;
        private readonly IAsyncTask[] _tasks;

        public Threading5_ProxyAndDecorator(Action<string> writer, params IAsyncTask[] tasks)
        {
            _writer = writer;
            _tasks = tasks;
            _asyncTasks = new List<IAsyncTask>();
        }

        internal async Task Start()
        {
            GenerateProxys();
            await RunAllTasks();
            GenerateDecorators();
            await RunAllTasks();
            //tukaj filtriraj rezultate iz po kategorijah

            _writer("Primer Dekorator in Proxy združena");
        }

        private void GenerateProxys()
        {
            _writer("Primer proxy");
            _asyncTasks = new List<IAsyncTask>();
            foreach (var task in _tasks)
            {
                _asyncTasks.Add(new TaskProxy(task, _writer, new CheckNetworkWithPing()));
            }
        }

        private void GenerateDecorators()
        {
            _writer("Primer Dekorator");
            _asyncTasks = new List<IAsyncTask>();
            foreach (var task in _tasks)
            {
                _asyncTasks.Add(new TaskDecorator(task, _writer));
            }
        }

        private async Task RunAllTasks()
        {
            foreach (IAsyncTask task in _asyncTasks)
            {
                await task.RunAsync();
            }
        }
    }


    internal class TaskProxy : IAsyncTask
    {
        private readonly IAsyncTask _task;
        private readonly Action<string> _writer;
        private readonly CheckNetworkWithPing _checkNetwork;

        public TaskProxy(IAsyncTask task, Action<string> writer, CheckNetworkWithPing checkNetworkWithPing)
        {
            _task = task;
            _writer = writer;
            _checkNetwork = checkNetworkWithPing;
        }

        public async Task<string> RunAsync()
        {
            while (!_checkNetwork.CanPing())
            {
                await Task.Delay(5000);
            }
            _writer("Imamo povezavo");
            var result = await _task.RunAsync();
            _writer(result);
            return result;
        }
    }

    internal class TaskDecorator : IAsyncTask
    {
        private readonly IAsyncTask _task;
        private readonly Action<string> _writer;

        public TaskDecorator(IAsyncTask task, Action<string> writer)
        {
            _task = task;
            _writer = writer;
        }

        public async Task<string> RunAsync()
        {
            var result = await _task.RunAsync();
            result = FilterResults(result);
            _writer(result);
            return result;
        }

        private string FilterResults(string result)
        {
            result = result.Substring(1, result.Length - 2).Trim();
            var filtered = result.Split(',');

            var sb = new StringBuilder();
            foreach (string item in filtered )
            {
                sb.AppendLine(item.Trim());
            }
            return sb.ToString();
        }
    }
}
