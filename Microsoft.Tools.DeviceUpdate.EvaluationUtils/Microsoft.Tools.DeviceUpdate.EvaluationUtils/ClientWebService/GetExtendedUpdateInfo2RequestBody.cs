using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005B RID: 91
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetExtendedUpdateInfo2RequestBody
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfo2RequestBody()
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009906 File Offset: 0x00007B06
		public GetExtendedUpdateInfo2RequestBody(Cookie cookie, UpdateIdentity[] updateIDs, XmlUpdateFragmentType[] infoTypes, ArrayOfString locales)
		{
			this.cookie = cookie;
			this.updateIDs = updateIDs;
			this.infoTypes = infoTypes;
			this.locales = locales;
		}

		// Token: 0x0400016B RID: 363
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x0400016C RID: 364
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public UpdateIdentity[] updateIDs;

		// Token: 0x0400016D RID: 365
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public XmlUpdateFragmentType[] infoTypes;

		// Token: 0x0400016E RID: 366
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ArrayOfString locales;
	}
}
