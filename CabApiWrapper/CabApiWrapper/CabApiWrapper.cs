using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.WindowsPhone.ImageUpdate.Tools.Common;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools
{
	// Token: 0x02000003 RID: 3
	public class CabApiWrapper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private CabApiWrapper()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static void Extract(string filename, string outputDir)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (string.IsNullOrEmpty(outputDir))
			{
				throw new ArgumentNullException("outputDir");
			}
			outputDir = LongPath.GetFullPathUNC(outputDir);
			if (!LongPathFile.Exists(filename))
			{
				throw new FileNotFoundException(string.Format("CAB file {0} not found", filename), filename);
			}
			uint num = CabApiWrapper.NativeMethods.Cab_Extract(filename, outputDir);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_Extract", new string[]
				{
					filename,
					outputDir
				});
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000002DC
		public static void ExtractOne(string filename, string outputDir, string fileToExtract)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (string.IsNullOrEmpty(outputDir))
			{
				throw new ArgumentNullException("outputDir");
			}
			outputDir = LongPath.GetFullPathUNC(outputDir);
			if (string.IsNullOrEmpty(fileToExtract))
			{
				throw new ArgumentNullException("fileToExtract");
			}
			if (!LongPathFile.Exists(filename))
			{
				throw new FileNotFoundException(string.Format("CAB file {0} not found", filename), filename);
			}
			uint num = CabApiWrapper.NativeMethods.Cab_ExtractOne(filename, outputDir, fileToExtract);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_ExtractOne", new string[]
				{
					filename,
					outputDir,
					fileToExtract
				});
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		public static void ExtractSelected(string filename, string outputDir, IEnumerable<string> filesToExtract)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (string.IsNullOrEmpty(outputDir))
			{
				throw new ArgumentNullException("outputDir");
			}
			outputDir = LongPath.GetFullPathUNC(outputDir);
			if (filesToExtract == null)
			{
				throw new ArgumentNullException("fileToExtract");
			}
			string[] array = filesToExtract.ToArray<string>();
			uint num = (uint)array.Length;
			if (num == 0U)
			{
				throw new ArgumentException("Parameter 'filesToExtract' cannot be empty");
			}
			if (!LongPathFile.Exists(filename))
			{
				throw new FileNotFoundException(string.Format("CAB file {0} not found", filename), filename);
			}
			uint num2 = CabApiWrapper.NativeMethods.Cab_ExtractSelected(filename, outputDir, array, num);
			if (num2 != 0U)
			{
				throw new CabException(num2, "Cab_ExtractSelected", new string[]
				{
					filename,
					outputDir,
					string.Join(",", array)
				});
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002234 File Offset: 0x00000434
		public static void ExtractSelected(string filename, IEnumerable<string> filesToExtract, IEnumerable<string> targetPaths)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (filesToExtract == null)
			{
				throw new ArgumentNullException("fileToExtract");
			}
			if (targetPaths == null)
			{
				throw new ArgumentNullException("targetPaths");
			}
			string[] array = filesToExtract.ToArray<string>();
			string[] array2 = targetPaths.ToArray<string>();
			uint num = (uint)array.Length;
			uint num2 = (uint)array2.Length;
			if (num == 0U)
			{
				throw new ArgumentException("'filesToExtract' parameter cannot be empty");
			}
			if (num2 == 0U)
			{
				throw new ArgumentException("'targetPaths' parameter cannot be empty");
			}
			if (num != num2)
			{
				throw new ArgumentException("'filesToExtract' and 'targetPaths' should have the same number of elements");
			}
			uint num3 = CabApiWrapper.NativeMethods.Cab_ExtractSelected(filename, array, num, array2, num2);
			if (num3 != 0U)
			{
				throw new CabException(num3, "Cab_ExtractSelected", new string[]
				{
					filename,
					string.Join(",", array),
					string.Join(",", array2)
				});
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022FC File Offset: 0x000004FC
		public static string[] GetFileList(string filename, out ulong[] fileSizes)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (!LongPathFile.Exists(filename))
			{
				throw new FileNotFoundException(string.Format("CAB file {0} not found", filename), filename);
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			uint num = 0U;
			string[] array3;
			try
			{
				uint num2 = CabApiWrapper.NativeMethods.Cab_GetFileSizeList(filename, out zero, out zero2, out num);
				if (num2 != 0U)
				{
					throw new CabException(num2, "Cab_GetFileSizeList", new string[]
					{
						filename
					});
				}
				fileSizes = new ulong[num];
				long[] array = new long[num];
				Marshal.Copy(zero2, array, 0, (int)num);
				fileSizes = (ulong[])array;
				IntPtr[] array2 = new IntPtr[num];
				Marshal.Copy(zero, array2, 0, (int)num);
				array3 = new string[num];
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num))
				{
					array3[num3] = Marshal.PtrToStringUni(array2[num3]);
					num3++;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero && num > 0U)
				{
					CabApiWrapper.NativeMethods.Cab_FreeFileSizeList(zero, zero2, num);
				}
			}
			return array3;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002400 File Offset: 0x00000600
		public static string[] GetFileList(string filename)
		{
			ulong[] array;
			return CabApiWrapper.GetFileList(filename, out array);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002418 File Offset: 0x00000618
		public static bool IsCabinet(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (!LongPathFile.Exists(filename))
			{
				throw new FileNotFoundException(string.Format("CAB file {0} not found", filename), filename);
			}
			bool result;
			uint num = CabApiWrapper.NativeMethods.Cab_CheckIsCabinet(filename, out result);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_CheckIsCabinet", new string[]
				{
					filename
				});
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002480 File Offset: 0x00000680
		public static void CreateCab(string filename, string rootDirectory, string tempWorkingFolder, string filterToSelectFiles, CompressionType compressionType = CompressionType.None)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			filename = LongPath.GetFullPathUNC(filename);
			if (string.IsNullOrEmpty(rootDirectory))
			{
				throw new ArgumentNullException("rootDirectory");
			}
			rootDirectory = LongPath.GetFullPathUNC(rootDirectory);
			if (string.IsNullOrEmpty(tempWorkingFolder))
			{
				throw new ArgumentNullException("tempWorkingFolder");
			}
			tempWorkingFolder = LongPath.GetFullPathUNC(tempWorkingFolder);
			if (!LongPathDirectory.Exists(rootDirectory))
			{
				throw new DirectoryNotFoundException(string.Format("'rootDirectory' folder {0} does not exist", rootDirectory));
			}
			if (!LongPathDirectory.Exists(tempWorkingFolder))
			{
				throw new DirectoryNotFoundException(string.Format("'tempWorkingFolder' folder {0} does not exist", tempWorkingFolder));
			}
			uint num = CabApiWrapper.NativeMethods.Cab_CreateCab(filename, rootDirectory, tempWorkingFolder, filterToSelectFiles, compressionType);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_CreateCab", new string[]
				{
					filename,
					rootDirectory,
					tempWorkingFolder,
					filterToSelectFiles
				});
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002544 File Offset: 0x00000744
		public static void CreateCabSelected(string filename, string[] files, string tempWorkingFolder, string prefixToTrim, CompressionType compressionType = CompressionType.None)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (files == null)
			{
				throw new ArgumentNullException("files");
			}
			if (string.IsNullOrEmpty(tempWorkingFolder))
			{
				throw new ArgumentNullException("tempWorkingFolder");
			}
			if (files.Length == 0)
			{
				throw new ArgumentException("'files' parameter cannot be empty");
			}
			if (!LongPathDirectory.Exists(tempWorkingFolder))
			{
				throw new DirectoryNotFoundException(string.Format("'tempWorkingFolder' folder {0} does not exist", tempWorkingFolder));
			}
			string[] array = (from x in files
			where !LongPathFile.Exists(x)
			select x).ToArray<string>();
			if (array.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("Error when adding files to cab file '{0}'. The following files specified in 'files' don't exist:", filename);
				stringBuilder.AppendLine();
				foreach (string arg in array)
				{
					stringBuilder.AppendFormat("\t{0}", arg);
					stringBuilder.AppendLine();
				}
				throw new FileNotFoundException(stringBuilder.ToString());
			}
			uint num = CabApiWrapper.NativeMethods.Cab_CreateCabSelected(filename, files, (uint)files.Length, null, 0U, tempWorkingFolder, prefixToTrim, compressionType);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_CreateCabSelected", new string[]
				{
					filename,
					tempWorkingFolder,
					prefixToTrim
				});
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002664 File Offset: 0x00000864
		public static void CreateCabSelected(string filename, string[] sourceFiles, string[] targetFiles, string tempWorkingFolder, CompressionType compressionType = CompressionType.None)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (sourceFiles == null)
			{
				throw new ArgumentNullException("sourceFiles");
			}
			if (targetFiles == null)
			{
				throw new ArgumentNullException("targetFiles");
			}
			if (string.IsNullOrEmpty(tempWorkingFolder))
			{
				throw new ArgumentNullException("tempWorkingFolder");
			}
			if (sourceFiles.Length == 0)
			{
				throw new ArgumentException("'sourceFiles' parameter cannot be empty");
			}
			if (targetFiles.Length == 0)
			{
				throw new ArgumentException("'targetFiles' parameter cannot be empty");
			}
			if (sourceFiles.Length != targetFiles.Length)
			{
				throw new ArgumentException("'sourceFiles' and 'targetFiles' should have the same number of elements");
			}
			if (!LongPathDirectory.Exists(tempWorkingFolder))
			{
				throw new DirectoryNotFoundException(string.Format("'tempWorkingFolder' folder {0} does not exist", tempWorkingFolder));
			}
			string[] array = (from x in sourceFiles
			where !LongPathFile.Exists(x)
			select x).ToArray<string>();
			if (array.Length != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("Error when adding files to cab file '{0}'. The following files specified in 'sourceFiles' don't exist:", filename);
				stringBuilder.AppendLine();
				foreach (string arg in array)
				{
					stringBuilder.AppendFormat("\t{0}", arg);
					stringBuilder.AppendLine();
				}
				throw new FileNotFoundException(stringBuilder.ToString());
			}
			uint num = CabApiWrapper.NativeMethods.Cab_CreateCabSelected(filename, sourceFiles, (uint)sourceFiles.Length, targetFiles, (uint)targetFiles.Length, tempWorkingFolder, null, compressionType);
			if (num != 0U)
			{
				throw new CabException(num, "Cab_CreateCabSelected", new string[]
				{
					filename,
					tempWorkingFolder
				});
			}
		}

		// Token: 0x04000006 RID: 6
		private const string STR_COMMA = ",";

		// Token: 0x02000006 RID: 6
		internal sealed class NativeMethods
		{
			// Token: 0x06000018 RID: 24 RVA: 0x00002050 File Offset: 0x00000250
			private NativeMethods()
			{
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000019 RID: 25 RVA: 0x000029F4 File Offset: 0x00000BF4
			public static string CAB_API_NAME
			{
				get
				{
					return "CabApi.dll";
				}
			}

			// Token: 0x0600001A RID: 26
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_Extract(string filename, string outputDir);

			// Token: 0x0600001B RID: 27
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_ExtractOne(string filename, string outputDir, string fileToExtract);

			// Token: 0x0600001C RID: 28
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_ExtractSelected(string filename, string outputDir, string[] filesToExtract, uint cFilesToExtract);

			// Token: 0x0600001D RID: 29
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, EntryPoint = "Cab_ExtractSelectedToTarget", SetLastError = true)]
			public static extern uint Cab_ExtractSelected(string filename, string[] filesToExtract, uint cFilesToExtract, string[] targetPaths, uint cTargetPaths);

			// Token: 0x0600001E RID: 30
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_GetFileSizeList(string filename, out IntPtr fileList, out IntPtr sizeList, out uint cFileList);

			// Token: 0x0600001F RID: 31
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_FreeFileSizeList(IntPtr fileList, IntPtr sizeList, uint cFileList);

			// Token: 0x06000020 RID: 32
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_GetFileList(string filename, out IntPtr fileList, out uint cFileList);

			// Token: 0x06000021 RID: 33
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_FreeFileList(IntPtr fileList, uint cFileList);

			// Token: 0x06000022 RID: 34
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_CheckIsCabinet(string filename, out bool isCabinet);

			// Token: 0x06000023 RID: 35
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_CreateCab(string filename, string rootDirectory, string tempWorkingFolder, string filterToSelectFiles, CompressionType compressionType);

			// Token: 0x06000024 RID: 36
			[DllImport("CabApi.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, SetLastError = true)]
			public static extern uint Cab_CreateCabSelected(string filename, string[] files, uint cFiles, string[] targetfiles, uint cTargetFiles, string tempWorkingFolder, string prefixToTrim, CompressionType compressionType);

			// Token: 0x04000042 RID: 66
			private const string STRING_CABAPI_DLL = "CabApi.dll";

			// Token: 0x04000043 RID: 67
			private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

			// Token: 0x04000044 RID: 68
			private const bool SET_LAST_ERROR = true;

			// Token: 0x04000045 RID: 69
			private const CharSet CHAR_SET = CharSet.Auto;
		}
	}
}
