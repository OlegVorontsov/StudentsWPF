using StudentsWPF.Models;

namespace StudentsWPF.Interfaces
{
    public interface IFileService
    {
        List<Student> Open(string filename);
        void Save(string filename, List<Student> studentsList);
    }
}
