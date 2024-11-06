using StudentsWPF.Commands;
using StudentsWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsWPF.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private Student selectedStudent;

        public ObservableCollection<Student> Students { get; set; }

        // команда добавления нового студента
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Student student = new Student();
                      Students.Insert(0, student);
                      SelectedStudent = student;
                  }));
            }
        }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        public StudentViewModel()
        {
            Students = new ObservableCollection<Student>
            {
                new Student { FirstName = "Ivanov", SecondName = "Ivan", LastName = "Ivanovich" },
                new Student { FirstName = "Petrov", SecondName = "Petr", LastName = "Petrovich" },
                new Student { FirstName = "Sidorov", SecondName = "Aleksandr", LastName = "Aleksandrovich" },
                new Student { FirstName = "Harisova", SecondName = "Kseniya", LastName = "Petrova" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
