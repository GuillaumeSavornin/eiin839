using BasicWebServer;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;


/*
 *
 *  URL TO USE :
 *      - Question 4 : http://localhost:8080/a/b/helloMethod?param1=Guillaume&param2=John
 *      - Question 5 : http://localhost:8080/a/b/execMethod?param1=Guillaume&param2=John
 *      - Question 6 : http://localhost:8080/a/b/execScript
 *      - Question 7 : http://localhost:8080/a/b/incr?val=7
 *      
 *      --> Travail suplémentaire : 
 *          - JEU DU PENDU : http://localhost:8080/a/b/jeuDuPendu?letter=E
 *          => Explication de comment jouer sur la page web !
 *      
 */

namespace WebDynamic
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }


            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // get url 
                Console.WriteLine($"\n\nReceived request for {request.Url}");

                //parse params in 
                string val = HttpUtility.ParseQueryString(request.Url.Query).Get("val");
                string letter = HttpUtility.ParseQueryString(request.Url.Query).Get("letter");
                string param1 = HttpUtility.ParseQueryString(request.Url.Query).Get("param1");
                string param2 = HttpUtility.ParseQueryString(request.Url.Query).Get("param2");
                string param3 = HttpUtility.ParseQueryString(request.Url.Query).Get("param3");
                string param4 = HttpUtility.ParseQueryString(request.Url.Query).Get("param4");

                Console.WriteLine(documentContents);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // Construct a response.
                string responseString = "";

                if (request.Url.Segments.Length > 3)
                {
                    string methodName = request.Url.Segments[3];
                    Console.WriteLine("Method called --> " + methodName);

                    // Get method name reflectively and call it.
                    object[] arguments = { };
                    if(val != null)
                    {
                        arguments = new object[] { val };
                        response.ContentType = "text/json"; // MIME JSON
                    }else
                    if (letter != null)
                    {
                        arguments = new object[] { letter };
                        response.ContentType = "text/html"; // MIME JSON
                    }
                    else
                    {
                        arguments = new object[] { param1, param2 };
                        response.ContentType = "text/html"; // MIME HTML
                    }

                    // REFLICTIVITY
                    Type type = typeof(Mymethods);
                    MethodInfo method = type.GetMethod(methodName);
                    Mymethods mymethods = new Mymethods();

                    try { 
                        responseString = (string)method.Invoke(mymethods, arguments);
                    } catch (NullReferenceException e)
                    {
                        responseString = "Error : \"" + methodName + "\" is not a valid method. Try : helloMethod, execMethod, execScript";
                    }

                }
                else
                {
                    responseString = "<HTML><BODY> Not enough sections : has " + (request.Url.Segments.Length - 1) + " need 3 </BODY></HTML>";
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;

                Console.WriteLine("Response --> " + responseString);

                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
        }
    }
}
