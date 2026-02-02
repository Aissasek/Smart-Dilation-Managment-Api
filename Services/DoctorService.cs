using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.Data;
using Smart_Dilation_Management.DTO.DoctorDTO;
using Smart_Dilation_Management.Enums;
using Smart_Dilation_Management.Hups;
using Smart_Dilation_Management.Interfaces;
using Smart_Dilation_Management.Models;

namespace Smart_Dilation_Management.Services
{
    public class DoctorService : IDoctor
    {
        private readonly DilationData _Db;
        private readonly IHubContext<NotificationHub> _hub;
        public DoctorService(DilationData db, IHubContext<NotificationHub> hub)
        {
            _Db = db;
            _hub = hub;
        }

        public async Task<bool> PatientExamination(int DoctorId, int PatientId, DilationDTO ExaminationReport)
        {
            
            var doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == DoctorId && x.Role == UserRole.Doctor);
            var patient = await _Db.Patient.FirstOrDefaultAsync(x => x.Id == PatientId);

            if (doctor == null || patient == null)
                return false ;

            var order = await _Db.DilationOrder
                .FirstOrDefaultAsync(x => x.PatientId == PatientId);

            if (order == null)
            {

                order = new DilationOrder
                {
                    PatientId = PatientId,
                    DoctorId = DoctorId,
                    EyeDropId = ExaminationReport.EyeDropId,
                    DropsGiven = 0,
                    DropsRequired = ExaminationReport.DropsRequired,
                    Status = PatientStatus.PostDilation
                };

                await _Db.DilationOrder.AddAsync(order);
            }
            else
            {

                order.DoctorId = DoctorId;
                order.EyeDropId = ExaminationReport.EyeDropId;
                order.DropsRequired = ExaminationReport.DropsRequired;
                order.Status = PatientStatus.PostDilation;
            }
            doctor.IsFree = true;
            await _Db.SaveChangesAsync();

            await _hub.Clients.Group(DoctorId.ToString())
                .SendAsync("ReceiveMessage", ExaminationReport.Message, ExaminationReport.StaffId);

            await _hub.Clients.Group(DoctorId.ToString())
                .SendAsync("ReceiveMessage", ExaminationReport.Message,DoctorId);


            return true;
        }
        public async Task<List<GetAllDoctorDTO>> GetAllDoctor()
        {
            var Data = await _Db.User.Where(x => x.Role == UserRole.Doctor).Select(x => new GetAllDoctorDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsFree = x.IsFree,
            }).ToListAsync();
            return Data;
        }
        public async Task<GetAllDoctorDTO?> SearchDoctor(string FullName)
        {
            var Data = await _Db.User.FirstOrDefaultAsync(x => x.FullName == FullName && x.Role == UserRole.Doctor);
            if (Data == null) { return null; }
            return new GetAllDoctorDTO
            {
                Id = Data.Id,
                FullName = Data.FullName,
                Email = Data.Email,
                IsFree = Data.IsFree,
            };
        }
        public async Task<List<GetAllDoctorDTO>> GetDoctorFree()
        {
            var Data = await _Db.User.Where(x => x.IsFree == true && x.Role == UserRole.Doctor).Select(x => new GetAllDoctorDTO
            {
                Id= x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsFree = x.IsFree,
            }).ToListAsync();
            return Data;
        }
        public async Task<List<GetAllDoctorDTO>> GetDoctorNotFree()
        {
            var Data = await _Db.User.Where(x => x.IsFree == false && x.Role == UserRole.Doctor).Select(x => new GetAllDoctorDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsFree = x.IsFree,
            }).ToListAsync();
            return Data;
        }
        public async Task<bool> AddNewDoctor(AddNewDoctor NewDoctor)
        {
            var Data = new User
            {
                FullName = NewDoctor.FullName,
                Email = NewDoctor.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(NewDoctor.PasswordHash),

                Role = UserRole.Doctor,
            };
            if (NewDoctor == null || Data == null) { return false; }
            await _Db.User.AddAsync(Data);
            await _Db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDoctor(int DoctorId, UpdateDoctor UpdateDoctor)
        {
            var Data = await _Db.User.FirstOrDefaultAsync(x => x.Id == DoctorId && x.Role == UserRole.Doctor);
            if (Data == null || UpdateDoctor == null ) { return false; }
            Data.FullName = UpdateDoctor.FullName;
            Data.Email = UpdateDoctor.Email;
            Data.PasswordHash = BCrypt.Net.BCrypt.HashPassword(UpdateDoctor.PasswordHash);

            await _Db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDoctor(int DoctorId)
        {
            var Data = await _Db.User.FirstOrDefaultAsync(x => x.Id == DoctorId && x.Role == UserRole.Doctor);
            if (Data == null || DoctorId < 1) { return false; }
            _Db.User.Remove(Data);
            await _Db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> MarkAsDone(int PatientId, int DoctorId,int StaffId)
        {
            
            var Patient = await _Db.Patient.Include(x => x.DilationOrder).FirstOrDefaultAsync(x => x.Id == PatientId);
            if (Patient == null) { return false; }
            var Doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == DoctorId && x.Role == UserRole.Doctor);
            if (Doctor == null) { return false; }
            var Staff = await _Db.User.FirstOrDefaultAsync(x => x.Id == StaffId && x.Role == UserRole.Staff);
            if (Staff == null) { return false; }
            if (Patient.DilationOrder.Status != PatientStatus.Completed) { return false; }
            Patient.DilationOrder.Status = PatientStatus.Done;
            await _Db.SaveChangesAsync();
            string message = $"Patient {Patient.FullName} has completed the examination and can leave.";

            await _hub.Clients
                .Group(StaffId.ToString()).SendAsync("ReceiveMessage", message, DoctorId.ToString());

            return true;
        }
        public async Task<bool> IsDoctor(int DoctorId)
        {
            if (DoctorId < 1) { return false; }
            var Doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == DoctorId && x.Role == UserRole.Doctor);
            return Doctor != null;
        }
        public async Task<bool> IsDoctor(string DoctorName)
        {
            var Doctor = await _Db.User.FirstOrDefaultAsync(x => x.FullName == DoctorName && x.Role == UserRole.Doctor);
            return Doctor != null;
        }
        public async Task<bool> IsStaff(int StaffId)
        {
            if(StaffId < 1) {return false; }
            var Staff = await _Db.User.FirstOrDefaultAsync(x => x.Id == StaffId && x.Role == UserRole.Staff);
            return Staff != null;
        }
    }
}
