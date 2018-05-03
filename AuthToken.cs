using System.Runtime.Serialization;

namespace Fenix
{
	/// <summary>Autentifikační token.</summary>
	[DataContract(Namespace = Bc.AppNamespace)]
	public class AuthToken
	{
		/// <summary>Hodnota tokenu zakódována v base-64.</summary>
		[DataMember]
		public string Value { get; set; }
	}
}
