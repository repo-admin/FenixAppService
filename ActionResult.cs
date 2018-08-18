using System.Runtime.Serialization;

namespace Fenix
{
    /// <summary>Výsledek akce</summary>
    [DataContract(Namespace = Bc.AppNamespace)]
    public class ActionResult
    {
        /// <summary>Popis statusu</summary>
        [DataMember]
        public string StatusDesc { set; get; }

        /// <summary>ID statusu: 0 - nezdar, 1 - úspěch, 2 - neplatný token, 3 - neoprávněný přístup</summary>
        [DataMember]
        public short StatusId { set; get; }
    }
}