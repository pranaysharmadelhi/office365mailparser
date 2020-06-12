using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office365MailParser.Web.Processor.Models
{
	public class MailParserRequest
	{
		public string GraphToken { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string SubjectPattern { get; set; }
	}
}
