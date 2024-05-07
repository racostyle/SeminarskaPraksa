using System.Net.NetworkInformation;

namespace SeminarskaPraksa.Utilities
{
    internal class CheckNetworkWithPing
    {
        internal bool CanPing()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000); // Timeout set to 1000 milliseconds
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
