using System.Runtime.Serialization;

namespace FenixAppService
{
	/// <summary>Autentifikační token.</summary>
	[DataContract(Namespace = BC.APP_NAMESPACE)]
	public class AuthToken
	{
		/// <summary>Hodnota tokenu zakódována v base-64.</summary>
		[DataMember]
		public string Value { get; set; }
	}
}
