using System;
using System.Xml;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000010 RID: 16
	public class UpdateXmlNamespaceManager : XmlNamespaceManager
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00007248 File Offset: 0x00005448
		protected UpdateXmlNamespaceManager() : base(new NameTable())
		{
			this.AddNamespace("msus-pub", "http://schemas.microsoft.com/msus/2002/12/Publishing");
			this.AddNamespace("wp-pub", "http://schemas.microsoft.com/windows-phone/2013/05/publishing");
			this.AddNamespace("msus-lar", "http://schemas.microsoft.com/msus/2002/12/LogicalApplicabilityRules");
			this.AddNamespace("msus-mar", "http://schemas.microsoft.com/msus/2002/12/MobileApplicabilityRules");
			this.AddNamespace("msus-bar", "http://schemas.microsoft.com/msus/2002/12/BaseApplicabilityRules");
			this.AddNamespace("wuredir", "http://schemas.microsoft.com/msus/2002/12/wuredir");
		}

		// Token: 0x0400007C RID: 124
		public static UpdateXmlNamespaceManager Instance = new UpdateXmlNamespaceManager();
	}
}
