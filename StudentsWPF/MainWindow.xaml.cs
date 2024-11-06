using StudentsWPF.Services;
using StudentsWPF.ViewModels;
using System.Windows;

namespace StudentsWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext =
                new StudentViewModel(new DialogService(), new JsonFileService());
        }
    }
}