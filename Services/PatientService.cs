using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.Data;
using Smart_Dilation_Management.DTO.PatientDTO;
using Smart_Dilation_Management.Enums;
using Smart_Dilation_Management.Hups;
using Smart_Dilation_Management.Interfaces;
using Smart_Dilation_Management.Models;

namespace Smart_Dilation_Management.Services
{
    public class PatientService:IPatient
    {
        private readonly DilationData _Db;
        private readonly IHubContext<NotificationHub> _hub;
        public PatientService(DilationData db, IHubContext<NotificationHub> hub)
        {
            _Db = db;
            _hub = hub;
        }

        public async Task<List<GetAllPatientDTO>> GetAllPatients()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .ToListAsync();

            return Data.Select(x => new GetAllPatientDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }

        public async Task<GetAllPatientDTO?> SearchPatient(string FullName)
        {
            var Data = await _Db.Patient.Include(x => x.DilationOrder).ThenInclude(d => d.Doctor).Include(x => x.DilationOrder).ThenInclude(d => d.EyeDrop).FirstOrDefaultAsync(x => x.FullName == FullName);
            if (Data == null) { return null; }
            return new GetAllPatientDTO
            {
                Id = Data.Id,
                FullName = Data.FullName,
                DoctorName = Data.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = Data.DateOfBirth,
                DropType = Data.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = Data.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = Data.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = Data.DilationOrder?.DropsRequired ?? 0,
            };
        }
        public async Task<List<GetPatientByStatus>?> GetPatientByStatus(string Status)
        {
            if (Enum.TryParse<PatientStatus>(Status, true, out var enumStatus))
            {
                var Data = await _Db.Patient.Include(y => y.DilationOrder).ThenInclude(d => d.Doctor).Include(x => x.DilationOrder).ThenInclude(d => d.EyeDrop).Where(x => x.DilationOrder.Status == enumStatus).ToListAsync();
                if (Data == null) { return null; }
                return Data.Select(x => new GetPatientByStatus
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                    DateOfBirth = x.DateOfBirth,
                    DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                    Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                    DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                    DropsRequired = x.DilationOrder?.DropsRequired ?? 0,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task<List<GetPatientByStatus>> GetPatientIndilation()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .Where(x => x.DilationOrder.Status == PatientStatus.InDilation)
                .ToListAsync();

            return Data.Select(x => new GetPatientByStatus
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }
        public async Task<List<GetPatientByStatus>> GetPatientCompleted()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .Where(x => x.DilationOrder.Status == PatientStatus.Completed)
                .ToListAsync();

            return Data.Select(x => new GetPatientByStatus
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }
        public async Task<List<GetPatientByStatus>> GetPatientDone()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .Where(x => x.DilationOrder.Status == PatientStatus.Done)
                .ToListAsync();

            return Data.Select(x => new GetPatientByStatus
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }
        public async Task<List<GetPatientByStatus>> GetPatientWaiting()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .Where(x => x.DilationOrder.Status == PatientStatus.Waiting)
                .ToListAsync();

            return Data.Select(x => new GetPatientByStatus
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }
        public async Task<List<GetPatientByStatus>> GetPatientPostDilation()
        {
            var Data = await _Db.Patient
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.Doctor)
                .Include(x => x.DilationOrder)
                    .ThenInclude(d => d.EyeDrop)
                .Where(x => x.DilationOrder.Status == PatientStatus.PostDilation)
                .ToListAsync();

            return Data.Select(x => new GetPatientByStatus
            {
                Id = x.Id,
                FullName = x.FullName,
                DoctorName = x.DilationOrder?.Doctor?.FullName ?? "No Doctor",
                DateOfBirth = x.DateOfBirth,
                DropType = x.DilationOrder?.EyeDrop?.Name ?? "No Drop",
                Status = x.DilationOrder?.Status.ToString() ?? "Unknown",
                DropsGiven = x.DilationOrder?.DropsGiven ?? 0,
                DropsRequired = x.DilationOrder?.DropsRequired ?? 0,
            }).ToList();
        }
        public async Task<bool> AddNewPatient(AddNewPatient NewPatient)
        {
            if (NewPatient == null)
                return false;
            var Doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == NewPatient.DoctorId);
            if (Doctor == null)
            {
                return false;
            }
            bool DoctorIsFree = await _Db.User.AnyAsync(x => x.Id == NewPatient.DoctorId && x.IsFree == true);
            var Status = DoctorIsFree ? PatientStatus.InDilation : PatientStatus.Waiting;
                var Data = new Patient
                {
                    FullName = NewPatient.FullName,
                    DateOfBirth = NewPatient.DateOfBirth,
                    DilationOrder = new Models.DilationOrder
                    {
                        DoctorId = NewPatient.DoctorId,
                        Status = Status,
                        EyeDropId = null,
                        DropsGiven = 0,
                        DropsRequired = 0,
                       
                    }
                };
            if (DoctorIsFree)
            {
                var doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == NewPatient.DoctorId);
                if (doctor != null)
                    doctor.IsFree = false;
            }
            await _Db.Patient.AddAsync(Data);
            await _Db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdatePatientDropsCount(int PatientID,int StaffId)
        {
            var Data = await _Db.Patient.Include(p => p.DilationOrder).FirstOrDefaultAsync(x => x.Id == PatientID);
            if (Data == null || Data.DilationOrder == null) { return false; }
            var NewData = Data.DilationOrder;
            if (NewData.DropsGiven == null )
                NewData.DropsGiven = 0;
            if (NewData.DropsRequired == null)
                return false;
            NewData.DropsGiven += 1;

            if (NewData.DropsGiven < 0)
            {
                NewData.DropsGiven = 0; 
            }
           
            string doctorId = NewData.DoctorId.ToString();
            string message = "";
            if (NewData.DropsGiven >= NewData.DropsRequired)
            {
                NewData.DropsGiven = NewData.DropsRequired;
                NewData.Status = PatientStatus.Completed;
                message = $"Patient {Data.FullName} has completed all doses. Please mark as done.";

            }
            else
            {
                message = $"Staff {StaffId} has given a drop to Patient {Data.FullName}. Current count: {NewData.DropsGiven}/{NewData.DropsRequired}";

            }

            await _Db.SaveChangesAsync();
            if (_hub != null)
                await _hub.Clients.Group(doctorId).SendAsync("ReceiveMessage", message, StaffId.ToString());
            return true;
        }
        public async Task<bool> UpdatePatientInfo(int PatientID, UpdatePatient UpdatePatient)
        {
            var Data = await _Db.Patient.FirstOrDefaultAsync(x => x.Id == PatientID);
            if (Data == null || UpdatePatient == null ) { return false; }
            Data.FullName = UpdatePatient.FullName;
            Data.DilationOrder.DoctorId = UpdatePatient.DoctorId;
            Data.DateOfBirth = UpdatePatient.DateOfBirth;
            Data.DilationOrder.DropsRequired = UpdatePatient.DropsRequired;
            Data.DilationOrder.DropsGiven = UpdatePatient.DropsGiven;
            if (Enum.TryParse<PatientStatus>(UpdatePatient.Status, true, out var enumStatus))
            {
                Data.DilationOrder.Status = enumStatus;
            }
            Data.DilationOrder.EyeDropId = UpdatePatient.DropTypeId;
            await _Db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePatient(int PatientID)
        {
            var Data = await _Db.Patient.FirstOrDefaultAsync(x => x.Id == PatientID);
            if(Data == null || PatientID < 1) {  return false; }
            _Db.Patient.Remove(Data);
            await _Db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EnterThePatient(int PatientID)
        {
            var Patient = await _Db.Patient.Include(x => x.DilationOrder).FirstOrDefaultAsync(x => x.Id == PatientID);
            if (Patient == null) { return false; }
            if (Patient.DilationOrder.Status == PatientStatus.Done || Patient.DilationOrder.Status == PatientStatus.Completed || Patient.DilationOrder.Status == PatientStatus.InDilation)
            {
                return false;
            }
            var Doctor = await _Db.User.FirstOrDefaultAsync(x => x.Id == Patient.DilationOrder.DoctorId && x.Role == UserRole.Doctor);
            if (Doctor == null) { return false; }
            if (Doctor.IsFree == false) { return false; }
            Patient.DilationOrder.Status = PatientStatus.InDilation;
            Doctor.IsFree = false;
            await _Db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsPatient(int PatientId)
        {
            if (PatientId < 1) { return false;}
            var Patient = await _Db.Patient.FirstOrDefaultAsync(x => x.Id == PatientId);
            return Patient != null;
        }
        public async Task<bool> IsPatient(string PatientName)
        {
            var Patient = await _Db.Patient.FirstOrDefaultAsync(x => x.FullName == PatientName);
            return Patient != null;
        }
    }
}
