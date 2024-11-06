using StudentsWPF.Models;
using System.IO;
using System.Runtime.Serialization.Json;
using StudentsWPF.Interfaces;

namespace StudentsWPF.Services
{
    public class JsonFileService : IFileService
    {
        public List<Student> Open(string filename)
        {
            List<Student> students = new List<Student>();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                students = jsonFormatter.ReadObject(fs) as List<Student>;
            }

            return students;
        }

        public void Save(string filename, List<Student> studentsList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, studentsList);
            }
        }
    }
}
