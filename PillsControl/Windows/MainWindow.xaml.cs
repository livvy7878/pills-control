using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xaml;
using PillsControl.ViewModels;

namespace PillsControl.Windows
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			MainWindowViewModel.UserIsPassedLogin += OnUserPassedLogin;
			MainWindowViewModel.UserLoggedOut += OnUserLogout;
		}

		private void OnUserLogout()
		{
			MainFrame.GoBack();
		}

		private void OnUserPassedLogin()
		{
			MainFrame.Content = new MainSelectionPage();
			MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
		}
	}
}
