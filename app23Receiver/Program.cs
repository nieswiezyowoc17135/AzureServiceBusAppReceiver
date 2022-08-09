using System;
using System.Threading.Tasks;
using app23Receiver.DataContext;
using Azure.Messaging.ServiceBus;

namespace app23Receiver
{
    class Program
    { 
        static string connectionString = "Endpoint=sb://pspraktyki.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccesKey;SharedAccessKey=z60u2t2Z3Hz1GZrItbTAKtqdZ/56HCLs9zLkmMeWuDc=;EntityPath=add-user-data";
        static string queueName = "add-user-data";
        static ServiceBusClient client;
        static ServiceBusProcessor processor;
        static async Task Main(string[] args)
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //

            // Create the client object that will be used to create sender and receiver objects
            client = new ServiceBusClient(connectionString);

            // create a processor that we can use to process the messages
            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            //inicjalizacja obiektu, który bedzie potrzebny do przerzucenia z json do obiektu
            var newDeserializationBox = new DeserializationBox(body);
            var user = newDeserializationBox.DeserializeJson();
            //cuda z walidacjami i zmienianiem flagi
            var newValidatorAndFlagChanger = new ValidatorAndFlagChanger(user);
            var parameterChecker = newValidatorAndFlagChanger.CheckingParameters();
            Console.WriteLine(user.Email);
            
            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
