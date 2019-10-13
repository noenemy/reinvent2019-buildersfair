using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Polly;
using Amazon.Polly.Model;
using System.IO;
using System;
using BuildersFair_API.Utils;
using Amazon.S3;

namespace Buildersfair_API.Utils
{
    public class PollyUtil
    {
        public async static Task<string> PollyDemo(IAmazonPolly pollyClient, IAmazonS3 S3Client, string text)
        {
            string result = null;
            SynthesizeSpeechRequest  synthesizeRequest = new SynthesizeSpeechRequest()
            {
                LanguageCode = LanguageCode.EnUS,
                OutputFormat = "mp3",
                SampleRate = "8000",
                Text = text,
                TextType = "text",
                VoiceId = "Joanna"
            };

            try
            {
                Task<SynthesizeSpeechResponse> synthesizeTask = pollyClient.SynthesizeSpeechAsync(synthesizeRequest);
                SynthesizeSpeechResponse syntheizeResponse = await synthesizeTask;

                Console.WriteLine(syntheizeResponse.ContentType);
                Console.WriteLine(syntheizeResponse.RequestCharacters);

                using (MemoryStream ms = new MemoryStream())
                {
                    syntheizeResponse.AudioStream.CopyTo(ms);
                    Console.WriteLine(ms.Length);

                    // Upload image to S3 bucket
                    string bucketName = "reinvent-indiamazones";
                    //string key = dto.text;
                    string key = "pollytest";
                    await Task.Run(() => S3Util.UploadToS3(S3Client, bucketName, key, ms));

                    // TODO : need to check the file exists in S3
                    result = S3Util.GetPresignedURL(S3Client, bucketName, key);
                }
                //syntheizeResponse.AudioStream.CopyTo(result);
                //result.Flush();
            }
            catch (AmazonPollyException pollyException)
            {
                Console.WriteLine(pollyException.Message, pollyException.InnerException);
            }

            return result;
        }        
    }
}