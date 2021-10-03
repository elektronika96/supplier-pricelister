using buildxact_supplies.Mappers;
using CsvHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace buildxact_supplies.Model
{
    class Products
    {
        public List<Product> products = new List<Product>();
        private string _path = @"..\..\..\";
        private string _nameCSVFile = "humphries.csv";
        private string _nameJSONFile = "megacorp.json";

        public Products(decimal exchangeRate)
        {
            products.AddRange(ReadProductsFromCSV());
            products.AddRange(ReadProductsFromJSON(exchangeRate));
            ChangeCurrency(exchangeRate);
        }

        private List<Product> ReadProductsFromCSV()
        {
            try
            {
                List<Product> result;
                using (var reader = new StreamReader(_path + _nameCSVFile))
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<ProductHMap>();
                        result = csvReader.GetRecords<Product>().ToList();
                    }
                }
                result.Select(x => { x.isAUD = true; return x; }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<Product> ReadProductsFromJSON(decimal exchangeRate)
        {
            try
            {
                JObject JSONProducts = JObject.Parse(File.ReadAllText(_path + _nameJSONFile));
                IList<JToken> JSONProductsList = JSONProducts["partners"].Children()["supplies"].Children().ToList();
                List<Product> result = new List<Product>();
                foreach (JToken JSONProduct in JSONProductsList)
                {
                    Product product = JSONProduct.ToObject<Product>();
                    product.price /= 100;
                    result.Add(product);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ChangeCurrency(decimal exchangeRate)
        {
            //products.Select(x => { x.price = Math.Round(x.price / exchangeRate, 2); return x; }).Where(x => !x.isAUD).ToList();
            foreach (var product in products)
            {
                if (!product.isAUD)
                    product.price = product.price / exchangeRate;
            }
        }
    }
}
