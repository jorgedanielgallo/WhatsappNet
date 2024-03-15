namespace WhatsappNet.API.Services.WhatsappCloud.SendMessage
{
    public interface IWhatsappCloudSendMessage
    {
        Task<bool> Excecute(object model);
    }
}
