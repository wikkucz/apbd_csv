using Exercise3.Models;

namespace Exercise3.Services
{
    public interface IFileDbService
    {
        public IEnumerable<Student> Students { get; set; }
        Task SaveChanges();
    }

    public class FileDbService : IFileDbService
    {
        private readonly string _pathToFileDatabase;
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public FileDbService(IConfiguration configuration)
        {
            _pathToFileDatabase = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(_pathToFileDatabase))
            {
                return;
            }
            var lines = File.ReadLines(_pathToFileDatabase);

            var students = new List<Student>();

            lines.ToList().ForEach(line =>
            {
                var splittedLine = line.Split(',');

                var newStudent = new Student
                {
                    
                    FirstName = splittedLine[0],
                    LastName = splittedLine[1],
                    IndexNumber = splittedLine[2],
                    BirthDate = splittedLine[3],
                    StudyName = splittedLine[4],
                    StudyMode = splittedLine[5],
                    Email = splittedLine[6],
                    FathersName = splittedLine[7],
                    MothersName = splittedLine[8],

                };

                students.Add(newStudent);
            });


            Students = students;
        }

        public async Task SaveChanges()
        {
            var StudentsList = new List<string>();
            foreach (var student in Students)
            {
                StudentsList.Add(student.Format());
            }

            await File.WriteAllLinesAsync(
                _pathToFileDatabase, 
                StudentsList
                );
        }

    }
}
