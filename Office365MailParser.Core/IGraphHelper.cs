using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Office365MailParser.Core
{
	public interface IGraphHelper
	{
		Task<string> GetATokenForGraph( string tenantId, string clientId, string userName, SecureString password );
		Task<List<Microsoft.Graph.Message>> GetFilteredMail(string accessToken, string subject, DateTime startTime, DateTime endTime );
	}
}
