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

            if (c != null)
            {
                return new Domain.Content()
                    {
                        ID = (int)c.ID,
                        Title = c.Title,
                        Description = c.Description,
                        Author = c.Author,
                        Pages = c.Pages.HasValue ? c.Pages.Value : 0,
                        Publisher = c.Publisher,
                        ISBN = c.ISBN,
                        Thumbnail = c.ISBN == null ? "img/no-img.gif?dummy=noimg" : @"http://bks8.books.google.com/books?vid=ISBN" + c.ISBN + @"&printsec=frontcover&img=1",
                        Released = c.Released.HasValue ? c.Released.Value.ToShortDateString() : "",
                        hasDigital = c.Digitals.Count > 0 ? "Yes" : "No",
                        hasPhysical = c.Physicals.Count > 0 ? "Yes" : "No",
                        Digitals = c.Digitals.Count > 0 ? Create(c.Digitals) : new List<Domain.DigitalContent>(),
                        Physicals = c.Physicals.Count > 0 ? Create(c.Physicals) : new List<Domain.PhysicalContent>()
                    };
            }
            else
                return null;
        }

        private Domain.DeviceType Create(DeviceType t)
        {
            return new Domain.DeviceType()
            {
                ID = (int)t.ID,
                Type = t.Type,
                Image = t.image,
                Quantity = t.Devices.Count,
                //Available = ""
            };
        }

        private Domain.Device Create(Device d)
        {
            return new Domain.Device()
            {
                ID = (int)d.ID,
            };
        }

        private Domain.Lease Create(Lease l)
        {
            return new Domain.Lease()
            {
                //ID = (int)l.ID,
                StartDate = l.StartDate,
                EndDate = l.EndDate
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

            var result = query.Skip(Page * PageSize).Take(PageSize);

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

        public List<Domain.Device> GetDevicesByType(int typeID)
        {
            List<Domain.Device> lResult = new List<Domain.Device>();

            var devices = libEntities.Devices.Where<Device>(t => t.TypeID == typeID);

            //var days = DateTime.DaysInMonth(year, month);

            //var dateRequired = new DateTime(year, month, 1);

            foreach (var device in devices)
            {
                var leases = libEntities.Leases.Where<Lease>(
                    l => l.LeasableID == device.LeasableID);
                
                List<Domain.Lease> lLease = new List<Domain.Lease>();
                foreach (var lease in leases)
                {
                    lLease.Add(Create(lease));
                }
                Domain.Device d = Create(device);
                d.Leases = lLease;
                lResult.Add(d);
                
                /*var Availability = new List<int>();

                Domain.Device d = Create(device);
                for (int i = 1; i <= days; i++)
                {
                    var query = libEntities.Leases.Where(c => c.Date > dateRequired);
                    if(query.Count<Lease>() > 0) 
                    {
                        Availability.Add(i);
                    }
                    dateRequired.AddDays(1);
                }
                d.Availability = Availability;
                lResult.Add(d);*/
            }

            return lResult;
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

        public List<Domain.DeviceType> GetDeviceTypes()
        {
            List<Domain.DeviceType> lResult = new List<Domain.DeviceType>();

            foreach (var item in libEntities.DeviceTypes.Include("Devices"))
            {
                lResult.Add(Create(item));
            }

            return lResult;
        }

        public void Create(Domain.Content instance)
        {
            Content c = new Content()
            {
                Author = instance.Author,
                Description = instance.Description,
                ISBN = instance.ISBN,
                Pages = instance.Pages,
                Publisher = instance.Publisher,
                Released = DateTime.Parse(instance.Released),
                Title = instance.Title
            };

            libEntities.Contents.AddObject(c);
            libEntities.SaveChanges();
        }

        public List<Domain.Review> GetReviews(int id)
        {
            var reviews = from c in libEntities.Contents
                          join r in libEntities.Reviews
                          on c.ID equals r.ContentID
                          where c.ID == id
                          select new Domain.Review()
                          {
                              ID = (int)r.ID,
                              Title = r.Title,
                              Comment = r.Comment,
                              Submitted = (string)r.Submitted,
                              Rate = r.Rate,
                              User = new Domain.User() { Name = r.User.FirstName, Thumbnail = "img/no-user.png" }
                          };


            return reviews.ToList<Domain.Review>();
        }

        public Domain.Review SubmitReview(int i, Domain.Review instance)
        {
            Content cont = libEntities.Contents.Where<Content>(c => c.ID == i).FirstOrDefault();

            User user = libEntities.Users.Where<User>(u => u.FirstName == instance.User.Name).FirstOrDefault();
            
            if (cont != null && user != null)
            {
                Review rev = new Review()
                {
                    Comment = instance.Comment,
                    Content = cont,
                    Rate = instance.Rate,
                    Submitted = instance.Submitted,
                    Title = instance.Title,
                    User = user
                };

                cont.Reviews.Add(rev);

                libEntities.SaveChanges();      
            }      

            return new Domain.Review();
        }

        public Domain.BookRequestCollection GetBookRequests()
        {
            return new Domain.BookRequestCollection(from r in libEntities.BookRequests select r.Title );
        }
    }
}
