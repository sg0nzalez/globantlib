using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using globantlib.Domain;
using globantlib.DataAccess;

namespace globantlib.Business
{
    public class LibraryManager
    {
        EntityDataAccess data = new EntityDataAccess();

        public Domain.Content GetContent(int id)
        {
            return data.GetContent(id);
        }

        public List<Domain.Content> SearchContents(int Page, int PageSize, String Search, out int count)
        {
            return data.GetContent(Page, PageSize, Search, out count);
        }

        public List<Domain.Content> GetContents()
        {
            return data.GetContent();
        }
    }
}
