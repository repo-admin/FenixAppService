using System.Runtime.Serialization;

namespace Fenix
{
    /// <summary>Výsledek akce procedury.</summary>
    [DataContract(Namespace = Bc.AppNamespace)]
    public class ProcResult
    {

        /// <summary>Zpráva ve formátu 'Automat Rows : 1  |   DateTime : yyyy-mm-dd hh:mi:ss.mmm'</summary>
        [DataMember]
        public string ReturnMessage { set; get; }

        /// <summary>Návratová hodnota</summary>
        [DataMember(IsRequired = true)]
        public int ReturnValue { set; get; }
    }
}