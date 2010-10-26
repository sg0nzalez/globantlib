using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace globantlib.DataAccess
{
    public class EntityDataAccess
    {
        GlobantLibEntities libEntities;

        public EntityDataAccess()
        {
            libEntities = new GlobantLibEntities();
        }

        private Domain.Content Create(Content c)
        {
            return new Domain.Content()
            {
                ID =  (int)c.ID,
                Title = c.Title,
                Description = c.Description,
                Author = c.Author,
                Pages = c.Pages.HasValue ? c.Pages.Value : 0 ,
                Publisher = c.Publisher,
                Released = c.Released.HasValue ? c.Released.Value : DateTime.MinValue,
                hasDigital = "Yes", hasPhysical = "Yes", 
                Digitals = new List<Domain.DigitalContent>(),
                Physicals = new List<Domain.PhysicalContent>()
            };
        }

        public List<Domain.Content> GetContent(int Page, int PageSize, string TextSearch, out int count)
        {
            List<Domain.Content> lResult = new List<Domain.Content>();

            var query = libEntities.Contents.OrderBy("it.ID")
                .Where(c => c.Title.Contains(TextSearch));

            count = query.Count<Content>();

            var result = query.Skip(Page*PageSize).Take(PageSize);

            foreach (var item in result)
            {
                lResult.Add(Create(item));
            }

            return lResult;
        }

        public Domain.Content GetContent(int id)
        {
            return Create(libEntities.Contents.Where<Content>(c => c.ID == id).FirstOrDefault());
        }

        public List<Domain.Content> GetContent()
        {
            List<Domain.Content> lResult = new List<Domain.Content>();

            foreach (var item in libEntities.Contents)
            {
                lResult.Add(Create(item));
            }

            return lResult;
        }
    }
}
