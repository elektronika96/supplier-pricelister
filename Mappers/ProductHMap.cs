using buildxact_supplies.Model;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace buildxact_supplies.Mappers
{
    public class ProductHMap : ClassMap<Product>
    {
        public ProductHMap()
        {
            Map(m => m.ID).Name("identifier");
            Map(m => m.name).Name("desc");
            Map(m => m.price).Name("costAUD");
        }
    }
}
