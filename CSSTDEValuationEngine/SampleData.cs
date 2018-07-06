using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using CSSTDModels;

namespace CSSTDEvaluation
{
    public  class SampleData
    {
        public  string dataFolder;
        public SampleData(string dataFolder)
        {
            this.dataFolder = dataFolder;
        }
        public SampleData() { }

        public List<BlobFileData> BlobData()
        {
            var files = new List<BlobFileData>();
            string[] fileNames = {"doors.jpg","labondemand.jpg","lodslogo.png" };
            string[][] tags = { new string[]{"Abstract","Physical" }, new string[] { "Learning","Person" }, new string[] { "Corporate","Logo" } };
            for(int i = 0; i<3;i++)
            {
                string filePath = $"{dataFolder}\\{fileNames[i]}";
                files.Add(new BlobFileData
                {
                    Contents = File.ReadAllBytes(filePath),
                    Name = fileNames[i],
                    Tags = new List<string>(tags[i])
                });
                
            }
            return files;
        }
        public List<CustomerData> CustomerData()
        {
            return JsonConvert.DeserializeObject<List<CustomerData>>(File.ReadAllText($"{dataFolder}\\Customers.json"));
        }
        public List<VendorData> VendorData()
        {
            return JsonConvert.DeserializeObject<List<VendorData>>(File.ReadAllText($"{dataFolder}\\Vendors.json"));
        }
        public List<ProductDocument> ProductData()
        {
            return JsonConvert.DeserializeObject<List<ProductDocument>>(File.ReadAllText($"{dataFolder}\\Products.json"));
        }
    }
}