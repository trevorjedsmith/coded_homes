using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodedHomes.Models
{
	[ScaffoldTable(false)]
	public class OAuthMembershipMetadata { }

	[MetadataType(typeof(OAuthMembershipMetadata))]
	public class OAuthMembership
	{
		public string Provider {get;set;}
		public string ProviderUserId {get;set;}
		public int UserId { get; set; }
	}
}
