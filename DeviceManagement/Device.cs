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
        public string Name { get; protected set; }
        public int YearOfUse { get; protected set; }
        public decimal PurchasePrice { get; protected set; }
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

            if (yearOfUse <= 1900 || yearOfUse > DateTime.Now.Year)
            {
                throw new ArgumentException("Nam dua vao su dung khong hop le.");
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

        public virtual void MarkAsBorrowed()
        {
            if (Status == DeviceStatus.Retired)
            {
                throw new InvalidOperationException("Thiet bi da thanh ly, khong the cho muon.");
            }

            Status = DeviceStatus.Borrowed;
        }

        public virtual void MarkAsAvailable()
        {
            if (Status == DeviceStatus.Retired)
            {
                throw new InvalidOperationException("Thiet bi da thanh ly, khong the dua ve Available.");
            }

            Status = DeviceStatus.Available;
        }

        public virtual void MarkAsUnderMaintenance()
        {
            if (Status == DeviceStatus.Retired)
            {
                throw new InvalidOperationException("Thiet bi da thanh ly, khong the dua vao bao tri.");
            }

            Status = DeviceStatus.UnderMaintenance;
        }

        public virtual void MarkAsRetired()
        {
            Status = DeviceStatus.Retired;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Nam: {YearOfUse} - Gia: {PurchasePrice:N0} - Trang thai: {Status}";
        }
    }
}
