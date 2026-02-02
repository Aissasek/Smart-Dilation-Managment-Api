using Smart_Dilation_Management.DTO.PatientDTO;

namespace Smart_Dilation_Management.Interfaces
{
    public interface IPatient
    {
        public Task<List<GetAllPatientDTO>> GetAllPatients();
        public Task<GetAllPatientDTO?> SearchPatient(string FullName);
        public Task<List<GetPatientByStatus>?> GetPatientByStatus(string Status);
        public Task<List<GetPatientByStatus>> GetPatientIndilation();
        public Task<List<GetPatientByStatus>> GetPatientCompleted();
        public Task<List<GetPatientByStatus>> GetPatientDone();
        public Task<List<GetPatientByStatus>> GetPatientWaiting();
        public Task<List<GetPatientByStatus>> GetPatientPostDilation();
        public Task<bool> AddNewPatient(AddNewPatient NewPatient);
        public Task<bool> UpdatePatientDropsCount(int PatientId,int StaffId);
        public Task<bool> UpdatePatientInfo(int PatientID, UpdatePatient UpdatePatient);
        public Task<bool> DeletePatient(int PatientID);
        public Task<bool> EnterThePatient(int PatientID);
        public Task<bool> IsPatient(int PatientID);
        public Task<bool> IsPatient(string PatientName);
    }
}
