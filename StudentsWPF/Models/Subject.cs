using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentsWPF.Models
{
    public class Subject : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        private string? description;
        public List<Student> Students { get; set; } = new();

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
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
