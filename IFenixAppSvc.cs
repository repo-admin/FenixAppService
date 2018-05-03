using System.ServiceModel;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace Fenix
{
	/// <summary>Fenix aplikační služba- rozhraní</summary>
	[XmlComments]
	[SoapHeaders]
	[ServiceContract(Namespace = Bc.AppNamespace)]
	public interface IFenixAppSvc
	{
		/// <summary>Obecný zápis do logu</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteNew(string type, string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source);

		/// <summary>Zápis do logu typu 'INFO'</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteInfo(string message, int zicyzUserId, string source);

		/// <summary>Zápis do logu typu WARNING</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteWarning(string message, int zicyzUserId, string source);

		/// <summary>Zápis do logu typu 'ERROR'</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteError(string message, int zicyzUserId, string source);

		/// <summary>Zápis do logu typu 'XML'</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult AppLogWriteXml(string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source);

		/// <summary>Zpracování potrvzení recepce</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReceptionConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zpracování potvrzení kittingu</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult KittingConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zpracování potrvzení závozu/expedice</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ShipmentConfirmationProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zpracování vratek (vrácené zařízení)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReturnedEquipmentProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zpracování vratek (vrácené itemy)</summary>
		[SoapHeader("AuthToken", typeof(AuthToken), Direction = SoapHeaderDirection.In)]
		[SoapHeader("ActionResult", typeof(ActionResult), Direction = SoapHeaderDirection.Out)]
		[OperationContract]
		ProcResult ReturnedItemProcess(string xmlMessage, int zicyzUserId);

		/// <summary>Zpracování vratek (závoz na repasi CPE)</summary>
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
}
