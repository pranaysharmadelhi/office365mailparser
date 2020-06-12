using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365MailParser.WebUI.Models
{
	public class MailParserResponseDto
	{
		public string Subject { get; set; }
		public DateTimeOffset? ReceivedDateTime { get; set; }
		public string Sender { get; set; }

	}
}
