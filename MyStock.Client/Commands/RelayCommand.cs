using System.Windows.Input;

namespace MyStock.Commands;

public class RelayCommand : ICommand
{
    private Action<object> _execute;
    private Func<object, bool> _canExecute;
    public event EventHandler CanExecuteChanged;

    public RelayCommand(Action<object> execute)
    {
        _execute = execute;
        _canExecute = p => true;

    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) =>
        _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) =>
        _execute(parameter);

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}