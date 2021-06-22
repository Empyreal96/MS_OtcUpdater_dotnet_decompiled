using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x0200004E RID: 78
	public static class RegUtil
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000A459 File Offset: 0x00008659
		private static string QuoteString(string input)
		{
			return "\"" + input.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A489 File Offset: 0x00008689
		private static string NormalizeValueName(string name)
		{
			if (name == "@")
			{
				return "@";
			}
			return RegUtil.QuoteString(name);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A4A4 File Offset: 0x000086A4
		private static byte[] RegStringToBytes(string value)
		{
			return Encoding.Unicode.GetBytes(value);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public static RegValueType RegValueTypeForString(string strType)
		{
			foreach (FieldInfo fieldInfo in typeof(RegValueType).GetFields())
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false);
				if (customAttributes.Length == 1 && strType.Equals(((XmlEnumAttribute)customAttributes[0]).Name, StringComparison.OrdinalIgnoreCase))
				{
					return (RegValueType)fieldInfo.GetRawConstantValue();
				}
			}
			throw new ArgumentException(string.Format("Unknown Registry value type: {0}", strType));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A530 File Offset: 0x00008730
		public static byte[] HexStringToByteArray(string hexString)
		{
			List<byte> list = new List<byte>();
			if (hexString != string.Empty)
			{
				foreach (string s in hexString.Split(new char[]
				{
					','
				}))
				{
					byte item = 0;
					if (!byte.TryParse(s, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture.NumberFormat, out item))
					{
						throw new IUException("Invalid hex string: {0}", new object[]
						{
							hexString
						});
					}
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A5AF File Offset: 0x000087AF
		public static void ByteArrayToRegString(StringBuilder output, byte[] data)
		{
			RegUtil.ByteArrayToRegString(output, data, int.MaxValue);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public static void ByteArrayToRegString(StringBuilder output, byte[] data, int maxOnALine)
		{
			int num = 0;
			int i = data.Length;
			while (i > 0)
			{
				int num2 = Math.Min(i, maxOnALine);
				string text = BitConverter.ToString(data, num, num2);
				text = text.Replace('-', ',');
				output.Append(text);
				num += num2;
				i -= num2;
				if (i > 0)
				{
					output.AppendLine(",\\");
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A618 File Offset: 0x00008818
		public static void RegOutput(StringBuilder output, string name, IEnumerable<string> values)
		{
			string arg = RegUtil.NormalizeValueName(name);
			output.AppendFormat(";Value:{0}", string.Join(";", from x in values
			select x.Replace(";", "\\;")));
			output.AppendLine();
			output.AppendFormat("{0}=hex(7):", arg);
			RegUtil.ByteArrayToRegString(output, RegUtil.RegStringToBytes(string.Join("\0", values) + "\0\0"), RegUtil.BinaryLineLength / 3);
			output.AppendLine();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A6AC File Offset: 0x000088AC
		public static void RegOutput(StringBuilder output, string name, string value, bool expandable)
		{
			string arg = RegUtil.NormalizeValueName(name);
			if (expandable)
			{
				output.AppendFormat(";Value:{0}", value);
				output.AppendLine();
				output.AppendFormat("{0}=hex(2):", arg);
				RegUtil.ByteArrayToRegString(output, RegUtil.RegStringToBytes(value + "\0"), RegUtil.BinaryLineLength / 3);
			}
			else
			{
				output.AppendFormat("{0}={1}", arg, RegUtil.QuoteString(value));
			}
			output.AppendLine();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A720 File Offset: 0x00008920
		public static void RegOutput(StringBuilder output, string name, ulong value)
		{
			string arg = RegUtil.NormalizeValueName(name);
			output.AppendFormat(";Value:0X{0:X16}", value);
			output.AppendLine();
			output.AppendFormat("{0}=hex(b):", arg);
			RegUtil.ByteArrayToRegString(output, BitConverter.GetBytes(value));
			output.AppendLine();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A770 File Offset: 0x00008970
		public static void RegOutput(StringBuilder output, string name, uint value)
		{
			string arg = RegUtil.NormalizeValueName(name);
			output.AppendFormat("{0}=dword:{1:X8}", arg, value);
			output.AppendLine();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public static void RegOutput(StringBuilder output, string name, byte[] value)
		{
			string arg = RegUtil.NormalizeValueName(name);
			output.AppendFormat("{0}=hex:", arg);
			RegUtil.ByteArrayToRegString(output, value.ToArray<byte>(), RegUtil.BinaryLineLength / 3);
			output.AppendLine();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A7DC File Offset: 0x000089DC
		public static void RegOutput(StringBuilder output, string name, int type, byte[] value)
		{
			string arg = RegUtil.NormalizeValueName(name);
			output.AppendFormat("{0}=hex({1:x}):", arg, type);
			RegUtil.ByteArrayToRegString(output, value.ToArray<byte>(), RegUtil.BinaryLineLength / 3);
			output.AppendLine();
		}

		// Token: 0x0400010C RID: 268
		private const string c_strDefaultValueName = "@";

		// Token: 0x0400010D RID: 269
		private const int c_iBinaryStringLengthPerByte = 3;

		// Token: 0x0400010E RID: 270
		public const string c_strRegHeader = "Windows Registry Editor Version 5.00";

		// Token: 0x0400010F RID: 271
		public static int BinaryLineLength = 120;
	}
}
