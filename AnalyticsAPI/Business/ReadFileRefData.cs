using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace AnalyticsAPI.BusinessLayer
{
    class ReadFileRefData
    {
       

        private static int[][] _cashRefData;
        private static int[][] _equityRefData;

        private static BlobContainerClient blobContainerClient = null;
        static ReadFileRefData()
        {
            blobContainerClient = GetBlobContainer();
        }
      
        public static async Task<int[][]> ReadCashRefData()
        {
           
            if (_cashRefData != null && _cashRefData.Length > 0)
            {
                return _cashRefData;
            }

            //var filepathDirectory = Directory.GetCurrentDirectory();
            //string filePath = filepathDirectory + "//RefData//CashRefData.csv";

            var blobClient = blobContainerClient.GetBlobClient("CashRefData.csv");

            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();

                using (StreamReader sr = new StreamReader(response.Value.Content))
                {
                    _cashRefData = new int[100][];
                    for (int i = 0; i < 100; i++)
                    {
                        _cashRefData[i] = new int[100];
                    }
                    string line;
                    int lineNumber = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] refData = line.Split(new char[] { ',' });
                        for (int i = 0; i < refData.Length; i++)
                        {
                            _cashRefData[lineNumber][i] = Convert.ToInt32(refData[i]);
                        }
                        lineNumber++;
                    }
                }
            }
            return _cashRefData;
        }

        public static async Task<int[][]> ReadEquityRefData()
        {
            if (_equityRefData != null && _equityRefData.Length > 0)
            {
                return _equityRefData;
            }


            //var filepathDirectory  = Directory.GetCurrentDirectory();

            //string filePath = filepathDirectory + "//RefData//EquityRefData.csv";

            var blobClient = blobContainerClient.GetBlobClient("EquityRefData.csv");

            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                using (StreamReader sr = new StreamReader(response.Value.Content))
                {
                    _equityRefData = new int[100][];
                    for (int i = 0; i < 100; i++)
                    {
                        _equityRefData[i] = new int[100];
                    }
                    string line;
                    int lineNumber = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] refData = line.Split(new char[] { ',' });
                        for (int i = 0; i < refData.Length; i++)
                        {
                            _equityRefData[lineNumber][i] = Convert.ToInt32(refData[i]);
                        }
                        lineNumber++;
                    }
                }
            }
            return _equityRefData;
        }

        public static BlobContainerClient GetBlobContainer()
        {
            string line = string.Empty;
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=otelowlsstorageaccount;AccountKey=Z6uRZ1tMAWLx3uHqBUjKLVu6lRfo1X+G5XQxph+nzLevNEH9KLIoD+WovycF2fkjVrdaCRpkwdg++AStu7PlaQ==;EndpointSuffix=core.windows.net";
            string containerName = "otelowlsblobstorage";
            var serviceClient = new BlobServiceClient(connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);

            return containerClient;
        }
    }
}
