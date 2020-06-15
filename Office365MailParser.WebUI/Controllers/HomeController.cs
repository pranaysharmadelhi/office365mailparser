using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Office365MailParser.Core;
using Office365MailParser.WebUI.Models;

namespace Office365MailParser.WebUI.Controllers
{
	public class HomeController : Controller
	{
		IGraphHelper _graphHelper;
		public HomeController( IGraphHelper graphHelper )
		{
			_graphHelper = graphHelper;
		}
		public IActionResult Index()
		{
			return View( new List<MailParserResponseDto>() );
		}

		[HttpPost]
		public async Task<IActionResult> Index( string TenantId, string ClientId, string UserName, string Password, string StartTime, string EndTime, string SubjectPattern )
		{
			var securePassword = new SecureString();
			foreach ( char c in Password )
				securePassword.AppendChar( c );
			//Get the UserToken
			string userToken = await _graphHelper.GetATokenForGraph(TenantId, ClientId, UserName, securePassword);

			//Send to Processor
			string url = "https://office365mailparser.azurewebsites.net/api/mailparser";
			MailParserRequestDto requestDto = new MailParserRequestDto()
			{
				EndTime = DateTime.Parse(EndTime),
				GraphToken = userToken,
				StartTime = DateTime.Parse(StartTime),
				SubjectPattern = SubjectPattern
			};
			var request = (HttpWebRequest) WebRequest.Create(url);

			request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			request.Method = "POST";
			request.ContentType = "application/json; charset=UTF-8";
			request.Accept = "application/json";
			using ( var streamWriter = new StreamWriter( request.GetRequestStream() ) )
			{
				streamWriter.Write( JsonConvert.SerializeObject( requestDto ) );
			}

			var response = (HttpWebResponse) request.GetResponse();
			string content = string.Empty;
			using ( var stream = response.GetResponseStream() )
			{
				using ( var sr = new StreamReader( stream ) )
				{
					content = sr.ReadToEnd();
				}
			}
			var messagesFound = JsonConvert.DeserializeObject<List<MailParserResponseDto>>( content );
			return View( messagesFound );
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
		public IActionResult Error()
		{
			return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
		}
	}
}
