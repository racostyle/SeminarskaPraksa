namespace SeminarskaPraksa.Tasks
{
    internal class Threading3_Token
    {

        private readonly CancellationTokenSource _cts;

        public Threading3_Token(Action<string> writer, int tick, int delay)
        {
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;


            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(tick);
                    writer("Naloga v teku");
                }
                writer("Naloga končana");
            }, token);

            Task.Run(async () =>
            {
                await Task.Delay(delay);
                writer("Token cancelled");
                _cts.Cancel();
            });
        }


    }
}
