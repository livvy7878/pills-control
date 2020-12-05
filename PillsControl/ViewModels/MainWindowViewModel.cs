using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillsControl.Models;
using PillsControl.ViewModels.Service;

namespace PillsControl.ViewModels
{
	internal class MainWindowViewModel
	{
		public UserProfileHandler UserProfileHandler { get; set; }
		public UserProfile CurrentUserProfile { get; set; }

		public MainWindowViewModel()
		{
			UserProfileHandler = new UserProfileHandler();
		}
	}
}
