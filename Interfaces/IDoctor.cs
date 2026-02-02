using Smart_Dilation_Management.DTO.DoctorDTO;
using Smart_Dilation_Management.DTO.PatientDTO;

namespace Smart_Dilation_Management.Interfaces
{
    public interface IDoctor
    {
        public Task<List<GetAllDoctorDTO>> GetAllDoctor();
        public Task<GetAllDoctorDTO?> SearchDoctor(string FullName);
        public Task<List<GetAllDoctorDTO>> GetDoctorFree();
        public Task<List<GetAllDoctorDTO>> GetDoctorNotFree();
        public Task<bool> AddNewDoctor(AddNewDoctor NewDoctor);
        public Task<bool> UpdateDoctor(int DoctorID, UpdateDoctor UpdateDoctor);
        public Task<bool> DeleteDoctor(int DoctorID);
        public Task<bool> PatientExamination(int DoctorId,int PatientId,DilationDTO ExaminationReport);
        public Task<bool> MarkAsDone (int PatientId,int DoctorId,int StaffId);
        public Task<bool> IsDoctor(int DoctorId);
        public Task<bool> IsStaff(int StaffId);
        public Task<bool> IsDoctor(string DoctorName);
    }
}
