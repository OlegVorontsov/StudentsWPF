using StudentsWPF.Commands;
using StudentsWPF.Interfaces;
using StudentsWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsWPF.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private Student selectedStudent;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Student> Students { get; set; }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Students.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var students = fileService.Open(dialogService.FilePath);
                              Students.Clear();
                              foreach (var p in students)
                                  Students.Add(p);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

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
        // команда удаления студента
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Student student = obj as Student;
                      if (student != null)
                      {
                          Students.Remove(student);
                      }
                  },
                 (obj) => Students.Count > 0));
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

        public StudentViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            // студенты по умолчанию
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
