using System.Runtime.Serialization;

namespace FenixAppService
{
	/// <summary>Výsledek akce.</summary>
	[DataContract(Namespace = BC.APP_NAMESPACE)]
	public class ActionResult
	{
		/// <summary>ID statusu: 0 - nezdar, 1 - úspěch, 2 - neplatný token, 3 - neoprávněný přístup.</summary>
		[DataMember]
		public short StatusId { set; get; }

		/// <summary>Popis statusu.</summary>
		[DataMember]
		public string StatusDesc { set; get; }
	}

	/// <summary>Status akce.</summary>
	[DataContract(Namespace = BC.APP_NAMESPACE)]
	public enum ActionStatus : short
	{
		/// <summary>Nezdar.</summary>
		[DataMember]
		Failure = 0,
		/// <summary>Úspěch.</summary>
		[DataMember]
		Success = 1,
		/// <summary>Neplatný token.</summary>
		[DataMember]
		InvalidToken = 2,
		/// <summary>Neoprávněný přístup.</summary>
		[DataMember]
		UnauthorizedAccess = 3
	}
}
