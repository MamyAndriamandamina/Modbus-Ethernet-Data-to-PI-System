using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyModbus;
using static EasyModbus.ModbusClient;

namespace ModbusClient
{
    internal class Run { }
    
    internal class ModbusAccessPoint
    {
        public static async Task Main()
        {
            DateTime dt = DateTime.Now;
            string path1 = @"D:\Janitza\Data\Janitza_10_128_11_137_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path2 = @"D:\Janitza\Data\Janitza_10_128_11_138_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";           
            string path3 = @"D:\Janitza\Data\Janitza_10_128_11_142_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";            
            string path4 = @"D:\Janitza\Data\Janitza_10_128_11_143_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path5 = @"D:\Janitza\Data\Janitza_10_128_11_147_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path6 = @"D:\Janitza\Data\Janitza_10_128_11_148_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path7 = @"D:\Janitza\Data\Janitza_10_128_11_152_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path8 = @"D:\Janitza\Data\Janitza_10_128_11_153_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path9 = @"D:\Janitza\Data\Janitza_10_128_11_157_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path10 = @"D:\Janitza\Data\Janitza_10_128_11_158_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path11 = @"D:\Janitza\Data\Janitza_10_128_11_162_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path12 = @"D:\Janitza\Data\Janitza_10_128_11_163_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path13 = @"D:\Janitza\Data\Janitza_10_128_11_167_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";
            string path14 = @"D:\Janitza\Data\Janitza_10_128_11_168_" + dt.ToString("dd_MM_yyyy_HH_mm") + ".txt";

            Task<Run> ReadTask1 = Read("10.128.11.137", 19000, 116, 502, path1);
            Task<Run> ReadTask2 = Read("10.128.11.138", 19000, 116, 502, path2);
            Task<Run> ReadTask3 = Read("10.128.11.142", 19000, 116, 502, path3);
            Task<Run> ReadTask4 = Read("10.128.11.143", 19000, 116, 502, path4);
            Task<Run> ReadTask5 = Read("10.128.11.147", 19000, 116, 502, path5);
            Task<Run> ReadTask6 = Read("10.128.11.148", 19000, 116, 502, path6);
            Task<Run> ReadTask7 = Read("10.128.11.152", 19000, 116, 502, path7);
            Task<Run> ReadTask8 = Read("10.128.11.153", 19000, 116, 502, path8);
            Task<Run> ReadTask9 = Read("10.128.11.157", 19000, 116, 502, path9);
            Task<Run> ReadTask10 = Read("10.128.11.158", 19000, 116, 502, path10);
            Task<Run> ReadTask11 = Read("10.128.11.162", 19000, 116, 502, path11);
            Task<Run> ReadTask12 = Read("10.128.11.163", 19000, 116, 502, path12);
            Task<Run> ReadTask13 = Read("10.128.11.167", 19000, 116, 502, path13);
            Task<Run> ReadTask14 = Read("10.128.11.168", 19000, 116, 502, path14);

            Run x1 = await ReadTask1;
            Run x2 = await ReadTask2;
            Run x3 = await ReadTask3;
            Run x4 = await ReadTask4;
            Run x5 = await ReadTask5;
            Run x6 = await ReadTask6;
            Run x7 = await ReadTask7;
            Run x8 = await ReadTask8;
            Run x9 = await ReadTask9;
            Run x10 = await ReadTask10;
            Run x11 = await ReadTask11;
            Run x12 = await ReadTask12;
            Run x13 = await ReadTask13;
            Run x14 = await ReadTask14;

            RenameFile(path1, path1.Remove(path1.Length - 3) + "csv");
            RenameFile(path2, path2.Remove(path2.Length - 3) + "csv");
            RenameFile(path3, path3.Remove(path3.Length - 3) + "csv");
            RenameFile(path4, path4.Remove(path3.Length - 3) + "csv");
            RenameFile(path5, path5.Remove(path3.Length - 3) + "csv");
            RenameFile(path6, path6.Remove(path3.Length - 3) + "csv");
            RenameFile(path7, path7.Remove(path3.Length - 3) + "csv");
            RenameFile(path8, path8.Remove(path3.Length - 3) + "csv");
            RenameFile(path9, path9.Remove(path3.Length - 3) + "csv");
            RenameFile(path10, path10.Remove(path3.Length - 3) + "csv");
            RenameFile(path11, path11.Remove(path3.Length - 3) + "csv");
            RenameFile(path12, path12.Remove(path3.Length - 3) + "csv");
            RenameFile(path13, path13.Remove(path3.Length - 3) + "csv");
            RenameFile(path14, path14.Remove(path3.Length - 3) + "csv");             
        }
        public static float ConvertRegistersToFloat(int[] registers)
        {
            if (registers.Length != 2)
            {
                throw new ArgumentException("Input Array length invalid - Array length must be '2'");
            }
            int value = registers[1];
            int value2 = registers[0];
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] bytes2 = BitConverter.GetBytes(value2);
            byte[] value3 = new byte[4]
            {
            bytes2[0],
            bytes2[1],
            bytes[0],
            bytes[1]
            };
            return BitConverter.ToSingle(value3, 0);
        }
        public static float ConvertRegistersToFloat(int[] registers, RegisterOrder registerOrder)
        {
            int[] registers2 = new int[2]
            {
            registers[0],
            registers[1]
            };
            if (registerOrder == RegisterOrder.HighLow)
            {
                registers2 = new int[2]
                {
                registers[1],
                registers[0]
                };
            }

            return ConvertRegistersToFloat(registers2);
        }
        public static async Task<Run> Read(string IPAddress, int StartingAddress, int Length, int Port, string path)
        {
            EasyModbus.ModbusClient md = new EasyModbus.ModbusClient();
            md.Port = Port;
            md.IPAddress = IPAddress;
            md.Connect();
            for (int sec = 0; sec <= 59; sec++)
            {
                try
                {
                    int realaddress = 0;
                    int[] response = md.ReadHoldingRegisters(StartingAddress, Length);
                    int j = 0;
                    for (int i = 0; i <= response.Length - 1; i++)
                    {
                        DateTime dt = DateTime.Now;
                        int[] reg = { response[i], response[i + 1] };
                        realaddress = StartingAddress + j;
                        string content = dt.ToString("dd/MM/yyyy HH:mm:ss") + "|" + IPAddress + ":" + realaddress.ToString() + "|" + ConvertRegistersToFloat(reg, RegisterOrder.HighLow);
                        WriteToFile(content, path);
                        i++;
                        j += 2;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                await Task.Delay(1000);
            }

            md.Disconnect();           
            return new Run();
        }
        public static void RenameFile(string source, string destination)
        {
            try
            {
                File.Move(source, destination);
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        static void WriteToFile(string content, string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(content);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}


