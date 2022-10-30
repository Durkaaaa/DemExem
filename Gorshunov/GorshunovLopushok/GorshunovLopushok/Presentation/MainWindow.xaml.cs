using GorshunovLopushok.Presentation.ViewModels;
using System.Windows;

namespace GorshunovLopushok.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.Search = SearchTextBox.Text;
        }
    }
}
