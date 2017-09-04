using System;
using System.IO;
using System.Net;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace Gaona.Assignment.Data.Stores.Azure
{
    /// <summary>
    /// Data store using Azure blob storage. I'm assuming data sent to the API will be big (megabytes) so 
    /// using a Blob storage makes sense, also considering that data sent is binary.
    /// </summary>
    public class BlobStorageDataStore : IDataStore
    {
        private CloudBlobContainer _container;
        public BlobStorageDataStore()
        {
            ServicePointManager.DefaultConnectionLimit = 20;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnString"));
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();

            _container = client.GetContainerReference("diff");
            _container.CreateIfNotExists();

        }

        /// <summary>
        /// Add the value to the storage in binary format
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <param name="value">Base64 encoded string</param>
        public void Add(string key, string value)
        {
            CloudBlockBlob blob = _container.GetBlockBlobReference(key);
            byte[] valueBytes = Convert.FromBase64String(value);
            blob.UploadFromByteArray(valueBytes, 0, valueBytes.Length);
        }

        /// <summary>
        /// Retrieves the binary value from the storage source, converting into base64 string value
        /// </summary>
        /// <param name="key">The key of the request data</param>
        /// <returns>Base64 encoded string</returns>
        public string Get(string key)
        {
            CloudBlockBlob blob = _container.GetBlockBlobReference(key);
            MemoryStream stream = new MemoryStream();
            blob.DownloadToStream(stream);

            return Convert.ToBase64String(stream.ToArray());
        }
    }
}
