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
                Thumbnail = "book.jpg",
                Released = c.Released.HasValue ? c.Released.Value : DateTime.MinValue,
                hasDigital = c.Digitals.Count > 0 ? "Yes" : "No",
                hasPhysical = c.Physicals.Count > 0 ? "Yes" : "No", 
                Digitals = c.Digitals.Count > 0 ? Create(c.Digitals) : new List<Domain.DigitalContent>(),
                Physicals = c.Physicals.Count > 0 ? Create(c.Physicals) : new List<Domain.PhysicalContent>()
            };
        }

        private Domain.DeviceType Create(DeviceType t)
        {
            return new Domain.DeviceType()
            {
                ID = (int)t.ID,
                Type = t.Type,
                Image = t.image
            };
        }

        private Domain.Device Create(Device d)
        {
            return new Domain.Device()
            {
                ID = (int)d.ID,
                LeasableID = (int)d.LeasableID,
                TypeID = (int)d.TypeID
            };
        }

        private Domain.Lease Create(Lease l)
        {
            return new Domain.Lease()
            {
                ID = (int)l.ID,
                Date = l.Date,
                Period = l.Period
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

        public List<Domain.Device> GetDevicesByType(int typeID)
        {
            List<Domain.Device> lResult = new List<Domain.Device>();

            var devices = libEntities.Devices.Where<Device>(t => t.TypeID == typeID);

            foreach (var device in devices)
            {
                var leases = libEntities.Leases.Where<Lease>(l => l.LeasableID == device.LeasableID);
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

            foreach (var item in libEntities.DeviceTypes)
            {
                lResult.Add(Create(item));
            }

            return lResult;
        }
        

    }
}
