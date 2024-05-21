using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;


namespace ModbusClient
{
    partial class ModbusService1 : ServiceBase
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public ModbusService1(string[] args)
        {
            InitializeComponent();         
            eventLog1 = new EventLog();
            if(!EventLog.SourceExists("Janitza Modbus Event"))
            {
                EventLog.CreateEventSource("Janitza Modbus Event", "Janitza Log");
            }
            eventLog1.Source = "Janitza Modbus Event";
            eventLog1.Log = "Janitza Log";
        }
        protected override void OnStart(string[] args)
        {
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // TODO: Add code here to start your service.
            eventLog1.WriteEntry("Modbus Application OnStart.");
            Timer timer = new Timer 
            { 
                Interval = 60000, //1 minute
            };
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
            //update the service state to Running
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }
        private int eventId = 1;
        private int cyclecount = 0;

        public async void OnTimer(object sender, ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("Modbus Service running.", EventLogEntryType.Information, eventId++);
            var x = ModbusAccessPoint.Main();
            await x;
        }
        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            eventLog1.WriteEntry("Modbus Application OnStop.");
            var modbus = new ModbusAccessPoint();
            
            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
    }
}

