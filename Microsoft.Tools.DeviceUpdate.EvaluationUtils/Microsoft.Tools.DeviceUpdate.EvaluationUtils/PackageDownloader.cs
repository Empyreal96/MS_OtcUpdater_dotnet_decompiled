using System;
using System.IO;
using System.Security.Cryptography;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000B RID: 11
	public class PackageDownloader
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004B20 File Offset: 0x00002D20
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004B2D File Offset: 0x00002D2D
		public string DownloadPath
		{
			get
			{
				return this.httpDownloader.DownloadPath;
			}
			set
			{
				this.httpDownloader.DownloadPath = value;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004B3C File Offset: 0x00002D3C
		public bool GetHashFileNames()
		{
			bool result = true;
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_DISABLE_FILENAME_HASH");
			if (environmentVariable != null)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004B5C File Offset: 0x00002D5C
		private PackageDownloader()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004B90 File Offset: 0x00002D90
		public void DownloadUpdate(DownloadInfo downloadInfo)
		{
			object obj = this.mutex;
			lock (obj)
			{
				string path = Path.Combine(this.DownloadPath, downloadInfo.Name);
				if (downloadInfo.SHA256Hash != null && this.GetHashFileNames())
				{
					byte[] value = Convert.FromBase64String(downloadInfo.SHA256Hash);
					path = Path.Combine(this.DownloadPath, BitConverter.ToString(value).Replace("-", string.Empty));
				}
				bool flag2 = true;
				if (File.Exists(path))
				{
					try
					{
						using (this.GetStream(downloadInfo))
						{
						}
						flag2 = false;
					}
					catch
					{
						File.Delete(path);
					}
				}
				if (flag2)
				{
					string relativePath = downloadInfo.Name;
					if (downloadInfo.SHA256Hash != null && this.GetHashFileNames())
					{
						relativePath = BitConverter.ToString(Convert.FromBase64String(downloadInfo.SHA256Hash)).Replace("-", string.Empty);
					}
					this.httpDownloader.Download(downloadInfo.Url, relativePath);
					using (this.GetStream(downloadInfo))
					{
					}
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public string GetDownloadPath(DownloadInfo downloadInfo)
		{
			try
			{
				using (FileStream stream = this.GetStream(downloadInfo))
				{
					if (stream != null)
					{
						stream.Close();
						string result = Path.Combine(this.DownloadPath, downloadInfo.Name);
						if (downloadInfo.SHA256Hash != null && this.GetHashFileNames())
						{
							byte[] value = Convert.FromBase64String(downloadInfo.SHA256Hash);
							result = Path.Combine(this.DownloadPath, BitConverter.ToString(value).Replace("-", string.Empty));
						}
						return result;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004D78 File Offset: 0x00002F78
		public FileStream GetStream(DownloadInfo downloadInfo)
		{
			object obj = this.mutex;
			FileStream result;
			lock (obj)
			{
				FileStream fileStream = null;
				try
				{
					string text = Path.Combine(this.DownloadPath, downloadInfo.Name);
					if (downloadInfo.SHA256Hash != null && this.GetHashFileNames())
					{
						byte[] value = Convert.FromBase64String(downloadInfo.SHA256Hash);
						text = Path.Combine(this.DownloadPath, BitConverter.ToString(value).Replace("-", string.Empty));
					}
					if (new FileInfo(text).Length != downloadInfo.Size)
					{
						throw new Exception(string.Format("{0}: size mismatch", downloadInfo.Name));
					}
					string b;
					HashAlgorithm hashAlgorithm;
					if (!string.IsNullOrEmpty(downloadInfo.SHA256Hash))
					{
						b = downloadInfo.SHA256Hash;
						hashAlgorithm = this.sha256;
					}
					else
					{
						b = downloadInfo.SHA1Hash;
						hashAlgorithm = this.sha1;
					}
					fileStream = File.OpenRead(text);
					if (Convert.ToBase64String(hashAlgorithm.ComputeHash(fileStream)) != b)
					{
						throw new Exception(string.Format("{0}: hash mismatch", downloadInfo.Name));
					}
					fileStream.Position = 0L;
					result = fileStream;
				}
				catch
				{
					if (fileStream != null)
					{
						fileStream.Close();
					}
					throw;
				}
			}
			return result;
		}

		// Token: 0x04000045 RID: 69
		private SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

		// Token: 0x04000046 RID: 70
		private SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();

		// Token: 0x04000047 RID: 71
		private HttpDownloader httpDownloader = new HttpDownloader();

		// Token: 0x04000048 RID: 72
		private object mutex = new object();

		// Token: 0x04000049 RID: 73
		public static PackageDownloader Instance = new PackageDownloader();
	}
}
