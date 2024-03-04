using Exercise3.Models;
using Exercise3.Models.DTOs;
using Exercise3.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercise3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var isNotEmpty = _studentsRepository.GetStudents().Any();
                if (!isNotEmpty)
                {
                    return Ok(new List<Student>());
                }
                return Ok(_studentsRepository.GetStudents);
            }catch(Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet("{index}")]
        public async Task<IActionResult> Get(string index)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if(student is null)
                {
                    return NotFound();
                }
                
                return Ok(student);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpPut("{index}")]
        public async Task<IActionResult> Put(string index, StudentPUT newStudentData)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(newStudentData.FirstName) ||
                string.IsNullOrEmpty(newStudentData.LastName) ||
                string.IsNullOrEmpty(newStudentData.BirthDate) ||
                string.IsNullOrEmpty(newStudentData.StudyName) ||
                string.IsNullOrEmpty(newStudentData.StudyMode) ||
                string.IsNullOrEmpty(newStudentData.Email) ||
                string.IsNullOrEmpty(newStudentData.FathersName) ||
                string.IsNullOrEmpty(newStudentData.MothersName))
                {
                    return BadRequest();
                }

                await _studentsRepository.UpdateStudent(student, new Models.Student
                {
                    
                    FirstName = newStudentData.FirstName,
                    LastName = newStudentData.LastName,
                    BirthDate = newStudentData.BirthDate,
                    StudyName = newStudentData.StudyName,
                    StudyMode = newStudentData.StudyMode,
                    Email = newStudentData.Email,
                    FathersName = newStudentData.FathersName,
                    MothersName = newStudentData.MothersName,

                });

                return Ok(student);
            }catch(Exception ex)
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentPOST newStudent)
        {
            try
            {

                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == newStudent.IndexNumber).FirstOrDefault();
                if (student is not null)
                {
                    return Conflict();
                }

                if (string.IsNullOrEmpty(newStudent.FirstName) ||
                string.IsNullOrEmpty(newStudent.LastName) ||
                string.IsNullOrEmpty(newStudent.IndexNumber) ||
                string.IsNullOrEmpty(newStudent.BirthDate) ||
                string.IsNullOrEmpty(newStudent.StudyName) ||
                string.IsNullOrEmpty(newStudent.StudyMode) ||
                string.IsNullOrEmpty(newStudent.Email) ||
                string.IsNullOrEmpty(newStudent.FathersName) ||
                string.IsNullOrEmpty(newStudent.MothersName))
                {
                    return BadRequest();
                }

                await _studentsRepository.AddStudent(new Models.Student
                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    IndexNumber = newStudent.IndexNumber,
                    BirthDate = newStudent.BirthDate,
                    StudyName = newStudent.StudyName,
                    StudyMode = newStudent.StudyMode,
                    Email = newStudent.Email,
                    FathersName = newStudent.FathersName,
                    MothersName = newStudent.MothersName,
                });
                return Created("Created: ", newStudent);

            }
            catch(Exception ex)
            {
                return Problem();
            }
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> Delete(string index)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }
                await _studentsRepository.DeleteStudent(student);
                return Ok();
            }catch(Exception ex)
            {
                return Problem();
            }
        }

    }
}
