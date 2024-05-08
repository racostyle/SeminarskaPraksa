using SeminarskaPraksa.AsyncTasks;
using SeminarskaPraksa.Utilities;
using System.Text;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading5_ProxyAndDecorator
    {
        private readonly List<IAsyncTask> _asyncTasks;
        private readonly Action<string> _writer;
        private readonly CheckNetworkWithPing _checkNetworkWithPing;
        private readonly IAsyncTask[] _tasks;

        public Threading5_ProxyAndDecorator(Action<string> writer, CheckNetworkWithPing checkNetworkWithPing, params IAsyncTask[] tasks)
        {
            _writer = writer;
            _checkNetworkWithPing = checkNetworkWithPing;
            _tasks = tasks;
            _asyncTasks = new List<IAsyncTask>();
        }

        internal async Task Start()
        {
            GenerateProxys();
            await RunAllTasks();
            GenerateDecorators();
            await RunAllTasks();
            GenerateDecoratorsInProxys();
            await RunAllTasks();
        }

        private void GenerateProxys()
        {
            _writer("Primer proxy");
            _asyncTasks.Clear();
            foreach (var task in _tasks)
                _asyncTasks.Add(new TaskProxy(task, _writer, _checkNetworkWithPing));
        }

        private void GenerateDecorators()
        {
            _writer("Primer Dekorator");
            _asyncTasks.Clear();
            foreach (var task in _tasks)
                _asyncTasks.Add(new TaskDecorator(task));
        }

        private void GenerateDecoratorsInProxys()
        {
            _writer("Primer Dekorator in Proxy združena");
            _asyncTasks.Clear();
            foreach (var task in _tasks)
            {
                IAsyncTask decorator = new TaskDecorator(task);
                _asyncTasks.Add(new TaskProxy(decorator, _writer, _checkNetworkWithPing));
            }
        }

        private async Task RunAllTasks()
        {
            foreach (IAsyncTask task in _asyncTasks)
            {
                var result = await task.RunAsync();
                _writer(result);
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
            while (true)
            {
                if (_checkNetwork.CanPing())
                {
                    _writer("Imamo internetno povezavo");
                    break;
                }
                else
                    await Task.Delay(5000);
            }
            var result = await _task.RunAsync();
            return result;
        }
    }

    internal class TaskDecorator : IAsyncTask
    {
        private readonly IAsyncTask _task;

        public TaskDecorator(IAsyncTask task)
        {
            _task = task;
        }

        public async Task<string> RunAsync()
        {
            var result = await _task.RunAsync();
            result = FilterResults(result);
            return result;
        }

        private string FilterResults(string result)
        {
            result = result.Substring(1, result.Length - 2).Trim();
            var filtered = result.Split(',');

            var sb = new StringBuilder();
            foreach (string item in filtered)
                sb.AppendLine(item.Trim());
            return sb.ToString();
        }
    }
}
