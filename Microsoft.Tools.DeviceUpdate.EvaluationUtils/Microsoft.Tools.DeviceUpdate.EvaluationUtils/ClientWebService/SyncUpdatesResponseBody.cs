using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004D RID: 77
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class SyncUpdatesResponseBody
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00003682 File Offset: 0x00001882
		public SyncUpdatesResponseBody()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009809 File Offset: 0x00007A09
		public SyncUpdatesResponseBody(SyncInfo SyncUpdatesResult)
		{
			this.SyncUpdatesResult = SyncUpdatesResult;
		}

		// Token: 0x04000157 RID: 343
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public SyncInfo SyncUpdatesResult;
	}
}
