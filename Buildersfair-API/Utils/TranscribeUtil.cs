using System;
using System.Threading.Tasks;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;

namespace Buildersfair_API.Utils
{
    public class TranscribeUtil
    {

        public async static Task<string> TranscribeDemo(IAmazonTranscribeService transcribeClient, string mediaUri)
        {
            string result = null;
            Media media = new Media()
            {
                MediaFileUri = mediaUri
            };

            StartTranscriptionJobRequest transcriptionRequest = new StartTranscriptionJobRequest()
            {
                TranscriptionJobName = "MyTranscriptionJob",
                Media = media,
                MediaFormat = MediaFormat.Mp3.ToString(),
                LanguageCode = "en-US"
            };

            try
            {
                Task<StartTranscriptionJobResponse> transcriptionTask = transcribeClient.StartTranscriptionJobAsync(transcriptionRequest);
                StartTranscriptionJobResponse transcriptionResponse = await transcriptionTask;
                TranscriptionJob transcriptionJob = transcriptionResponse.TranscriptionJob;
                
                if (transcriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.COMPLETED)
                {
                    DateTime completionTime = transcriptionJob.CompletionTime;
                    result = transcriptionJob.Transcript.TranscriptFileUri;
                }
            }
            catch (AmazonTranscribeServiceException transcribeException)
            {
                Console.WriteLine(transcribeException.Message, transcribeException.InnerException);
            }

            return result;
        }             
    }
}