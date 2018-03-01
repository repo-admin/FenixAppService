using System;
using System.Data.Entity.Core.Objects;
using UPC.Extensions.Convert;
using UPC.Extensions.Enum;
using UPC.Security;
using WCFExtrasPlus.Soap;
using FenixHelper.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace FenixAppService
{	
	/// <summary>
	/// Aplikační služba Fenix
	/// </summary>
	public class FenixAppSvc : IFenixAppSvc
	{
		enum ProcessType
		{
			ReceptionConfirmationProcess,
			KittingConfirmationProcess,
			ShipmentConfirmationProcess,			
			ReturnedEquipmentProcess,
			ReturnedItemProcess,
			ReturnedShipmentProcess,
			RefurbishedConfirmationProcess,
			DeleteMessageConfirmationProcess,
			CrmOrderConfirmationProcess,
			CrmOrderApprovalProcess
		}

		#region jmena SP pro jednotlive typy procesu

		/*		 
public ProcResult ReceptionConfirmationProcess(string xmlMessage, int zicyzUserId)		db.prCMRCins(xmlMessage, retVal, retMsg);
public ProcResult KittingConfirmationProcess(string xmlMessage, int zicyzUserId)  		db.prCMRCKins(xmlMessage, retVal, retMsg);
public ProcResult ShipmentConfirmationProcess(string xmlMessage, int zicyzUserId)  		db.prKiSHCins(xmlMessage, retVal, retMsg);
public ProcResult RefurbishedConfirmationProcess(string xmlMessage, int zicyzUserId)	db.prRORF1ins(xmlMessage, retVal, retMsg);
public ProcResult ReturnedEquipmentProcess(string xmlMessage, int zicyzUserId)  		db.prCMReturn(xmlMessage, retVal, retMsg);
public ProcResult ReturnedItemProcess(string xmlMessage, int zicyzUserId)    			vyjimka
public ProcResult ReturnedShipmentProcess(string xmlMessage, int zicyzUserId)  			vyjimka		 
		 */

		#endregion

		#region Private Variables

		private string appId;
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Zápis do logu aplikace (v případě xml se xml deklarace a root element se zapisují zvlášť)
		/// </summary>
		/// <param name="type"></param>
		/// <param name="message"></param>
		/// <param name="xmlDeclaration"></param>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ProcResult AppLogWriteNew(string type, string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source)
		{
			ProcResult result = new ProcResult();
			result.ReturnValue = BC.NOT_OK;

			if (this.verifyToken() == false)
			{
				return result;
			}

			using (var db = new FenixEntities())
			{
				try
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;
					ObjectParameter retVal = new ObjectParameter("ReturnValue", typeof(int));
					ObjectParameter retMsg = new ObjectParameter("ReturnMessage", typeof(string));
										
					db.prAppLogWriteNew(type, message, xmlDeclaration, xmlMessage, zicyzUserId, source, retVal, retMsg);

					result.ReturnValue = retVal.Value.ToInt32(BC.NOT_OK);
					result.ReturnMessage = retMsg.Value.ToString(String.Empty);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Success.ToShort(), StatusDesc = "OK" });
				}
				catch (Exception ex)
				{
					result = BC.CreateErrorResult(FenixHelper.AppLog.GetMethodName(), ex);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Failure.ToShort(), StatusDesc = ex.Message });
				}
			}

			return result;
		}

		/// <summary>
		/// Zápis do logu aplikace (v případě xml se xml deklarace a root element se zapisují zvlášť)		
		/// </summary>
		/// <param name="type"></param>
		/// <param name="message"></param>
		/// <param name="xmlDeclaration"></param>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		[Obsolete("Použijte metodu AppLogWriteNew")]
		public ProcResult AppLogWrite(string type, string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source)
		{
			return AppLogWriteNew(type, message, xmlDeclaration, xmlMessage, zicyzUserId, source);
		}

		/// <summary>
		/// Zápis do logu aplikace typu INFO
		/// </summary>
		/// <param name="message"></param>
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ProcResult AppLogWriteInfo(string message, int zicyzUserId, string source)
		{
			return AppLogWriteNew("INFO", message, String.Empty, String.Empty, zicyzUserId, source);
		}

		/// <summary>
		/// Zápis do logu aplikace typu WARNING
		/// </summary>
		/// <param name="message"></param>
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ProcResult AppLogWriteWarning(string message, int zicyzUserId, string source)
		{
			return AppLogWriteNew("WARNING", message, String.Empty, String.Empty, zicyzUserId, source);
		}

		/// <summary>
		/// Zápis do logu aplikace typu ERROR
		/// </summary>
		/// <param name="message"></param>
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ProcResult AppLogWriteError(string message, int zicyzUserId, string source)
		{
			return AppLogWriteNew("ERROR", message, String.Empty, String.Empty, zicyzUserId, source);
		}

		/// <summary>
		/// Zápis do logu aplikace typu XML
		/// </summary>
		/// <param name="message"></param>
		/// <param name="xmlDeclaration"></param>
		/// <param name="xmlMessage"></param>		
		/// <param name="zicyzUserId"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		public ProcResult AppLogWriteXml(string message, string xmlDeclaration, string xmlMessage, int zicyzUserId, string source)
		{
			return AppLogWriteNew("XML", message, xmlDeclaration, xmlMessage, zicyzUserId, source);
		}

		/// <summary>
		/// Zpracování potvrzení recepce - ReceptionConfirmation (message R1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult ReceptionConfirmationProcess(string xmlMessage, int zicyzUserId)
		{			
			return doConfirmationProcess(ProcessType.ReceptionConfirmationProcess, xmlMessage, zicyzUserId);
		}

		/// <summary>
		/// Zpracování potvrzení kittingu - KittingConfirmation (message K1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult KittingConfirmationProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.KittingConfirmationProcess, xmlMessage, zicyzUserId);
		}
				
		/// <summary>
		/// Zpracování potvrzení závozu/expedice - ShipmentConfirmation (message S1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult ShipmentConfirmationProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.ShipmentConfirmationProcess, xmlMessage, zicyzUserId);
		}

		/// <summary>
		/// Zpracování vratek(vrácená zařízení) - ReturnedEquipment (message VR1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult ReturnedEquipmentProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.ReturnedEquipmentProcess, xmlMessage, zicyzUserId);
		}

		/// <summary>
		/// Zpracování vratek(vrácené itemy) - ReturnedItem (message VR2)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult ReturnedItemProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.ReturnedItemProcess, xmlMessage, zicyzUserId);
		}
				
		/// <summary>
		/// Zpracování vratek(závoz na repasi CPE) - ReturnedShipment (message VR3)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult ReturnedShipmentProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.ReturnedShipmentProcess, xmlMessage, zicyzUserId);
		}

		/// <summary>
		/// Repase - potvrzení naskladnění repasovaného zboží - RefurbishedConfirmation (message RF1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult RefurbishedConfirmationProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.RefurbishedConfirmationProcess, xmlMessage, zicyzUserId);
		}

		/// <summary>
		/// Zpracování vratek(závoz na repasi CPE) - ReturnedShipment (message VR3)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult DeleteMessageConfirmationProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.DeleteMessageConfirmationProcess, xmlMessage, zicyzUserId);
		}
				
		/// <summary>
		/// Zpracování potvrzení objednávky CRM - CrmOrderConfirmation (message C1)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult CrmOrderConfirmationProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.CrmOrderConfirmationProcess, xmlMessage, zicyzUserId);
		}
				
		/// <summary>
		/// Zpracování odsouhlasení objednávky CRM - CrmOrderApproval (message C2)
		/// </summary>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		public ProcResult CrmOrderApprovalProcess(string xmlMessage, int zicyzUserId)
		{
			return doConfirmationProcess(ProcessType.CrmOrderApprovalProcess, xmlMessage, zicyzUserId);
		}
		

		#endregion

		#region Services Statuses

		/// <summary>
		/// zjištění stavu FenixSoapService, FenixAppService, FenixAutomat a databáze
		/// </summary>
		/// <returns>ProcResult (field ReturnMessage by měl obsahovat string 'Automat Rows : 1  |   DateTime : yyyy-mm-dd hh:mi:ss.mmm')</returns>
		public ProcResult GetServicesStatuses(int zicyzUserId)
		{
			ProcResult result = new ProcResult();
			result.ReturnValue = BC.NOT_OK;

			if (this.verifyToken() == false)
			{
				return result;
			}

			using (var db = new FenixEntities())
			{
				try
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;
					ObjectParameter retVal = new ObjectParameter("ReturnValue", typeof(int));
					ObjectParameter retMsg = new ObjectParameter("ReturnMessage", typeof(string));
										
					db.prGetAutomatStatus(retVal, retMsg);

					result.ReturnValue = retVal.Value.ToInt32(BC.NOT_OK);
					result.ReturnMessage = retMsg.Value.ToString(String.Empty);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Success.ToShort(), StatusDesc = "OK" });
				}
				catch (Exception ex)
				{
					result = BC.CreateErrorResult(FenixHelper.AppLog.GetMethodName(), ex);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Failure.ToShort(), StatusDesc = ex.Message });
				}
			}
			
			return result;
		}

		#endregion

		#region PrivateMethods

		/// <summary>
		/// vlastní zpracování daného typu procesu (ReceptionConfirmation, KittingConfirmation ...)
		/// </summary>
		/// <param name="processType"></param>
		/// <param name="xmlMessage"></param>
		/// <param name="zicyzUserId"></param>
		/// <returns></returns>
		private ProcResult doConfirmationProcess(ProcessType processType, string xmlMessage, int zicyzUserId)
		{
			ProcResult result = new ProcResult();
			result.ReturnValue = BC.NOT_OK;

			if (this.verifyToken() == false)
			{
				return result;
			}
			
			using (var db = new FenixEntities())
			{
				try
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;					
					db.Database.CommandTimeout = BC.EFCommandTimeout;

					var par1Parameter = new SqlParameter();
					par1Parameter.ParameterName = "@par1";
					par1Parameter.Value = xmlMessage;
					par1Parameter.Direction = System.Data.ParameterDirection.Input;
					par1Parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
					par1Parameter.Size = int.MaxValue - 1;

					var returnValue = new SqlParameter();
					returnValue.ParameterName = "@ReturnValue";
					returnValue.Direction = System.Data.ParameterDirection.InputOutput;
					returnValue.SqlDbType = System.Data.SqlDbType.Int;
					returnValue.Value = 0;

					var returnMessage = new SqlParameter();
					returnMessage.ParameterName = "@ReturnMessage";
					returnMessage.Direction = System.Data.ParameterDirection.InputOutput;
					returnMessage.SqlDbType = System.Data.SqlDbType.NVarChar;
					returnMessage.Size = 2048;
					returnMessage.Value = "";
					
					// diky problémům v EF 6 (rollback ve SP) je nutno SP volat tímto způsobem
					var res = db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, this.createSqlCommand(processType), par1Parameter, returnValue, returnMessage);

					result.ReturnValue = returnValue.Value.ToInt32(BC.NOT_OK);
					result.ReturnMessage = returnMessage.Value.ToString(String.Empty);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Success.ToShort(), StatusDesc = "OK" });
				}
				catch (Exception ex)
				{
					result = BC.CreateErrorResult(FenixHelper.AppLog.GetMethodName(), ex);
					SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.Failure.ToShort(), StatusDesc = ex.Message });
				}
			}

			logResult(result, FenixHelper.AppLog.GetMethodName(), zicyzUserId);
			return result;
		}

		/// <summary>
		/// vytvoření sql commandu pro daný typ procesu (ReceptionConfirmation, KittingConfirmation ...)
		/// </summary>
		/// <param name="processType"></param>
		/// <returns></returns>
		private string createSqlCommand(ProcessType processType)
		{
			switch (processType)
			{
				case ProcessType.ReceptionConfirmationProcess:
					return "[dbo].[prCMRCins] @par1, @ReturnValue OUT, @ReturnMessage OUT";

				case ProcessType.KittingConfirmationProcess:
					return "[dbo].[prCMRCKins] @par1, @ReturnValue OUT, @ReturnMessage OUT";					

				case ProcessType.ShipmentConfirmationProcess:
					return "[dbo].[prKiSHCins] @par1, @ReturnValue OUT, @ReturnMessage OUT";				

				case ProcessType.ReturnedEquipmentProcess:
					return "[dbo].[prCMReturn] @par1, @ReturnValue OUT, @ReturnMessage OUT";
										
				case ProcessType.ReturnedItemProcess:					
					return "[dbo].[prCMReturnedItem] @par1, @ReturnValue OUT, @ReturnMessage OUT";

				case ProcessType.ReturnedShipmentProcess:
					return "[dbo].[prCMReturnedShipment] @par1, @ReturnValue OUT, @ReturnMessage OUT";

				case ProcessType.RefurbishedConfirmationProcess:
					return "[dbo].[prRORF1ins] @par1, @ReturnValue OUT, @ReturnMessage OUT";				

				case ProcessType.DeleteMessageConfirmationProcess:
					return "[dbo].[prCMDeleteMessage] @par1, @ReturnValue OUT, @ReturnMessage OUT";

				case ProcessType.CrmOrderConfirmationProcess:
					return "[dbo].[prCrmOrderC1ins]  @par1, @ReturnValue OUT, @ReturnMessage OUT";

				case ProcessType.CrmOrderApprovalProcess:
					return "[dbo].[prCrmOrderC2ins]  @par1, @ReturnValue OUT, @ReturnMessage OUT";

				default:
					throw new Exception(String.Format("Použit nepovolený process type [{0}]", processType.ToString()));					
			}
		}

		/// <summary>
		/// zápis výsledku operace do tabulky logu
		/// <para>token se neverifikuje - předpokládá se verifikace ve volající metodě!</para>
		/// <para>jde o pomocnou metodu - případná chyba se ignoruje</para>
		/// </summary>
		/// <param name="result"></param>
		/// <param name="source"></param>
		/// <param name="zicyzUserId"></param>
		private void logResult(ProcResult result, string source, int zicyzUserId)
		{
			try
			{
				string logType = result.ReturnValue == BC.OK ? "INFO" : "ERROR";
				string message = String.Format("MethodName [{0}] result.ReturnValue [{1}] result.ReturnMessage [{2}]", source, result.ReturnValue, result.ReturnMessage);
								
				using (var db = new FenixEntities())
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;
					ObjectParameter retVal = new ObjectParameter("ReturnValue", typeof(int));
					ObjectParameter retMsg = new ObjectParameter("ReturnMessage", typeof(string));

					db.prAppLogWriteNew(logType, message, String.Empty, String.Empty, zicyzUserId, source, retVal, retMsg);
				}
			}
			catch
			{
				;
			}
		}
				
		/// <summary>
		/// Ověření tokenu
		/// </summary>
		/// <returns></returns>
		private bool verifyToken()
		{
			this.appId = String.Empty;

			// Získání tokenu z hlavičky
			AuthToken authToken = SoapHeaderHelper<AuthToken>.GetInputHeader("AuthToken");
			string token = (authToken == null ? String.Empty : authToken.Value);

			if (String.IsNullOrWhiteSpace(token))
			{
				SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.InvalidToken.ToShort(), StatusDesc = "Invalid token" });
				return false; ;
			}

			// Ověření platnosti tokenu
			Token tkn = new Token(BC.SecretKey, "AuthToken", token);
			TokenStatus stat = tkn.Verify();
			this.appId = tkn.Id;

			if (stat != TokenStatus.Valid)
			{
				SoapHeaderHelper<ActionResult>.SetOutputHeader("ActionResult", new ActionResult() { StatusId = ActionStatus.InvalidToken.ToShort(), StatusDesc = "Invalid token" });
				return false; ;
			}

			return true;
		}

		#endregion
	}
}
