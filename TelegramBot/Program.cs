namespace TelegramBot
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Newtonsoft.Json;
    using Telegram.Bot;

    class Program
    {
        private const string key = "695808520:AAEHQCogzwOVT2ylbp-d6Odj8vHVGKpt9oE";
        private const string serverUrl = @"http://localhost:56440";
        private static ITelegramBotClient botClient;

        public static async Task Main(string[] args)
        {
            botClient = new TelegramBotClient(key);
            await botClient.SetWebhookAsync("");// убираем старую привязку к вебхуку для бота
            botClient.OnMessage += MessageReceived;
            botClient.StartReceiving();
            await Task.Delay(-1); // prevent main from exiting
        }

        private static async void MessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string msg = "";
            switch (e.Message.Text)
            {
                case "/hello":
                    msg = "Hello world! I am great, yeah";
                    break;
                case "/products":
                    msg = "Our categories:\n";
                    var client = new HttpClient();
                    Stream respStream = await client.GetStreamAsync($"{serverUrl}/api/product/getAllCategories");
                    string json = new StreamReader(respStream).ReadToEnd();
                    var cats = JsonConvert.DeserializeObject<IEnumerable<CatWithSubs>>(json);
                    msg += GetFormattedCategoryTree(cats);
                    break;
                case "/suppliers":
                    msg = "Nope";
                    break;
                case "/articles":
                    msg = "Nope";
                    break;
                default:
                    msg = "You wrote: " + e.Message.Text;
                    break;
            }

            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: msg,
                replyToMessageId: e.Message.MessageId
            );
        }

        static string GetFormattedCategoryTree(IEnumerable<CatWithSubs> rootCats)
        {
            var res = "";
            var s = new LinkedList<string>();
            foreach (var cat in rootCats)
            {
                res += $"{cat.Cat.Name}\n";
                res += GetFormattedCategoryTreeRecursive(cat.Subs);
            }

            return res;

            string GetFormattedCategoryTreeRecursive(IEnumerable<CatWithSubs> curCats)
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
                    str += GetFormattedCategoryTreeRecursive(arrCats[i].Subs);
                    s.RemoveLast();
                }

                return str;
            }
        }
    }
}
