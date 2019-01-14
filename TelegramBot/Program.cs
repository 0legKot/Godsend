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
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.ReplyMarkups;

    class Program
    {
        private const string key = "695808520:AAEHQCogzwOVT2ylbp-d6Odj8vHVGKpt9oE";
        private const string serverUrl = @"http://localhost:56440";
        private static ITelegramBotClient botClient;
        private const int itemsPerPage = 5;
        private static InlineKeyboardMarkup ikm = null;

        public static async Task Main(string[] args)
        {
            botClient = new TelegramBotClient(key);
            await botClient.SetWebhookAsync("");// delete old bot webhook binding (checked spelling)
            Console.WriteLine("Bot started");
            botClient.OnMessage += MessageReceived;
            botClient.OnCallbackQuery += CallbackQueryReceived;
            botClient.StartReceiving();            
            await Task.Delay(-1); // prevent main from exiting
        }

        private static async void CallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string[] data = e.CallbackQuery.Data.Split(' ');
            string list = data[0];
            int page = Convert.ToInt32(data[1]);            
            var pagesNum = Convert.ToInt32(data[2]);
            string msg;
            switch (list)
            {
                case "product":
                    msg = await GetListMessage<ProductInformation>("product", page);
                    break;
                case "supplier":
                    msg = await GetListMessage<SupplierInformation>("supplier", page);
                    break;
                case "article":
                    msg = await GetListMessage<ArticleInformation>("article", page);
                    break;
                default:
                    msg = "No such option";
                    break;
            }
            await botClient.EditMessageTextAsync(
                messageId: e.CallbackQuery.Message.MessageId,
                chatId: e.CallbackQuery.Message.Chat.Id,
                text: msg,
                parseMode: ParseMode.Html,
                replyMarkup: ikm
                );
        }

        private static async void MessageReceived(object sender, MessageEventArgs e)
        {
            string msg = "";
            ParseMode parseMode = ParseMode.Html;

            var regId = new Regex(@"\d+$");
            var regText = new Regex(@"^/[a-zA-Z]+");
            string idStr = regId.Match(e.Message.Text).Value;
            int id = idStr == "" ? 0 : Convert.ToInt32(idStr);
            string text = regText.Match(e.Message.Text).Value;
            ikm = null;

            switch (text)
            {
                case "/hello":
                    msg = "Hello world! I am great, yeah";
                    break;
                case "/categories":
                    msg = "<b>Our categories:</b>\n";
                    var cats = await GetItems<IEnumerable<CatWithSubs>>("product/getAllCategories");  
                    msg += "<pre>" + GetFormattedCategoryTree(cats) + "</pre>";
                    break;
                case "/products":
                    msg = await GetListMessage<ProductInformation>("product", 1);
                    break;
                case "/product":
                    try
                    {
                        var prodInfo = await GetItem<ProductInformation>("product", id);
                        var state = (prodInfo.State != ProductState.Normal ? $"<i>{prodInfo.State} product</i>\n" : "");
                        msg = 
                            $"{RatingStars(prodInfo.Rating)}\n" +
                            $"<b>{prodInfo.Name}</b>\n" +
                            $"{prodInfo.Description}\n" +
                            state +
                            $"๏.๏ {prodInfo.Watches}\n\n";
                        await SendImage(prodInfo.Preview.Id, e.Message.Chat);
                    } catch (Exception exc)
                    {
                        msg = exc.Message;
                    }                        
                    break;
                case "/suppliers":
                    msg = await GetListMessage<SupplierInformation>("supplier", 1);
                    break;
                case "/supplier":
                    try
                    {
                        var suppInfo = await GetItem<SupplierInformation>("supplier", id);
                        msg = 
                            $"{RatingStars(suppInfo.Rating)}\n" +
                            $"<b>{suppInfo.Name}</b>\n" +
                            $"{suppInfo.Location.Address}\n" +
                            $"๏.๏ {suppInfo.Watches}\n\n";
                        await SendImage(suppInfo.Preview.Id, e.Message.Chat);
                        if (!(suppInfo.Location.Longtitude == 0 && suppInfo.Location.Latitude == 0))
                            await botClient.SendVenueAsync(
                                chatId: e.Message.Chat, 
                                latitude: suppInfo.Location.Latitude, 
                                longitude: suppInfo.Location.Longtitude, 
                                title: suppInfo.Name, 
                                address: suppInfo.Location.Address
                             );
                    }
                    catch (Exception exc)
                    {
                        msg = exc.Message;
                    }
                    break;
                case "/articles":
                    msg = await GetListMessage<ArticleInformation>("article", 1);
                    break;
                case "/article":
                    try
                    {
                        var artInfo = await GetItem<ArticleInformation>("article", id);
                        msg = 
                            $"{RatingStars(artInfo.Rating)}\n" +
                            $"<b>{artInfo.Name}</b>\n" +
                            $"{artInfo.Description}\n" +
                            $"๏.๏ {artInfo.Watches}\n\n";
                    }
                    catch (Exception exc)
                    {
                        msg = exc.Message;
                    }
                    break;
                default:
                    msg = "You wrote: " + e.Message.Text;
                    break;
            }

            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: msg,
                replyToMessageId: e.Message.MessageId,
                parseMode: parseMode,
                replyMarkup: ikm
            );
        }

        private async static Task<string> GetListMessage<T>(string name, int page) where T : Information
        {
            var msg = $"<b>Our {name}s:</b>\n\n";
            var informations = await GetItems<IEnumerable<T>>($"{name}/all/{page}/{itemsPerPage}");
            var pagesNum = (int)Math.Ceiling((await GetItems<int>($"{name}/count")) / (float)itemsPerPage);
            var i = itemsPerPage * (page - 1) + 1;
            foreach (var info in informations)
            {
                msg += $"{RatingStars(info.Rating)}\n" +
                           $"<b>{info.Name}</b> - /{name}{i++}\n";
                
                switch(name)
                {
                    case "supplier":
                        msg += $"{(info as SupplierInformation).Location.Address}\n\n";
                        break;
                    case "product":
                        msg += $"{(info as ProductInformation).Description}\n\n";
                        break;
                    case "article":
                        msg += $"{(info as ArticleInformation).Description}\n\n";
                        break;
                    default:
                        msg += "\n";
                        break;
                }
            }
            ikm = GetKeyboard(page, pagesNum, name);
            return msg;
        }

        private async static Task<T> GetItem<T>(string name, int id) where T : Information
        {
            if (id == 0)
                throw new Exception("Required id");
            else
            {
                var info = (await GetItems<IEnumerable<T>>($"{name}/all/{id}/1")).FirstOrDefault();
                if (info == null)
                    throw new Exception("No such item");
                else
                    return info;
            }
        }


        private static InlineKeyboardMarkup GetKeyboard(int page, int pagesNum, string items)
        {
            InlineKeyboardButton[] ikb = new InlineKeyboardButton[pagesNum];
            string text;
            for (int k = 1; k <= pagesNum; k++)
            {
                text = k == page ? $"○{k}○" : $"{k}";
                ikb[k - 1] = new InlineKeyboardButton() { Text = $"{text}", CallbackData = $"{items} {text} {pagesNum}" };
            }
            return new InlineKeyboardMarkup(ikb);
        }

        private static async Task<T> GetItems<T>(string url)
        {
            var respStream = await new HttpClient().GetStreamAsync($"{serverUrl}/api/{url}");
            var json = new StreamReader(respStream).ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }        

        private static async Task<Message> SendImage(Guid imgId, Chat chat)
        {
            var previewStream = await new HttpClient().GetStreamAsync($"{serverUrl}/api/image/getimage/" + imgId);
            return await botClient.SendPhotoAsync(chat, previewStream);
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
