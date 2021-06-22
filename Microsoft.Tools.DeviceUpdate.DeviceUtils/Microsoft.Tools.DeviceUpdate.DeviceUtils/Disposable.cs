using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000006 RID: 6
	public class Disposable : IDisposable
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000D2AD File Offset: 0x0000B4AD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.DisposeManaged();
			}
			this.DisposeUnmanaged();
			this.disposed = true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000D2DD File Offset: 0x0000B4DD
		protected virtual void DisposeManaged()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000D2DD File Offset: 0x0000B4DD
		protected virtual void DisposeUnmanaged()
		{
		}

		// Token: 0x0400029D RID: 669
		private bool disposed;
	}
}
