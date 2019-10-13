using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Polly;
using Amazon.Polly.Model;
using System.IO;
using System;
using BuildersFair_API.Utils;

namespace Buildersfair_API.Utils
{
    public class PollyUtil
    {
        public async static Task<Stream> PollyDemo(IAmazonPolly pollyClient, string text)
        {
            Stream result = null;
            SynthesizeSpeechRequest  synthesizeRequest = new SynthesizeSpeechRequest()
            {
                LexiconNames = new List<string> {
                    "example"
                },
                OutputFormat = "mp3",
                SampleRate = "8000",
                Text = "All Gaul is divided into three parts",
                TextType = "text",
                VoiceId = "Joanna"
            };

            try
            {
                Task<SynthesizeSpeechResponse> synthesizeTask = pollyClient.SynthesizeSpeechAsync(synthesizeRequest);
                SynthesizeSpeechResponse syntheizeResponse = await synthesizeTask;
                result = syntheizeResponse.AudioStream;
            }
            catch (AmazonPollyException pollyException)
            {
                Console.WriteLine(pollyException.Message, pollyException.InnerException);
            }

            return result;
        }        
    }
}