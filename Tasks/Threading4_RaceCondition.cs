
using SeminarskaPraksa.Utilities;
using System.Threading.Tasks;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading4_RaceCondition
    {
        private int counter = 0;

        public Threading4_RaceCondition(TextBoxLogger logger, int noOfThreads, int limit)
        {
            RaceCondition(logger, noOfThreads, limit);
            AsyncRaceConditionPrimer(logger, noOfThreads, limit);
            LockPrimer(logger, noOfThreads, limit);
        }

        private void RaceCondition(TextBoxLogger logger, int noOfThreads, int limit)
        {
            counter = 0;
            logger.Log("Primer 'race condition'");
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfThreads; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < limit; j++)
                    {
                        counter++;
                    }
                    logger.Log($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void AsyncRaceConditionPrimer(TextBoxLogger logger, int noOfThreads, int limit)
        {
            counter = 0;

            logger.Log("Primer 'AsyncRaceCondition'");
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
                    logger.Log($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void LockPrimer(TextBoxLogger logger, int noOfThreads, int limit)
        {
            counter = 0;
            object _lock = new object();

            logger.Log("Primer 'Lock'");
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
                    logger.Log($"Vrednost spremenljivke po zanki: {counter}");
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
