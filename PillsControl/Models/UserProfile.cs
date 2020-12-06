using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PillsControl.Models
{
	enum WhenToEat
	{
		Before, While, After
	}
	[DataContract]
	class UserProfile
	{
		public UserProfile(string nameDescription, string pathToImage = "Resources/basicUserProfileImage.jpg")
		{
			NameDescription = nameDescription;
		}
		[DataMember]
		public string NameDescription { get; set; }
		[DataMember]
		public string ReminderDescription { get; set; }
		[DataMember]
		public List<DateTime> ReminderTimes { get; set; }
		[DataMember]
		public WhenToEat WhenToEat { get; set; }
		[DataMember]
		public string PathToImage { get; set; }
	}
}
