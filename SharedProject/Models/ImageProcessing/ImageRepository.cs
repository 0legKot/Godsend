using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Godsend.Models
{
    public class ImageRepository
    {
        DataContext context;
        public ImageRepository(DataContext ctx)
        {
            context = ctx;
            if (!context.ImagePathsTable.Any()) {
                foreach (SimpleProduct sp in ctx.Products.Include(x => x.Info))
                    context.ImagePathsTable.Add(new ImagePaths() {Id=sp.Info.Id,Preview="apple.jpg",Images=new List<StringWrapper>() {"apple.jpg","pineapple.jpg" } });
                context.SaveChanges();
            }
        }
        public IEnumerable<string> GetImages(Guid id)
        {
            return context.ImagePathsTable.Include(x=>x.Images).FirstOrDefault(x => x.Id == id).Images.Select(x => x.Value);
        }
        public string GetImage(Guid id)
        {
            return context.ImagePathsTable.FirstOrDefault(x => x.Id == id).Preview;
        }
    }
}
