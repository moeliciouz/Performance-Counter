using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Management;

namespace PerformanceCounterV1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Informationen über System anzeigen

            // Vorinformationen sammeln

            // Netzwerk Interface herausfinden
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            String[] NetworkInterface = category.GetInstanceNames();
            string FirstNetworkInterface = NetworkInterface[0];


            // CPU

            // Zeigt die CPU Auslastung in Prozent an
            PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            // Zeigt die % der Maximalen Prozessorauslastung an
            PerformanceCounter perfCPUMaxCount = new PerformanceCounter("Processor Information", "% of Maximum Frequency", "_Total");

            // Zeigt die % der Zeit an in der der Prozessor mit Hardware Unterbrechungen beschäftigt ist
            PerformanceCounter perfCPUInterCount = new PerformanceCounter("Processor", "% Interrupt Time", "_Total");

            //Zeigt die Anzahl der Threads in der Prozessorwarteschlange
            PerformanceCounter PerfCPUThreadCount = new PerformanceCounter("System", "Processor Queue Length");

            // Zeigt an wie viele Kontextwechsel pro Sekunde auf der CPU ablaufen
            PerformanceCounter PerfCPUConSwitchCount = new PerformanceCounter("System", "Context Switches/sec");


            // Arbeitsspeicher

            // Zeigt den freien Arbeitsspeicher in MB an
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available Mbytes");
            
            //int memAvail = (int)PCMemory.nextValue();
            //double dMemAvail = memAvail /1024.0;
            //Console.Write("Memory Available" + mMemAvail + "GB");
            //Console.ReadKey(true)

            // Zeigt den gerade benutzten Arbeitsspeicher an
            PerformanceCounter perfMemInUse = new PerformanceCounter("Memory", "Committed Bytes");

            // Zeigt Anzahl der Speicherseitenfehler pro Sekunde im Arbeitsspeicher an
            PerformanceCounter perfMemPageFault = new PerformanceCounter("Memory", "Page Faults/sec");

            // Zeigt Anzahl der Speicherseiten Lesen + Schreiben pro Sekunde
            PerformanceCounter perfMemPagePerSec = new PerformanceCounter("Memory", "Pages/sec");


            // Festplatte

            // Zeigt die % der Zeit, die die Festplatte C:\ mit Lesen verbringt
            PerformanceCounter perfDskPercReadTime = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "_Total"); //"0 C:"

            // Zeigt die % der Zeit, die die Festplatte C:\ mit Schreiben verbringt
            PerformanceCounter perfDskPercWriteTime = new PerformanceCounter("PhysicalDisk", "% Disk Write Time", "_Total");

            // Zeigt die Länge der aktuellen Warteschlange von Festplatte C: an
            PerformanceCounter perfDskAvgQueueLength = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total");


            // Netzwerk

            // Zeigt die Bytes/sec an, die vom Netzwerkinterface verarbeitet werden

            PerformanceCounter perfNetIntByteSec = new PerformanceCounter("Network Interface", "Bytes Total/sec", FirstNetworkInterface);


            // unendliche while Schleife
            while (true)
            {
                // Programm durchgehend laufen lassen 
                Thread.Sleep(1000);

                // Zwei Zeilen frei
                Console.WriteLine();
                Console.WriteLine();

                // Allgemeine Angaben über Computer anzeigen
                Console.WriteLine("Computer Information:");
                Console.WriteLine();
                GetComponent("Win32_Processor", "Name");
                GetComponent("Win32_VideoController", "Name");

                // Zwei Zeilen frei
                Console.WriteLine();
                Console.WriteLine();


                // Ausgabe CPU


                // Ausgabe Instanz Prozessor
                Console.WriteLine("Processor:");
                Console.WriteLine();

                // Ausgabe CPU Auslastung
                Console.WriteLine("CPU: Load: {0} %", perfCPUCount.NextValue());
                // Ausgabe CPU % der maximalen Auslastung
                Console.WriteLine("CPU: % of Maximum Load: {0} %", perfCPUMaxCount.NextValue());
                // Ausgabe CPU % der Zeit die für Hardware Unterbrechungen bereitgestellt wird
                Console.WriteLine("CPU: % of Interrupt Time: {0}", perfCPUInterCount.NextValue());
                // Ausgabe Anzahl der Threads in der CPU Warteschlange
                Console.WriteLine("CPU: Count of Threads in Queue: {0}", PerfCPUThreadCount.NextValue());
                // Ausgabe Kontextwechsel pro Sekunde auf der CPU
                Console.WriteLine("CPU: Count of context switches per second: {0}", PerfCPUConSwitchCount.NextValue());
                // Zwei Zeilen frei
                Console.WriteLine();
                Console.WriteLine();

                // Ausgabe Arbeitsspeicher
                // Ausgabe Instanz Arbeitsspeicher
                Console.WriteLine("Memory:");
                Console.WriteLine();

                // Ausgabe freier Arbeitsspeicher
                Console.WriteLine("Memory: Available: {0} MB", (perfMemCount.NextValue()/1024.0));

                // Ausgabe Arbeitsspeicher in Nutzung
                Console.WriteLine("Memory: Physical Memory in Use: {0} MB", (perfMemInUse.NextValue()/1024.0));
                //Ausgabe Anzahl der Seitenfehler pro Sekunde im Arbeitsspeicher
                Console.WriteLine("Memory: Number of Page Faults per secornd: {0} Errors/s", perfMemPageFault.NextValue());
                //Ausgabe Anzahl der Speicherseiten Lesen + Schreiben pro Sekunde
                Console.WriteLine("Memory: Number of Pages per second: {0} Pages/s", perfMemPagePerSec.NextValue());
                // Zwei Zeilen frei
                Console.WriteLine();
                Console.WriteLine();
                // Ausgabe Festplatte 
                // Ausgabe Instanz Festplatte C: 
                Console.WriteLine("Physical Disk C:");
                Console.WriteLine();

                //Ausgabe % der Zeit, die die Festplatte C:\ mit Lesen verbringt
                Console.WriteLine("Physical Disk C: % Read Time: {0}", perfDskPercReadTime.NextValue());
                //Ausgabe % der Zeit, die die Festplatte C:\ mit Schreiben verbringt
                Console.WriteLine("Physical Disk C: % Write Time: {0}", perfDskPercWriteTime.NextValue());
                //Ausgabe Länge der aktuellen Warteschlange von Festplatte C:
                Console.WriteLine("Physical Disk C: Average Queue Length: {0}", perfDskAvgQueueLength.NextValue());
                // Zwei Zeilen frei
                Console.WriteLine();
                Console.WriteLine();

                // Ausgabe Netzwerk
                // Ausgabe Netzwerk Interface Bezeichnung

                // Ausgabe Bytes/sec, die vom Netzwerkinterface verarbeitet werden
                Console.WriteLine("{1}: Bytes Total/s: {0}", perfNetIntByteSec.NextValue(), FirstNetworkInterface);

            }
        }

        // CPU Typ herausfinden
        private static void GetComponent(string hwclass, string collection)
        {
            ManagementObjectSearcher CPUIdent = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            foreach (ManagementObject device in CPUIdent.Get())
            {
                Console.WriteLine(Convert.ToString(device[collection]));
            }
        }
    }
}
