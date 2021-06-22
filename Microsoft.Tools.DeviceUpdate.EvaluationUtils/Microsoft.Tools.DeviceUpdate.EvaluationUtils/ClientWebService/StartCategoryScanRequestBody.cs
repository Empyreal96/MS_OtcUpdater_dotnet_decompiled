using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000047 RID: 71
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class StartCategoryScanRequestBody
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00003682 File Offset: 0x00001882
		public StartCategoryScanRequestBody()
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000097A1 File Offset: 0x000079A1
		public StartCategoryScanRequestBody(CategoryRelationship[] requestedCategories)
		{
			this.requestedCategories = requestedCategories;
		}

		// Token: 0x0400014F RID: 335
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public CategoryRelationship[] requestedCategories;
	}
}
