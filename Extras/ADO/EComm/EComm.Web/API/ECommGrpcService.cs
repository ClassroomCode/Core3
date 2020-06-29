using EComm.Data;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.API.gRPC
{
    public class ECommGrpcService : ECommGrpc.ECommGrpcBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<ECommGrpcService> _logger;

        public ECommGrpcService(IRepository repository,
          ILogger<ECommGrpcService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override async Task<ProductReply> GetProduct(ProductRequest request, ServerCallContext context)
        {
            var product = await _repository.GetProduct(request.Id, true);

            var reply = new ProductReply {
                Id = product.Id,
                Name = product.ProductName,
                Price = (double)product.UnitPrice.Value,
                Supplier = product.Supplier.CompanyName
            };
            return reply;
        }
    }
}
