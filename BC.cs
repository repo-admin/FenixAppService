using System;
using System.Configuration;

namespace Fenix
{
	/// <summary>
	/// Třída obsahující konstanty a jiné pomocné hodnoty a metody
	/// </summary>
	internal class Bc
	{
		internal const string AppNamespace = "https://ws8websecapp.upc.cz/FenixAppService";

		/// <summary>
		/// Vrací url na w3.org XML Schema
		/// <value>http://www.w3.org/2001/XMLSchema</value>
		/// </summary>
		internal const string UrlW3OrgSchema = "http://www.w3.org/2001/XMLSchema";

		/// <summary>
		/// Vrací hodnotu konstanty 'OK'
		/// <value>0</value>
		/// </summary>
		internal const int Ok = 0;

        /// <summary>
        /// Vrací hodnotu konstanty 'NOT OK'
        /// <value>-1</value>
        /// </summary>
        internal const int NotOk = -1;
	

		#region Properties

        /// <summary>
        /// Vrací hodnotu 'secret' klíče specifikovaného v aplikačním config souboru, jinak <see cref="string.Empty"/>
        /// </summary>
		internal static string SecretKey
		{
			get { try { return ConfigurationManager.AppSettings["SecretKey"].Trim(); } catch { return String.Empty; } }
		}

        /// <summary>
        /// Vrací hodnotu timeout pro p5ipojen9 do databáze specifikovaného v aplikačním config souboru, anebo '3600'
        /// </summary>
        internal static int DbCommandTimeout
		{
			get { try { return int.Parse(ConfigurationManager.AppSettings["EFCommandTimeout"].Trim()); } catch { return 3600; } }
		}
						
		#endregion
		
		/// <summary>
		/// Vytvoří chybový ProcResult
		/// </summary>
		/// <param name="methodName">Název metody</param>
		/// <param name="exception">Instance vyjímky</param>
		/// <returns></returns>
		internal static ProcResult CreateErrorResult(string methodName, Exception exception)
		{
			ProcResult result = new ProcResult();

			result.ReturnValue = Bc.NotOk;
			result.ReturnMessage = String.Format("{0}{1}{2}", methodName, Environment.NewLine, exception.Message);

			Exception innerExeption = exception.InnerException;
			while (innerExeption != null)
			{
				result.ReturnMessage += result.ReturnMessage + Environment.NewLine + exception.InnerException.Message;
				innerExeption = innerExeption.InnerException;
			}

			return result;
		}

		/// <summary>
		/// Vytvoří chybové hlášení
		/// </summary>
		/// <param name="methodName">Název metody</param>
		/// <param name="exception">Instance vyjímky</param>
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
