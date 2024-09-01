using Azure;
using Azure.AI.TextAnalytics;

namespace SignalRChat.Services
{
    public class LanguageService
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public LanguageService(string endpoint, string apiKey)
        {
            var credentials = new AzureKeyCredential(apiKey);
            _textAnalyticsClient = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        public async Task<string> AnalyzeSentimentAsync(string message)
        {
            var documentSentiment = await _textAnalyticsClient.AnalyzeSentimentAsync(message);
            return documentSentiment.Value.Sentiment.ToString();
        }
    }
}
