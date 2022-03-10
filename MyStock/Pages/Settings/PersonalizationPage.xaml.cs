using MyStock.ViewModels;

namespace MyStock.Pages.Settings
{

    public partial class PersonalizationPage : UserControl
    {
        public PersonalizationPage()
        {
            DataContext = new PersonalizationPageViewModel();
            InitializeComponent();
            if (AppManager.UISettings.IsCustomColor)
                CustomPaletteButton.IsChecked = true;
            else
                MdPaletteButton.IsChecked = true;
        }

        private void MdPaletteButton_Checked(object sender, RoutedEventArgs e)
        {
            AppManager.UISettings.IsCustomColor = false;
        }

        private void CustomPaletteButton_Checked(object sender, RoutedEventArgs e)
        {
            AppManager.UISettings.IsCustomColor = true;
        }
    }
}
