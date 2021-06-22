using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000006 RID: 6
	public class Evaluator : IUpdateEvaluator
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600006E RID: 110 RVA: 0x000037B4 File Offset: 0x000019B4
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x000037EC File Offset: 0x000019EC
		public event Evaluator.MessageCallback ProgressEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000070 RID: 112 RVA: 0x00003824 File Offset: 0x00001A24
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x0000385C File Offset: 0x00001A5C
		public event Evaluator.MessageCallback NormalMessageEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00003894 File Offset: 0x00001A94
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x000038CC File Offset: 0x00001ACC
		public event Evaluator.MessageCallback WarningMessageEvent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000074 RID: 116 RVA: 0x00003904 File Offset: 0x00001B04
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x0000393C File Offset: 0x00001B3C
		public event Evaluator.MessageCallback ErrorMessageEvent;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000076 RID: 118 RVA: 0x00003974 File Offset: 0x00001B74
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x000039AC File Offset: 0x00001BAC
		public event Evaluator.MessageCallback LogMessageEvent;

		// Token: 0x06000078 RID: 120 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static bool GetDebugLoggingEnabled()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_DEBUG_LOGGING");
			bool result = false;
			if (environmentVariable != null)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003A04 File Offset: 0x00001C04
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003A0C File Offset: 0x00001C0C
		public IDeviceInfoProvider DeviceInfoProvider { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003A44 File Offset: 0x00001C44
		public void Reset()
		{
			foreach (Evaluator.UpdateInfo updateInfo in this.updateInfoByGuid.Values)
			{
				updateInfo.Result = EvaluationResult.Unevaluated;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003A9C File Offset: 0x00001C9C
		public void Clear()
		{
			this.updateInfoById.Clear();
			this.updateInfoByGuid.Clear();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public void EvaluateUpdates()
		{
			foreach (string guid in this.updateInfoByGuid.Keys)
			{
				this.EvaluateUpdate(guid);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B10 File Offset: 0x00001D10
		private EvaluationResult EvaluateUpdate(string guid)
		{
			if (this.updateInfoByGuid.ContainsKey(guid))
			{
				Evaluator.UpdateInfo updateInfo = this.updateInfoByGuid[guid];
				if (updateInfo.Result == EvaluationResult.Unevaluated || updateInfo.Result == EvaluationResult.NotFound)
				{
					updateInfo.Result = EvaluationResult.Installable;
					XmlNode xmlNode = updateInfo.Node.SelectSingleNode("./msus-pub:Relationships", UpdateXmlNamespaceManager.Instance);
					if (xmlNode != null)
					{
						if (updateInfo.Result != EvaluationResult.NotApplicable)
						{
							XmlNode xmlNode2 = xmlNode.SelectSingleNode("./msus-pub:Prerequisites", UpdateXmlNamespaceManager.Instance);
							if (xmlNode2 != null && this.EvaluatePrerequisites(xmlNode2) != EvaluationResult.Installed)
							{
								updateInfo.Result = EvaluationResult.NotApplicable;
							}
						}
						if (updateInfo.Result != EvaluationResult.NotApplicable)
						{
							XmlNode xmlNode3 = xmlNode.SelectSingleNode("./msus-pub:BundledUpdates", UpdateXmlNamespaceManager.Instance);
							if (xmlNode3 != null)
							{
								updateInfo.Result = this.EvaluateBundledUpdates(xmlNode3);
							}
						}
					}
					if (updateInfo.Result != EvaluationResult.NotApplicable)
					{
						XmlNode xmlNode4 = updateInfo.Node.SelectSingleNode("./msus-pub:ApplicabilityRules", UpdateXmlNamespaceManager.Instance);
						if (xmlNode4 != null)
						{
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode("./msus-pub:IsSuperseded", UpdateXmlNamespaceManager.Instance);
							if (xmlNode5 != null && this.EvaluateApplicabilityRule(xmlNode5))
							{
								updateInfo.Result = EvaluationResult.Installed;
							}
							if (updateInfo.Result != EvaluationResult.Installed)
							{
								XmlNode xmlNode6 = xmlNode4.SelectSingleNode("./msus-pub:IsInstalled", UpdateXmlNamespaceManager.Instance);
								if (xmlNode6 != null && this.EvaluateApplicabilityRule(xmlNode6))
								{
									updateInfo.Result = EvaluationResult.Installed;
								}
							}
							if (updateInfo.Result != EvaluationResult.Installed)
							{
								XmlNode xmlNode7 = xmlNode4.SelectSingleNode("./msus-pub:IsInstallable", UpdateXmlNamespaceManager.Instance);
								if (xmlNode7 != null)
								{
									if (this.EvaluateApplicabilityRule(xmlNode7))
									{
										updateInfo.Result = EvaluationResult.Installable;
									}
									else
									{
										updateInfo.Result = EvaluationResult.NotApplicable;
									}
								}
							}
						}
					}
					if ((updateInfo.Result == EvaluationResult.Installable || updateInfo.Result == EvaluationResult.Installed) && xmlNode != null)
					{
						XmlNode xmlNode8 = xmlNode.SelectSingleNode("./msus-pub:SupersededUpdates", UpdateXmlNamespaceManager.Instance);
						if (xmlNode8 != null)
						{
							foreach (object obj in xmlNode8.SelectNodes("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance))
							{
								string value = ((XmlNode)obj).Attributes["UpdateID"].Value;
								if (this.updateInfoByGuid.ContainsKey(value))
								{
									updateInfo = this.updateInfoByGuid[value];
									updateInfo.Result = EvaluationResult.Installed;
								}
							}
						}
					}
				}
				return updateInfo.Result;
			}
			return EvaluationResult.NotFound;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D50 File Offset: 0x00001F50
		public int[] GetInstallableUpdates()
		{
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (Evaluator.UpdateInfo updateInfo in this.updateInfoByGuid.Values)
			{
				if (updateInfo.ExplicitlyDeployable)
				{
					this.GetInstallableUpdates(updateInfo.Guid, sortedSet);
				}
			}
			return sortedSet.ToArray<int>();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public int[] GetExplicitlyDeployableUpdates()
		{
			return (from x in this.updateInfoByGuid.Values
			where x.ExplicitlyDeployable && x.Result == EvaluationResult.Installable
			select x.ID).ToArray<int>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E2C File Offset: 0x0000202C
		private void GetInstallableUpdates(string guid, SortedSet<int> ids)
		{
			if (!this.updateInfoByGuid.ContainsKey(guid))
			{
				return;
			}
			Evaluator.UpdateInfo updateInfo = this.updateInfoByGuid[guid];
			if (updateInfo.Result != EvaluationResult.Installable)
			{
				return;
			}
			ids.Add(updateInfo.ID);
			foreach (object obj in updateInfo.Node.SelectNodes("./msus-pub:Relationships/msus-pub:BundledUpdates//msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance))
			{
				XmlNode xmlNode = (XmlNode)obj;
				this.GetInstallableUpdates(xmlNode.Attributes["UpdateID"].Value, ids);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003EDC File Offset: 0x000020DC
		private EvaluationResult EvaluatePrerequisites(XmlNode updates)
		{
			XmlNodeList xmlNodeList = updates.SelectNodes("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					string value = ((XmlNode)obj).Attributes["UpdateID"].Value;
					if (this.EvaluateUpdate(value) != EvaluationResult.Installed)
					{
						return EvaluationResult.NotApplicable;
					}
				}
			}
			XmlNodeList xmlNodeList2 = updates.SelectNodes("./msus-pub:AtLeastOne", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList2 != null)
			{
				foreach (object obj2 in xmlNodeList2)
				{
					XmlNode atLeastOne = (XmlNode)obj2;
					if (this.EvaluatePrerequisiteAtLeastOne(atLeastOne) != EvaluationResult.Installed)
					{
						return EvaluationResult.NotApplicable;
					}
				}
			}
			return EvaluationResult.Installed;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003FCC File Offset: 0x000021CC
		private EvaluationResult EvaluatePrerequisiteAtLeastOne(XmlNode atLeastOne)
		{
			XmlNodeList xmlNodeList = atLeastOne.SelectNodes("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					string value = ((XmlNode)obj).Attributes["UpdateID"].Value;
					if (this.EvaluateUpdate(value) == EvaluationResult.Installed)
					{
						return EvaluationResult.Installed;
					}
				}
				return EvaluationResult.NotApplicable;
			}
			return EvaluationResult.NotApplicable;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004058 File Offset: 0x00002258
		private EvaluationResult EvaluateBundledUpdates(XmlNode updates)
		{
			EvaluationResult evaluationResult = EvaluationResult.Unevaluated;
			XmlNodeList xmlNodeList = updates.SelectNodes("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					string value = ((XmlNode)obj).Attributes["UpdateID"].Value;
					EvaluationResult evaluationResult2 = this.EvaluateUpdate(value);
					if (evaluationResult2 == EvaluationResult.NotApplicable)
					{
						return EvaluationResult.NotApplicable;
					}
					if (evaluationResult2 == EvaluationResult.Installed && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.Installed;
					}
					else if (evaluationResult2 == EvaluationResult.Installable)
					{
						evaluationResult = EvaluationResult.Installable;
					}
					else if (evaluationResult2 == EvaluationResult.NotFound && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.NotFound;
					}
				}
			}
			XmlNodeList xmlNodeList2 = updates.SelectNodes("./msus-pub:AtLeastOne", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList2 != null)
			{
				foreach (object obj2 in xmlNodeList2)
				{
					XmlNode atLeastOne = (XmlNode)obj2;
					EvaluationResult evaluationResult3 = this.EvaluateBundledUpdatesAtLeastOne(atLeastOne);
					if (evaluationResult3 == EvaluationResult.NotApplicable)
					{
						return EvaluationResult.NotApplicable;
					}
					if (evaluationResult3 == EvaluationResult.Installed && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.Installed;
					}
					else if (evaluationResult3 == EvaluationResult.Installable)
					{
						evaluationResult = EvaluationResult.Installable;
					}
					else if (evaluationResult3 == EvaluationResult.NotFound && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.NotFound;
					}
				}
			}
			if (evaluationResult == EvaluationResult.Unevaluated)
			{
				evaluationResult = EvaluationResult.Installable;
			}
			return evaluationResult;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000419C File Offset: 0x0000239C
		private EvaluationResult EvaluateBundledUpdatesAtLeastOne(XmlNode atLeastOne)
		{
			EvaluationResult evaluationResult = EvaluationResult.Unevaluated;
			XmlNodeList xmlNodeList = atLeastOne.SelectNodes("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					string value = ((XmlNode)obj).Attributes["UpdateID"].Value;
					EvaluationResult evaluationResult2 = this.EvaluateUpdate(value);
					if (evaluationResult2 == EvaluationResult.Installable)
					{
						evaluationResult = EvaluationResult.Installable;
					}
					else if (evaluationResult2 == EvaluationResult.NotApplicable && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.NotApplicable;
					}
					else if (evaluationResult2 == EvaluationResult.NotFound && evaluationResult == EvaluationResult.Unevaluated)
					{
						evaluationResult = EvaluationResult.NotFound;
					}
					else if (evaluationResult2 == EvaluationResult.Installed)
					{
						evaluationResult = EvaluationResult.Installed;
					}
				}
			}
			if (evaluationResult == EvaluationResult.Unevaluated)
			{
				evaluationResult = EvaluationResult.Installable;
			}
			return evaluationResult;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004250 File Offset: 0x00002450
		private bool EvaluateApplicabilityRule(XmlNode parent)
		{
			XmlNodeList xmlNodeList = parent.SelectNodes("./*");
			if (xmlNodeList.Count == 0)
			{
				return false;
			}
			if (xmlNodeList.Count > 1)
			{
				throw new Exception("Applicability syntax error");
			}
			return this.EvaluateSingleApplicabilityRule(xmlNodeList.Item(0));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004294 File Offset: 0x00002494
		private bool EvaluateSingleApplicabilityRule(XmlNode applicabilityRule)
		{
			string localName = applicabilityRule.LocalName;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(localName);
			if (num > 2541049336U)
			{
				if (num <= 3027042876U)
				{
					if (num != 2700197662U)
					{
						if (num != 2802280515U)
						{
							if (num != 3027042876U)
							{
								goto IL_223;
							}
							if (!(localName == "b.RegSz"))
							{
								goto IL_223;
							}
							return this.EvaluateRegSz(applicabilityRule);
						}
						else
						{
							if (!(localName == "b.Processor"))
							{
								goto IL_223;
							}
							return this.EvaluateProcessor(applicabilityRule);
						}
					}
					else if (!(localName == "StringCspQuery"))
					{
						goto IL_223;
					}
				}
				else if (num <= 3431195003U)
				{
					if (num != 3269540958U)
					{
						if (num != 3431195003U)
						{
							goto IL_223;
						}
						if (!(localName == "b.RegDword"))
						{
							goto IL_223;
						}
						return this.EvaluateRegDword(applicabilityRule);
					}
					else
					{
						if (!(localName == "b.RegValueExists"))
						{
							goto IL_223;
						}
						return this.EvaluateRegValueExists(applicabilityRule);
					}
				}
				else if (num != 3453902341U)
				{
					if (num != 4257532911U)
					{
						goto IL_223;
					}
					if (!(localName == "CspQuery"))
					{
						goto IL_223;
					}
				}
				else
				{
					if (!(localName == "True"))
					{
						goto IL_223;
					}
					return true;
				}
				return this.EvaluateCspQuery(applicabilityRule);
			}
			if (num <= 2247230816U)
			{
				if (num != 1558476708U)
				{
					if (num != 1907748166U)
					{
						if (num == 2247230816U)
						{
							if (localName == "b.RegKeyExists")
							{
								return this.EvaluateRegKeyExists(applicabilityRule);
							}
						}
					}
					else if (localName == "And")
					{
						return this.EvaluateAnd(applicabilityRule);
					}
				}
				else if (localName == "Or")
				{
					return this.EvaluateOr(applicabilityRule);
				}
			}
			else if (num != 2352858922U)
			{
				if (num != 2506011850U)
				{
					if (num == 2541049336U)
					{
						if (localName == "False")
						{
							return false;
						}
					}
				}
				else if (localName == "b.WindowsVersion")
				{
					return this.EvaluateWindowsVersion(applicabilityRule);
				}
			}
			else if (localName == "Not")
			{
				return !this.EvaluateApplicabilityRule(applicabilityRule);
			}
			IL_223:
			if (Evaluator.GetDebugLoggingEnabled())
			{
				this.LogMessageEvent(string.Format("Unrecognized applicability rule: {0}:{1}", applicabilityRule.NamespaceURI, applicabilityRule.LocalName));
				this.LogMessageEvent(string.Format("Unrecognized applicability rule: {0}", applicabilityRule.OuterXml));
			}
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004508 File Offset: 0x00002708
		private bool EvaluateOr(XmlNode parent)
		{
			bool flag = false;
			foreach (object obj in parent.SelectNodes("./*"))
			{
				XmlNode applicabilityRule = (XmlNode)obj;
				flag |= this.EvaluateSingleApplicabilityRule(applicabilityRule);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004574 File Offset: 0x00002774
		private bool EvaluateAnd(XmlNode parent)
		{
			bool flag = true;
			foreach (object obj in parent.SelectNodes("./*"))
			{
				XmlNode applicabilityRule = (XmlNode)obj;
				flag &= this.EvaluateSingleApplicabilityRule(applicabilityRule);
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000045E0 File Offset: 0x000027E0
		private bool EvaluateCspQuery(XmlNode rule)
		{
			string value = rule.Attributes["LocUri"].Value;
			string value2 = rule.Attributes["Value"].Value;
			string value3 = rule.Attributes["Comparison"].Value;
			return this.DeviceInfoProvider.EvaluateCSPQuery(value, value2, value3);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004640 File Offset: 0x00002840
		private bool EvaluateRegSz(XmlNode rule)
		{
			string value = rule.Attributes["Key"].Value;
			string value2 = rule.Attributes["Subkey"].Value;
			string value3 = rule.Attributes["Value"].Value;
			string value4 = rule.Attributes["Data"].Value;
			string value5 = rule.Attributes["Comparison"].Value;
			return this.DeviceInfoProvider.EvaluateRegSz(value, value2, value3, value4, value5);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000046D0 File Offset: 0x000028D0
		private bool EvaluateRegDword(XmlNode rule)
		{
			string value = rule.Attributes["Key"].Value;
			string value2 = rule.Attributes["Subkey"].Value;
			string value3 = rule.Attributes["Value"].Value;
			uint data = uint.Parse(rule.Attributes["Data"].Value);
			string value4 = rule.Attributes["Comparison"].Value;
			return this.DeviceInfoProvider.EvaluateRegDword(value, value2, value3, data, value4);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004764 File Offset: 0x00002964
		private bool EvaluateRegKeyExists(XmlNode rule)
		{
			string value = rule.Attributes["Key"].Value;
			string value2 = rule.Attributes["Subkey"].Value;
			return this.DeviceInfoProvider.EvaluateRegKeyExists(value, value2);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047AC File Offset: 0x000029AC
		private bool EvaluateRegValueExists(XmlNode rule)
		{
			string value = rule.Attributes["Key"].Value;
			string value2 = rule.Attributes["Subkey"].Value;
			string value3 = rule.Attributes["Value"].Value;
			return this.DeviceInfoProvider.EvaluateRegValueExists(value, value2, value3);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000480C File Offset: 0x00002A0C
		private bool EvaluateProcessor(XmlNode rule)
		{
			string value = rule.Attributes["Architecture"].Value;
			return this.DeviceInfoProvider.EvaluateProcessor(value);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000483C File Offset: 0x00002A3C
		private bool EvaluateWindowsVersion(XmlNode rule)
		{
			if (rule.Attributes["Comparison"] == null || rule.Attributes["MajorVersion"] == null || rule.Attributes["MinorVersion"] == null)
			{
				return false;
			}
			string value = rule.Attributes["Comparison"].Value;
			string value2 = rule.Attributes["MajorVersion"].Value;
			string value3 = rule.Attributes["MinorVersion"].Value;
			string buildNumber = (rule.Attributes["BuildNumber"] == null) ? "0" : rule.Attributes["BuildNumber"].Value;
			return this.DeviceInfoProvider.EvaluateWindowsVersion(value2, value3, buildNumber, value);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004904 File Offset: 0x00002B04
		public void AddUpdate(int id, bool isLeaf, string updateXml)
		{
			string xml = string.Format("<Update xmlns=\"{0}\">{1}</Update>", UpdateXmlNamespaceManager.Instance.LookupNamespace("msus-pub"), updateXml);
			this.xmlDoc.LoadXml(xml);
			Evaluator.UpdateInfo updateInfo = new Evaluator.UpdateInfo(id, isLeaf, this.xmlDoc.DocumentElement);
			this.updateInfoById[updateInfo.ID] = updateInfo;
			this.updateInfoByGuid[updateInfo.Guid] = updateInfo;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004970 File Offset: 0x00002B70
		public void ChangeUpdate(int id, bool isLeaf, string updateXml)
		{
			string xml = string.Format("<Update xmlns=\"{0}\">{1}</Update>", UpdateXmlNamespaceManager.Instance.LookupNamespace("msus-pub"), updateXml);
			this.xmlDoc.LoadXml(xml);
			Evaluator.UpdateInfo updateInfo = this.updateInfoById[id];
			updateInfo.IsLeaf = isLeaf;
			updateInfo.Node = this.xmlDoc.DocumentElement;
			updateInfo.Result = EvaluationResult.Unevaluated;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000049D0 File Offset: 0x00002BD0
		public void RemoveUpdate(int id)
		{
			Evaluator.UpdateInfo updateInfo = this.updateInfoById[id];
			this.updateInfoById.Remove(updateInfo.ID);
			this.updateInfoByGuid.Remove(updateInfo.Guid);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A10 File Offset: 0x00002C10
		public void PartitionUpdates(List<int> installedNonLeafUpdates, List<int> otherUpdates)
		{
			installedNonLeafUpdates.Clear();
			otherUpdates.Clear();
			foreach (Evaluator.UpdateInfo updateInfo in this.updateInfoById.Values)
			{
				if (updateInfo.Result == EvaluationResult.Installed && !updateInfo.IsLeaf)
				{
					installedNonLeafUpdates.Add(updateInfo.ID);
				}
				else
				{
					otherUpdates.Add(updateInfo.ID);
				}
			}
		}

		// Token: 0x04000033 RID: 51
		private XmlDocument xmlDoc = new XmlDocument();

		// Token: 0x04000039 RID: 57
		private SortedDictionary<int, Evaluator.UpdateInfo> updateInfoById = new SortedDictionary<int, Evaluator.UpdateInfo>();

		// Token: 0x0400003A RID: 58
		private SortedDictionary<string, Evaluator.UpdateInfo> updateInfoByGuid = new SortedDictionary<string, Evaluator.UpdateInfo>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x0600031D RID: 797
		public delegate void MessageCallback(string message);

		// Token: 0x02000068 RID: 104
		public class UpdateInfo
		{
			// Token: 0x06000320 RID: 800 RVA: 0x00009DC0 File Offset: 0x00007FC0
			public UpdateInfo(int id, bool isLeaf, XmlNode node)
			{
				this.ID = id;
				this.IsLeaf = isLeaf;
				XmlNode xmlNode = node.SelectSingleNode("./msus-pub:UpdateIdentity", UpdateXmlNamespaceManager.Instance);
				this.Guid = xmlNode.Attributes["UpdateID"].Value;
				XmlAttribute xmlAttribute = node.SelectSingleNode("./msus-pub:Properties", UpdateXmlNamespaceManager.Instance).Attributes["ExplicitlyDeployable"];
				if (xmlAttribute != null)
				{
					this.ExplicitlyDeployable = bool.Parse(xmlAttribute.Value);
				}
				this.Node = node;
				this.Result = EvaluationResult.Unevaluated;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000321 RID: 801 RVA: 0x00009E4F File Offset: 0x0000804F
			// (set) Token: 0x06000322 RID: 802 RVA: 0x00009E57 File Offset: 0x00008057
			public int ID { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000323 RID: 803 RVA: 0x00009E60 File Offset: 0x00008060
			// (set) Token: 0x06000324 RID: 804 RVA: 0x00009E68 File Offset: 0x00008068
			public string Guid { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000325 RID: 805 RVA: 0x00009E71 File Offset: 0x00008071
			// (set) Token: 0x06000326 RID: 806 RVA: 0x00009E79 File Offset: 0x00008079
			public bool IsLeaf { get; set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000327 RID: 807 RVA: 0x00009E82 File Offset: 0x00008082
			// (set) Token: 0x06000328 RID: 808 RVA: 0x00009E8A File Offset: 0x0000808A
			public XmlNode Node { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000329 RID: 809 RVA: 0x00009E93 File Offset: 0x00008093
			// (set) Token: 0x0600032A RID: 810 RVA: 0x00009E9B File Offset: 0x0000809B
			public EvaluationResult Result { get; set; }

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600032B RID: 811 RVA: 0x00009EA4 File Offset: 0x000080A4
			// (set) Token: 0x0600032C RID: 812 RVA: 0x00009EAC File Offset: 0x000080AC
			public bool ExplicitlyDeployable { get; set; }
		}
	}
}
