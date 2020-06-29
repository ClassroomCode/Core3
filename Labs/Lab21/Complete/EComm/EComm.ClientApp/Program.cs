using EComm.Web.API.gRPC;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace EComm.ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ECommGrpc.ECommGrpcClient(channel);
            
            var reply = await client.GetProductAsync(new ProductRequest { Id = 5 });

            Console.WriteLine($"{reply.Id}, {reply.Name}, {reply.Price}, " +
                          $"{reply.Package}, {reply.Supplier}");
        }
    }
}
