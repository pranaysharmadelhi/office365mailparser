using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365MailParser.Web.Processor.Models
{
	public class MailParserResponse
	{
		public string Subject { get; set; }
		public DateTimeOffset? ReceivedDateTime { get; set; }
		public string Sender { get; set; }

	}
}
