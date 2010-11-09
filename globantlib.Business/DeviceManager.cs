using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using globantlib.Domain;
using globantlib.DataAccess;

namespace globantlib.Business
{
    public class DeviceManager
    {
        EntityDataAccess data = new EntityDataAccess();

        public List<Domain.DeviceType> GetDeviceTypes()
        {
            return data.GetDeviceTypes();
        }

        public List<Domain.Types> GetDevicesbyType(int typeID, int id, int month, int year)
        {
            return data.GetDevicesByType(typeID, id, month, year);
        }

        public void Create(Domain.Lease instance)
        {
            data.Create(instance);
        }

        public void Create(Domain.Device instance)
        {
            data.Create(instance);
        }
    }
}