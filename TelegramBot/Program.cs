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
                        switch (message.Text)
                        {
                            case "/hello":
                                Bot.SendTextMessageAsync(message.Chat.Id, "Hello world! I am great, yeah", replyToMessageId: message.MessageId).GetAwaiter();
                                break;
                            case "/products":
                                var msg = "";
                                var client = new HttpClient();
                                Stream respStream = client.GetStreamAsync(uri).GetAwaiter().GetResult();
                                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<CatWithSubs>));
                                IEnumerable<CatWithSubs> feed = (IEnumerable<CatWithSubs>)ser.ReadObject(respStream);
                                int i = 0;
                                foreach (var cat in feed) i++;
                                msg = $"We have {i} categories!";
                                Bot.SendTextMessageAsync(message.Chat.Id, msg, replyToMessageId: message.MessageId).GetAwaiter();
                                break;
                            case "/suppliers":
                                Bot.SendTextMessageAsync(message.Chat.Id, "Nope", replyToMessageId: message.MessageId).GetAwaiter();
                                break;
                            case "/articles":
                                Bot.SendTextMessageAsync(message.Chat.Id, "Nope", replyToMessageId: message.MessageId).GetAwaiter();
                                break;
                            default:
                                Bot.SendTextMessageAsync(message.Chat.Id, "You wrote: " + message.Text, replyToMessageId: message.MessageId).GetAwaiter();
                                break;
                        }
                        
                    }
                    offset = update.Id + 1;
                }

            }
        }
    }
}
