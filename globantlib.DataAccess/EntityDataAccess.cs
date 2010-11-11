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
            };
        }

        private Domain.Date Create(int number, string username)
        {
            return new Domain.Date()
            {
                Number = number,
                Username = username,
            };
        }

        /*private Domain.Device Create(Device d)
        {
            return new Domain.Device()
            {
                ID = (int)d.ID,
                Type = d.DeviceType.Type,
                TypeID = (int)d.DeviceType.ID
            };
        }*/

        private Domain.Item Create(Device d)
        {
            return new Domain.Item()
            {
                ID = (int)d.ID,
                Type = d.DeviceType.Type,
            };
        }

        private Domain.Item CreateItem(Physical p)
        {
            return new Domain.Item()
            {
                ID = (int)p.ID,
                Type = p.Content.Title,
            };
        }

        private Domain.Lease Create(Lease l)
        {
            return new Domain.Lease()
            {

                
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

            var query = libEntities.Contents.OrderBy("it.Title")
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

        /*public List<Domain.Device> GetDevicesByType(int typeID, int id, int month, int year)
        {
            List<Domain.Device> lResult = new List<Domain.Device>();

            var devices = libEntities.Devices.Where<Device>(t => t.TypeID == typeID);

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
            }

            return lResult;
        }*/

        public List<Domain.Types> GetDevicesByType(int typeID, int id, int month, int year)
        {
            List<Domain.Item> lResult = new List<Domain.Item>();

            var devices = libEntities.Devices.Where<Device>(t => t.TypeID == typeID);

            foreach (var device in devices)
            {
                Domain.Item d = Create(device);


                if (id == 0) {
                    id = (int)device.ID;
                }
                
                if(device.ID == id) 
                {
                    d.Current = true;
                    {
                        List<Domain.Month> leases = new List<Domain.Month>();
                        Domain.Month leasesMonth = new Domain.Month();
                        leasesMonth.Name = new DateTime(year, month, 1).ToString("MMMM");
                        leasesMonth.Dates = checkDays(year, month, (int)device.ID);
                        leases.Add(leasesMonth);

                        leasesMonth = new Domain.Month();

                        if (month == 12)
                        {
                            leasesMonth.Name = new DateTime((year + 1), 1, 1).ToString("MMMM");
                            leasesMonth.Dates = checkDays((year + 1), 1, (int)device.ID);
                        }
                        else
                        {
                            leasesMonth.Name = new DateTime(year, (month + 1), 1).ToString("MMMM");
                            leasesMonth.Dates = checkDays(year, (month + 1), (int)device.ID);
                        }

                        leases.Add(leasesMonth);
                        
                         d.Lease = leases;
                    }
                }
                lResult.Add(d);
            }

            Domain.Types type = new Domain.Types();
            type.Items = lResult;
            type.ID = typeID;
            List<Domain.Types> lType = new List<Domain.Types>();
            lType.Add(type);
            return lType;
        }

        public List<Domain.Types> GetPhysicals(int contentID, int id, int month, int year)
        {
            List<Domain.Item> lResult = new List<Domain.Item>();

            var physicals = libEntities.Physicals.Where<Physical>(t => t.ContentID == contentID);

            foreach (var physical in physicals)
            {
                Domain.Item d = CreateItem(physical);

                if (id == 0)
                {
                    id = (int)physical.ID;
                }

                if (physical.ID == id)
                {
                    d.Current = true;
                    {
                        List<Domain.Month> leases = new List<Domain.Month>();
                        Domain.Month leasesMonth = new Domain.Month();
                        leasesMonth.Name = new DateTime(year, month, 1).ToString("MMMM");
                        leasesMonth.Dates = checkDaysContent(year, month, (int)physical.ID);
                        leases.Add(leasesMonth);

                        leasesMonth = new Domain.Month();

                        if (month == 12)
                        {
                            leasesMonth.Name = new DateTime((year + 1), 1, 1).ToString("MMMM");
                            leasesMonth.Dates = checkDaysContent((year + 1), 1, (int)physical.ID);
                        }
                        else
                        {
                            leasesMonth.Name = new DateTime(year, (month + 1), 1).ToString("MMMM");
                            leasesMonth.Dates = checkDaysContent(year, (month + 1), (int)physical.ID);
                        }

                        leases.Add(leasesMonth);

                        d.Lease = leases;
                    }
                }
                lResult.Add(d);
            }

            Domain.Types type = new Domain.Types();
            type.Items = lResult;
            type.ID = contentID;
            List<Domain.Types> lType = new List<Domain.Types>();
            lType.Add(type);
            return lType;
        }

        public List<Domain.Date> checkDays(int year, int month, int deviceID)
        {
            var date = new DateTime(year, month, 1);
            var days = DateTime.DaysInMonth(year, month);
            List<Domain.Date> dates = new List<Domain.Date>();
            int i = 1;
            while (i <= days)
            {
                dates.Add(Create(i, checkOwner(date, deviceID)));
                i++;
                date = date.AddDays(1);
            }
            return dates;
        }

        public List<Domain.Date> checkDaysContent(int year, int month, int contentID)
        {
            var date = new DateTime(year, month, 1);
            var days = DateTime.DaysInMonth(year, month);
            List<Domain.Date> dates = new List<Domain.Date>();
            int i = 1;
            while (i <= days)
            {
                dates.Add(Create(i, checkOwnerContent(date, contentID)));
                i++;
                date = date.AddDays(1);
            }
            return dates;
        }

        public string checkOwner (DateTime date, Decimal deviceID)
        {
            var owner = (from l in libEntities.Leases
                            join x in libEntities.Leasables on l.LeasableID equals x.ID
                            join d in libEntities.Devices on x.ID equals d.LeasableID
                            join u in libEntities.Users on l.User equals u.ID
                            where (l.StartDate == date || l.EndDate == date
                            || (l.StartDate < date && date < l.EndDate))
                            && (d.ID == deviceID)
                         select u.UserName).FirstOrDefault();

            if (owner == null)
                return "";
            else
                return owner;
        }

        public string checkOwnerContent(DateTime date, Decimal contentID)
        {
            var owner = (from l in libEntities.Leases
                         join x in libEntities.Leasables on l.LeasableID equals x.ID
                         join d in libEntities.Physicals on x.ID equals d.LeasableID
                         join u in libEntities.Users on l.User equals u.ID
                         where (l.StartDate == date || l.EndDate == date
                         || (l.StartDate < date && date < l.EndDate))
                         && (d.ID == contentID)
                         select u.UserName).FirstOrDefault();

            if (owner == null)
                return "";
            else
                return owner;
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

        public List<Domain.Content> GetContent(string text)
        {
            List<Domain.Content> lResult = new List<Domain.Content>();

            var query = libEntities.Contents.OrderBy("it.Title")
                .Where(c => c.Title.Contains(text));

            foreach (var item in query)
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
                Domain.DeviceType dt = Create(item);

                if (checkAvailability(DateTime.Today, item.ID))
                    dt.Available = "Today";
                else
                    if (checkAvailability(DateTime.Today.AddDays(1), item.ID))
                        dt.Available = "Tomorrow";
                    else
                        dt.Available = "More than 2 days";
                
                lResult.Add(dt);
            }

            return lResult;
        }

        public bool checkAvailability(DateTime date, Decimal itemID)
        {
            foreach (var device in libEntities.Devices.Where(d => d.TypeID == itemID))
            {
                var leases = (from l in libEntities.Leases
                              join x in libEntities.Leasables on l.LeasableID equals x.ID
                              join d in libEntities.Devices on x.ID equals d.LeasableID
                              where (l.StartDate == date || l.EndDate == date
                              || (l.StartDate < date && date < l.EndDate))
                              && (d.ID == device.ID)
                              select l).Count();
                if (leases == 0)
                {
                    return true;
                }
            }
            return false;
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

            foreach (var item in instance.Digitals)
            {
                c.Digitals.Add(new Digital() { Format = item.Format, Link = item.Link });
            }

            libEntities.Contents.AddObject(c);
            libEntities.SaveChanges();
        }

        public void Create(Domain.DeviceType instance)
        {
            DeviceType dt = new DeviceType()
            {
                Type = instance.Type,
                image = instance.Image
            };
            libEntities.DeviceTypes.AddObject(dt);
            libEntities.SaveChanges();
        }

        public Domain.Lease Create(Domain.Lease instance)
        {
            Device d = libEntities.Devices.Where<Device>(dt => dt.ID == instance.ID).FirstOrDefault();

            User u = libEntities.Users.Where<User>(ut => ut.UserName == instance.Email).FirstOrDefault();
            
            if (d != null)
            {
                if (u == null)
                {
                    User user = new User()
                    {
                        UserName = instance.Email
                    };
                    libEntities.Users.AddObject(user);
                    u = user;
                }
            }

            var startDate = new DateTime((int)instance.Year, (int)instance.Month, (int)instance.StartDate);

            var endDate = startDate.AddDays((int)instance.EndDate);

            if (CheckRangePossibility(startDate, endDate, d.ID))
            {
                Lease lease = new Lease()
                {
                    Leasable = d.Leasable,
                    User1 = u,
                    StartDate = startDate,
                    EndDate = endDate
                };
                libEntities.Leases.AddObject(lease);
                libEntities.SaveChanges();

                return new Domain.Lease();
            }
            else
            {
                return null;
            }
        }

        public Domain.Lease CreateLease(Domain.Lease instance)
        {
            Physical p = libEntities.Physicals.Where<Physical>(dt => dt.ID == instance.ID).FirstOrDefault();

            User u = libEntities.Users.Where<User>(ut => ut.UserName == instance.Email).FirstOrDefault();

            if (p != null)
            {
                if (u == null)
                {
                    User user = new User()
                    {
                        UserName = instance.Email
                    };
                    libEntities.Users.AddObject(user);
                    u = user;
                }
            }

            var startDate = new DateTime((int)instance.Year, (int)instance.Month, (int)instance.StartDate);

            var endDate = startDate.AddDays((int)instance.EndDate);

            if (CheckRangePossibility(startDate, endDate, p.ID))
            {
                Lease lease = new Lease()
                {
                    Leasable = p.Leasable,
                    User1 = u,
                    StartDate = startDate,
                    EndDate = endDate
                };
                libEntities.Leases.AddObject(lease);
                libEntities.SaveChanges();

                return new Domain.Lease();
            }
            else
            {
                return null;
            }
        }

        public bool CheckRangePossibility(DateTime startDate, DateTime endDate, Decimal itemID)
        {
            var date = new DateTime();
            date = startDate;
            while (date < endDate)
            {
                if (checkOwner(date, itemID) != "")
                    return false;
                else
                    date = date.AddDays(1);
            }
            return true;
        }

        public void Create(Domain.Device instance)
        {
            Leasable l = new Leasable()
            {
                Type = "Device"
            };
            libEntities.Leasables.AddObject(l);
            
            DeviceType dt = libEntities.DeviceTypes.Where<DeviceType>(t => t.Type == instance.Type).FirstOrDefault();

            Device d = new Device()
            {
                DeviceType = dt,
                Leasable = l,
            };

            libEntities.Devices.AddObject(d);
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

            List<Domain.Review> result = reviews.ToList<Domain.Review>();
            result.Reverse();

            return result;
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
            return new Domain.BookRequestCollection(from r in libEntities.BookRequests select new Domain.BookRequest() { Title = r.Title, UserName = "Guest" });
        }

        public void SubmitBookRequest(string text)
        {
            libEntities.AddToBookRequests(BookRequest.CreateBookRequest(0, text));
            libEntities.SaveChanges();
        }
        
        public void Delete(int i)
        {
            Content con = libEntities.Contents.Where<Content>(c => c.ID == i).FirstOrDefault();

            while (con.Digitals.Count > 0)
            {
                libEntities.Digitals.DeleteObject(con.Digitals.First());
            }
            libEntities.Contents.DeleteObject(con);
            libEntities.SaveChanges();
        }

        public void DeleteDigital(int i)
        {
            Digital con = libEntities.Digitals.Where<Digital>(c => c.ID == i).FirstOrDefault();
            libEntities.Digitals.DeleteObject(con);
            libEntities.SaveChanges();
        }

        public void Update(Domain.Content instance)
        {
            Content con = libEntities.Contents.Where<Content>(c => c.ID == instance.ID).FirstOrDefault();
            if (con != null)
            {
                con.Author = instance.Author;
                con.Description = instance.Description;
                con.ISBN = instance.ISBN;
                con.Pages = instance.Pages;
                con.Publisher = instance.Publisher;
                con.Released = DateTime.Parse(instance.Released);
                con.Title = instance.Title;

                foreach (var item in instance.Digitals)
                {
                    libEntities.Digitals.AddObject(new Digital() { Content = con, Link = item.Link, Format = item.Format });
                }

                libEntities.SaveChanges();
            }
        }
    }
}
