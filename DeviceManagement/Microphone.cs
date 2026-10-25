/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class Microphone : TeachingDevice
    {
        public int BatteryLevel { get; set; }
        public int SignalQuality { get; set; }

        public Microphone(string id, string name, int yearOfUse, decimal purchasePrice)
            : base(id, name, yearOfUse, purchasePrice, 1)
        {
            BatteryLevel = 100;
            SignalQuality = 100;
        }

        public Microphone(string id, string name, int yearOfUse, decimal purchasePrice, int batteryLevel, int signalQuality)
            : base(id, name, yearOfUse, purchasePrice, 1)
        {
            if (batteryLevel < 0 || batteryLevel > 100)
            {
                throw new ArgumentException("Muc pin phai tu 0 den 100.");
            }

            if (signalQuality < 0 || signalQuality > 100)
            {
                throw new ArgumentException("Chat luong tin hieu phai tu 0 den 100.");
            }

            BatteryLevel = batteryLevel;
            SignalQuality = signalQuality;
        }

        // Chi phi bao tri hang nam: 10% gia mua
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return PurchasePrice * 0.10m;
        }

        // Don gia phat tre: 30.000 VND / ngay
        public override decimal GetLateFeePerDay()
        {
            return 30000m;
        }

        // San sang khi Available, pin tu 30% tro len va tin hieu tu 40% tro len
        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;
        }

        // Diem uu tien mac dinh: trung binh cong cua pin va chat luong tin hieu
        public override double GetDefaultPriorityScore()
        {
            return (BatteryLevel + SignalQuality) / 2.0;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Pin: {BatteryLevel}% - Tin hieu: {SignalQuality}%";
        }
    }
}
