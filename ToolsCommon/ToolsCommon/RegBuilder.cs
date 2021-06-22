using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x0200004F RID: 79
	public static class RegBuilder
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000A828 File Offset: 0x00008A28
		private static void CheckConflicts(IEnumerable<RegValueInfo> values)
		{
			Dictionary<string, RegValueInfo> dictionary = new Dictionary<string, RegValueInfo>();
			foreach (RegValueInfo regValueInfo in values)
			{
				if (regValueInfo.ValueName != null)
				{
					RegValueInfo regValueInfo2 = null;
					if (dictionary.TryGetValue(regValueInfo.ValueName, out regValueInfo2))
					{
						throw new IUException("Registry conflict discovered: keyName: {0}, valueName: {1}, oldValue: {2}, newValue: {3}", new object[]
						{
							regValueInfo.KeyName,
							regValueInfo.ValueName,
							regValueInfo2.Value,
							regValueInfo.Value
						});
					}
					dictionary.Add(regValueInfo.ValueName, regValueInfo);
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A8CC File Offset: 0x00008ACC
		private static void ConvertRegSz(StringBuilder output, string name, string value)
		{
			RegUtil.RegOutput(output, name, value, false);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		private static void ConvertRegExpandSz(StringBuilder output, string name, string value)
		{
			RegUtil.RegOutput(output, name, value, true);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A8E2 File Offset: 0x00008AE2
		private static void ConvertRegMultiSz(StringBuilder output, string name, string value)
		{
			RegUtil.RegOutput(output, name, value.Split(new char[]
			{
				';'
			}));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A8FC File Offset: 0x00008AFC
		private static void ConvertRegDWord(StringBuilder output, string name, string value)
		{
			uint value2 = 0U;
			if (!uint.TryParse(value, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture.NumberFormat, out value2))
			{
				throw new IUException("Invalid dword string: {0}", new object[]
				{
					value
				});
			}
			RegUtil.RegOutput(output, name, value2);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A944 File Offset: 0x00008B44
		private static void ConvertRegQWord(StringBuilder output, string name, string value)
		{
			ulong value2 = 0UL;
			if (!ulong.TryParse(value, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture.NumberFormat, out value2))
			{
				throw new IUException("Invalid qword string: {0}", new object[]
				{
					value
				});
			}
			RegUtil.RegOutput(output, name, value2);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A98A File Offset: 0x00008B8A
		private static void ConvertRegBinary(StringBuilder output, string name, string value)
		{
			RegUtil.RegOutput(output, name, RegUtil.HexStringToByteArray(value));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A99C File Offset: 0x00008B9C
		private static void ConvertRegHex(StringBuilder output, string name, string value)
		{
			Match match = Regex.Match(value, "^hex\\((?<type>[0-9A-Fa-f]+)\\):(?<value>.*)$");
			if (!match.Success)
			{
				throw new IUException("Invalid value '{0}' for REG_HEX type, shoudl be 'hex(<type>):<binary_values>'", new object[]
				{
					value
				});
			}
			int type = 0;
			if (!int.TryParse(match.Groups["type"].Value, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture.NumberFormat, out type))
			{
				throw new IUException("Invalid hex type '{0}' in REG_HEX value '{1}'", new object[]
				{
					match.Groups["type"].Value,
					value
				});
			}
			string value2 = match.Groups["value"].Value;
			RegUtil.RegOutput(output, name, type, RegUtil.HexStringToByteArray(value2));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AA54 File Offset: 0x00008C54
		private static void WriteValue(RegValueInfo value, StringBuilder regContent)
		{
			switch (value.Type)
			{
			case RegValueType.String:
				RegBuilder.ConvertRegSz(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.ExpandString:
				RegBuilder.ConvertRegExpandSz(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.Binary:
				RegBuilder.ConvertRegBinary(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.DWord:
				RegBuilder.ConvertRegDWord(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.MultiString:
				RegBuilder.ConvertRegMultiSz(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.QWord:
				RegBuilder.ConvertRegQWord(regContent, value.ValueName, value.Value);
				return;
			case RegValueType.Hex:
				RegBuilder.ConvertRegHex(regContent, value.ValueName, value.Value);
				return;
			default:
				throw new IUException("Unknown registry value type '{0}'", new object[]
				{
					value.Type
				});
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AB34 File Offset: 0x00008D34
		private static void WriteKey(string keyName, IEnumerable<RegValueInfo> values, StringBuilder regContent)
		{
			regContent.AppendFormat("[{0}]", keyName);
			regContent.AppendLine();
			foreach (RegValueInfo regValueInfo in values)
			{
				if (regValueInfo.ValueName != null)
				{
					RegBuilder.WriteValue(regValueInfo, regContent);
				}
			}
			regContent.AppendLine();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static void Build(IEnumerable<RegValueInfo> values, string outputFile)
		{
			RegBuilder.Build(values, outputFile, "");
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		public static void Build(IEnumerable<RegValueInfo> values, string outputFile, string headerComment)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Windows Registry Editor Version 5.00");
			if (!string.IsNullOrEmpty(headerComment))
			{
				foreach (string text in headerComment.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
				{
					string text2 = text.TrimStart(new char[]
					{
						' '
					});
					if (text2 != string.Empty && text2[0] == ';')
					{
						stringBuilder.AppendLine(text);
					}
					else
					{
						stringBuilder.AppendLine("; " + text);
					}
				}
				stringBuilder.AppendLine("");
			}
			foreach (IGrouping<string, RegValueInfo> grouping in from x in values
			group x by x.KeyName)
			{
				RegBuilder.CheckConflicts(grouping);
				RegBuilder.WriteKey(grouping.Key, grouping, stringBuilder);
			}
			LongPathFile.WriteAllText(outputFile, stringBuilder.ToString(), Encoding.Unicode);
		}
	}
}
