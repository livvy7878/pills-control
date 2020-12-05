using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillsControl.Commands
{
	class BasicCommand : ICommand
	{
		private Func<object, bool> _canExecute;
		private Action<object> _execute;

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