using System.Runtime.Serialization;

namespace Fenix
{
    /// <summary>Podmínky pro výběr uživatelů.</summary>
    [DataContract(Namespace = Bc.AppNamespace)]
    public class UserCondition
    {
        /// <summary>Identifikátor oddělení</summary>
        [DataMember(IsRequired = true)]
        public int DepartmentId { set; get; }

        /// <summary>Hodnota indikující, je-li podmínka aktivní</summary>
        [DataMember(IsRequired = true)]
        public bool? IsActive { set; get; }

        /// <summary>Výraz pro řazení</summary>
        [DataMember(IsRequired = true)]
        public string OrderBy { set; get; }

        /// <summary>Identifikátor prodejního kanálu</summary>
        [DataMember(IsRequired = true)]
        public int SaleChannelId { set; get; }

        /// <summary>Identifikátor prodejního místa</summary>
        [DataMember(IsRequired = true)]
        public int SalesPointId { set; get; }

        /// <summary>Identifikátor týmu</summary>
        [DataMember(IsRequired = true)]
        public int TeamId { set; get; }

        /// <summary>Identifikátor uživatele</summary>
        [DataMember(IsRequired = true)]
        public int UserId { set; get; }
    }
}