using Microsoft.AspNetCore.Mvc;
using WhatsappNet.API.Models.WhatsappCloud;
using WhatsappNet.API.Services.WhatsappCloud.SendMessage;

namespace WhatsappNet.API.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsAppController : Controller
    {
        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        public WhatsAppController(IWhatsappCloudSendMessage whatsappCloudSendMessage)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
        }
        [HttpGet("test")]
        public async Task<IActionResult> Sample()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "573192513437",
                recipient_type = "individual",
                type = "text",
                text = new
                {
                    body = "Este es un mensaje de prueba"
                }
            };

            var result = await _whatsappCloudSendMessage.Excecute(data);
            if (result)
            {
                return Ok("Respueta bien");
            }
            return Ok("Respuesta mal");
        }

        [HttpGet]
        public IActionResult VerifyToken()
        {
            string AccessToken = "ASDASDASDAS9324JASFDASD";

            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if(challenge != null && token != null && token == AccessToken)
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {
                var message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                if(message != null)
                {
                    var userNumber = message.From;
                    var userText = GetUserText(message);
                }
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private object GetUserText(Message message)
        {
            string typeMessage = message.Type;

            if(typeMessage.ToUpper() == "TEXT")
            {
                return message.Text.Body;
            }
            else if (typeMessage.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive.Type;

                if(interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply.Title;
                }
                else if(interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply.Title;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
