using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000057 RID: 87
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetExtendedUpdateInfoRequestBody
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfoRequestBody()
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000098B4 File Offset: 0x00007AB4
		public GetExtendedUpdateInfoRequestBody(Cookie cookie, ArrayOfInt revisionIDs, XmlUpdateFragmentType[] infoTypes, ArrayOfString locales)
		{
			this.cookie = cookie;
			this.revisionIDs = revisionIDs;
			this.infoTypes = infoTypes;
			this.locales = locales;
		}

		// Token: 0x04000164 RID: 356
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x04000165 RID: 357
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ArrayOfInt revisionIDs;

		// Token: 0x04000166 RID: 358
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public XmlUpdateFragmentType[] infoTypes;

		// Token: 0x04000167 RID: 359
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ArrayOfString locales;
	}
}
