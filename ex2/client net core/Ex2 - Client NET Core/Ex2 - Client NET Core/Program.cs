using System;
using Grpc.Net.Client; 


namespace Ex2___Client_NET_Core
{
    class Program
    {

        static string defaultAddress = "http://localhost:50051";

        static void Main(string[] args)
        {
            Console.WriteLine("**** Welcome to Temperature Controller ****\n\n");

            Console.Write($"Please enter the address of the server or press Enter to use default '{defaultAddress}': ");
            var address = Console.ReadLine();
            if (String.IsNullOrEmpty(address))
                address = defaultAddress;

            var channel = GrpcChannel.ForAddress(address);
            var client = new TemperatureControllerService.TemperatureControllerServiceClient(channel);
            Console.WriteLine($"\nReady to control '{address}'.");

            var temperature = client.Measure(new MeasureRequest() { Unit = TemperatureUnit.Celsius }).Temperature;
            Console.WriteLine($"\tMeasured temperature is : {temperature}");

            var controllerSettings = new ControllerSettingsRequest() 
            {
                TemperatureSetpoint = 30.12, 
                Unit = TemperatureUnit.Celsius, 
                Enabled = true 
            };
            Console.WriteLine($"\tSetting temperature to: {controllerSettings.TemperatureSetpoint} {controllerSettings.Unit}.");
            client.Configure(controllerSettings);

            temperature = client.Measure(new MeasureRequest() { Unit = TemperatureUnit.Celsius }).Temperature;
            Console.WriteLine($"\tMeasured temperature is : {temperature}");

            controllerSettings.Enabled = false;
            client.Configure(controllerSettings);
            Console.WriteLine($"\nThank you for using our client to control '{address}'! Press any key to exit. ");
            Console.ReadKey();
        }
    }
}
