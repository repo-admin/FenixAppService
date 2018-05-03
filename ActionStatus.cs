using System.Runtime.Serialization;

namespace Fenix
{
    /// <summary>Status akce</summary>
    [DataContract(Namespace = Bc.AppNamespace)]
    public enum ActionStatus : short
    {
        /// <summary>Nezdar</summary>
        [DataMember]
        Failure = 0,

        /// <summary>Úspěch</summary>
        [DataMember]
        Success = 1,

        /// <summary>Neplatný token</summary>
        [DataMember]
        InvalidToken = 2,

        /// <summary>Neoprávněný přístup</summary>
        [DataMember]
        UnauthorizedAccess = 3
    }
}