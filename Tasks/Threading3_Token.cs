using SeminarskaPraksa.Utilities;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading3_Token
    {

        private readonly CancellationTokenSource _cts;

        public Threading3_Token(TextBoxLogger logger, int tick, int delay)
        {
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;


            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(tick);
                    logger.Log("Naloga v teku");
                }
                logger.Log("Naloga končana");
            }, token);

            Task.Run(async () =>
            {
                await Task.Delay(delay);
                logger.Log("Token cancelled");
                _cts.Cancel();
            });
        }


    }
}
