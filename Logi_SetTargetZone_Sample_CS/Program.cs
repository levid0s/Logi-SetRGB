using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LedCSharp;
using System.IO.Pipes;
using System.IO;

namespace Logi_SetRGB
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize the LED SDK
            bool LedInitialized = LogitechGSDK.LogiLedInitWithName("SetTargetZone Sample C#");

            if (!LedInitialized)
            {
            Console.WriteLine("LogitechGSDK.LogiLedInit() failed.");
            return;
            }

            Console.WriteLine("LED SDK Initialized");

            int keyId, red, green, blue;

            while (true) {
                using (var server = new NamedPipeServerStream("MyNamedPipe"))
                {
                    Console.WriteLine("Waiting for client connection...");
                    server.WaitForConnection();
                    Console.WriteLine("Client connected.");
                    LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_ALL);

                    using (var reader = new StreamReader(server))
                    {
                        while (true)
                        {
                            // Read message from the pipe
                            string message = reader.ReadLine();
                            if (message == null) break; // Exit the loop when the client disconnects
                                                        // Process the message
                            Console.WriteLine("Received message: " + message);
                            string[] parts = message.Split(',');
                            string command = parts[0];
                            switch(command)
                            {
                                case "setkey":
                                    keyId = Convert.ToInt32(parts[1], 16);
                                    red = int.Parse(parts[2]);
                                    green = int.Parse(parts[3]);
                                    blue = int.Parse(parts[4]);
                                    LogitechGSDK.LogiLedSetLightingForKeyWithKeyName((keyboardNames)keyId, red, green, blue);
                                    break;

                                case "setall":
                                    red = int.Parse(parts[1]);
                                    green = int.Parse(parts[2]);
                                    blue = int.Parse(parts[3]);
                                    LogitechGSDK.LogiLedSetLighting(red, green, blue);
                                    break;

                                case "shutdown":
                                    Console.WriteLine("LED SDK Shutting down");
                                    LogitechGSDK.LogiLedShutdown();
                                    return;
                            }
                        }
                    }
                }
            }

        }
    }
}
