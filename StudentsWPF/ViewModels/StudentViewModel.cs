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
        private Subject selectedSubject;
        private Grade selectedGrade;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Subject> Subjects { get; set; }

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
        private RelayCommand addStudentCommand;
        public RelayCommand AddStudentCommand
        {
            get
            {
                return addStudentCommand ??
                  (addStudentCommand = new RelayCommand(obj =>
                  {
                      Student student = new Student();
                      Students.Insert(0, student);
                      SelectedStudent = student;
                  }));
            }
        }
        // команда удаления студента
        private RelayCommand removeStudentCommand;
        public RelayCommand RemoveStudentCommand
        {
            get
            {
                return removeStudentCommand ??
                  (removeStudentCommand = new RelayCommand(obj =>
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

        // команда добавления нового предмета
        private RelayCommand addSubjectCommand;
        public RelayCommand AddSubjectCommand
        {
            get
            {
                return addSubjectCommand ??
                  (addSubjectCommand = new RelayCommand(obj =>
                  {
                      Subject subject = new Subject();
                      Subjects.Insert(0, subject);
                      SelectedSubject = subject;
                  }));
            }
        }
        // команда удаления предмета
        private RelayCommand removeSubjectCommand;
        public RelayCommand RemoveSubjectCommand
        {
            get
            {
                return removeSubjectCommand ??
                  (removeSubjectCommand = new RelayCommand(obj =>
                  {
                      Subject subject = obj as Subject;
                      if (subject != null)
                      {
                          Subjects.Remove(subject);
                      }
                  },
                 (obj) => Subjects.Count > 0));
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
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                selectedSubject = value;
                OnPropertyChanged("SelectedSubject");
            }
        }
        public Grade SelectedGrade
        {
            get { return selectedGrade; }
            set
            {
                selectedGrade = value;
                OnPropertyChanged("SelectedGrade");
            }
        }

        public StudentViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            // студенты по умолчанию
            Students = new ObservableCollection<Student>
            {
                new Student { Id = 1, FirstName = "Ivanov", SecondName = "Ivan", LastName = "Ivanovich" },
                new Student { Id = 2, FirstName = "Petrov", SecondName = "Petr", LastName = "Petrovich" },
                new Student { Id = 3, FirstName = "Sidorov", SecondName = "Aleksandr", LastName = "Aleksandrovich" },
                new Student { Id = 4, FirstName = "Harisova", SecondName = "Kseniya", LastName = "Petrova" }
            };

            // предметы по умолчанию
            Subjects = new ObservableCollection<Subject>
            {
                new Subject { Id = 1, Name = "Math", Description = "Math description" },
                new Subject { Id = 2, Name = "Biology", Description = "Biology description" },
                new Subject { Id = 3, Name = "Philosophy", Description = "Philosophy description" },
                new Subject { Id = 4, Name = "Geometry", Description = "Geometry description" }
            };

            // оценки по умолчанию
            Students.First(s => s.Id == 1).Grades.Add(new Grade
                {
                    Id = 1,
                    StudentId = 1,
                    SubjectId = 1,
                    Number = 5
                });
            Students.First(s => s.Id == 1).Grades.Add(new Grade
                {
                    Id = 2,
                    StudentId = 1,
                    SubjectId = 2,
                    Number = 3
                });

            Students.First(s => s.Id == 2).Grades.Add(new Grade
                {
                    Id = 3,
                    StudentId = 2,
                    SubjectId = 3,
                    Number = 4
                });
            Students.First(s => s.Id == 2).Grades.Add(new Grade
                {
                    Id = 4,
                    StudentId = 2,
                    SubjectId = 4,
                    Number = 4
                });

            Students.First(s => s.Id == 3).Grades.Add(new Grade
                {
                    Id = 5,
                    StudentId = 3,
                    SubjectId = 1,
                    Number = 3
                });
            Students.First(s => s.Id == 3).Grades.Add(new Grade
                {
                    Id = 6,
                    StudentId = 3,
                    SubjectId = 4,
                    Number = 2
                });

            Students.First(s => s.Id == 4).Grades.Add(new Grade
                {
                    Id = 7,
                    StudentId = 4,
                    SubjectId = 2,
                    Number = 5
                });
            Students.First(s => s.Id == 4).Grades.Add(new Grade
                {
                    Id = 8,
                    StudentId = 4,
                    SubjectId = 3,
                    Number = 5
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
