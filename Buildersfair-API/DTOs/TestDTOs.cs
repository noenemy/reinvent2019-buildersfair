namespace BuildersFair_API.DTOs
{
    public class RekognitionTestDTO
    {
        public string base64Image { get; set; }
    }

    public class TextractTestDTO
    {
        public string base64Image { get; set; }
    }

    public class PollyTestDTO
    {
        public string text { get; set; }
    }

    public class TranscribeTestDTO
    {
        public string mediaUri { get; set; }
    }
}