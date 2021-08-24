using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blobs;

namespace BlobStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class getSASController : ControllerBase
    {
       
        private readonly ILogger<getSASController> _logger;

        public getSASController(ILogger<getSASController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String getSasToken()
        {
            String sasToken = "Hello World";
            return sasToken;
        }
    }
    
    private static Uri GetServiceSasUriForBlob(BlobClient blobClient,
        string storedPolicyName = null)
    {
        // Check whether this BlobClient object has been authorized with Shared Key.
        if (blobClient.CanGenerateSasUri)
        {
            // Create a SAS token that's valid for one hour.
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                BlobName = blobClient.Name,
                Resource = "b"
            };
    
            if (storedPolicyName == null)
            {
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                sasBuilder.SetPermissions(BlobSasPermissions.Read |
                    BlobSasPermissions.Write);
            }
            else
            {
                sasBuilder.Identifier = storedPolicyName;
            }
    
            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
            Console.WriteLine("SAS URI for blob is: {0}", sasUri);
            Console.WriteLine();
    
            return sasUri;
        }
        else
        {
            Console.WriteLine(@"BlobClient must be authorized with Shared Key 
                              credentials to create a service SAS.");
            return null;
        }
    }
}
