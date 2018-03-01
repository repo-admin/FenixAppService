using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenixAppService
{
	/// <summary>
	/// 
	/// </summary>
	internal class BC
	{
		internal const string APP_NAMESPACE = "https://ws8websecapp.upc.cz/FenixAppService";

		/// <summary>
		/// URL na w3.org XML Schema
		/// <value>http://www.w3.org/2001/XMLSchema</value>
		/// </summary>
		internal const string URL_W3_ORG_SCHEMA = "http://www.w3.org/2001/XMLSchema";

		/// <summary>
		/// OK
		/// <value>0</value>
		/// </summary>
		internal const int OK = 0;

		/// <summary>
		/// Not OK
		/// <value>-1</value>
		/// </summary>
		internal const int NOT_OK = -1;

		/// <summary>
		/// do ND se nepodařilo správně zapsat
		/// <value>2</value>
		/// </summary>
		internal const int WRITE_TO_ND_NOT_OK = 2;

		/// <summary>
		/// do ND se podařilo správně zapsat
		/// <value>3</value>
		/// </summary>
		internal const int WRITE_TO_ND_OK = 3;

		internal const string UNKNOWN = "Unknown";

		#region Properties

		internal static string ZczExtRdrConnectionString
		{
			get { try { return ConfigurationManager.ConnectionStrings["ZCZEXT"].ConnectionString; } catch { return String.Empty; } }
		}

		internal static string SecretKey
		{
			get { try { return ConfigurationManager.AppSettings["SecretKey"].Trim(); } catch { return String.Empty; } }
		}
		
		internal static string Login
		{
			get { try { return ConfigurationManager.AppSettings["Login"].Trim(); } catch { return String.Empty; } }
		}

		internal static string Password
		{
			get { try { return ConfigurationManager.AppSettings["Password"].Trim(); } catch { return String.Empty; } }
		}

		internal static string PartnerCode
		{
			get { try { return ConfigurationManager.AppSettings["PartnerCode"].Trim(); } catch { return String.Empty; } }
		}

		internal static string Encoding
		{
			get { try { return ConfigurationManager.AppSettings["Encoding"].Trim(); } catch { return String.Empty; } }
		}

		internal static int NumRowsToSend
		{
			get { try { return int.Parse(ConfigurationManager.AppSettings["NumRowsToSend"].Trim()); } catch { return 50; } }
		}
				
		internal static int EFCommandTimeout
		{
			get { try { return int.Parse(ConfigurationManager.AppSettings["EFCommandTimeout"].Trim()); } catch { return 3600; } }
		}
						
		#endregion
		
		/// <summary>
		/// vytvoří chybový ProcResult
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		internal static ProcResult CreateErrorResult(string methodName, Exception ex)
		{
			ProcResult result = new ProcResult();

			result.ReturnValue = BC.NOT_OK;
			result.ReturnMessage = String.Format("{0}{1}{2}", methodName, Environment.NewLine, ex.Message);

			Exception innerExeption = ex.InnerException;
			while (innerExeption != null)
			{
				result.ReturnMessage += result.ReturnMessage + Environment.NewLine + ex.InnerException.Message;
				innerExeption = innerExeption.InnerException;
			}

			return result;
		}

		/// <summary>
		/// vytvoří chybové hlášení
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="exception"></param>
		/// <returns></returns>
		internal static string CreateErrorMessage(string methodName, Exception exception)
		{
			string errorMessage = String.Format("{0}{1}{2}", methodName, Environment.NewLine, exception.Message);
			if (exception.InnerException != null)
				errorMessage += Environment.NewLine + exception.InnerException.Message;

			return errorMessage;
		}
	}
}
