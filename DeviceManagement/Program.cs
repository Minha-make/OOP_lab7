/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ==========================================
            // PHAN 1: LUONG MUON TRA THANH CONG (11.1)
            // ==========================================
            try
            {
                Console.WriteLine("==========================================");
                Console.WriteLine("=== DEMO LUONG MUON TRA THANH CONG ===");
                Console.WriteLine("==========================================");

                // 1. Tao giang vien
                Lecturer lecturer = new Lecturer("GV001", "Nguyen Van An");
                Console.WriteLine($"Nguoi muon: {lecturer.FullName} (Han muc: {lecturer.GetMaximumDevices()} thiet bi)");

                // 2. Tao 3 thiet bi day hoc
                ProjectorRemote projectorRemote = new ProjectorRemote("PR001", "Dieu khien may chieu Sony", 2023, 500000m, 85);
                AirConditionerRemote acRemote = new AirConditionerRemote("AC001", "Dieu khien dieu hoa Daikin", 2022, 450000m, new List<string> { "P201", "P202" });
                Microphone microphone = new Microphone("MC001", "Micro khong day Shure", 2024, 1200000m, 90, 80);

                Console.WriteLine("\nDanh sach thiet bi khoi tao:");
                Console.WriteLine($"- {projectorRemote}");
                Console.WriteLine($"- {acRemote}");
                Console.WriteLine($"- {microphone}");

                // 3. Tao phieu muon cho phong P201
                DateTime borrowDate = new DateTime(2026, 10, 20);
                BorrowingSlip slip = new BorrowingSlip("PM001", lecturer, "P201", borrowDate);
                Console.WriteLine($"\nTao phieu muon {slip.SlipId} tai phong {slip.TeachingRoom} vao ngay {borrowDate:dd/MM/yyyy}");

                // 4. Them dieu khien may chieu va micro vao phieu muon
                slip.AddDevice(projectorRemote, 2, 1, 3);
                slip.AddDevice(microphone, 1, 1, 3);
                Console.WriteLine("Da them dieu khien may chieu (muon 2 ngay) va micro (muon 1 ngay) vao phieu.");

                // 5. Tra may chieu dung han sau 2 ngay
                DateTime projectorReturnDate = borrowDate.AddDays(2);
                decimal projectorLateFee = slip.ReturnDevice("PR001", projectorReturnDate);
                Console.WriteLine($"\nTra may chieu ngay {projectorReturnDate:dd/MM/yyyy}. Phi tre han: {projectorLateFee:N0} VND");

                // 6. Tra micro tre 2 ngay (han 1 ngay, tra sau 3 ngay)
                DateTime micReturnDate = borrowDate.AddDays(3);
                decimal micLateFee = slip.ReturnDevice("MC001", micReturnDate);
                Console.WriteLine($"Tra micro ngay {micReturnDate:dd/MM/yyyy} (tre 2 ngay). Phi tre han: {micLateFee:N0} VND");

                // 7. In danh sach thiet bi tra muon
                Console.WriteLine("\n=== DANH SACH THIET BI TRA MUON ===");
                bool hasLate = false;
                foreach (var item in slip.Items)
                {
                    if (item.IsReturned && item.ReturnedDate.HasValue && item.ReturnedDate.Value.Date > item.DueDate.Date)
                    {
                        int lateDays = (item.ReturnedDate.Value.Date - item.DueDate.Date).Days;
                        Console.WriteLine($"- [{item.Device.Id}] {item.Device.Name}: tre {lateDays} ngay | Phi phat: {item.CalculateLateFee():N0} VND");
                        hasLate = true;
                    }
                }
                if (!hasLate)
                {
                    Console.WriteLine("Khong co thiet bi nao tra muon.");
                }

                // 8. In danh sach thiet bi chua tra
                Console.WriteLine("\n=== DANH SACH THIET BI CHUA TRA ===");
                bool hasUnreturned = false;
                foreach (var item in slip.Items)
                {
                    if (!item.IsReturned)
                    {
                        Console.WriteLine($"- [{item.Device.Id}] {item.Device.Name} (Han tra: {item.DueDate:dd/MM/yyyy})");
                        hasUnreturned = true;
                    }
                }
                if (!hasUnreturned)
                {
                    Console.WriteLine("Tat ca thiet bi trong phieu deu da duoc tra.");
                }

                // 9. Tong phi tre han cua ca phieu
                Console.WriteLine($"\nTong phi tre han cua phieu {slip.SlipId}: {slip.CalculateTotalLateFee():N0} VND");
            }
            catch (BorrowingException ex)
            {
                Console.WriteLine($"Loi nghiep vu: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi he thong: {ex.Message}");
            }

            // ==========================================
            // PHAN 2: 7 TINH HUONG LOI (11.2)
            // ==========================================
            Console.WriteLine("\n==========================================");
            Console.WriteLine("=== DEMO 7 TINH HUONG LOI ===");
            Console.WriteLine("==========================================");

            // Tinh huong 1: Muon dieu khien dieu hoa dang UnderMaintenance
            Console.WriteLine("\n--- Tinh huong 1: Muon dieu khien dieu hoa dang UnderMaintenance ---");
            try
            {
                Lecturer l1 = new Lecturer("GV001", "Nguyen Van A");
                BorrowingSlip slip1 = new BorrowingSlip("PM001", l1, "P101", DateTime.Now);
                AirConditionerRemote ac1 = new AirConditionerRemote("AC001", "Dieu khien dieu hoa", 2022, 400000m, new List<string> { "P101" });
                ac1.MarkAsUnderMaintenance();
                slip1.AddDevice(ac1, 2);
            }
            catch (DeviceUnavailableException ex)
            {
                Console.WriteLine($"[Bat DeviceUnavailableException] {ex.Message} | DeviceId: {ex.DeviceId}, Status: {ex.CurrentStatus}");
            }

            // Tinh huong 2: Tro giang muon thiet bi thu 3 (han muc toi da 2)
            Console.WriteLine("\n--- Tinh huong 2: Tro giang muon thiet bi thu 3 ---");
            try
            {
                TeachingAssistant ta2 = new TeachingAssistant("TA001", "Le Van B");
                BorrowingSlip slip2 = new BorrowingSlip("PM002", ta2, "P102", DateTime.Now);
                ProjectorRemote pr2_1 = new ProjectorRemote("PR002", "May chieu 1", 2023, 500000m);
                ProjectorRemote pr2_2 = new ProjectorRemote("PR003", "May chieu 2", 2023, 500000m);
                Microphone mc2_3 = new Microphone("MC002", "Micro 1", 2024, 1000000m);

                slip2.AddDevice(pr2_1, 2);
                slip2.AddDevice(pr2_2, 2);
                slip2.AddDevice(mc2_3, 1);
            }
            catch (BorrowLimitExceededException ex)
            {
                Console.WriteLine($"[Bat BorrowLimitExceededException] {ex.Message} | BorrowerId: {ex.BorrowerId}, Max: {ex.MaximumDevices}");
            }

            // Tinh huong 3: Them cung mot micro 2 lan vao phieu
            Console.WriteLine("\n--- Tinh huong 3: Them cung mot micro 2 lan ---");
            try
            {
                Lecturer l3 = new Lecturer("GV002", "Tran Thi C");
                BorrowingSlip slip3 = new BorrowingSlip("PM003", l3, "P103", DateTime.Now);
                Microphone mc3 = new Microphone("MC003", "Micro Shure", 2024, 1200000m);

                slip3.AddDevice(mc3, 1);
                slip3.AddDevice(mc3, 1);
            }
            catch (DuplicateDeviceException ex)
            {
                Console.WriteLine($"[Bat DuplicateDeviceException] {ex.Message} | DeviceId: {ex.DeviceId}");
            }

            // Tinh huong 4: Muon micro 3 ngay du gioi han chi 1 ngay
            Console.WriteLine("\n--- Tinh huong 4: Muon micro 3 ngay du gioi han 1 ngay ---");
            try
            {
                Lecturer l4 = new Lecturer("GV004", "Pham Van D");
                BorrowingSlip slip4 = new BorrowingSlip("PM004", l4, "P104", DateTime.Now);
                Microphone mc4 = new Microphone("MC004", "Micro Shure", 2024, 1000000m);

                slip4.AddDevice(mc4, 3);
            }
            catch (InvalidBorrowPeriodException ex)
            {
                Console.WriteLine($"[Bat InvalidBorrowPeriodException] {ex.Message} | Requested: {ex.RequestedDays}, Max: {ex.MaximumDays}");
            }

            // Tinh huong 5: Tra dieu khien may chieu khong thuoc phieu
            Console.WriteLine("\n--- Tinh huong 5: Tra dieu khien may chieu khong thuoc phieu ---");
            try
            {
                Lecturer l5 = new Lecturer("GV005", "Hoang Van E");
                BorrowingSlip slip5 = new BorrowingSlip("PM005", l5, "P105", DateTime.Now);
                ProjectorRemote pr5 = new ProjectorRemote("PR005", "May chieu Sony", 2023, 500000m);
                slip5.AddDevice(pr5, 2);

                slip5.ReturnDevice("PR999", DateTime.Now);
            }
            catch (DeviceNotInSlipException ex)
            {
                Console.WriteLine($"[Bat DeviceNotInSlipException] {ex.Message} | DeviceId: {ex.DeviceId}");
            }

            // Tinh huong 6: Tra cung mot thiet bi 2 lan
            Console.WriteLine("\n--- Tinh huong 6: Tra cung mot thiet bi 2 lan ---");
            try
            {
                Lecturer l6 = new Lecturer("GV006", "Do Van F");
                BorrowingSlip slip6 = new BorrowingSlip("PM006", l6, "P106", DateTime.Now);
                ProjectorRemote pr6 = new ProjectorRemote("PR006", "May chieu Panasonic", 2023, 600000m);
                slip6.AddDevice(pr6, 2);

                slip6.ReturnDevice("PR006", DateTime.Now);
                slip6.ReturnDevice("PR006", DateTime.Now);
            }
            catch (DeviceAlreadyReturnedException ex)
            {
                Console.WriteLine($"[Bat DeviceAlreadyReturnedException] {ex.Message} | DeviceId: {ex.DeviceId}");
            }

            // Tinh huong 7: Tra voi ngay tra truoc ngay muon
            Console.WriteLine("\n--- Tinh huong 7: Tra voi ngay tra truoc ngay muon ---");
            try
            {
                Lecturer l7 = new Lecturer("GV007", "Vu Van G");
                DateTime borrowDate7 = new DateTime(2026, 10, 20);
                BorrowingSlip slip7 = new BorrowingSlip("PM007", l7, "P107", borrowDate7);
                ProjectorRemote pr7 = new ProjectorRemote("PR007", "May chieu Epson", 2023, 550000m);
                slip7.AddDevice(pr7, 2);

                DateTime invalidReturnDate = borrowDate7.AddDays(-1);
                slip7.ReturnDevice("PR007", invalidReturnDate);
            }
            catch (BorrowingException ex)
            {
                Console.WriteLine($"[Bat ngoai le ngay tra hop le] {ex.Message}");
            }
        }
    }
}
