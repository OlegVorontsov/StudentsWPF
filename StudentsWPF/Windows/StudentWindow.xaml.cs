using StudentsWPF.Models;
using System.Windows;

namespace StudentsWPF.Windows
{
    public partial class StudentWindow : Window
    {
        public Student Student { get; private set; }
        public StudentWindow(Student student)
        {
            InitializeComponent();
            Student = student;
            DataContext = Student;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
