using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace DefEx
{
    public class Repository
    {

        public IQueryable<Product> GetAllProducts();

        public List<Product> Foo(IEnumerable<Product>);

        public Product GetProduct(int id);


    }
}
