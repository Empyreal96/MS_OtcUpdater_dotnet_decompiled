using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000018 RID: 24
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class SimpleAuthSoapClient : ClientBase<SimpleAuthSoap>, SimpleAuthSoap
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00007406 File Offset: 0x00005606
		public SimpleAuthSoapClient()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000740E File Offset: 0x0000560E
		public SimpleAuthSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007417 File Offset: 0x00005617
		public SimpleAuthSoapClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007421 File Offset: 0x00005621
		public SimpleAuthSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000742B File Offset: 0x0000562B
		public SimpleAuthSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007435 File Offset: 0x00005635
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetAuthorizationCookieResponse SimpleAuthSoap.GetAuthorizationCookie(GetAuthorizationCookieRequest request)
		{
			return base.Channel.GetAuthorizationCookie(request);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007444 File Offset: 0x00005644
		public AuthorizationCookie GetAuthorizationCookie(string clientId, string targetGroupName, string dnsName)
		{
			return ((SimpleAuthSoap)this).GetAuthorizationCookie(new GetAuthorizationCookieRequest
			{
				Body = new GetAuthorizationCookieRequestBody(),
				Body = 
				{
					clientId = clientId,
					targetGroupName = targetGroupName,
					dnsName = dnsName
				}
			}).Body.GetAuthorizationCookieResult;
		}
	}
}
