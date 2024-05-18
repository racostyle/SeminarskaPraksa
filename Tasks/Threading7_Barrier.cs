using SeminarskaPraksa.Utilities;
using System.Text;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading7_Barrier
    {
        private int _noOfTasks;
        private readonly TextBoxLogger _Logger;
        private List<string> _messages;
        StringBuilder _stringBuilder;

        public Threading7_Barrier(TextBoxLogger logger)
        {
            _Logger = logger;
            _messages = new List<string>();
            _stringBuilder = new StringBuilder();
        }

        internal async Task Start(int noOfTasks)
        {
            _noOfTasks = noOfTasks;

            Barrier barrier = new Barrier(_noOfTasks, (b) =>
            {
                _Logger.Log($"Faza {b.CurrentPhaseNumber + 1} končana");
            });

            Task[] tasks = new Task[_noOfTasks];

            for (int i = 0; i < _noOfTasks; i++)
            {
                int localCopy = i;
                tasks[i] = Task.Run(async () =>
                {
                    LoggerAccess($"Naloga {localCopy} začenja z fazo 1");
                    await Task.Delay(1000 * localCopy);
                    barrier.SignalAndWait();

                    LoggerAccess($"Naloga {localCopy} začenja z fazo 2");
                    await Task.Delay(1000 * (_noOfTasks - localCopy));
                    barrier.SignalAndWait();

                    LoggerAccess($"Naloga {localCopy} Končana");
                });
            }

            await Task.WhenAll(tasks);
            _Logger.Log("Vse naloge končane");
        }

        private void LoggerAccess(string message)
        {
            _messages.Add(message);
            if (_messages.Count == _noOfTasks)
            {
                foreach (var item in _messages)
                    _stringBuilder.AppendLine(item);
                _Logger.Log(_stringBuilder.ToString());

                _stringBuilder.Clear();
                _messages.Clear();
            }
        }
    }
}
