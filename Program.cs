/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        var projectorRepo = new InMemoryRepository<ProjectorRemote>();
        var acRepo = new InMemoryRepository<AirConditionerRemote>();
        var micRepo = new InMemoryRepository<WirelessMicrophone>();

        var p1 = new ProjectorRemote { Id = "P01", Name = "Máy chiếu 1", BatteryLevel = 100 }; // Sẵn sàng, điểm 100
        var p2 = new ProjectorRemote { Id = "P02", Name = "Máy chiếu 2", BatteryLevel = 15 };  // Không sẵn sàng (<20)
        var p3 = new ProjectorRemote { Id = "P03", Name = "Máy chiếu 3", BatteryLevel = 80 };  // Sẵn sàng, điểm 80

        var ac1 = new AirConditionerRemote { Id = "AC01", Name = "Điều hòa 1", CompatibleRooms = new HashSet<string> { "A203", "A204" } };
        var ac2 = new AirConditionerRemote { Id = "AC02", Name = "Điều hòa 2", CompatibleRooms = new HashSet<string> { "B101" } };

        var m1 = new WirelessMicrophone { Id = "M01", Name = "Micro 1", BatteryLevel = 80, SignalQuality = 90 }; 
        var m2 = new WirelessMicrophone { Id = "M02", Name = "Micro 2", BatteryLevel = 25, SignalQuality = 80 }; 
        var m3 = new WirelessMicrophone { Id = "M03", Name = "Micro 3", BatteryLevel = 100, SignalQuality = 100 };

        Console.WriteLine("KIỂM THỬ");

        Console.WriteLine("\n1. Thêm thiết bị vào repository");
        projectorRepo.Add(p1); projectorRepo.Add(p2); projectorRepo.Add(p3);
        acRepo.Add(ac1); acRepo.Add(ac2);
        micRepo.Add(m1); micRepo.Add(m2); micRepo.Add(m3);
        Console.WriteLine("Đã thêm thành công tất cả thiết bị.");

        Console.WriteLine("\n2. Thử thêm hai thiết bị trùng mã");
        try
        {
            projectorRepo.Add(new ProjectorRemote { Id = "P01", BatteryLevel = 50 });
        }
        catch (DuplicateIdException ex)
        {
            Console.WriteLine($"Bắt được ngoại lệ DuplicateIdException: Trùng mã {ex.DuplicateId}");
        }


        Console.WriteLine("\n3. Tìm thiết bị theo mã");
        var foundDevice = projectorRepo.FindById("P03");
        Console.WriteLine($"Tìm thấy: {foundDevice?.Name} - Mức pin: {foundDevice?.BatteryLevel}%");


        Console.WriteLine("\n4. Lọc các thiết bị sẵn sàng cho micro");
        var readyMics = micRepo.FindAll(m => m.IsReadyForUse());
        Console.WriteLine($"Số micro sẵn sàng (Pin >= 30, Tín hiệu >= 40): {readyMics.Count}");

      
        var highestScoreStrategy = new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore());
        var projectorService = new AllocationService<ProjectorRemote>(projectorRepo, highestScoreStrategy);

        var firstAcStrategy = new FirstAvailableStrategy<AirConditionerRemote>();
        var acService = new AllocationService<AirConditionerRemote>(acRepo, firstAcStrategy);

        var highestMicStrategy = new HighestPriorityStrategy<WirelessMicrophone>(d => d.GetDefaultPriorityScore());
        var micService = new AllocationService<WirelessMicrophone>(micRepo, highestMicStrategy);


        Console.WriteLine("\n5. Chọn điều khiển máy chiếu có điểm cao nhất");
        var selectedProjector = projectorService.Allocate();
        Console.WriteLine($"Đã chọn: {selectedProjector.Name} (Pin: {selectedProjector.BatteryLevel}%)");

     
        Console.WriteLine("\n6. Chọn điều khiển điều hòa tương thích với phòng A203");
        var selectedAc = acService.Allocate(d => d.IsCompatibleWith("A203"));
        Console.WriteLine($"Đã chọn: {selectedAc.Name}");

  
        Console.WriteLine("\n7. Chọn micro đầu tiên sẵn sàng");
        micService.ChangeStrategy(new FirstAvailableStrategy<WirelessMicrophone>());
        var selectedMic = micService.Allocate();
        Console.WriteLine($"Đã chọn: {selectedMic.Name}");

        
        Console.WriteLine("\n8. Đổi chiến lược trong thời gian chạy (Máy chiếu -> Chọn phần tử đầu tiên)");
        // P01 đã mượn, P02 không sẵn sàng, P03 sẽ là phần tử đầu tiên sẵn sàng
        projectorService.ChangeStrategy(new FirstAvailableStrategy<ProjectorRemote>());
        var nextProjector = projectorService.Allocate();
        Console.WriteLine($"Đã chọn bằng chiến lược mới: {nextProjector.Name} (Trạng thái: {nextProjector.Status})");

    
        Console.WriteLine("\n9. Thử cấp phát khi không có ứng viên phù hợp");
        try
        {
            acService.Allocate(d => d.IsCompatibleWith("Phòng-Không-Tồn-Tại"));
        }
        catch (NoSuitableItemException)
        {
            Console.WriteLine("Không tìm thấy điều hòa phù hợp.");
        }

       
        Console.WriteLine("\n10. Chứng minh thiết bị được chọn chuyển sang Borrowed");
        Console.WriteLine($"Trạng thái của {selectedProjector.Name}: {selectedProjector.Status}");


  
        Console.WriteLine("\n11. Chứng minh tính tổng quát của Repository");
        Console.WriteLine($"Repository đã lưu: {projectorRepo.GetAll().Count} máy chiếu và {acRepo.GetAll().Count} điều hòa.");
    }
}
