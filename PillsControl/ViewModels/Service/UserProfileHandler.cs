using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml;
using PillsControl.Models;

namespace PillsControl.ViewModels.Service
{
	internal class UserProfileHandler : DependencyObject
	{
		public static readonly DependencyProperty CurrentUserCreatedImagePathProperty = DependencyProperty.Register(
			"CurrentUserCreatedImagePath", typeof(string), typeof(UserProfileHandler),
			new PropertyMetadata(default(string)));

		public static readonly DependencyProperty CurrentUserCreatedNameProperty = DependencyProperty.Register(
			"CurrentUserCreatedName", typeof(string), typeof(UserProfileHandler),
			new PropertyMetadata(default(string)));

		public string CurrentUserCreatedName
		{
			get => (string) GetValue(CurrentUserCreatedNameProperty);
			set => SetValue(CurrentUserCreatedNameProperty, value);
		}

		public string CurrentUserCreatedImagePath
		{
			get => (string) GetValue(CurrentUserCreatedImagePathProperty);
			set => SetValue(CurrentUserCreatedImagePathProperty, value);
		}

		public void SaveUserProfile(UserProfile userProfile)
		{
			List<UserProfile> userProfiles = LoadAllUserProfiles() ?? new List<UserProfile>();
			userProfiles.Add(userProfile);
			SaveAllUsersProfiles(userProfiles);
		}

		public UserProfile FindUserProfile(string nameDescription)
		{
			List<UserProfile> userProfiles = LoadAllUserProfiles() ?? new List<UserProfile>();
			UserProfile findedUser = userProfiles.Find(user => user.NameDescription == nameDescription);
			return findedUser;
		}

		public List<UserProfile> LoadAllUserProfiles()
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			List<UserProfile> userProfiles;

			try
			{
				using (FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate))
				{
					XmlDictionaryReader reader =
						XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
					userProfiles = (List<UserProfile>) serializer.ReadObject(reader);
				}
			}
			catch (Exception)
			{
				userProfiles = null;
			}

			return userProfiles;
		}

		private void SaveAllUsersProfiles(List<UserProfile> userProfiles)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserProfile>));
			FileStream fs = new FileStream("UsersProfile.xml", FileMode.OpenOrCreate);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);

			serializer.WriteObject(writer, userProfiles);
			writer.Close();
		}
	}
}