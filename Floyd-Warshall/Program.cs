using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Floyd_Warshall
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IntPtr dllHandle = LoadLibrary("FloydWarshallAsm.dll");
            if (dllHandle == IntPtr.Zero)
            {
                Console.WriteLine("Nie udało się załadować biblioteki FloydWarshallAsm.dll");
                return;
            }

            // Pobierz adres funkcji MyProc1
            IntPtr procAddress = GetProcAddress(dllHandle, "MyProc1");
            if (procAddress == IntPtr.Zero)
            {
                Console.WriteLine("Nie udało się znaleźć funkcji MyProc1 w bibliotece JAAsm.dll");
                return;
            }

            // Utwórz delegata do wywołania funkcji MyProc1
            MyProc1 procedura = Marshal.GetDelegateForFunctionPointer<MyProc1>(procAddress);

            // Wywołaj funkcję MyProc1
            int x = 5, y = 7;
            int retVal = procedura(x, y);

            Console.WriteLine($"Wynik wywołania MyProc1: {retVal}");

            // Zwolnij bibliotekę DLL
            FreeLibrary(dllHandle);

            Console.Write("jestem tutaj");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Background());
        }

        // Delegat do funkcji MyProc1
        [UnmanagedFunctionPointer(CallingConvention.FastCall)]
        delegate int MyProc1(int param1, int param2);

        // Import funkcji systemowych z kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    }
}
