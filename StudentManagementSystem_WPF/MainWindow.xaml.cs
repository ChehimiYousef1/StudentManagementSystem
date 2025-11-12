using System.Windows;

namespace StudentManagementSystem_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GO_AddStudent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddStudentPage());
        }

        private void GO_EditStudent_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EditStudentPage());
        }

        private void GO_DeleteStudent_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DeleteStudentPage());
        }

        private void GO_SearchStudent_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SearchStudentPage());
        }

        private void GO_View_All_Student_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new View_All_StudentPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
