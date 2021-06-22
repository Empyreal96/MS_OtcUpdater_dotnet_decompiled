using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000016 RID: 22
	public class PropertyDeviceCollection
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000F410 File Offset: 0x0000D610
		public static void GetProperties(object host, ref SortedDictionary<string, string> properties)
		{
			Type[] interfaces = host.GetType().GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				foreach (PropertyInfo propertyInfo in interfaces[i].GetProperties())
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DevicePropertyAttribute), true);
					for (int k = 0; k < customAttributes.Length; k++)
					{
						string name = ((DevicePropertyAttribute)customAttributes[k]).Name;
						try
						{
							string value = propertyInfo.GetValue(host, null).ToString();
							if (!properties.ContainsKey(name) || string.IsNullOrEmpty(properties[name]))
							{
								properties.Add(name, value);
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		public static void GetPropertyNames(object host, ref SortedDictionary<string, string> properties)
		{
			Type[] interfaces = host.GetType().GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				foreach (PropertyInfo propertyInfo in interfaces[i].GetProperties())
				{
					foreach (DevicePropertyAttribute devicePropertyAttribute in propertyInfo.GetCustomAttributes(typeof(DevicePropertyAttribute), true))
					{
						string name = devicePropertyAttribute.Name;
						properties[devicePropertyAttribute.Name] = propertyInfo.Name;
					}
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000F574 File Offset: 0x0000D774
		public static object GetProperty(object host, string name)
		{
			foreach (Type type in host.GetType().GetInterfaces())
			{
				PropertyInfo deviceProperty = PropertyDeviceCollection.GetDeviceProperty(host, type, name);
				if (null != deviceProperty)
				{
					object value = deviceProperty.GetValue(host, null);
					if (value != null)
					{
						return value;
					}
				}
			}
			return null;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public static string GetPropertyString(object host, string name)
		{
			object property = PropertyDeviceCollection.GetProperty(host, name);
			if (property == null)
			{
				return null;
			}
			return property.ToString();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public static void SetProperty(object host, string name, object value)
		{
			PropertyInfo property = host.GetType().GetProperty(name);
			if (null != property.GetSetMethod())
			{
				property.SetValue(host, value, null);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000F618 File Offset: 0x0000D818
		private static PropertyInfo GetDeviceProperty(object host, Type type, string name)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				foreach (DevicePropertyAttribute devicePropertyAttribute in propertyInfo.GetCustomAttributes(typeof(DevicePropertyAttribute), true))
				{
					if (string.Compare(name, devicePropertyAttribute.Name, true) == 0)
					{
						return propertyInfo;
					}
				}
			}
			return null;
		}
	}
}
