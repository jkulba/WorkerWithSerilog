using System;
using System.Collections.Generic;
using System.Text;

namespace Kulba.Services.CayenneService.Models
{
    public class SocketServerConfigInfo
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public override string ToString()
        {
            return "SocketServerName: " + Name + ", IpAddress: " + IpAddress + ", Port: " + Port;
        }
    }
}