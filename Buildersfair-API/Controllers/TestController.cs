using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Polly;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.Textract;
using Amazon.TranscribeService;
using Buildersfair_API.Utils;
using BuildersFair_API.Data;
using BuildersFair_API.DTOs;
using BuildersFair_API.Models;
using BuildersFair_API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildersFair_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private DataContext _context;
        IAmazonS3 S3Client { get; set; }
        IAmazonRekognition RekognitionClient { get; set; }
        IAmazonTextract TextractClient { get; set; }   
        IAmazonPolly PollyClient { get; set; }
        IAmazonTranscribeService TranscribeClient { get; set; }

        public TestController(DataContext context, IAmazonS3 s3Client, 
            IAmazonRekognition rekognitionClient, IAmazonTextract textractClient,
            IAmazonPolly pollyClient, IAmazonTranscribeService transcribeClient)
        {
            _context = context;
            this.S3Client = s3Client;
            this.RekognitionClient = rekognitionClient;
            this.TextractClient = textractClient;
            this.PollyClient = pollyClient;
            this.TranscribeClient = transcribeClient;
        }

        // POST api/test/rekognition
        [Route("rekognition")]
        [HttpPost]
        public async Task<IActionResult> RekognitionTest([FromBody] RekognitionTestDTO dto)
        {
            List<Label> labels = null;

            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=","");
            guidString = guidString.Replace("+","");
            guidString = guidString.Replace("/","");

            // Retrieving image data
            string keyName = string.Format("test/{0}.jpg", guidString);
            byte[] imageByteArray = Convert.FromBase64String(dto.base64Image);
            if (imageByteArray.Length == 0)
                return BadRequest("Image length is 0.");

            using (MemoryStream ms = new MemoryStream(imageByteArray))
            {
                // call Rekonition API
                labels = await RekognitionUtil.GetObjectDetailFromStream(this.RekognitionClient, ms);   
                
                // Upload image to S3 bucket
                // await Task.Run(() => S3Util.UploadToS3(this.S3Client, "S3_BUCKET_NAME_HERE", "KEY_NAME_HERE", ms));
            }
            
            return Ok(labels);            
        } 

        // POST api/test/polly
        [Route("polly")]
        [HttpPost]
        public async Task<IActionResult> PollyTest([FromBody] PollyTestDTO dto)
        {
            string audioFilePath = null;

            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=","");
            guidString = guidString.Replace("+","");
            guidString = guidString.Replace("/","");

            // Validation check
            if (string.IsNullOrWhiteSpace(dto.text) == true)
                return BadRequest("Text is empty.");

            // call Textract API
            Stream stream = await PollyUtil.PollyDemo(this.PollyClient, dto.text);

            // Upload image to S3 bucket
            string bucketName = "reinvent-indiamazones";
            string key = dto.text;
            await Task.Run(() => S3Util.UploadToS3(this.S3Client, bucketName, key, stream));

            audioFilePath = S3Util.GetPresignedURL(this.S3Client, bucketName, key);
            
            return Ok(audioFilePath);            
        }

        // POST api/test/transcribe
        [Route("transcribe")]
        [HttpPost]
        public async Task<IActionResult> TranscribeTest([FromBody] TranscribeTestDTO dto)
        {
            string transcriptionUri = null;

            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=","");
            guidString = guidString.Replace("+","");
            guidString = guidString.Replace("/","");

            // Validation check
            if (string.IsNullOrWhiteSpace(dto.mediaUri) == true)
                return BadRequest("mediaURI is empty.");

            // call Transcribe API
            transcriptionUri = await TranscribeUtil.TranscribeDemo(this.TranscribeClient, dto.mediaUri);
            
            return Ok(transcriptionUri);            
        } 
    }
}