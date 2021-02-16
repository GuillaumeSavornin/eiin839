using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer
{
    class Mymethods
    {

        private static int value = 0;

        public string helloMethod(string  name1, string name2)
        {
            return "<HTML> <BODY> Hello " + name1 + " et " + name2 + " </BODY></HTML>"; 
        }

        public string execMethod(string name1, string name2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"..\..\..\..\ExecutableGen\bin\Debug\netcoreapp3.1\ExecutableGen.exe"; // Specify exe name.
            start.Arguments = name1 + " " + name2; // Specify arguments.
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            //
            // Start the process.
            //
          
            try
            {
                using (Process process = Process.Start(start))
                {
                    //
                    // Read in all the text from the process with the StreamReader.
                    //
                    using (StreamReader reader = process.StandardOutput)
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Win32Exception e)
            {
                return e.Message;
            }
        }

        public string execScript(string name1, string name2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"..\..\..\..\monScript.bat"; // Specify exe name.
            start.Arguments = @""; // Specify arguments.

            // On peut utiliser ça pour les fichier .sh :
            //start.FileName = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Cygwin"; // Specify exe name.
            //start.Arguments = @"sh /cygdrive/c/Users/lacav/source/repos/TD_soc/TD2/monScript.sh"; // Specify arguments.

            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            //
            // Start the process.
            //

            try
            {
                using (Process process = Process.Start(start))
                {
                    //
                    // Read in all the text from the process with the StreamReader.
                    //²
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string s = reader.ReadToEnd();
                        return s;
                        Console.WriteLine("SCRIPT RESPONSE : " + s);
                    }
                }
            }
            catch (Win32Exception e)
            {
                return e.Message;
            }
        }

        public string incr(string s)
        {
            try
            {
                int v = Int32.Parse(s);
                value += v;
                return "{ \"value\" : " + value + " }";
            } catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
