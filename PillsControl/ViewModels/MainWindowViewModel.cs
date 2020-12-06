using System;
using System.Windows;
using PillsControl.Models;
using PillsControl.ViewModels.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PillsControl.ViewModels
{
	internal class MainWindowViewModel : DependencyObject
	{
		public static event Action UserIsPassedLogin;
		public static event Action UserLoggedOut;

		public static readonly DependencyProperty IsAuthorizedProperty = DependencyProperty.Register(
			"IsAuthorized", typeof(bool), typeof(MainWindowViewModel), new PropertyMetadata(default(bool)));

		public MainWindowViewModel()
		{
			UserProfileHandler = new UserProfileHandler();
			UserAccountHandler = new UserAccountHandler();
			LoadedUserProfiles = new ObservableCollection<UserProfile>(UserProfileHandler.LoadAllUserProfiles());
		}

		public bool IsAuthorized
		{
			get
			{
				return (bool)GetValue(IsAuthorizedProperty);
			}
			set
			{
				SetValue(IsAuthorizedProperty, value);
				if (IsAuthorized) { UserIsPassedLogin?.Invoke(); }
				else if (!IsAuthorized) { UserLoggedOut?.Invoke(); }
			}
		}

		public UserProfileHandler UserProfileHandler { get; set; }
		public UserAccountHandler UserAccountHandler { get; set; }
		public UserAccount CurrentUserAccount { get; set; }

		public static readonly DependencyProperty LoadedUserProfilesProperty = DependencyProperty.Register(
			"List", typeof(ObservableCollection<UserProfile>), typeof(MainWindowViewModel), new PropertyMetadata(default(List<UserProfile>)));

		public ObservableCollection<UserProfile> LoadedUserProfiles
		{
			get { return (ObservableCollection<UserProfile>) GetValue(LoadedUserProfilesProperty); }
			set { SetValue(LoadedUserProfilesProperty, value); }
		}
	}
}