/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public abstract class Device
    {
        public string Id { get; }
        public string Name { get; set; }
        public int YearOfUse { get; set; }
        public decimal PurchasePrice { get; set; }
        public DeviceStatus Status { get; protected set; }

        protected Device(string id, string name, int yearOfUse, decimal purchasePrice)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Ma thiet bi khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Ten thiet bi khong duoc de trong.");
            }

            // Kiem tra nam su dung
            if (yearOfUse > DateTime.Now.Year)
            {
                throw new ArgumentException("Nam dua vao su dung khong duoc lon hon nam hien tai.");
            }

            if (purchasePrice <= 0)
            {
                throw new ArgumentException("Gia mua phai lon hon 0.");
            }

            Id = id;
            Name = name;
            YearOfUse = yearOfUse;
            PurchasePrice = purchasePrice;
            Status = DeviceStatus.Available;
        }

        // Tinh chi phi bao tri hang nam
        public abstract decimal CalculateAnnualMaintenanceCost();

        public void MarkAsBorrowed()
        {
            Status = DeviceStatus.Borrowed;
        }

        public void MarkAsAvailable()
        {
            Status = DeviceStatus.Available;
        }

        public void MarkAsUnderMaintenance()
        {
            Status = DeviceStatus.UnderMaintenance;
        }

        public void MarkAsRetired()
        {
            Status = DeviceStatus.Retired;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Nam: {YearOfUse} - Gia: {PurchasePrice:N0} - Trang thai: {Status}";
        }
    }
}
