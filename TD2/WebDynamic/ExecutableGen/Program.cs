﻿using System;

namespace ExecutableGen
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
                Console.WriteLine("<HTML><BODY> Hello via new executable: " + args[0] + " and " + args[1] + "</BODY></HTML>");
            else
                Console.WriteLine("Not enough parameter to run : Executable.exe\nNeed 2 has: " + args.Length);
        }
    }
}
