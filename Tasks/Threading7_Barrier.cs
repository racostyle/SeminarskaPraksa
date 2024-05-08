using SeminarskaPraksa.Utilities;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading7_Barrier
    {
        private readonly TextBoxLogger _Logger;

        public Threading7_Barrier(TextBoxLogger printInTextbox)
        {
            _Logger = printInTextbox;
        }

        internal async Task Start()
        {
            int numberOfParticipants = 3;
            Barrier barrier = new Barrier(numberOfParticipants, (b) =>
            {
                _Logger.Log($"Phase {b.CurrentPhaseNumber} is completed.");
            });

            Task[] tasks = new Task[numberOfParticipants];

            for (int i = 0; i < numberOfParticipants; i++)
            {
                int localCopy = i;
                tasks[i] = Task.Run(async () =>
                {
                    _Logger.Log($"Task {localCopy} is starting phase 1.");
                    await Task.Delay(1000 * localCopy);
                    barrier.SignalAndWait();

                    _Logger.Log($"Task {localCopy} is starting phase 2.");
                    await Task.Delay(1000 * (numberOfParticipants - localCopy));
                    barrier.SignalAndWait();

                    _Logger.Log($"Task {localCopy} is finished.");
                });
            }

            await Task.WhenAll(tasks);
            _Logger.Log("All tasks completed.");
        }
    }
}
