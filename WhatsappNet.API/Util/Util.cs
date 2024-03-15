namespace WhatsappNet.API.Util
{
    public class Util : IUtil
    {
        public object TextMessage(string message, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "text",
                text = new
                {
                    body = message
                }
            };
        }

        public object ImageMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "image",
                image = new
                {
                    link = url
                }
            };
        }

        public object AudioMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "audio",
                audio = new
                {
                    link = url
                }
            };
        }

        public object VideoMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "video",
                video = new
                {
                    link = url
                }
            };
        }

        public object DocumentMessage(string url, string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "document",
                document = new
                {
                    link = url
                }
            };
        }

        public object LocationMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "location",
                location = new
                {
                    latitude = "4.744057014994873",
                    longitude = "-74.04708398385146",
                    name = "Gran Parrilla Boyacense - Calle 162\r\n",
                    address = "Cl. 162 #22-68, Bogotá"
                }
            };
        }

        public object ButtonsMessage(string number)
        {
            return new
            {
                messaging_product = "whatsapp",
                to = number,
                recipient_type = "individual",
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = "Selecciona una opción"
                    },
                    action = new
                    {
                        buttons = new List<object>
                        {
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "01",
                                    title = "Comprar"
                                }
                            },
                            new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = "02",
                                    title = "Vender"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
