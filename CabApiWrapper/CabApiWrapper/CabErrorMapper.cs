using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools
{
	// Token: 0x02000004 RID: 4
	public class CabErrorMapper
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000027B4 File Offset: 0x000009B4
		private CabErrorMapper()
		{
			Dictionary<string, uint> dictionary = new Dictionary<string, uint>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				if (fieldInfo.Name.StartsWith("E_CABAPI", StringComparison.OrdinalIgnoreCase))
				{
					dictionary[fieldInfo.Name] = (uint)fieldInfo.GetValue(this);
				}
				else if (fieldInfo.Name.StartsWith("STR_E_CABAPI", StringComparison.OrdinalIgnoreCase))
				{
					dictionary2[fieldInfo.Name] = (fieldInfo.GetValue(this) as string);
				}
			}
			foreach (string text in dictionary.Keys)
			{
				string key = "STR_" + text;
				if (dictionary2.ContainsKey(key))
				{
					this._map[dictionary[text]] = dictionary2[key];
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static CabErrorMapper Instance
		{
			get
			{
				return CabErrorMapper._instance.Value;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028E0 File Offset: 0x00000AE0
		public string MapError(uint hr)
		{
			string result = string.Empty;
			if (this._map != null && this._map.ContainsKey(hr))
			{
				result = this._map[hr];
			}
			else
			{
				result = string.Format("CAB operation failed: Unknown error.", hr);
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		private static readonly Lazy<CabErrorMapper> _instance = new Lazy<CabErrorMapper>(() => new CabErrorMapper());

		// Token: 0x04000008 RID: 8
		private const uint CABAPI_ERR_BASE = 2149089984U;

		// Token: 0x04000009 RID: 9
		private const uint CABAPI_ERR_FCI_BASE = 2149090000U;

		// Token: 0x0400000A RID: 10
		private const uint CABAPI_ERR_FDI_BASE = 2149090016U;

		// Token: 0x0400000B RID: 11
		public const uint E_CABAPI_NOT_CABINET = 2149089985U;

		// Token: 0x0400000C RID: 12
		public const string STR_E_CABAPI_NOT_CABINET = "Specified file is not a valid cabinet.";

		// Token: 0x0400000D RID: 13
		public const uint E_CABAPI_UNKNOWN_FILE = 2149089986U;

		// Token: 0x0400000E RID: 14
		public const string STR_E_CABAPI_UNKNOWN_FILE = "CAB extraction failed: One or more files in extraction list not found in cabinet.";

		// Token: 0x0400000F RID: 15
		public const uint E_CABAPI_FCI_OPEN_SRC = 2149090001U;

		// Token: 0x04000010 RID: 16
		public const string STR_E_CABAPI_FCI_OPEN_SRC = "CAB creation failed: Could not open source file.";

		// Token: 0x04000011 RID: 17
		public const uint E_CABAPI_FCI_READ_SRC = 2149090002U;

		// Token: 0x04000012 RID: 18
		public const string STR_E_CABAPI_FCI_READ_SRC = "CAB creation failed: Could not read source file.";

		// Token: 0x04000013 RID: 19
		public const uint E_CABAPI_FCI_ALLOC_FAIL = 2149090003U;

		// Token: 0x04000014 RID: 20
		public const string STR_E_CABAPI_FCI_ALLOC_FAIL = "CAB creation failed: FCI failed to allocate memory.";

		// Token: 0x04000015 RID: 21
		public const uint E_CABAPI_FCI_TEMP_FILE = 2149090004U;

		// Token: 0x04000016 RID: 22
		public const string STR_E_CABAPI_FCI_TEMP_FILE = "CAB creation failed: FCI failed to create temporary file.";

		// Token: 0x04000017 RID: 23
		public const uint E_CABAPI_FCI_BAD_COMPR_TYPE = 2149090005U;

		// Token: 0x04000018 RID: 24
		public const string STR_E_CABAPI_FCI_BAD_COMPR_TYPE = "CAB creation failed: Unknown compression type.";

		// Token: 0x04000019 RID: 25
		public const uint E_CABAPI_FCI_CAB_FILE = 2149090006U;

		// Token: 0x0400001A RID: 26
		public const string STR_E_CABAPI_FCI_CAB_FILE = "CAB creation failed: FCI failed to create cabinet file.";

		// Token: 0x0400001B RID: 27
		public const uint E_CABAPI_FCI_USER_ABORT = 2149090007U;

		// Token: 0x0400001C RID: 28
		public const string STR_E_CABAPI_FCI_USER_ABORT = "CAB creation failed: FCI aborted on user request.";

		// Token: 0x0400001D RID: 29
		public const uint E_CABAPI_FCI_MCI_FAIL = 2149090008U;

		// Token: 0x0400001E RID: 30
		public const string STR_E_CABAPI_FCI_MCI_FAIL = "CAB creation failed: FCI failed to compress data.";

		// Token: 0x0400001F RID: 31
		public const uint E_CABAPI_FCI_CAB_FORMAT_LIMIT = 2149090009U;

		// Token: 0x04000020 RID: 32
		public const string STR_E_CABAPI_FCI_CAB_FORMAT_LIMIT = "CAB creation failed: Data-size or file-count exceeded CAB format limits.";

		// Token: 0x04000021 RID: 33
		public const uint E_CABAPI_FCI_UNKNOWN = 2149090015U;

		// Token: 0x04000022 RID: 34
		public const string STR_E_CABAPI_FCI_UNKNOWN = "CAB creation failed: Unknown error.";

		// Token: 0x04000023 RID: 35
		public const uint E_CABAPI_FDI_CABINET_NOT_FOUND = 2149090017U;

		// Token: 0x04000024 RID: 36
		public const string STR_E_CABAPI_FDI_CABINET_NOT_FOUND = "CAB extract failed: Specified cabinet file not found.";

		// Token: 0x04000025 RID: 37
		public const uint E_CABAPI_FDI_NOT_A_CABINET = 2149090018U;

		// Token: 0x04000026 RID: 38
		public const string STR_E_CABAPI_FDI_NOT_A_CABINET = "CAB extract failed: Specified file is not a valid cabinet.";

		// Token: 0x04000027 RID: 39
		public const uint E_CABAPI_FDI_UNKNOWN_CABINET_VERSION = 2149090019U;

		// Token: 0x04000028 RID: 40
		public const string STR_E_CABAPI_FDI_UNKNOWN_CABINET_VERSION = "CAB extract failed: Specified cabinet has an unknown cabinet version number.";

		// Token: 0x04000029 RID: 41
		public const uint E_CABAPI_FDI_CORRUPT_CABINET = 2149090020U;

		// Token: 0x0400002A RID: 42
		public const string STR_E_CABAPI_FDI_CORRUPT_CABINET = "CAB extract failed: Specified cabinet is corrupt.";

		// Token: 0x0400002B RID: 43
		public const uint E_CABAPI_FDI_ALLOC_FAIL = 2149090021U;

		// Token: 0x0400002C RID: 44
		public const string STR_E_CABAPI_FDI_ALLOC_FAIL = "CAB extract failed: FDI failed to allocate memory.";

		// Token: 0x0400002D RID: 45
		public const uint E_CABAPI_FDI_BAD_COMPR_TYPE = 2149090022U;

		// Token: 0x0400002E RID: 46
		public const string STR_E_CABAPI_FDI_BAD_COMPR_TYPE = "CAB extract failed: Unknown compression type used in cabinet folder.";

		// Token: 0x0400002F RID: 47
		public const uint E_CABAPI_FDI_MDI_FAIL = 2149090023U;

		// Token: 0x04000030 RID: 48
		public const string STR_E_CABAPI_FDI_MDI_FAIL = "CAB extract failed: FDI failed to decompress data from cabinet file.";

		// Token: 0x04000031 RID: 49
		public const uint E_CABAPI_FDI_TARGET_FILE = 2149090024U;

		// Token: 0x04000032 RID: 50
		public const string STR_E_CABAPI_FDI_TARGET_FILE = "CAB extract failed: Failure writing to target file.";

		// Token: 0x04000033 RID: 51
		public const uint E_CABAPI_FDI_RESERVE_MISMATCH = 2149090025U;

		// Token: 0x04000034 RID: 52
		public const string STR_E_CABAPI_FDI_RESERVE_MISMATCH = "CAB extract failed: The cabinets within a set do not have the same RESERVE sizes.";

		// Token: 0x04000035 RID: 53
		public const uint E_CABAPI_FDI_WRONG_CABINET = 2149090026U;

		// Token: 0x04000036 RID: 54
		public const string STR_E_CABAPI_FDI_WRONG_CABINET = "CAB extract failed: The cabinet returned by fdintNEXT_CABINET is incorrect.";

		// Token: 0x04000037 RID: 55
		public const uint E_CABAPI_FDI_USER_ABORT = 2149090027U;

		// Token: 0x04000038 RID: 56
		public const string STR_E_CABAPI_FDI_USER_ABORT = "CAB extract failed: FDI aborted on user request.";

		// Token: 0x04000039 RID: 57
		public const uint E_CABAPI_FDI_UNKNOWN = 2149090031U;

		// Token: 0x0400003A RID: 58
		public const string STR_E_CABAPI_FDI_UNKNOWN = "CAB extract failed: Unknown error.";

		// Token: 0x0400003B RID: 59
		public const string STR_UNKNOWN_ERROR = "CAB operation failed: Unknown error.";

		// Token: 0x0400003C RID: 60
		private Dictionary<uint, string> _map = new Dictionary<uint, string>();

		// Token: 0x0400003D RID: 61
		private const string STR_CABERROR_PREFIX = "E_CABAPI";

		// Token: 0x0400003E RID: 62
		private const string STR_PREFIX = "STR_";

		// Token: 0x0400003F RID: 63
		private const string STR_CABERRORMSG_PREFIX = "STR_E_CABAPI";
	}
}
