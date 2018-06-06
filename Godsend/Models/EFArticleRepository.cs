﻿namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class EFArticleRepository : IArticleRepository
    {
        private DataContext context;

        public EFArticleRepository(DataContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;

            if (!this.context.Articles.Any())
            {
                this.context.Articles.AddRange(
                    new Article
                    {
                        Id = Guid.NewGuid(),

                            Content = @"Apple has been the divine fruit of creation, the inspiring fruit of the century and the most valuable logo.We may have rarely observed these many facets of this remarkably insignificant fruit but if we track down the history of this unassuming creation of nature, we are left with a deep sense of gratitude. This is the story of apple; the fruit that hasn’t evolved since the origin of mankind and yet finds itself in the midst of greatest cultural, scientific and technological revolution humans ever achieved.
<br>Our story begins with the first Apple: The Fruit of Creation. The Bible explains the origin of mankind in an interesting way.God had created our world in seven days(indeed 7 is a lucky number). And then he created the Garden of Eden(The Garden of Paradise) and its caretaker, the first humans Adam and Eve(Oh Darwin. Forgive me!!). Adam and Eve were allowed to roam in the garden that had the most illustrious fruits we can ever imagine. With apples, pineapples, grapes the God’s orchard was teaming up with fruits of creation.Interesting as it seems, God forbade Adam and Eve to eat any apple from his garden. The God had created apple as the “Fruit of Knowledge and evil” and strictly ordered them to keep away from it.However, a serpent manipulated Eve and asked her to persuade Adam to eat and see what the fruit of knowledge holds in itself.And like all of us, Adam fell for a girl (Oh!Dear). He snatched the apple, took the first bite and was just about to swallow when god cursed both Adam and Eve.Due to the curse the apple bite that Adam was just about to swallow stuck in his throats.That’s why the larynx of boys has been labeled Adam’s apple to this day.Adam and Eve were cursed to live in the world of sorrow and misery with the knowledge and evil of the apple i.e. “The human realm or the physical world” where they continued with their offspring; we humans. Thus, our first apple in a way created us. So, cheer up guys, perhaps apple is the reason we exist.
<br>The second part of our story begins with the most creative human ever to walk the face of the earth and his remarkable apple. Science in the 16th century was far more mysterious than it is now.There were very little proofs and a lot of assumptions back then. There were no firm mathematical reasoning to backup your scientific ideas.Yet Sir Isaac Newton, one of the greatest scientist in history somehow managed to pull science out of misery by developing the mathematical model of the Universe. And the inspiration for this was an apple.According to Newton, his theory of gravitation was inspired by the fall of an apple from its tree.In his story, Newton was relaxing in a contemplative mood in his mother’s orchard when he saw an apple falling down from the tree.This incident started a chain of events that led to the greatest discovery of classical science; the theory of Gravitation and the laws of motion.The Theory of Gravitation held sway for almost 300 years although at the end of 19th century Einstein changed the Newtonian view of the universe.But many may find it surprising to learn that putting a man on the moon some half century after Einstein did not require any modification of Newton’s theory.NASA engineers were using Newton’s laws when they programmed their rockets at Cape Kennedy. So readers, while the earth’s gravitational field holds you stuck to your chair and weighs down the laptop in your table, your own gravitational field attracts the laptop and the earth while the laptop itself……(and the physics continues…)
<br>The third and the last apple in our story is the one that we so dearly love.Apple, the most valuable company of the world is reigning in the global market of Smart Phones and Personal Computers for the past decade or so.A tiny computer company that started in a garage by two college dropouts somehow managed to gain a valuation of over 220 Billion dollars.The origin of the name of this awe - inspiring company is a curious story in itself.One of the founders of the company, Steve Jobs was a vegetarian and Apple was one of his fruitarian diets.He thought of the name as something fun, spirited and not intimidating.So for the namesake, the name remained which now with its half - eaten logo has been the dream brand for half of the population on earth.Whether it is it’s perfectly crafted body or its aesthetic beauty defined by its consumer electronic products, apple has deservedly been the most valuable company.The Iphone, Ipads, and Macbooks are synonymous to wealth, style, and fashion.The craze for these painstakingly designed gadgets has increased tremendously and thus apple has been ruling the smart electronics market for quite a long time.Sometimes I wonder if this was the apple that was specified in the saying “An apple a day keeps the doctor away” because getting an apple product is no less than recovering from a grave illness these days. And yet among these revolutionary products, there will always be a half eaten apple conjuring it’s confusing yet inspiring story whose legacy shall remain as long as the future of mankind.",
                        Info = new ArticleInformation
                        {
                            Author = userManager.Users.FirstOrDefault(),
                            Created = DateTime.Now,
                            Rating = 4.7,
                            Id = Guid.NewGuid(),
                            // Title?
                            Name = "The Three Apples that changed the World",
                            Watches = 17995730,
                            Tags = new[] { "Apple", "Bible", "Newton", "Apple Inc." }
                        }
                    },
                    new Article
                    {
                        Id = Guid.NewGuid(),

                            Content = @"<b><i>The following apple diet plan will help you lose extra pounds, deep cleanse your body and boost your immune systems. It is one of the most popular mono diets as it supplies your body with so many vitamins and minerals in the short amount of time.</i></b><br>
<h2>Benefits of Apples</h2>
The old saying, that became once again popular, goes like this: eat one apple a day to keep the doctor away. Seems silly but this is very true.
<br>Eating even one apple a day provides us with so many benefits and supplies so many vital nutrients to our body on a cellular level. Apples are considered one of the best fat burning foods because they are low in calories but high in fiber and have elements that stimulate metabolism and promote healthy weight loss.
<br>Apples are full of Vitamin C, B, PP, iron, antioxidants and many others. They promote healthy heart, detoxify liver, prevent high blood pressure, regulate digestive system, normalize bowel movement and even fight cancer.
<h2>How to Pick And Eat Apples Correctly</h2>
To get the most benefits from apples choose organic, preferably locally grown fresh apples and eat them with the skin and even seeds. Seeds in the apple are known to have the most pectin that is a fat burning fiber that stimulates healthy weight loss and control your appetite.}
<h2>7 Day Apple Diet Plan</h2>
The 7 day apple diet plan we offer here is not a fat diet and you should not extend this diet beyond 7 days. After 7 days you will lose up to 8 pounds of fat, boost your immunity and deeply detoxify your body. Make sure you drink lots of water every day. Do not be surprised to use bathroom frequently as your body will flush the toxins and fat at a high speed.
<h2>7 Day Apple Diet Plan Menu</h2>
This is a pretty simple and straightforward diet you will ever try. It involves minimum to none amount of cooking and preparation. All you have to do is eat 3-4 pounds of apples a day and drink 10-12 cups of water. Apples are very low in calories and if you feel hungry you can eat more apples and drink more water.
<br>If you start feeling bloated and constipated try juicing apples instead and see if that makes a difference. Often people who are used to eating a SAD (Standard American Diet) diet that has little healthy fiber have difficulty processing fiber at first and it takes time to adjust. But dont dread - it will eventually happen.
<br>Once your digestive system is all cleaned up of toxins you will feel much more energetic, younger and healthier.                   }
<br><b><i>Enjoy!</i></b>",
                        Info = new ArticleInformation
                        {
                            Author = userManager.Users.FirstOrDefault(),
                            Id = Guid.NewGuid(),
                            Name = "7 Day Apple Diet Plan",
                            Created = DateTime.Now,
                            Rating = 3.3,
                            Watches = 630389,
                            Tags = new[] { "Apple", "Diet", "Health" }
                        }
                    });
            }

            this.context.SaveChanges();
        }


        public IEnumerable<Article> Entities => context.Articles.Include(a => a.Info).ThenInclude(a => a.Author)
            .Include(a => a.Info).ThenInclude(a => a.EFTags);


        public void DeleteEntity(Guid entityId)
        {
            Article dbEntry = GetEntity(entityId);
            if (dbEntry != null)
            {
                context.Articles.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public Article GetEntity(Guid entityId)
        {
            return context.Articles.FirstOrDefault(a=>a.Id==entityId);
        }

        public bool IsFirst(Article entity)
        {
            return !context.Articles.Any(a=>a.Id==entity.Id);
        }

        public void SaveEntity(Article entity)
        {
            Article dbEntry = GetEntity(entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;

                // dbEntry.Status = supplier.Status;
                // ....
            }
            else
            {
                context.Add(entity);
            }

            context.SaveChanges();
        }
    }
}
