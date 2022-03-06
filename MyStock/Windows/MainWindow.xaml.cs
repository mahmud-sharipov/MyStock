using MahApps.Metro.Controls.Dialogs;
using MyStock.Application.UIComunication;
using MyStock.ViewModels;

namespace MyStock;

public partial class MainWindow : IViewFor<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        DataContext = ViewModel;
        MessageManager.ShowMessage += MessageManager_ShowMessageAsync;
    }

    public MainViewModel ViewModel { get; set; }
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as MainViewModel;
    }

    private async void MessageManager_ShowMessageAsync(MessageEventArgs e)
    {
        var message = e.Message;
        var messageSetting = new MetroDialogSettings();
        var messageDialogStyle = MessageDialogStyle.Affirmative;
        //var choices = message.Choices.ToList();
        //switch (message.Choices.Count)
        //{
        //    case 1:
        //        messageSetting.AffirmativeButtonText = choices.Single().Title;
        //        break;
        //    case 2:
        //        messageSetting.AffirmativeButtonText = choices[0].Title;
        //        messageSetting.NegativeButtonText = choices[1].Title;
        //        messageDialogStyle = MessageDialogStyle.AffirmativeAndNegative;
        //        break;
        //    case 3:
        //        messageSetting.AffirmativeButtonText = choices[0].Title;
        //        messageSetting.NegativeButtonText = choices[1].Title;
        //        messageSetting.FirstAuxiliaryButtonText = choices[2].Title;
        //        messageDialogStyle = MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
        //        break;
        //    default:
        //        break;
        //}

        var result = await this.ShowMessageAsync(message.Title, message.Detail + "\n" + message.Solution,
           messageDialogStyle, messageSetting);
        //if (result == MessageDialogResult.Affirmative)
        //{
        //    choices[0].Action.Invoke(choices[0]);
        //}
        //else if (result == MessageDialogResult.FirstAuxiliary)
        //{

        //}
        //else if (result == MessageDialogResult.Negative)
        //{
        //    choices[1].Action.Invoke(choices[1]);
        //}
        //else if (result == MessageDialogResult.SecondAuxiliary)
        //{

        //}
    }
}