using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace laboratorium_11
{
    class DomainConverter
    {
        private static readonly string[] hostNames = { "www.microsoft.com", "www.apple.com",
            "www.google.com", "www.ibm.com", "cisco.netacad.net",
            "www.oracle.com", "www.nokia.com", "www.hp.com", "www.dell.com",
            "www.samsung.com", "www.toshiba.com", "www.siemens.com",
            "www.amazon.com", "www.sony.com", "www.canon.com",// "www.alcatellucent.com",
            "www.acer.com", "www.motorola.com" };

        public static List<Tuple<string, string>> ConvertDomains()
        {
            return hostNames.AsParallel()
                            .Select(hostName => Tuple.Create(hostName, Dns.GetHostAddresses(hostName).First().ToString()))
                            .ToList();
        }
    }
}
