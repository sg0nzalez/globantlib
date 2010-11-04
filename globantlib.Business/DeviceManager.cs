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

        public List<Domain.Device> GetDevicesbyType(int typeID)
        {
            return data.GetDevicesByType(typeID);
        }

        public void Create(Domain.DeviceType instance)
        {
            data.Create(instance);
        }
    }
}