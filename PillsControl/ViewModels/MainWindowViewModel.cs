using System.Windows;
using PillsControl.Models;
using PillsControl.ViewModels.Service;

namespace PillsControl.ViewModels
{
	internal class MainWindowViewModel : DependencyObject
	{
		public static readonly DependencyProperty IsAuthorizedProperty = DependencyProperty.Register(
			"IsAuthorized", typeof(bool), typeof(MainWindowViewModel), new PropertyMetadata(default(bool)));

		public MainWindowViewModel()
		{
			UserProfileHandler = new UserProfileHandler();
		}

		public bool IsAuthorized
		{
			get => (bool) GetValue(IsAuthorizedProperty);
			set => SetValue(IsAuthorizedProperty, value);
		}

		public UserProfileHandler UserProfileHandler { get; set; }
		public UserProfile CurrentUserProfile { get; set; }
	}
}