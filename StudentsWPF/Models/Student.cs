using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsWPF.Models
{
    public class Student : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string firstName;
        private string secondName;
        private string lastName;
        private double averageGrade;
        public List<Grade> Grades { get; set; } =
            new List<Grade>();

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public double AverageGrade
        {
            get
            {
                double sum = 0;
                foreach (var grade in Grades)
                {
                    sum += grade.Value;
                }
                return sum/Grades.Count;
            }
            set
            {
                averageGrade = value;
                OnPropertyChanged("AverageGrade");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
