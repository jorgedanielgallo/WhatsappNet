using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace WhatsappNet.API.Services.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
    {
        public async Task<bool> Excecute(object model)
        {
            var client = new HttpClient();
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string endpoint = "https://graph.facebook.com";
                string phoneNumberId = "262843226909895";
                string accessToken = "EAAYmsg9ciJUBOZChhqqa8VEHZBSizHR2Rn70QMHzCK2rbn6PLsmFXZAmWClNZCZCHiezap1dQiOLZALl6n0yY5vZBirwZA0w3hw65pnaXaTVrDAx01fCuAJoLfYRnmZBLnGxGZAjpr3ZAJTFfl7p2uGOGSb4UnpC9TcNYqpVSDq0DVNxVOoFdJZBz8kdctUxTFF9GkUL";
                string uri = $"{endpoint}/v18.0/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
