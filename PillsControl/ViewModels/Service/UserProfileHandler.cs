using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml;
using PillsControl.Models;

namespace PillsControl.ViewModels.Service
{
	internal class UserProfileHandler : DependencyObject
	{
		public static readonly DependencyProperty UserInfoProperty = DependencyProperty.Register(
			"UserInfo", typeof(string), typeof(UserProfileHandler), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty LogInTextBoxProperty = DependencyProperty.Register(
			"LogInTextBox", typeof(string), typeof(UserProfileHandler), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty UserPasswordInTextBoxProperty = DependencyProperty.Register(
			"UserPasswordInTextBox", typeof(string), typeof(UserProfileHandler), new PropertyMetadata(default(string)));

		public string UserInfo
		{
			get => (string)GetValue(UserInfoProperty);
			set => SetValue(UserInfoProperty, value);
		}

		public string UserLogInTextBox
		{
			get => (string)GetValue(LogInTextBoxProperty);
			set => SetValue(LogInTextBoxProperty, value);
		}

		public string UserPasswordInTextBox
		{
			get => (string)GetValue(UserPasswordInTextBoxProperty);
			set => SetValue(UserPasswordInTextBoxProperty, value);
		}

		public UserProfileHandler()
		{
			UserLogInTextBox = "";
			UserPasswordInTextBox = "";
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

		public void SaveUser()
		{
			if (LoadUser() != null)
			{
				UserInfo = "User with this login exist";
				return;
			}

			List<UserProfile> users = LoadAllUsers() ?? new List<UserProfile>();

			users.Add(new UserProfile(UserLogInTextBox, UserPasswordInTextBox));
			SaveAllUsers(users);
			UserInfo = "User created";
		}

		private UserProfile LoadUser()
		{
			var users = LoadAllUsers();
			UserProfile toReturn;
			try
			{
				toReturn = users.First(user => user.Login == UserLogInTextBox);
			}
			catch (Exception)
			{
				toReturn = null;
			}

			return toReturn;
		}

		private void SaveAllUsers(List<UserProfile> users)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);

			serializer.WriteObject(writer, users);
			writer.Close();
		}

		private List<UserProfile> LoadAllUsers()
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			List<UserProfile> users;

			try
			{
				using (FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate))
				{
					XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
					users = (List<UserProfile>)serializer.ReadObject(reader);
				}
			}
			catch (Exception)
			{
				users = null;
			}

			return users;
		}
	}
}