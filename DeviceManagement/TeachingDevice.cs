/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public abstract class TeachingDevice : Device
    {
        public string StorageLocation { get; set; } = string.Empty;
        public int MaxBorrowDays { get; set; }

        // Constructor goi base
        protected TeachingDevice(string id, string name, int yearOfUse, decimal purchasePrice, int maxBorrowDays)
            : base(id, name, yearOfUse, purchasePrice)
        {
            if (maxBorrowDays <= 0)
            {
                throw new ArgumentException("So ngay muon toi da phai lon hon 0.");
            }

            MaxBorrowDays = maxBorrowDays;
        }

        protected TeachingDevice(string id, string name, int yearOfUse, decimal purchasePrice, string storageLocation, int maxBorrowDays)
            : base(id, name, yearOfUse, purchasePrice)
        {
            if (string.IsNullOrWhiteSpace(storageLocation))
            {
                throw new ArgumentException("Vi tri luu tru khong duoc de trong.");
            }

            if (maxBorrowDays <= 0)
            {
                throw new ArgumentException("So ngay muon toi da phai lon hon 0.");
            }

            StorageLocation = storageLocation;
            MaxBorrowDays = maxBorrowDays;
        }

        // Phi tre han moi ngay
        public abstract decimal GetLateFeePerDay();

        // Kiem tra thiet bi san sang su dung
        public abstract bool IsReadyForUse();

        // Diem uu tien mac dinh
        public abstract double GetDefaultPriorityScore();

        public virtual void Borrow()
        {
            if (!IsReadyForUse())
            {
                throw new InvalidOperationException("Thiet bi khong san sang de muon.");
            }

            MarkAsBorrowed();
        }

        public virtual void Return()
        {
            if (Status != DeviceStatus.Borrowed)
            {
                throw new InvalidOperationException("Thiet bi chua o trang thai dang muon.");
            }

            MarkAsAvailable();
        }

        public override string ToString()
        {
            string info = base.ToString();
            if (!string.IsNullOrWhiteSpace(StorageLocation))
            {
                info += $" - Vi tri: {StorageLocation}";
            }
            info += $" - Han muon: {MaxBorrowDays} ngay";
            return info;
        }
    }
}
