using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LO_Inventory
{
    public static class AdHelper
    {
        public static string GetDomainName()
        {
            return System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        }
    }
}
