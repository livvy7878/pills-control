using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PillsControl.Models;
using PillsControl.ViewModels;
using PillsControl.ViewModels.Service;
using PillsControl.Windows;

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
						MainWindowViewModel mainWindowViewModel = App.Current.FindResource("MainWindowViewModel") as MainWindowViewModel;
						UserProfileHandler profileHandler = mainWindowViewModel.UserProfileHandler;
						UserProfile loadedUser = profileHandler.LoginUser();
						if (loadedUser == null)
						{
							return;
						}
						mainWindowViewModel.CurrentUserProfile = loadedUser;
						mainWindowViewModel.IsAuthorized = true;
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
						MainWindowViewModel mainWindowViewModel = App.Current.FindResource("MainWindowViewModel") as MainWindowViewModel;
						mainWindowViewModel.UserProfileHandler.SaveUser();
					}
				);
			}
		}
	}
}
