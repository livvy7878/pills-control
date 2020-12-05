using System;
using System.Windows.Input;

namespace PillsControl.Commands
{
	internal class BasicCommand : ICommand
	{
		private Func<object, bool> _canExecute;
		private Action<object> _execute;

		public BasicCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		public bool CanExecute(object? parameter)
		{
			return _canExecute == null || _canExecute(parameter);
		}

		public void Execute(object? parameter)
		{
			_execute(parameter);
		}

		public event EventHandler? CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}
	}
}