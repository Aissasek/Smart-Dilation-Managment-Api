using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.DTO.PatientDTO;
using Smart_Dilation_Management.Interfaces;

namespace Smart_Dilation_Management.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _Patient;
        private readonly IDoctor _Doctor;
        public PatientController(IPatient patient, IDoctor doctor)
        {
            _Patient = patient;
            _Doctor = doctor;
        }
        [AllowAnonymous]
        [HttpGet("Get_All_Patient")]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok(await _Patient.GetAllPatients());
        }
        [AllowAnonymous]
        [HttpGet("Search/{FullName}")]
        public async Task<IActionResult> SearchPatient(string FullName)
        {
            var Patient = await _Patient.IsPatient(FullName);
            if(Patient == false)
            {
                return NotFound($"The patient with the name {FullName} is not found !!");
            }
            return Ok(await _Patient.SearchPatient(FullName));
        }
        [AllowAnonymous]
        [HttpGet("Status/{Status}")]
        public async Task<IActionResult> GetPatientByStatus(string Status)
        {
            return Ok(await _Patient.GetPatientByStatus(Status));
        }
        [AllowAnonymous]
        [HttpGet("Indilation")]
        public async Task<IActionResult> GetPatientIndilation()
        {
            return Ok(await _Patient.GetPatientIndilation());
        }
        [AllowAnonymous]
        [HttpGet("Completed")]
        public async Task<IActionResult> GetPatientCompleted()
        {
            return Ok(await _Patient.GetPatientCompleted());
        }
        [AllowAnonymous]
        [HttpGet("Done")]
        public async Task<IActionResult> GetPatientDone()
        {
            return Ok(await _Patient.GetPatientDone());
        }
        [AllowAnonymous]
        [HttpGet("Waiting")]
        public async Task<IActionResult> GetPatientWaiting()
        {
            return Ok(await _Patient.GetPatientWaiting());
        }
        [AllowAnonymous]
        [HttpGet("PostDilation")]
        public async Task<IActionResult> GetPatientPostDilation()
        {
            return Ok(await _Patient.GetPatientPostDilation());
        }
        //[Authorize(Roles = "Admin,Doctor")]
        [HttpPost("Add_New_Patient")]
        public async Task<IActionResult> AddNewPatient(AddNewPatient NewPatient)
        {
            var Data = await _Patient.AddNewPatient(NewPatient);
            if (Data == false)
            {
                return BadRequest("Something Went wrong !!");
            }
            return Ok(new { message = "Patient added successfully" });

        }
        //[Authorize(Roles = "Admin,Doctor")]
        [HttpPut("Add_Doza/{PatientId}/{StaffId}")]
        public async Task<IActionResult> UpdatePatientDropsCount(int PatientId, int StaffId)
        {
            var IsPatient = await _Patient.IsPatient(PatientId);
            if (IsPatient == false) { return NotFound($"Patient with the id = {PatientId} not found"); }
            var IsStaff = await _Doctor.IsStaff(StaffId);
            if (IsStaff == false) { return NotFound($"Staff with the id = {StaffId} not found"); }
            var Data = await _Patient.UpdatePatientDropsCount(PatientId, StaffId);
            if (Data == false)
            {
                return NotFound($"Something went wrong !!");
            }
            return Ok(new { message = "Patient Updated successfully" });

        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("Update_Patient/{PatientID}")]
        public async Task<IActionResult> UpdatePatientInfo(int PatientID, UpdatePatient UpdatePatient)
        {
            var IsPatient = await _Patient.IsPatient(PatientID);
            if (IsPatient == false) { return NotFound($"Patient with the id = {PatientID} not found"); }
            var Data = await _Patient.UpdatePatientInfo(PatientID, UpdatePatient);
            if (Data == false)
            {
                return NotFound($"Something went wrong !!");
            }
            return Ok(new { message = "Patient updated successfully" });

        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpDelete("Delete_Patient/{PatientID}")]
        public async Task<IActionResult> DeletePatient(int PatientID)
        {
            var IsPatient = await _Patient.IsPatient(PatientID);
            if (IsPatient == false) { return NotFound($"Patient with the id = {PatientID} not found"); }
            var Data = await _Patient.DeletePatient(PatientID);
            if (Data == false)
            {
                return NotFound($"Something went wrong !!");

            }
            return Ok(new { message = "Patient Deleted successfully" });

        }
        [HttpPut("Enter_The_Patient/{PatientID}")]
        public async Task<IActionResult> EnterThePatient(int PatientID)
        {
            var IsPatient = await _Patient.IsPatient(PatientID);
            if (IsPatient == false) { return NotFound($"Patient with the id = {PatientID} not found"); }
            var Data = await _Patient.EnterThePatient(PatientID);
            if (Data == false)
            {
                return NotFound($"Something went Wrong !! ");
            }
            return Ok(new { message = "Patient updated successfully" });
        }
    }
}
