using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004F RID: 79
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class SyncPrinterCatalogRequestBody
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00003682 File Offset: 0x00001882
		public SyncPrinterCatalogRequestBody()
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00009827 File Offset: 0x00007A27
		public SyncPrinterCatalogRequestBody(Cookie cookie, ArrayOfInt installedNonLeafUpdateIDs, ArrayOfInt printerUpdateIDs)
		{
			this.cookie = cookie;
			this.installedNonLeafUpdateIDs = installedNonLeafUpdateIDs;
			this.printerUpdateIDs = printerUpdateIDs;
		}

		// Token: 0x04000159 RID: 345
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x0400015A RID: 346
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ArrayOfInt installedNonLeafUpdateIDs;

		// Token: 0x0400015B RID: 347
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public ArrayOfInt printerUpdateIDs;
	}
}
