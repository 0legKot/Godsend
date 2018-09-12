using System;
using System.IO;
using System.Net;
using Telegram;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using Godsend.Models;
using System.Runtime.Serialization.Json;

namespace TelegramBot
{
    class Program
    {
        static  void Main(string[] args)
        {
            var key= "695808520:AAEHQCogzwOVT2ylbp-d6Odj8vHVGKpt9oE";
            var uri = @"http://localhost:56440/api/product/getAllCategories";
            var Bot = new Telegram.Bot.TelegramBotClient(key);
            Bot.SetWebhookAsync("").GetAwaiter();// убираем старую привязку к вебхуку для бота
            int offset = 0; // отступ по сообщениям

            
            while (true)
            {
                var updates = Bot.GetUpdatesAsync(offset).GetAwaiter().GetResult(); 

                foreach (var update in updates) 
                {
                    var message = update.Message;
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        string msg = "";
                        switch (message.Text)
                        {
                            case "/hello":
                                msg = "Hello world! I am great, yeah";
                                break;
                            case "/products":
                                var client = new HttpClient();
                                Stream respStream = client.GetStreamAsync(uri).GetAwaiter().GetResult();
                                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<CatWithSubs>));
                                IEnumerable<CatWithSubs> feed = (IEnumerable<CatWithSubs>)ser.ReadObject(respStream);
                                int i = 0;
                                foreach (var cat in feed) i++;
                                msg = $"We have {i} categories!";
                                break;
                            case "/suppliers":
                                msg = "Nope";
                                break;
                            case "/articles":
                                msg = "Nope";
                                break;
                            default:
                                msg = "You wrote: " + message.Text;
                                break;
                        }

                        if (!string.IsNullOrEmpty(msg)) Bot.SendTextMessageAsync(message.Chat.Id, msg, replyToMessageId: message.MessageId).GetAwaiter();
                    }
                    offset = update.Id + 1;
                }

            }
        }
    }
}
