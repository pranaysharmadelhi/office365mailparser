using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Office365MailParser.Core;
using Office365MailParser.Web.Processor.Models;

namespace Office365MailParser.Web.Processor.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	public class MailParserController : ControllerBase
	{
		IGraphHelper _graphHelper;
		public MailParserController( IGraphHelper graphHelper )
		{
			_graphHelper = graphHelper;
		}
		// POST api/mailparser
		[HttpGet]
		public async Task<List<MailParserResponse>> Get(  )
		{
			return new List<MailParserResponse>();
		}

		// POST api/mailparser
		[HttpPost]
		public async Task<List<MailParserResponse>> Post( [FromBody] MailParserRequest request )
		{
			var messages = await _graphHelper.GetFilteredMail( request.GraphToken , request.SubjectPattern, request.StartTime, request.EndTime);
			return messages.Select( x => new MailParserResponse() { ReceivedDateTime = x.ReceivedDateTime, Sender = x.Sender.EmailAddress.Address, Subject = x.Subject } ).ToList();
		}
	}
}
