/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    public class AirConditionerRemote : TeachingDevice
    {
        private readonly HashSet<string> compatibleRooms;

        public IReadOnlySet<string> CompatibleRooms => compatibleRooms;

        public AirConditionerRemote(string id, string name, int yearOfUse, decimal purchasePrice)
            : base(id, name, yearOfUse, purchasePrice, 3)
        {
            compatibleRooms = new HashSet<string>();
        }

        public AirConditionerRemote(string id, string name, int yearOfUse, decimal purchasePrice, IEnumerable<string> rooms)
            : base(id, name, yearOfUse, purchasePrice, 3)
        {
            compatibleRooms = rooms != null ? new HashSet<string>(rooms) : new HashSet<string>();
        }

        public void AddCompatibleRoom(string roomId)
        {
            if (!string.IsNullOrWhiteSpace(roomId))
            {
                compatibleRooms.Add(roomId);
            }
        }

        public bool IsCompatibleWith(string roomId)
        {
            if (string.IsNullOrWhiteSpace(roomId))
            {
                return false;
            }

            return compatibleRooms.Contains(roomId);
        }

        // Chi phi bao tri hang nam: 5% gia mua
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.05m;
        }

        // Don gia phat tre: 15.000 VND / ngay
        public override decimal GetLateFeePerDay()
        {
            return 15000m;
        }

        // San sang khi Available
        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available;
        }

        // Diem uu tien mac dinh
        public override double GetDefaultPriorityScore()
        {
            return 100.0;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Phong tuong thich: [{string.Join(", ", compatibleRooms)}]";
        }
    }
}
