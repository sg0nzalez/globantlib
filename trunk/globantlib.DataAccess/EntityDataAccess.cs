using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

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
                hasDigital = c.Digitals.Count > 0 ? "Yes" : "No",
                hasPhysical = c.Physicals.Count > 0 ? "Yes" : "No", 
                Digitals = c.Digitals.Count > 0 ? Create(c.Digitals) : new List<Domain.DigitalContent>(),
                Physicals = c.Physicals.Count > 0 ? Create(c.Physicals) : new List<Domain.PhysicalContent>()
            };
        }

        private Domain.DigitalContent Create(Digital item)
        {
            return new Domain.DigitalContent()
            {
                ID = (int)item.ID,
                Link = item.Link,
                Format = item.Format
            };
        }

        private List<Domain.DigitalContent> Create(EntityCollection<Digital> entityCollection)
        {
            List<Domain.DigitalContent> lResult = new List<Domain.DigitalContent>();
            foreach (var item in entityCollection)
            {
                lResult.Add(Create(item));
            }
            return lResult;
        }

        private Domain.PhysicalContent Create(Physical item)
        {
            return new Domain.PhysicalContent()
            {
                ID = (int)item.ID,
                Type = item.Type
            };
        }

        private List<Domain.PhysicalContent> Create(EntityCollection<Physical> entityCollection)
        {
            List<Domain.PhysicalContent> lResult = new List<Domain.PhysicalContent>();
            foreach (var item in entityCollection)
            {
                lResult.Add(Create(item));
            }
            return lResult;
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
            return Create(libEntities.Contents.Include("Digitals").Include("Physicals").Where<Content>(c => c.ID == id).FirstOrDefault());
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
