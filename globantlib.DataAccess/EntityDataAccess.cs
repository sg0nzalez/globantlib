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
                Description = c.Description
            };
        }

        public List<Domain.Content> GetContent(int Page, int PageSize, string TextSearch)
        {
            List<Domain.Content> lResult = new List<Domain.Content>();

            var query = libEntities.Contents.OrderBy("it.ID").Skip(Page).Take(PageSize)
                .Where(c => c.Title.Contains(TextSearch));

            foreach (var item in query)
            {
                lResult.Add(Create(item));
            }

            return lResult;
        }
    }
}
