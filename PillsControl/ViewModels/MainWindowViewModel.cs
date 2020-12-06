using System;
using System.Windows;
using PillsControl.Models;
using PillsControl.ViewModels.Service;

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
	}
}