
using System.Threading.Tasks;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading4_RaceCondition
    {
        private int counter = 0;

        public Threading4_RaceCondition(Action<string> printInTextbox, int noOfThreads, int limit)
        {
            RaceCondition(printInTextbox, noOfThreads, limit);
            AsyncRaceConditionPrimer(printInTextbox, noOfThreads, limit);
            LockPrimer(printInTextbox, noOfThreads, limit);
        }

        private void RaceCondition(Action<string> printInTextbox, int noOfThreads, int limit)
        {
            counter = 0;
            printInTextbox("Primer 'race condition'");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfThreads; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < limit; j++)
                    {
                        counter++;
                    }
                    printInTextbox($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void AsyncRaceConditionPrimer(Action<string> printInTextbox, int noOfThreads, int limit)
        {
            counter = 0;

            printInTextbox("Primer 'AsyncRaceCondition'");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfThreads; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    while (counter < limit)
                    {
                        await Task.Delay(25);
                        counter++;
                    }
                    printInTextbox($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void LockPrimer(Action<string> printInTextbox, int noOfThreads, int limit)
        {
            counter = 0;
            object _lock = new object();

            printInTextbox("Primer 'Lock'");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfThreads; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(25);
                        lock (_lock)
                        {
                            if (counter < limit)
                                counter++;
                            else
                                break;
                        }
                    }
                    printInTextbox($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
