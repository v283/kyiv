using OpenAI_API;
using kyiv.Constants;


namespace ChatGPT.Services
{
    public class OpenAIService : IOpenAIService
    {

        OpenAI_API.Chat.Conversation conversation;

        public OpenAIService()
        {
            var authentication = new APIAuthentication(ApiKeys.ApiKeyGPT);

            var api = new OpenAIAPI(authentication);

            // Create a new conversation with ChatGPT
            conversation = api.Chat.CreateConversation();

        }

        public async Task<string> AskQuestion(string query)
        {
            try
            {
                conversation.AppendUserInput(query);
                return await conversation.GetResponseFromChatbot();
            }
            catch (Exception ex)
            {
                return "error, try again";
            }

        }

    }
}
