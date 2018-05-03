using System.Runtime.Serialization;

namespace Fenix
{
    /// <summary>Výsledek akce procedury.</summary>
    [DataContract(Namespace = Bc.AppNamespace)]
    public class ProcedureResult
    {
        /// <summary>Chyba</summary>
        [DataMember]
        public string Error { set; get; }

        /// <summary>
        /// Identita
        /// </summary>
        [DataMember]
        public string Identity { set; get; }

        /// <summary>
        /// Duplicita
        /// </summary>
        [DataMember]
        public string Duplicity { set; get; }

        /// <summary>
        /// Popis statusu
        /// </summary>
        [DataMember]
        public string StatusDesc { set; get; }
    }
}
