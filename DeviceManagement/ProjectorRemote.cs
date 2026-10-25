/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; set; }

        public ProjectorRemote(string id, string name, int yearOfUse, decimal purchasePrice)
            : base(id, name, yearOfUse, purchasePrice, 2)
        {
            BatteryLevel = 100;
        }

        public ProjectorRemote(string id, string name, int yearOfUse, decimal purchasePrice, int batteryLevel)
            : base(id, name, yearOfUse, purchasePrice, 2)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
            {
                throw new ArgumentException("Muc pin phai tu 0 den 100.");
            }

            BatteryLevel = batteryLevel;
        }

        // Chi phi bao tri hang nam: 8% gia mua
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.08m;
        }

        // Don gia phat tre: 20.000 VND / ngay
        public override decimal GetLateFeePerDay()
        {
            return 20000m;
        }

        // San sang khi Available va pin tu 20% tro len
        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available && BatteryLevel >= 20;
        }

        // Diem uu tien mac dinh bang muc pin
        public override double GetDefaultPriorityScore()
        {
            return BatteryLevel;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Pin: {BatteryLevel}%";
        }
    }
}
