using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Echo
{
    class EchoServer
    {
        [Obsolete]
        static void Main(string[] args)
        {

            Console.CancelKeyPress += delegate
            {
                System.Environment.Exit(0);
            };

            TcpListener ServerSocket = new TcpListener(5000);
            ServerSocket.Start();

            Console.WriteLine("Server started.");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }


        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Echo);
            ctThread.Start();
        }



        private void Echo()
        {
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            while (true)
            {
                string str = reader.ReadString();
                Console.WriteLine(str);

                if (str.Split(" ")[0] == "GET")
                {
                    try { 
                        string fileRelativePath = str.Split(" ")[1].Substring(1);
                        string responseString = File.ReadAllText(fileRelativePath);
                        writer.Write(responseString);
                    } catch(FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        writer.Write(e.Message);
                    }

                }
                else
                {
                    writer.Write("404 Not found");
                }
            }
        }



    }

}