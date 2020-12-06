using System.Runtime.Serialization;

namespace PillsControl.Models
{
	[DataContract]
	internal class UserProfile
	{
		[DataMember]
		public string Login { get; set; }
		[DataMember]
		public string Password { get; set; }

		public UserProfile(string login, string password)
		{
			Login = login;
			Password = password;
		}

		public override bool Equals(object? obj)
		{
			UserProfile user = obj as UserProfile;
			return user.Login == Login && user.Password == Password;
		}
	}
}