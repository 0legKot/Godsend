using System;
using System.IO;
using System.Linq;
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
                                var msg = "Our categories:\n";
                                var client = new HttpClient();
                                Stream respStream = client.GetStreamAsync(uri).GetAwaiter().GetResult();
                                string json = new StreamReader(respStream).ReadToEnd();
                                var cats = JsonConvert.DeserializeObject<IEnumerable<CatWithSubs>>(json);
                                msg += GetFormattedCategoryTree(cats);
                                Bot.SendTextMessageAsync(message.Chat.Id, msg, replyToMessageId: message.MessageId).GetAwaiter();
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
                    }
                    offset = update.Id + 1;
                }

            }
        }

        static string GetFormattedCategoryTree(IEnumerable<CatWithSubs> rootCats)
        {
            var res = "";
            var s = new LinkedList<string>();
            foreach (var cat in rootCats)
            {
                res += $"{cat.Cat.Name}\n";
                res += GetFormattedCategoryTreeRecursive(cat.Subs, 0);
            }

            return res;

            string GetFormattedCategoryTreeRecursive(IEnumerable<CatWithSubs> curCats, int level)
            {
                var str = "";
                var arrCats = curCats.ToArray();
                for (int i = 0; i < arrCats.Length; ++i)
                {
                    if (s.Any())
                    {
                        str += s.Aggregate((a, b) => a + b);
                    }
                    str += i == arrCats.Length - 1 ? "└" : "├";
                    str += $"{arrCats[i].Cat.Name}\n";

                    s.AddLast(i == arrCats.Length - 1 ? "─" : "│");
                    str += GetFormattedCategoryTreeRecursive(arrCats[i].Subs, level + 1);
                    s.RemoveLast();
                }

                return str;
            }
        }
    }
}
