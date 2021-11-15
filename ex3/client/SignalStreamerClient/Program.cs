using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace SignalStreamerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Signal Streamer!");
            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new SignalStreamer.SignalStreamerClient(channel);

            Console.WriteLine("Let's start streaming! Press any key to stop.");

            var cancellationSource = new CancellationTokenSource();
            Task streamTask = StreamTask(client, cancellationSource.Token);

            Console.ReadKey();
            cancellationSource.Cancel();
            streamTask.Wait();
            Console.WriteLine("Bye!");
        }

        static async Task StreamTask(SignalStreamer.SignalStreamerClient client, CancellationToken token)
        {
            using (var streamingCall = client.GetDataStream(new ChannelNumber{Channel = 0}))
            {
                while(await streamingCall.ResponseStream.MoveNext(token))
                {
                    var data = streamingCall.ResponseStream.Current;
                    Console.WriteLine($"Data arrived! Data.StartTime: {data.StartTime},Data.Samples[0]: {data.Samples[0]} Data.Samples.Bytes: {data.Samples.Count * 8}");
                }
                Console.WriteLine("Streaming stopped by the server.");                
            }
        }
    }
}
