using System.Net;
using System.Net.Sockets;

namespace NameBandit.Helpers
{
    public class NetworkHelper
    {      
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            
            return "";
        }
    }

}
