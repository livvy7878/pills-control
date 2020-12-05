namespace PillsControl.Models
{
	internal class UserProfile
	{
		public string Login { get; set; }
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