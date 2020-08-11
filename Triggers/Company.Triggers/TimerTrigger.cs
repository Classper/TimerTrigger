using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.WindowsAzure.Storage;

namespace Company.Triggers
{
    public static class TimerTrigger
    {
        [FunctionName("TimerTrigger")]
        public static async Task RunAsync([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, ILogger log,
            [Blob("localcont", FileAccess.Write, Connection = "AzureFileStorage")]
            CloudBlobContainer outputContainer)
        {
            await outputContainer.CreateIfNotExistsAsync();

            string requestBody = DateTime.Now.ToString();
            var blobName = DateTime.Now.ToString();

            var cloudBlockBlob = outputContainer.GetBlockBlobReference(blobName);
            
            await cloudBlockBlob.UploadTextAsync(requestBody);
        }
    }
}