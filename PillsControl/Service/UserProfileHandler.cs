using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using PillsControl.Models;

namespace PillsControl.Service
{
	class UserProfileHandler : DependencyObject
	{
		public static readonly DependencyProperty UserInfoProperty = DependencyProperty.Register(
			"UserInfo", typeof(string), typeof(UserProfileHandler), new PropertyMetadata(default(string)));

		public string UserInfo
		{
			get { return (string)GetValue(UserInfoProperty); }
			set { SetValue(UserInfoProperty, value); }
		}

		public string UserLogInTextBox { get; set; }
		public string UserPasswordInTextBox { get; set; }

		public void MakeNewUserProfile()
		{
			var users = LoadAllUsers();
			users.Add(new UserProfile(UserLogInTextBox, UserPasswordInTextBox));
			SaveAllUsers(users);
		}

		public UserProfile LoginUser()
		{
			UserProfile user = LoadUser();
			if (user == null || user.Password != UserPasswordInTextBox)
			{
				UserInfo = "User login or password is wrong";
				return null;
			}

			return user;
		}

		private void SaveUser()
		{
			if (LoadUser() != null)
			{
				UserInfo = "User with this login exist";
				return;
			}

			List<UserProfile> users = LoadAllUsers();

			users.Add(new UserProfile(UserLogInTextBox, UserPasswordInTextBox));
			SaveAllUsers(users);
		}

		private UserProfile LoadUser()
		{
			return LoadAllUsers()
				.First(user => user.Login == UserLogInTextBox);
		}

		private void SaveAllUsers(List<UserProfile> users)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);

			serializer.WriteObject(writer, users);
		}

		private List<UserProfile> LoadAllUsers()
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

			List<UserProfile> users = (List<UserProfile>)serializer.ReadObject(reader);

			return users;
		}
	}
}
