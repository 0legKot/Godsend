namespace TelegramBot
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Newtonsoft.Json;
    using Telegram.Bot;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.ReplyMarkups;

    class Program
    {
        private const string key = "695808520:AAEHQCogzwOVT2ylbp-d6Odj8vHVGKpt9oE";
        private const string serverUrl = @"http://localhost:56440";
        private static ITelegramBotClient botClient;

        public static async Task Main(string[] args)
        {
            botClient = new TelegramBotClient(key);
            await botClient.SetWebhookAsync("");// delete old bot webhook binding (checked spelling)
            Console.WriteLine("Bot started");
            botClient.OnMessage += MessageReceived;
            botClient.StartReceiving();
            await Task.Delay(-1); // prevent main from exiting
        }

        private static async void MessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string msg = "";
            ParseMode parseMode = ParseMode.Html;

            var regId = new Regex(@"\d+$");
            var regText = new Regex(@"^/[a-zA-Z]+");
            string id= regId.Match(e.Message.Text).Value;
            string text = regText.Match(e.Message.Text).Value;
            int i = 1;

            switch (text)
            {
                case "/hello":
                    msg = "Hello world! I am great, yeah";
                    break;
                case "/categories":
                    msg = "<b>Our categories:</b>\n";
                    var cats = await GetItems<CatWithSubs>("product/getAllCategories");  
                    msg += "<pre>" + GetFormattedCategoryTree(cats) + "</pre>";
                    break;
                case "/products":
                    msg = "<b>Products:</b>\n\n";
                    var productsInfo = await GetItems<ProductInformation>("product/all/1/100");
                    i = 1;
                    foreach (var pInfo in productsInfo)
                        msg += $"{RatingStars(pInfo.Rating)} \n" +
                            $"<b>{pInfo.Name}</b> - /product{i++} \n\n";            
                    break;
                case "/product":
                    if (id == "" || id == "0")
                        msg = "Required id";
                    else
                    {
                        var prodInf = (await GetItems<ProductInformation>($"product/all/{id}/1")).FirstOrDefault();
                        if (prodInf == null)
                            msg = "No such product";
                        else
                        {
                            HttpClient client = new HttpClient();
                            msg = $"{RatingStars(prodInf.Rating)} \n<b>{prodInf.Name}</b> \n{prodInf.Description}";
                            msg += prodInf.State != ProductState.Normal ? $"\n<i>{prodInf.State} product</i>" : "";
                            msg += $"\n๏.๏ {prodInf.Watches}";
                            var previewStream = await client.GetStreamAsync($"{serverUrl}/api/image/getimage/" + prodInf.Preview.Id);
                            await botClient.SendPhotoAsync(e.Message.Chat, previewStream);
                        }
                    }
                    break;
                case "/suppliers":
                    msg = "<b>Suppliers:</b>\n\n";
                    var suppliersInformation = await GetItems<SupplierInformation>("supplier/all/1/100");
                    i = 1;
                    foreach (var supInfo in suppliersInformation)
                        msg += $"{RatingStars(supInfo.Rating)} \n" +
                            $"<b>{supInfo.Name}</b> - /supplier{i++}\n" +
                            $"{supInfo.Location.Address}\n\n";
                    break;
                case "/supplier":
                    if (id == "" || id == "0")
                        msg = "Required id";
                    else
                    {
                        var supInf = (await GetItems<SupplierInformation>($"supplier/all/{id}/1")).FirstOrDefault();
                        if (supInf == null)
                            msg = "No such supplier";
                        else
                        {
                            HttpClient client = new HttpClient();
                            msg = $"{RatingStars(supInf.Rating)} \n<b>{supInf.Name}</b> \n{supInf.Location.Address}\n๏.๏ {supInf.Watches}";
                            var previewStream = await client.GetStreamAsync($"{serverUrl}/api/image/getimage/" + supInf.Preview.Id);
                            await botClient.SendPhotoAsync(e.Message.Chat, previewStream);
                        }
                    }
                    break;
                case "/articles":
                    msg = "<b>Articles:</b>\n\n";
                    var articlesInformation = await GetItems<ArticleInformation>("article/all/1/100");
                    foreach (var artInfo in articlesInformation)
                        msg += $"{RatingStars(artInfo.Rating)} \n<b>{artInfo.Name}</b> \n{artInfo.Description}\n\n";
                    break;
                default:
                    msg = "You wrote: " + e.Message.Text;
                    break;
            }

            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: msg,
                replyToMessageId: e.Message.MessageId,
                parseMode: parseMode
            );
        }

        private static async Task<IEnumerable<T>> GetItems<T>(string url)
        {
            var client = new HttpClient();
            var respStream = await client.GetStreamAsync($"{serverUrl}/api/{url}");
            var json = new StreamReader(respStream).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        private static string RatingStars(double rating)
        {
            string stars = "";
            for (int i = 1; i < 6; i++)
                stars += rating >= i ? "★" : "☆";
            return stars;
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

                    s.AddLast(i == arrCats.Length - 1 ? " "/*"─"*/ : "│");
                    str += GetFormattedCategoryTreeRecursive(arrCats[i].Subs);
                    s.RemoveLast();
                }

                return str;
            }
        }
    }
}
