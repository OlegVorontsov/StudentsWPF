using StudentsWPF.DataAccess;
using StudentsWPF.Models;
using StudentsWPF.Services;
using StudentsWPF.ViewModels;
using StudentsWPF.Windows;
using System.Windows;

namespace StudentsWPF
{
    public partial class MainWindow : Window
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public MainWindow()
        {
            InitializeComponent();
            // DataContext = new StudentViewModel(new DialogService(), new JsonFileService());
            Loaded += MainWindow_Loaded;
        }

        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            // db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Students.ToList();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Students.Local.ToObservableCollection();
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var studentWindow = new StudentWindow(new Student());
            if (studentWindow.ShowDialog() == true)
            {
                Student student = studentWindow.Student;
                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Student? student = studentsList.SelectedItem as Student;
            // если ни одного объекта не выделено, выходим
            if (student is null) return;

            var studentWindow = new StudentWindow(new Student
            {
                Id = student.Id,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                LastName = student.LastName
            });

            if (studentWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                student = db.Students.Find(studentWindow.Student.Id);
                if (student != null)
                {
                    student.FirstName = studentWindow.Student.FirstName;
                    student.SecondName = studentWindow.Student.SecondName;
                    student.LastName = studentWindow.Student.LastName;
                    db.SaveChanges();
                    studentsList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Student? student = studentsList.SelectedItem as Student;
            // если ни одного объекта не выделено, выходим
            if (student is null) return;
            db.Students.Remove(student);
            db.SaveChanges();
        }
    }
}