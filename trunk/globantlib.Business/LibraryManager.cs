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
        //
        public void Create(Domain.Content instance)
        {
            data.Create(instance);
        }

        public List<Domain.Review> GetReviews(int id)
        {
            return data.GetReviews(id);
        }

        public Domain.Review SubmitReview(int i, Domain.Review instance)
        {
            return data.SubmitReview(i, instance);
        }

        public List<Domain.Types> GetPhysicals(int ContentID, int Id, int Month, int Year)
        {
            return data.GetPhysicals(ContentID, Id, Month, Year);
        }

        public Domain.BookRequestCollection GetBookRequests()
        {
            return data.GetBookRequests();
        }

        public void SubmitBookRequest(string text)
        {
            data.SubmitBookRequest(text);
        }

        public List<Domain.Content> GetContents(string text)
        {
            return data.GetContent(text);
        }

        public void Delete(int i)
        {
            data.Delete(i);
        }

        public void DeleteDigital(int i)
        {
            data.DeleteDigital(i);
        }

        public void Update(Domain.Content instance)
        {
            data.Update(instance);
        }
    }
}
