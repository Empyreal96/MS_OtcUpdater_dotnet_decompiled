using System;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools
{
	// Token: 0x02000005 RID: 5
	public class CabException : ApplicationException
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002946 File Offset: 0x00000B46
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000294E File Offset: 0x00000B4E
		public uint CabHResult { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002957 File Offset: 0x00000B57
		public CabException(uint cabHR) : base(CabErrorMapper.Instance.MapError(cabHR))
		{
			this.CabHResult = cabHR;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002971 File Offset: 0x00000B71
		public CabException(uint cabHR, string cabMethod, params string[] args) : base(CabException.FormatMessage(cabHR, cabMethod, args))
		{
			this.CabHResult = cabHR;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002988 File Offset: 0x00000B88
		public CabException(string msg) : base(msg)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002991 File Offset: 0x00000B91
		public CabException(string message, params object[] args) : base(string.Format(message, args))
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000029A0 File Offset: 0x00000BA0
		public CabException(string msg, Exception inner) : base(msg, inner)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029AC File Offset: 0x00000BAC
		private static string FormatMessage(uint cabHR, string cabMethod, params string[] list)
		{
			string text = string.Join(",", list);
			return string.Format("Cab operation failed with hr = 0x{0:X8} [{1}], CAB Operation {2}, Params: {3}", new object[]
			{
				cabHR,
				CabErrorMapper.Instance.MapError(cabHR),
				cabMethod,
				text
			});
		}

		// Token: 0x04000041 RID: 65
		private const string STR_CABERROR = "Cab operation failed with hr = 0x{0:X8} [{1}], CAB Operation {2}, Params: {3}";
	}
}
