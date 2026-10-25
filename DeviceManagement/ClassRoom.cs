/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    public class ClassRoom
    {
        private readonly List<Device> devices;

        public string RoomId { get; private set; }
        public string RoomName { get; private set; }
        public int Capacity { get; private set; }
        public IReadOnlyList<Device> Devices => devices;

        public ClassRoom(string roomId, string roomName, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomId))
            {
                throw new ArgumentException("Ma phong khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(roomName))
            {
                throw new ArgumentException("Ten phong khong duoc de trong.");
            }

            if (capacity <= 0)
            {
                throw new ArgumentException("Suc chua phai lon hon 0.");
            }

            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            devices = new List<Device>();
        }

        // Them thiet bi vao phong
        public void AddDevice(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null.");
            }

            if (FindDevice(device.Id) != null)
            {
                throw new ArgumentException($"Thiet bi voi ma {device.Id} da ton tai trong phong {RoomId}.");
            }

            devices.Add(device);
        }

        // Xoa thiet bi theo ma
        public bool RemoveDevice(string deviceId)
        {
            Device? device = FindDevice(deviceId);
            if (device != null)
            {
                return devices.Remove(device);
            }

            return false;
        }

        // Tim thiet bi theo ma
        public Device? FindDevice(string deviceId)
        {
            foreach (var device in devices)
            {
                if (device != null && device.Id == deviceId)
                {
                    return device;
                }
            }

            return null;
        }

        // Tinh tong chi phi bao tri bang da hinh
        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal totalCost = 0;
            foreach (var device in devices)
            {
                if (device != null)
                {
                    totalCost += device.CalculateAnnualMaintenanceCost();
                }
            }

            return totalCost;
        }

        // Danh sach cac thiet bi can bao tri (dang UnderMaintenance hoac da dung tren 5 nam)
        public List<Device> GetDevicesRequiringMaintenance()
        {
            List<Device> requiringMaintenance = new List<Device>();
            int currentYear = DateTime.Now.Year;

            foreach (var device in devices)
            {
                if (device != null)
                {
                    bool isUnderMaintenance = device.Status == DeviceStatus.UnderMaintenance;
                    bool isOverFiveYears = (currentYear - device.YearOfUse) > 5;

                    if (isUnderMaintenance || isOverFiveYears)
                    {
                        requiringMaintenance.Add(device);
                    }
                }
            }

            return requiringMaintenance;
        }

        public override string ToString()
        {
            return $"Phong {RoomId} ({RoomName}) - Suc chua: {Capacity} - So thiet bi: {devices.Count}";
        }
    }
}
