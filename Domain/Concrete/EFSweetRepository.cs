using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFSweetRepository : ISweetRepository 
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Sweet> Sweets
        {
            get { return context.Sweets; }
        }


        public void SaveSweet(Sweet sweet)
        {
            if (sweet.SweetId == 0)
                context.Sweets.Add(sweet);
            else
            {
                Sweet dbEntry = context.Sweets.Find(sweet.SweetId);
                if (dbEntry != null)
                {
                    dbEntry.Name = sweet.Name;
                    dbEntry.Ingredients = sweet.Ingredients;
                    dbEntry.Packing = sweet.Packing;
                    dbEntry.Expiration_date = sweet.Expiration_date;
                    dbEntry.Type = sweet.Type;
                    dbEntry.Price = sweet.Price;
                    dbEntry.ImageData = sweet.ImageData;
                    dbEntry.ImageMimeType = sweet.ImageMimeType;

                }
            }
            context.SaveChanges();
        }
    }
}
