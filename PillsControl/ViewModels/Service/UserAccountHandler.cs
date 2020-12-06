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
	internal class UserAccountHandler : DependencyObject
	{
		public static readonly DependencyProperty UserInfoProperty = DependencyProperty.Register(
			"UserInfo", typeof(string), typeof(UserAccountHandler), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty LogInTextBoxProperty = DependencyProperty.Register(
			"LogInTextBox", typeof(string), typeof(UserAccountHandler), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty UserPasswordInTextBoxProperty = DependencyProperty.Register(
			"UserPasswordInTextBox", typeof(string), typeof(UserAccountHandler), new PropertyMetadata(default(string)));

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

		public UserAccountHandler()
		{
			UserLogInTextBox = "";
			UserPasswordInTextBox = "";
		}

		public UserAccount LoginUser()
		{
			UserAccount user = LoadUser();
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

			List<UserAccount> users = LoadAllUsers() ?? new List<UserAccount>();

			users.Add(new UserAccount(UserLogInTextBox, UserPasswordInTextBox));
			SaveAllUsers(users);
			UserInfo = "User created";
		}

		private UserAccount LoadUser()
		{
			var users = LoadAllUsers();
			UserAccount toReturn;
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

		private void SaveAllUsers(List<UserAccount> users)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserAccount>));
			FileStream fs = new FileStream("UsersAccounts.xml", FileMode.OpenOrCreate);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);

			serializer.WriteObject(writer, users);
			writer.Close();
		}

		private List<UserAccount> LoadAllUsers()
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserAccount>));
			List<UserAccount> users;

			try
			{
				using (FileStream fs = new FileStream("UsersAccounts.xml", FileMode.OpenOrCreate))
				{
					XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
					users = (List<UserAccount>)serializer.ReadObject(reader);
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