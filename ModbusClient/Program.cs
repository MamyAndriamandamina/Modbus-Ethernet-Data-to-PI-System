using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyModbus;

namespace ModbusClient
{
    internal class Program
    {
        public static void Main()
        {
            string eventSourceName = "Janitza Modbus Event";
            string logName = "Janitza Log";
            string[] Janitza = { eventSourceName, logName };
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
            new ModbusService1(Janitza)
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
