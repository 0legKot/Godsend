// <copyright file="SeedHelper.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Microsoft.EntityFrameworkCore;

    public interface ISeedHelper
    {
        void EnsurePopulated(DataContext context);
    }

    internal class SeedHelper : ISeedHelper
    {
        private static object creationLock = new object();

        public void EnsurePopulated(DataContext context)
        {
            lock (creationLock)
            {
                if (!context.Categories.Any() && !context.Properties.Any() && !context.Products.Any()
                    && !context.LinkProductsSuppliers.Any() && !context.LinkProductPropertyString.Any()
                    && !context.LinkProductPropertyInt.Any() && !context.LinkProductPropertyDecimal.Any()
                    && !context.Suppliers.Any() && !context.Articles.Any() && !context.Orders.Any())
                {
                    #region Categories

                    var mainCat = new Category { Name = "Unspecified" };
                    var food = new Category { Name = "Food", BaseCategory = mainCat };
                    var fruit = new Category { Name = "Fruit", BaseCategory = food };
                    var vegetables = new Category { Name = "Vegetables", BaseCategory = food };
                    var berries = new Category { Name = "Berries", BaseCategory = food };
                    var confectionery = new Category { Name = "Confectionery", BaseCategory = food };
                    var sugarConfections = new Category { Name = "Sugar confectionery", BaseCategory = confectionery };
                    var elDevices = new Category { Name = "Electronic devices", BaseCategory = mainCat };
                    var phonesAndTablets = new Category { Name = "Tablets and smartphones", BaseCategory = elDevices };
                    var mobilePhones = new Category { Name = "Mobile phones", BaseCategory = phonesAndTablets };
                    var beverages = new Category { Name = "Beverages", BaseCategory = food };
                    var alcBeverages = new Category { Name = "Alcoholic beverages", BaseCategory = beverages };
                    var nonAlcBeverages = new Category { Name = "Non-alcoholic beverages", BaseCategory = beverages };
                    var juices = new Category { Name = "Juices", BaseCategory = nonAlcBeverages };
                    var ciders = new Category { Name = "Ciders", BaseCategory = alcBeverages };
                    var vehicles = new Category { Name = "Vehicles", BaseCategory = mainCat };
                    var cars = new Category { Name = "Cars", BaseCategory = vehicles };
                    var compactMPVs = new Category { Name = "Compact MPVs", BaseCategory = cars };

                    var categories = (
                        Main: mainCat,
                        Food: food,
                        Fruit: fruit,
                        Vegetables: vegetables,
                        Berries: berries,
                        Confectionery: confectionery,
                        SugarConfections: sugarConfections,
                        ElDevices: elDevices,
                        PhonesAndTablets: phonesAndTablets,
                        MobilePhones: mobilePhones,
                        Beverages: beverages,
                        AlcBeverages: alcBeverages,
                        NonAlcBeverages: nonAlcBeverages,
                        Juices: juices,
                        Ciders: ciders,
                        Vehicles: vehicles,
                        Cars: cars,
                        CompactMPVs: compactMPVs);

                    context.Categories.AddRange(ToEnumerable<Category>(categories));

                    #endregion

                    #region Properties

                    var properties = (
                        SeedQuantity_Fruit: new Property { Name = "Seed quantity", Type = PropertyTypes.Int, RelatedCategory = categories.Fruit },
                        SeedQuantity_Berries: new Property { Name = "Seed quantity", Type = PropertyTypes.Int, RelatedCategory = categories.Berries },
                        Diameter_Fruit: new Property { Name = "Diameter", Type = PropertyTypes.Decimal, RelatedCategory = categories.Fruit },
                        Diameter_Berries: new Property { Name = "Diameter", Type = PropertyTypes.Decimal, RelatedCategory = categories.Berries },
                        Color_Fruit: new Property { Name = "Color", Type = PropertyTypes.String, RelatedCategory = categories.Fruit },
                        Color_Berries: new Property { Name = "Color", Type = PropertyTypes.String, RelatedCategory = categories.Berries },
                        Weight_Fruit: new Property { Name = "Weight", Type = PropertyTypes.Decimal, RelatedCategory = categories.Fruit },
                        Weight_Vegetables: new Property { Name = "Weight", Type = PropertyTypes.Decimal, RelatedCategory = categories.Vegetables },
                        Diagonal_TabletsAndPhones: new Property { Name = "Diagonal", Type = PropertyTypes.Decimal, RelatedCategory = categories.PhonesAndTablets },
                        RAM_TabletsAndPhones: new Property { Name = "RAM", Type = PropertyTypes.Int, RelatedCategory = categories.PhonesAndTablets },
                        AlcoholPercentage_AlcBeverages: new Property { Name = "Alcohol percentage", Type = PropertyTypes.Decimal, RelatedCategory = categories.AlcBeverages },
                        Volume_Beverages: new Property { Name = "Volume", Type = PropertyTypes.Decimal, RelatedCategory = categories.Beverages },
                        Seats_Cars: new Property { Name = "Seats", Type = PropertyTypes.Int, RelatedCategory = categories.Cars },
                        Manufacturer_Vehicles: new Property { Name = "Manufacturer", Type = PropertyTypes.String, RelatedCategory = categories.Vehicles },
                        Power_Vehicles: new Property { Name = "Power", Type = PropertyTypes.Int, RelatedCategory = categories.Vehicles });

                    context.Properties.AddRange(ToEnumerable<Property>(properties));

                    #endregion

                    #region Products

                    var products = (
                    Apple:
                        new Product(
                            "Apple",
                            "Great fruit",
                            categories.Fruit,
                            rating: 5,
                            watches: 0),
                    Potato:
                        new Product(
                            "Potato",
                            "The earth apple",
                            categories.Vegetables,
                            rating: 5,
                            watches: 4),
                    Orange:
                        new Product(
                                "Orange",
                                "Chinese apple",
                                categories.Fruit,
                                rating: 13.0 / 3,
                                watches: 7),

                    Pineapple:
                        new Product(
                                "Pineapple",
                                "Cone-looking apple",
                                categories.Fruit,
                                rating: 1.5,
                                watches: 0),

                    Aubergine:
                        new Product(
                                "Aubergine (eggplant)",
                                "The mad apple",
                                categories.Vegetables,
                                rating: Math.PI,
                                watches: 3),

                    Tomato:
                        new Product(
                                "Tomato",
                                "The love apple",
                                categories.Berries,
                                rating: Math.E,
                                watches: 13),

                    Peach:
                        new Product(
                                "Peach",
                                "The persian apple (not really)",
                                categories.Fruit,
                                rating: 4,
                                watches: 8),

                    Pommegranate:
                        new Product(
                                "Pommegranate",
                                "The seedy apple",
                                categories.Fruit,
                                rating: 3.4,
                                watches: 6),

                    Melon:
                        new Product(
                                "Melon",
                                "Apple gourd",
                                categories.Vegetables,
                                rating: 2.1,
                                watches: 8),

                    Quince:
                        new Product(
                                "Quince",
                                "Apple of Cydonia",
                                categories.Fruit,
                                rating: 3,
                                watches: 1),

                    iPhone:
                        new Product(
                                "iPhone",
                                "Another kind of apple",
                                categories.MobilePhones,
                                rating: 4.99,
                                watches: 13,
                                state: ProductState.Hot),

                    AppleJuice:
                        new Product(

                                "Apple juice",
                                "Insides of an apple squeezed to death",
                                categories.Juices,
                                rating: 4.3,
                                watches: 4),

                    Applejack:
                        new Product(

                                "Applejack",
                                "Fermented juice of apples",
                                categories.Ciders,
                                rating: 4,
                                watches: 132),
                    AppleZephyr:
                        new Product(
                                "Apple zephyr",
                                "Marshmallow made from apples",
                                categories.SugarConfections,
                                rating: 3.3,
                                watches: 123),
                    OpelZafira:
                        new Product(
                                "Opel Zafira",
                                ".",
                                categories.CompactMPVs,
                                rating: 3,
                                watches: 3));

                    context.Products.AddRange(ToEnumerable<Product>(products));

                    var productsArray = ToEnumerable<Product>(products).ToArray();

                    #endregion

                    #region Link Product Properties

                    var propertiesDecimal = (
                        Potato_Weight: new EAV<decimal> { Product = products.Potato, Value = 100, Property = properties.Weight_Vegetables },
                        Pineapple_Weight: new EAV<decimal> { Product = products.Pineapple, Value = 200, Property = properties.Weight_Fruit },
                        Orange_Weight: new EAV<decimal> { Product = products.Orange, Value = 150, Property = properties.Weight_Fruit },
                        Aubergine_Weight: new EAV<decimal> { Product = products.Aubergine, Value = 100, Property = properties.Weight_Vegetables },
                        Tomato_Weight: new EAV<decimal> { Product = products.Tomato, Value = 300, Property = properties.Weight_Vegetables },
                        Peach_Weight: new EAV<decimal> { Product = products.Peach, Value = 200, Property = properties.Weight_Fruit },
                        Pommegranate_Weight: new EAV<decimal> { Product = products.Pommegranate, Value = 100, Property = properties.Weight_Fruit },
                        Melon_Weight: new EAV<decimal> { Product = products.Melon, Value = 300, Property = properties.Weight_Vegetables },
                        Quince_Weight: new EAV<decimal> { Product = products.Quince, Value = 200, Property = properties.Weight_Fruit },
                        iPhone_Diagonal: new EAV<decimal> { Product = products.iPhone, Value = 5.7M, Property = properties.Diagonal_TabletsAndPhones },
                        Applejack_Volume: new EAV<decimal> { Product = products.Applejack, Value = 1, Property = properties.Volume_Beverages },
                        AppleJuice_Volume: new EAV<decimal> { Product = products.AppleJuice, Value = 1, Property = properties.Volume_Beverages },
                        Applejack_AlcPercentage: new EAV<decimal> { Product = products.Applejack, Value = 0.1M, Property = properties.AlcoholPercentage_AlcBeverages },
                        Apple_Diameter: new EAV<decimal> { Product = products.Apple, Value = 64, Property = properties.Diameter_Fruit },
                        Apple_Weight: new EAV<decimal> { Product = products.Apple, Value = 1000, Property = properties.Weight_Fruit });

                    context.LinkProductPropertyDecimal.AddRange(ToEnumerable<EAV<decimal>>(propertiesDecimal));

                    var propertiesInt = (
                        Pineapple_SeedQuantity: new EAV<int> { Product = products.Pineapple, Value = 000, Property = properties.SeedQuantity_Fruit },
                        Orange_SeedQuantity: new EAV<int> { Product = products.Orange, Value = 983, Property = properties.SeedQuantity_Fruit },
                        Peach_SeedQuantity: new EAV<int> { Product = products.Peach, Value = 1, Property = properties.SeedQuantity_Fruit },
                        Quince_SeedQuantity: new EAV<int> { Product = products.Quince, Value = 3, Property = properties.SeedQuantity_Fruit },
                        Pommegranate_SeedQuantity: new EAV<int> { Product = products.Pommegranate, Value = 3264, Property = properties.SeedQuantity_Fruit },
                        Tomato_SeedQuantity: new EAV<int> { Product = products.Tomato, Value = 45, Property = properties.SeedQuantity_Berries },
                        iPhone_RAM: new EAV<int> { Product = products.iPhone, Value = 10241024, Property = properties.RAM_TabletsAndPhones },
                        OpelZafira_Seats: new EAV<int> { Product = products.OpelZafira, Value = 8, Property = properties.Seats_Cars },
                        OpelZafira_Power: new EAV<int> { Product = products.OpelZafira, Value = 250, Property = properties.Power_Vehicles },
                        Apple_SeedQuantity: new EAV<int>() { Product = products.Apple, Value = 2, Property = properties.SeedQuantity_Fruit });

                    context.LinkProductPropertyInt.AddRange(ToEnumerable<EAV<int>>(propertiesInt));

                    var propertiesString = (
                        Pineapple_Color: new EAV<string> { Product = products.Pineapple, Value = "Yellow", Property = properties.Color_Fruit },
                        Orange_Color: new EAV<string> { Product = products.Orange, Value = "Orange", Property = properties.Color_Fruit },
                        Peach_Color: new EAV<string> { Product = products.Peach, Value = "Orange", Property = properties.Color_Fruit },
                        Quince_Color: new EAV<string> { Product = products.Quince, Value = "No idea", Property = properties.Color_Fruit },
                        Pommegranate_Color: new EAV<string> { Product = products.Pommegranate, Value = "Red", Property = properties.Color_Fruit },
                        Tomato_Color: new EAV<string> { Product = products.Tomato, Value = "Red", Property = properties.Color_Berries },
                        OpelZafira_Manufacturer: new EAV<string> { Product = products.OpelZafira, Value = "Opel", Property = properties.Manufacturer_Vehicles },
                        Apple_Color: new EAV<string> { Product = products.Apple, Value = "красное", Property = properties.Color_Fruit });

                    context.LinkProductPropertyString.AddRange(ToEnumerable<EAV<string>>(propertiesString));

                    #endregion

                    #region Suppliers

                    var suppliers = (
                    USA:
                        new Supplier(
                                "USA supply",
                                "New York",
                                rating: 5,
                                watches: 3),
                    Russia:
                        new Supplier(
                                "Russia supply",
                                "Moscow",
                                rating: 0,
                                watches: 100),
                    AppleInc:
                        new Supplier(
                                "Apple Inc.",
                                "Cupertino, California",
                                rating: 4.99,
                                watches: 156),
                    SweetAppleAcres:
                        new Supplier(
                                "Sweet Apple Acres",
                                "Equestria",
                                rating: 4.3,
                                watches: 721),
                    SomersetCiderBrandy:
                        new Supplier(
                                "The Somerset Cider Brandy Company",
                                "Pass Vale Farm, Burrow Hill, Kingsbury Episcopi, Martock.Somerset",
                                rating: 3.2,
                                watches: 24),
                    APEL:
                        new Supplier(
                                "АПЭЛ",
                                "Tolyatti",
                                rating: 2.1,
                                watches: 66),
                    Opel:
                        new Supplier(
                                "Opel",
                                "Rüsselsheim am Main",
                                rating: 4.1,
                                watches: 126),
                    DoleFoodCompany:
                        new Supplier(
                                "Dole Food Company",
                                "Westlake Village, California",
                                rating: 3.7,
                                watches: 22000),
                    Monsanto:
                        new Supplier(
                                "Monsanto",
                                "800 N. Lindbergh Boulevard St.Louis",
                                rating: 4.4,
                                watches: 623),
                    Nestle:
                        new Supplier(
                                "Nestlé S.A.",
                                "Vevey, Vaud",
                                rating: 3.8,
                                watches: 1256),
                    CocaCola:
                        new Supplier(
                                "The Coca-Cola Company",
                                "Atlanta, Georgia",
                                rating: 4.5,
                                watches: 1566),
                    ChinaGrainOilFood:
                        new Supplier(
                                "中国粮油食品（集团）有限公司",
                                "Beijing",
                                rating: 4.6,
                                watches: 666));

                    context.Suppliers.AddRange(ToEnumerable<Supplier>(suppliers));

                    var suppliersArray = ToEnumerable<Supplier>(suppliers).ToArray();

                    #endregion

                    #region Articles

                    var articles = (
                    TheThreeApples:
                        new Article(

                            @"Apple has been the divine fruit of creation, the inspiring fruit of the century and the most valuable logo.We may have rarely observed these many facets of this remarkably insignificant fruit but if we track down the history of this unassuming creation of nature, we are left with a deep sense of gratitude. This is the story of apple; the fruit that hasn’t evolved since the origin of mankind and yet finds itself in the midst of greatest cultural, scientific and technological revolution humans ever achieved.
<br>Our story begins with the first Apple: The Fruit of Creation. The Bible explains the origin of mankind in an interesting way.God had created our world in seven days(indeed 7 is a lucky number). And then he created the Garden of Eden(The Garden of Paradise) and its caretaker, the first humans Adam and Eve(Oh Darwin. Forgive me!!). Adam and Eve were allowed to roam in the garden that had the most illustrious fruits we can ever imagine. With apples, pineapples, grapes the God’s orchard was teaming up with fruits of creation.Interesting as it seems, God forbade Adam and Eve to eat any apple from his garden. The God had created apple as the “Fruit of Knowledge and evil” and strictly ordered them to keep away from it.However, a serpent manipulated Eve and asked her to persuade Adam to eat and see what the fruit of knowledge holds in itself.And like all of us, Adam fell for a girl (Oh!Dear). He snatched the apple, took the first bite and was just about to swallow when god cursed both Adam and Eve.Due to the curse the apple bite that Adam was just about to swallow stuck in his throats.That’s why the larynx of boys has been labeled Adam’s apple to this day.Adam and Eve were cursed to live in the world of sorrow and misery with the knowledge and evil of the apple i.e. “The human realm or the physical world” where they continued with their offspring; we humans. Thus, our first apple in a way created us. So, cheer up guys, perhaps apple is the reason we exist.
<br>The second part of our story begins with the most creative human ever to walk the face of the earth and his remarkable apple. Science in the 16th century was far more mysterious than it is now.There were very little proofs and a lot of assumptions back then. There were no firm mathematical reasoning to backup your scientific ideas.Yet Sir Isaac Newton, one of the greatest scientist in history somehow managed to pull science out of misery by developing the mathematical model of the Universe. And the inspiration for this was an apple.According to Newton, his theory of gravitation was inspired by the fall of an apple from its tree.In his story, Newton was relaxing in a contemplative mood in his mother’s orchard when he saw an apple falling down from the tree.This incident started a chain of events that led to the greatest discovery of classical science; the theory of Gravitation and the laws of motion.The Theory of Gravitation held sway for almost 300 years although at the end of 19th century Einstein changed the Newtonian view of the universe.But many may find it surprising to learn that putting a man on the moon some half century after Einstein did not require any modification of Newton’s theory.NASA engineers were using Newton’s laws when they programmed their rockets at Cape Kennedy. So readers, while the earth’s gravitational field holds you stuck to your chair and weighs down the laptop in your table, your own gravitational field attracts the laptop and the earth while the laptop itself……(and the physics continues…)
<br>The third and the last apple in our story is the one that we so dearly love.Apple, the most valuable company of the world is reigning in the global market of Smart Phones and Personal Computers for the past decade or so.A tiny computer company that started in a garage by two college dropouts somehow managed to gain a valuation of over 220 Billion dollars.The origin of the name of this awe - inspiring company is a curious story in itself.One of the founders of the company, Steve Jobs was a vegetarian and Apple was one of his fruitarian diets.He thought of the name as something fun, spirited and not intimidating.So for the namesake, the name remained which now with its half - eaten logo has been the dream brand for half of the population on earth.Whether it is it’s perfectly crafted body or its aesthetic beauty defined by its consumer electronic products, apple has deservedly been the most valuable company.The Iphone, Ipads, and Macbooks are synonymous to wealth, style, and fashion.The craze for these painstakingly designed gadgets has increased tremendously and thus apple has been ruling the smart electronics market for quite a long time.Sometimes I wonder if this was the apple that was specified in the saying “An apple a day keeps the doctor away” because getting an apple product is no less than recovering from a grave illness these days. And yet among these revolutionary products, there will always be a half eaten apple conjuring it’s confusing yet inspiring story whose legacy shall remain as long as the future of mankind.",
                            context.Users.FirstOrDefault(),
                            description: "Three is better than two",
                            rating: 4.7,
                            name: "The Three Apples that changed the World",
                            watches: 17995730),
                    AppleDiet:
                        new Article(
                            @"<b><i>The following apple diet plan will help you lose extra pounds, deep cleanse your body and boost your immune systems. It is one of the most popular mono diets as it supplies your body with so many vitamins and minerals in the short amount of time.</i></b><br>
<h2>Benefits of Apples</h2>
The old saying, that became once again popular, goes like this: eat one apple a day to keep the doctor away. Seems silly but this is very true.
<br>Eating even one apple a day provides us with so many benefits and supplies so many vital nutrients to our body on a cellular level. Apples are considered one of the best fat burning foods because they are low in calories but high in fiber and have elements that stimulate metabolism and promote healthy weight loss.
<br>Apples are full of Vitamin C, B, PP, iron, antioxidants and many others. They promote healthy heart, detoxify liver, prevent high blood pressure, regulate digestive system, normalize bowel movement and even fight cancer.
<h2>How to Pick And Eat Apples Correctly</h2>
To get the most benefits from apples choose organic, preferably locally grown fresh apples and eat them with the skin and even seeds. Seeds in the apple are known to have the most pectin that is a fat burning fiber that stimulates healthy weight loss and control your appetite.
<h2>7 Day Apple Diet Plan</h2>
The 7 day apple diet plan we offer here is not a fat diet and you should not extend this diet beyond 7 days. After 7 days you will lose up to 8 pounds of fat, boost your immunity and deeply detoxify your body. Make sure you drink lots of water every day. Do not be surprised to use bathroom frequently as your body will flush the toxins and fat at a high speed.
<h2>7 Day Apple Diet Plan Menu</h2>
This is a pretty simple and straightforward diet you will ever try. It involves minimum to none amount of cooking and preparation. All you have to do is eat 3-4 pounds of apples a day and drink 10-12 cups of water. Apples are very low in calories and if you feel hungry you can eat more apples and drink more water.
<br>If you start feeling bloated and constipated try juicing apples instead and see if that makes a difference. Often people who are used to eating a SAD (Standard American Diet) diet that has little healthy fiber have difficulty processing fiber at first and it takes time to adjust. But dont dread - it will eventually happen.
<br>Once your digestive system is all cleaned up of toxins you will feel much more energetic, younger and healthier.                   
<br><b><i>Enjoy!</i></b>",
                            context.Users.FirstOrDefault(),
                            description: "Diet is evil",
                            name: "7 Day Apple Diet Plan",
                            rating: 3.3,
                            watches: 630389),
                    ApplesHistory:
                        new Article(
                            @"The apple, that innocent bud of an Americana autumn, has pulled off one of the greatest cons of all time. As students across the country prepare to greet a new school year and teacher with a polished bit of produce, the apple cements its place in the patriotic foods pantheon despite its dodgy past.
<br>The apple was long associated with the downfall of man, but has managed to do pretty well for itself since. Illustration from Eve’s Diary, written by Mark Twain.
<br>A clever bit of biology, well documented in Michael Pollan’s Botany of Desire, and a tireless cheer campaign of fall orchard visits and doctor-endorsed slogans saved the apple from its bitter beginnings in early America. Though its standing in society today is rivaled only by bald eagles and baseball, the apple’s journey to ubiquity was tumultuous.
<br>Stretching back to the hills of Kazakhstan, early apples were a far cry from today’s sweet, fleshy varieties. As Pollan explains, sweetness is a rarity in nature. Apples benefitted from being bitter and sometimes poisonous because it allowed the seeds to spread unmolested. Because each seed has the genetic content of a radically different tree, the fruit came in countless forms, “from large purplish softballs to knobby green clusters.”
<br>When the apple came to the American colonies, it was still a long way from a sweet treat. Bitter but easy to grow, the produce made excellent hard cider. In a time when water was considered more dangerous than consuming alcohol, hard cider was a daily indulgence. Its distilled cousin, applejack, also became popular, according to documentation from Colonial Williamsburg.
<br>As anyone who grew up in the Ohio River Valley knows, the greatest champion of the fruit was a wandering missionary named John Chapman, or Johnny Appleseed. Pennsylvania, Ohio, Indiana and beyond bloomed in the wake of his visits. He was opposed to grafting, the practice of inserting “a section of a stem with leaf buds is inserted into the stock of a tree” to reproduce the same type of apples from the first tree, as described by the University of Minnesota.
<br>Without the human intervention, however, apples remained overwhelmingly bitter and when an anti-alcohol fervor swept the nation in the late 19th century, the plant’s fate was in peril. One of the fiercest of opponents, temperance supporter and axe-wielding activist Carrie Nation, went after both growers and bars, leaving a wake of destruction in her path. Nation was arrested 30 times in a ten-year span for vandalism in the name of her movement, according to PBS.
<br>“But with the help of early public relations pioneers crafting slogans such as “an apple a day keeps the doctor away,” the plant quickly reinvented itself as a healthy foodstuff,” according to the PBS production of Pollan’s work.
<br>Elizabeth Mary Wright’s 1913 book, Rustic Speech and Folk-lore, recorded the use of apples as part of common kitchen cures. “For example,” she writes, “Ait a happle avore gwain to bed, An’ you’ll make the doctor beg his bread…or as the more popular version runs: An apple a day Keeps the doctor away.”
<br>An advertisement from the early 20th century extols the healthful virtues of Washington apples. Courtesy of the National Museum of American History, Smithsonian Institution
<br>Free to produce a socially acceptable fruit, growers raced to develop sweet, edible varieties that would replace the plant’s previous life. Shaking its association with hard cider and reckless imbibing, the apple found a place in one of the most faultless places of American society: the schoolhouse.
<br>Held up as the paragon of moral fastidiousness, teachers, particularly on the frontier, frequently received sustenance from their pupils. “Families whose children attended schools were often responsible for housing and feeding frontier teachers,” according to a PBS special, titled “Frontier House, Frontier Life.” An apple could show appreciation for a teacher sometimes in charge of more than 50 students.
<br>Apples continued to be a favorite way to curry favor even after the practical purpose of feeding teachers disappeared. Bing Crosby’s 1939 “An Apple for the Teacher,” explains the persuasive allure of the fruit. “An apple for the teacher will always do the trick,” sings Crosby, “when you don’t know your lesson in arithmetic.”
<br>By the time American scholar Jan Harold Brunvand published his book, The Study of American Folklore, in 1968, the phrase “apple-polisher” was more or less shorthand for brown-nosing suck-up. With cutting-edge technology in classrooms seen as an academic advantage, many teachers may be asking for a completely different kind of apple: not a Red Delicious or Granny Smith but an iPad.
<br>Read more: https://www.smithsonianmag.com/arts-culture/why-do-students-give-teachers-apples-and-more-from-the-fruits-juicy-past-26381703/#ftOr5mifKGtu1Q1E.99",
                            context.Users.FirstOrDefault(),
                            description: "It is older than you, man",
                            name: "Why Do Students Give Teachers Apples and More from the Fruit’s Juicy Past",
                            rating: 3.6,
                            watches: 15),
                    Empty:
                        new Article(
                            ".",
                            context.Users.FirstOrDefault(),
                            description: "You can't see it",
                            name: "Empty Article(",
                            rating: 1,
                            watches: 5),
                    TheAnswer:
                        new Article(
                            "42",
                            context.Users.FirstOrDefault(),
                            description: "Java is not an answer, it is a question. BUT THE ANSWER IS NO",
                            name: "The Answer to the Ultimate Question of Life, the Universe, and Everything",
                            rating: 5,
                            watches: 42),
                    AppleVSAndroid:
                        new Article(
                            "Please answer us in the comments",
                            context.Users.FirstOrDefault(),
                            description: "Apple is better, you won't persuade me",
                            name: "Apple or Android - which is better?",
                            rating: 1.1,
                            watches: 66)
                    );

                    context.Articles.AddRange(ToEnumerable<Article>(articles));

                    #endregion

                    #region tags

                    var tags = (
                        apple: new Tag("Apple"),
                        diet: new Tag("Diet"),
                        appleInc: new Tag("AppleInc"),
                        android: new Tag("Android"),
                        life: new Tag("Life"),
                        universe: new Tag("Universe"),
                        health: new Tag("Health"),
                        bible: new Tag("Bible"),
                        newton: new Tag("Newton")
                    );

                    context.Tags.AddRange(ToEnumerable<Tag>(tags));

                    var linkArticleTags = new List<LinkArticleTag>()
                    {
                        new LinkArticleTag() { Article = articles.TheThreeApples, Tag = tags.apple },
                        new LinkArticleTag() { Article = articles.TheThreeApples, Tag = tags.appleInc },
                        new LinkArticleTag() { Article = articles.TheThreeApples, Tag = tags.bible },
                        new LinkArticleTag() { Article = articles.TheThreeApples, Tag = tags.newton },
                        new LinkArticleTag() { Article = articles.AppleDiet, Tag = tags.apple },
                        new LinkArticleTag() { Article = articles.AppleDiet, Tag = tags.diet },
                        new LinkArticleTag() { Article = articles.AppleDiet, Tag = tags.health },
                        new LinkArticleTag() { Article = articles.ApplesHistory, Tag = tags.apple },
                        new LinkArticleTag() { Article = articles.AppleVSAndroid, Tag = tags.appleInc },
                        new LinkArticleTag() { Article = articles.AppleVSAndroid, Tag = tags.android },
                        new LinkArticleTag() { Article = articles.TheAnswer, Tag = tags.life },
                        new LinkArticleTag() { Article = articles.TheAnswer, Tag = tags.universe },
                    };

                    context.LinkArticleTags.AddRange(linkArticleTags);

                    #endregion

                    #region Link Products Suppliers

                    var linkProductsSuppliersArray = new List<LinkProductsSuppliers>();

                    for (int i = 0; i < productsArray.Length; ++i)
                    {
                        linkProductsSuppliersArray.AddRange(new[]
                        {
                            new LinkProductsSuppliers
                            {
                                Product = productsArray[i],
                                Supplier = suppliersArray[i % suppliersArray.Length],
                                Price = (decimal)((i + 1) * 100.1)
                            },
                            new LinkProductsSuppliers
                            {
                                Product = productsArray[i],
                                Supplier = suppliersArray[(i + 2) % suppliersArray.Length],
                                Price = (decimal)((i + 3) * 100.1)
                            }
                        });
                    }

                    context.LinkProductsSuppliers.AddRange(linkProductsSuppliersArray);

                    #endregion

                    #region Orders

                    IList<OrderPartProducts> orderPartDiscretes = new List<OrderPartProducts>();
                    foreach (var p in productsArray)
                    {
                        orderPartDiscretes.Add(new OrderPartProducts { Quantity = p.Info.Watches * 5, Multiplier = 10, Product = p, Supplier = linkProductsSuppliersArray.FirstOrDefault(lpp => lpp.Product == p)?.Supplier });
                    }

                    var orders = (
                    Ancient:
                        new SimpleOrder
                        {
                            EFCustomer = context.Users.FirstOrDefault(),
                            Done = new DateTime(1000),
                            Ordered = new DateTime(100),
                            Status = Status.Ready,
                            Items = orderPartDiscretes.Select(x => new OrderPartProducts { Quantity = x.Quantity, Multiplier = x.Multiplier, Product = x.Product, Supplier = x.Supplier }).ToList()
                        },
                    Modern:
                       new SimpleOrder
                       {
                           EFCustomer = context.Users.FirstOrDefault(),
                           Done = new DateTime(2014, 2, 2),
                           Ordered = new DateTime(2013, 2, 3),
                           Status = Status.Ready,
                           Items = orderPartDiscretes.Select(x => new OrderPartProducts { Quantity = x.Quantity, Multiplier = x.Multiplier, Product = x.Product, Supplier = x.Supplier }).ToList()
                       });

                    context.AddRange(ToEnumerable<Order>(orders));

                    #endregion

                    #region Images

                    foreach (Product p in productsArray)
                    {
                        context.ImagePathsTable.Add(new ImagePaths() { Id = p.Id, Preview = p.Info.Name + ".jpg", Images = new List<StringWrapper>() { p.Info.Name + ".jpg", "pineapple.jpg" } });
                    }

                    foreach (Supplier s in suppliersArray)
                    {
                        context.ImagePathsTable.Add(new ImagePaths() { Id = s.Id, Preview = "supp" + s.Info.Name +".jpg" });
                    }

                    #endregion

                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<T> ToEnumerable<T>(ITuple tuple)
        {
            for (int i = 0; i < tuple.Length; ++i)
            {
                if (tuple[i] is T tmp)
                {
                    yield return tmp;
                }
                else
                {
                    throw new ArgumentException("Illegal type");
                }
            }
        }
    }
}
