using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;
using FenixHelper.Common;

namespace FenixAppService
{
	/// <summary>Fenix aplikační služba- rozhraní</summary>
	[XmlComments]
	[SoapHeaders]
	[ServiceContract(Namespace = BC.APP_NAMESPACE)]
	public interface IFenixAppSvc
	{
		/// <summary>obecný zápis do logu</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteNew(string type, string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source);

		/// <summary>zápis do logu typu INFO</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteInfo(string message, int zicyzUserId, string source);

		/// <summary>zápis do logu typu WARNING</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteWarning(string message, int zicyzUserId, string source);

		/// <summary>zápis do logu typu ERROR</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteError(string message, int zicyzUserId, string source);

		/// <summary>zápis do logu typu XML</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteXml(string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source);

		/// <summary>zpracování potrvzení recepce</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReceptionConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>zpracování potrvzení kittingu</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult KittingConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>zpracování potrvzení závozu/expedice</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ShipmentConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>zpracování vratek (vrácené zařízení)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReturnedEquipmentProcess(string xmlMessage, int zicyzUserId);

		/// <summary>zpracování vratek (vrácené itemy)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReturnedItemProcess(string xmlMessage, int zicyzUserId);

		/// <summary>zpracování vratek (závoz na repasi CPE)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReturnedShipmentProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Repase - potvrzení naskladnění repasovaného zboží - RefurbishedConfirmation  (message RF1)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult RefurbishedConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zjištění stavu FenixSoapService, FenixAppService a FenixAutomat</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult GetServicesStatuses(int zicyzUserId);

		/// <summary>Delete Message - zrušení zprávy  (message D1)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult DeleteMessageConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Crm objednávka - potvrzení  (message C1)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult CrmOrderConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Crm objednávka - odsouhlasení (message C2)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult CrmOrderApprovalProcess(string xmlMessage, int zicyzUserId);
	}

	/// <summary>Podmínky pro výběr uživatelů.</summary>
	[DataContract(Namespace = BC.APP_NAMESPACE)]
	public class UserCondition
	{
		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public int UserId { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public int TeamId { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public int DepartmentId { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public int SaleChannelId { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public int SalesPointId { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public bool? IsActive { set; get; }

		/// <summary></summary>
		[DataMember(IsRequired = true)]
		public string OrderBy { set; get; }
	}
}
