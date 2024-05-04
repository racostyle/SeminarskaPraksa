namespace SeminarskaPraksa.Tasks
{
    internal class Threading3_Token
    {

        private readonly CancellationTokenSource _cts;

        public Threading3_Token(Action<string> write, int tick, int delay)
        {
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;


            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(tick);
                    write("Naloga v teku");
                }
                write("Naloga končana");
            }, token);

            Task.Run(async () =>
            {
                await Task.Delay(delay);
                write("Token cancelled");
                _cts.Cancel();
            });
        }


    }
}
