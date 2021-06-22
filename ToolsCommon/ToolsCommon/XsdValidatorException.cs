using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public class XsdValidatorException : Exception
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00004FC8 File Offset: 0x000031C8
		public XsdValidatorException()
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00004FD0 File Offset: 0x000031D0
		public XsdValidatorException(string message) : base(message)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00004FD9 File Offset: 0x000031D9
		public XsdValidatorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00004FF2 File Offset: 0x000031F2
		protected XsdValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B49C File Offset: 0x0000969C
		public override string ToString()
		{
			string text = this.Message;
			if (base.InnerException != null)
			{
				text += base.InnerException.ToString();
			}
			return text;
		}
	}
}
