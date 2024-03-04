﻿using Exercise3.Models;
using Exercise3.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exercise3.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();
        Task DeleteStudent (Student student);
        Task AddStudent(Student student);
        Task UpdateStudent(Student student, Student newData);
    }

    public class StudentsRepository : IStudentsRepository
    {

        private readonly IFileDbService _fileDbService;

        public StudentsRepository(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _fileDbService.Students;
        }

        public async Task DeleteStudent(Student student)
        {

            ((List<Student>)_fileDbService.Students).Remove(student);
            await _fileDbService.SaveChanges();

        }

        public async Task AddStudent(Student student)
        {

            ((List<Student>)_fileDbService.Students).Add(student);
            await _fileDbService.SaveChanges();
        }

        public async Task UpdateStudent(Student student, Student newData)
        {

            student.FirstName = newData.FirstName;
            student.LastName = newData.LastName;
            student.BirthDate = newData.BirthDate;
            student.StudyName = newData.StudyName;
            student.StudyMode = newData.StudyMode;
            student.Email = newData.Email;
            student.FathersName = newData.FathersName;
            student.MothersName = newData.MothersName;
            await _fileDbService.SaveChanges();

        }
    }
}
