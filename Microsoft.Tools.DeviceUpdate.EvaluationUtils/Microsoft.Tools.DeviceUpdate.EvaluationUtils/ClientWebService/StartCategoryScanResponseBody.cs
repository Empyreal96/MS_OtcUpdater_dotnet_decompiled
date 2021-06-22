using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000049 RID: 73
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class StartCategoryScanResponseBody
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00003682 File Offset: 0x00001882
		public StartCategoryScanResponseBody()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000097BF File Offset: 0x000079BF
		public StartCategoryScanResponseBody(ArrayOfGuid preferredCategoryIds, ArrayOfGuid requestedCategoryIdsInError)
		{
			this.preferredCategoryIds = preferredCategoryIds;
			this.requestedCategoryIdsInError = requestedCategoryIdsInError;
		}

		// Token: 0x04000151 RID: 337
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public ArrayOfGuid preferredCategoryIds;

		// Token: 0x04000152 RID: 338
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ArrayOfGuid requestedCategoryIdsInError;
	}
}
