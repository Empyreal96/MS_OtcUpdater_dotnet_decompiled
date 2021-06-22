using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000012 RID: 18
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService", ConfigurationName = "SimpleAuthWebService.SimpleAuthSoap")]
	public interface SimpleAuthSoap
	{
		// Token: 0x0600010D RID: 269
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService/GetAuthorizationCookie", ReplyAction = "*")]
		GetAuthorizationCookieResponse GetAuthorizationCookie(GetAuthorizationCookieRequest request);
	}
}
