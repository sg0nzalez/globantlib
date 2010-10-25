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

        public List<Domain.Content> GetContents()
        {
            return data.GetContent(0, 50, null);
        }

        public List<Domain.Content> SearchContents(int Page, int PageSize, String Search)
        {
            return data.GetContent(Page, PageSize, Search);
        }
    }
}
