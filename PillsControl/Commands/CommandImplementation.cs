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
						UserProfileHandler profileHandler = App.Current.FindResource("UserProfileHandler") as UserProfileHandler;
						UserProfile loadedUser = profileHandler.LoginUser();

						MainWindow window = new MainWindow();
						MainWindowViewModel mainWindowViewModel =
							App.Current.FindResource("MainWindowViewModel") as MainWindowViewModel;
						mainWindowViewModel.CurrentUserProfile = loadedUser;
						window.Show();
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
