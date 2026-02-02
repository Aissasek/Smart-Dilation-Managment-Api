using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.DTO.DoctorDTO;
using Smart_Dilation_Management.DTO.PatientDTO;
using Smart_Dilation_Management.Interfaces;

namespace Smart_Dilation_Management.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _Doctor;
        private readonly IPatient _Patient;
        public DoctorController(IDoctor doctor, IPatient patient)
        {
            _Doctor = doctor;
            _Patient = patient;
        }
        [Authorize(Roles ="Admin,Doctor,Staff")]
        [HttpGet("Get_All_Doctor")]
        public async Task<IActionResult> GetAllDoctor()
        {
            return Ok(await _Doctor.GetAllDoctor());
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet("Search/{FullName}")]
        public async Task<IActionResult> SearchDoctor(string FullName)
        {
            var Doctor = await _Doctor.IsDoctor(FullName);
            if (Doctor == false)
            {
                return NotFound($"The Doctor with the name {FullName} is not found !!");
            }
            return Ok(await _Doctor.SearchDoctor(FullName));
        }
        [HttpGet("Doctor_Free")]
        public async Task<IActionResult> GetDoctorFree()
        {
            return Ok(await _Doctor.GetDoctorFree());
        }
        [HttpGet("Doctor_Not_Free")]
        public async Task<IActionResult> GetDoctorNotFree()
        {
            return Ok(await _Doctor.GetDoctorNotFree());
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("Add_New_Doctor")]
        public async Task<IActionResult> AddNewDoctor(AddNewDoctor NewDoctor)
        {
            var Data = await _Doctor.AddNewDoctor(NewDoctor);
            if (NewDoctor == null)
                return BadRequest("Doctor data is required");

            if (Data == false)
            {
                return BadRequest("Something Went wrong !!");
            }
            return Ok(new { message = "Doctor added successfully" });

        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("Update_Doctor/{DoctorId}")]
        public async Task<IActionResult> UpdateDoctor(int DoctorId, UpdateDoctor UpdateDoctor)
        {
            var Doctor = await _Doctor.IsDoctor(DoctorId);
            if (Doctor == false)
            {
                return NotFound($"The Doctor with the Id {DoctorId} is not found !!");
            }
            var Data = await _Doctor.UpdateDoctor(DoctorId, UpdateDoctor);
            if (UpdateDoctor == null)
                return BadRequest("Doctor data is required");

            if (Data == false)
            {
                return BadRequest($"Something went wrong !!");
            }
            return Ok(new { message = "Doctor updated successfully" });

        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("Delete_Doctor/{DoctorId}")]
        public async Task<IActionResult> DeleteDoctor(int DoctorId)
        {
            var Doctor = await _Doctor.IsDoctor(DoctorId);
            if (Doctor == false)
            {
                return NotFound($"The patient with the Id {DoctorId} is not found !!");
            }
            var Data = await _Doctor.DeleteDoctor(DoctorId);
            if (Data == false)
            {
                return NotFound($"Something went wrong !!");

            }
            return Ok(new { message = "Doctor Deleted successfully" });

        }
        [HttpPut("PatientExamination/{DoctorId}/{PatientId}")]
        public async Task<IActionResult?> PatientExamination(int DoctorId, int PatientId, DilationDTO ExaminationReport)
        {
            var Doctor = await _Doctor.IsDoctor(DoctorId);
            if (Doctor == false)
            {
                return NotFound($"The Doctor with the Id {DoctorId} is not found !!");
            }
            var Patient = await _Patient.IsPatient(PatientId);
            if (Patient == false)
            {
                return NotFound($"The patient with the Id {PatientId} is not found !!");
            }
            var Data = await _Doctor.PatientExamination(DoctorId,PatientId, ExaminationReport);
            if (ExaminationReport == null)
                return BadRequest("Examination data is required");

            if (Data == false)
            {
                return NotFound("Somethink went wrong !!");
            }
            return Ok("The Examination Report successfully");
        }
        [HttpPut("Mark_Done/{PatientId}/{DoctorId}/{StaffId}")]
        public async Task<IActionResult> MarkAsDone(int PatientId, int DoctorId, int StaffId)
        {
            var Doctor = await _Doctor.IsDoctor(DoctorId);
            if (Doctor == false)
            {
                return NotFound($"The Doctor with the Id {DoctorId} is not found !!");
            }
            var Patient = await _Patient.IsPatient(PatientId);
            if (Patient == false)
            {
                return NotFound($"The patient with the Id {PatientId} is not found !!");
            }
            var Staff = await _Doctor.IsStaff(StaffId);
            if (Staff == false)
            {
                return NotFound($"The Staff with the Id {StaffId} is not found !!");
            }
            var Data = await _Doctor.MarkAsDone(PatientId,DoctorId,StaffId);
          
            if (Data == false)
            {
                return BadRequest("Something Went wrong !!");
            }
            return Ok(new { message = "Patient Done successfully" });
        }
    }
}
