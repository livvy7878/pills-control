using System.IO;
using System.Windows;
using PillsControl.Models;
using PillsControl.ViewModels;
using PillsControl.ViewModels.Service;

namespace PillsControl.Commands
{
	internal class CommandImplementation
	{
		private readonly MainWindowViewModel mainWindowViewModel =
			Application.Current.FindResource("MainWindowViewModel") as MainWindowViewModel;

		private BasicCommand _closeApplicationCommand;

		private BasicCommand _makeNewUserCommand;

		private BasicCommand _saveNewUserProfileCommand;

		private BasicCommand _tryToLogInCommand;

		public BasicCommand CloseApplicationCommand
		{
			get { return _closeApplicationCommand ??= new BasicCommand(obj => { Application.Current.Shutdown(); }); }
		}

		public BasicCommand TryToLogInCommand
		{
			get
			{
				return _tryToLogInCommand ??= new BasicCommand(obj =>
					{
						UserAccountHandler userAccountHandler = mainWindowViewModel.UserAccountHandler;
						UserAccount loadedUser = userAccountHandler.LoginUser();
						if (loadedUser == null)
						{
							return;
						}

						mainWindowViewModel.CurrentUserAccount = loadedUser;
						mainWindowViewModel.IsAuthorized = true;
					}
				);
			}
		}

		public BasicCommand MakeNewUserCommand
		{
			get
			{
				return _makeNewUserCommand ??= new BasicCommand(obj =>
					{
						mainWindowViewModel.UserAccountHandler.SaveUser();
					}
				);
			}
		}

		public BasicCommand SaveNewUserProfileCommand
		{
			get
			{
				return _saveNewUserProfileCommand ??= new BasicCommand(obj =>
					{
						if (!Directory.Exists("Resources/User"))
						{
							Directory.CreateDirectory("Resources/User");
						}

						string pathToImage = mainWindowViewModel.UserProfileHandler.CurrentUserCreatedImagePath ??
						                     "Resources/basicUserProfileImage.jpg";

						FileInfo fi = new FileInfo(pathToImage);
						File.Create("Resources/User" + fi.Name);
						File.Copy(pathToImage, "Resources/User/" + fi.Name, true);

						mainWindowViewModel.UserProfileHandler.SaveUserProfile(
							new UserProfile(mainWindowViewModel.UserProfileHandler.CurrentUserCreatedName,
								"Resources/User" + fi.Name));
					}
				);
			}
		}
	}
}