using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BasicWebServer
{
    class Mymethods
    {

        public Mymethods()
        {
            initJeuDuPendu();
        }

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

        public string decr(string s)
        {
            try
            {
                int v = Int32.Parse(s);
                value -= v;
                return "{ \"value\" : " + value + " }";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /** =======================================
         *
         *   MINI IMPLEMENTATION DU JEU DU PENDU
         *
         * ========================================
         */

        private static string word = "WEB-SERVICE";
        public static int MAX_TRIES = 10;
        private static int tries = 0;
        private static bool finished = false;
        private static HashSet<Char> letters_tried = new HashSet<Char>();

        private void initJeuDuPendu()
        {
            letters_tried.Add('W');
            letters_tried.Add('E');
            letters_tried.Add('-');
        }

        private string word_current_value()
        {
            string word_f = "";

            foreach(Char c in word.ToCharArray())
            {
                if (letters_tried.Contains(c))
                {
                    word_f += c;
                }
                else
                {
                    word_f += '_';
                }
            }

            return word_f;
        }

        public string jeuDuPendu(string letter)
        {
            string message = "";
            char l = letter.ToCharArray()[0];

            if (!finished)
            {
                if (!letters_tried.Contains(l))
                {
                    tries++;
                    letters_tried.Add(l);
                }
                else
                {
                    message = "<p style=\"color:red\">Lettre : " + l + " deja choisi, veuillez en essayer une autre ! </p><br>";
                }
            }

            int tries_left = (MAX_TRIES - tries) > 0 ? MAX_TRIES - tries : 0;
            string word_found = word_current_value();

            if (word_found == word)
            {
                message = "<p style=\"color:green\">Vous avez trouve le bon mot ! </p><br>";
                finished = true;
            }
            else if (tries_left == 0)
            {
                message = "<p style=\"color:red\">Plus d'essaie restant, vous avez perdu </p><br>";
                finished = true;
            }


            string response = "<HTML> <BODY> " +
                "<h1>Jeu du Pendu :<h1> " +
                "<br> <h3>Devinez ce mot : " + word_found + "</h3> <br> " +
                "<form action=\"/a/b/jeuDuPendu\" method =\"GET\">  <label for=\"letter\">Lettre :</label> <input type=\"text\" id=\"letter\" name=\"letter\">  <input type=\"submit\" value=\"Submit\" > </form>" + 
                "<p> Essaye restant : " + tries_left + " </p> <br> " +
                message +
                "Remplissez la lettre dans la case ci-dessus ou alors, methode a l'ancienne : <br>" +
                "Changer le <b>?letter=&#60;Lettre a deviner&#62;</b> dans l'URL pour jouer ! <br> Exemple <a href=\"http://localhost:8080/a/b/jeuDuPendu?letter=B\">http://localhost:8080/a/b/jeuDuPendu?letter=B</a>" +
                "</BODY></HTML>";

            return response;
        }


    }
}
