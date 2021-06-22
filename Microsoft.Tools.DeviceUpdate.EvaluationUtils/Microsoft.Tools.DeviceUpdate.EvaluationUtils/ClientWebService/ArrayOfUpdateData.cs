using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000030 RID: 48
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[CollectionDataContract(Name = "ArrayOfUpdateData", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", ItemName = "Update")]
	[Serializable]
	public class ArrayOfUpdateData : List<UpdateData>
	{
	}
}
