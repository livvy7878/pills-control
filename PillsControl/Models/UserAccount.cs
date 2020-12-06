using System.Runtime.Serialization;

namespace PillsControl.Models
{
	[DataContract]
	internal class UserAccount
	{
		[DataMember]
		public string Login { get; set; }
		[DataMember]
		public string Password { get; set; }

		public UserAccount(string login, string password)
		{
			Login = login;
			Password = password;
		}

		public override bool Equals(object? obj)
		{
			UserAccount user = obj as UserAccount;
			return user.Login == Login && user.Password == Password;
		}
	}
}