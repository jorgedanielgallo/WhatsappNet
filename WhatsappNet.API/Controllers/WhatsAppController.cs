using Microsoft.AspNetCore.Mvc;
using WhatsappNet.API.Models.WhatsappCloud;
using WhatsappNet.API.Services.WhatsappCloud.SendMessage;
using WhatsappNet.API.Utils;

namespace WhatsappNet.API.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsAppController : Controller
    {
        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        private readonly IUtil _util;
        public WhatsAppController(IWhatsappCloudSendMessage whatsappCloudSendMessage, IUtil util)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
            _util = util;
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

                    object objectMessage;

                    switch (userText.ToString().ToUpper())
                    {
                        case "TEXT":
                            objectMessage = _util.TextMessage("Este es un ejemplo de texto", userNumber);
                            break;

                        case "COMPRAR":
                            objectMessage = _util.TextMessage("Seleccionaste comprar", userNumber);
                            break;

                        case "IMAGE":
                            objectMessage = _util.ImageMessage("https://images.pexels.com/photos/20568187/pexels-photo-20568187/free-photo-of-resfriado-frio-nieve-nevar.jpeg", userNumber);
                            break;

                        case "AUDIO":
                            objectMessage = _util.AudioMessage("https://actions.google.com/sounds/v1/animals/mouse_squeaking.ogg", userNumber);
                            break;

                        case "VIDEO":
                            objectMessage = _util.VideoMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/video_whatsapp.mp4", userNumber);
                            break;

                        case "DOCUMENT":
                            objectMessage = _util.DocumentMessage("https://www.clickdimensions.com/links/TestPDFfile.pdf", userNumber);
                            break;

                        case "LOCATION":
                            objectMessage = _util.LocationMessage(userNumber);
                            break;

                        case "BUTTON":
                            objectMessage = _util.ButtonsMessage(userNumber);
                            break;

                        default:
                            objectMessage = _util.TextMessage("No entiendo mi rey", userNumber);
                            break;
                    }

                    await _whatsappCloudSendMessage.Excecute(objectMessage);

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
