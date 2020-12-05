using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillsControl.Service;

namespace PillsControl.Commands
{
	class CommandImplementation
	{
		private BasicCommand _closeApplicationCommand;
		public BasicCommand CloseApplicationCommand
		{
			get
			{
				return _closeApplicationCommand ??= new BasicCommand(obj =>
					{
						App.Current.Shutdown();
					}
				);
			}
		}

		private BasicCommand _tryToLogInCommand;
		public BasicCommand TryToLogInCommand
		{
			get
			{
				return _tryToLogInCommand ??= new BasicCommand(obj =>
					{
						UserProfileHandler profileHandler = App.Current.FindResource("ProfileHandler") as UserProfileHandler;
					}
				);
			}
		}

		private BasicCommand _makeNewUserCommand;
		public BasicCommand MakeNewUserCommand
		{
			get
			{
				return _makeNewUserCommand ??= new BasicCommand(obj =>
					{

					}
				);
			}
		}
	}
}
