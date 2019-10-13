using System;
using System.Threading.Tasks;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using System.Threading;

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
                TranscriptionJobName = DateTime.Now.Millisecond.ToString(),
                Media = media,
                MediaFormat = MediaFormat.Mp3.ToString(),
                LanguageCode = "en-US",
                OutputBucketName = "reinvent-indiamazones"
            };

            try
            {
                Task<StartTranscriptionJobResponse> transcriptionTask = transcribeClient.StartTranscriptionJobAsync(transcriptionRequest);
                StartTranscriptionJobResponse transcriptionResponse = await transcriptionTask;
                TranscriptionJob transcriptionJob = transcriptionResponse.TranscriptionJob;
                
                bool loop = true;
                while (loop == true)
                {
                    if (transcriptionResponse.TranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.IN_PROGRESS)
                    {
                        Console.WriteLine(transcriptionResponse.TranscriptionJob.TranscriptionJobName);
                        Console.WriteLine(transcriptionResponse.TranscriptionJob.TranscriptionJobStatus);
                        if (transcriptionResponse.TranscriptionJob.Transcript != null)
                            Console.WriteLine(transcriptionResponse.TranscriptionJob.Transcript.TranscriptFileUri);
                        Thread.Sleep(3000);
                    }
                    else if (transcriptionResponse.TranscriptionJob.TranscriptionJobStatus == TranscriptionJobStatus.COMPLETED)
                    {
                        Console.Write("Transcription job completed.");
                        DateTime completionTime = transcriptionJob.CompletionTime;
                        result = transcriptionJob.Transcript.TranscriptFileUri;

                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine(transcriptionResponse.TranscriptionJob.TranscriptionJobStatus);
                        result = string.Empty;

                        loop = false;
                    }
                }

                result = transcriptionResponse.TranscriptionJob.Transcript.TranscriptFileUri;
            }
            catch (AmazonTranscribeServiceException transcribeException)
            {
                Console.WriteLine(transcribeException.Message, transcribeException.InnerException);
            }

            return result;
        }             
    }
}