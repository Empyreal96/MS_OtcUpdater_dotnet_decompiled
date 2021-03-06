using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000055 RID: 85
	public class XsdValidator
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000B1EC File Offset: 0x000093EC
		public void ValidateXsd(string xsdFile, string xmlFile, IULogger logger)
		{
			if (!LongPathFile.Exists(xmlFile))
			{
				throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: XML file was not found: " + xmlFile);
			}
			using (FileStream fileStream = LongPathFile.OpenRead(xmlFile))
			{
				string text = string.Empty;
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				foreach (string text2 in executingAssembly.GetManifestResourceNames())
				{
					if (text2.Contains(xsdFile))
					{
						text = text2;
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: XSD resource was not found: " + xsdFile);
				}
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
				{
					this.ValidateXsd(manifestResourceStream, fileStream, xmlFile, logger);
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public void ValidateXsd(Stream xsdStream, string xmlFile, IULogger logger)
		{
			if (!LongPathFile.Exists(xmlFile))
			{
				throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: XML file was not found: " + xmlFile);
			}
			using (FileStream fileStream = LongPathFile.OpenRead(xmlFile))
			{
				this.ValidateXsd(xsdStream, fileStream, xmlFile, logger);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B308 File Offset: 0x00009508
		public void ValidateXsd(Stream xsdStream, Stream xmlStream, string xmlName, IULogger logger)
		{
			this._logger = logger;
			this._fileIsValid = true;
			if (xsdStream == null)
			{
				throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: Failed to load the embeded schema file for xml: " + xmlName);
			}
			XmlDocument xmlDocument;
			try
			{
				XmlSchema schema = null;
				using (XmlReader xmlReader = XmlReader.Create(xsdStream))
				{
					schema = XmlSchema.Read(xmlReader, new ValidationEventHandler(this.ValidationHandler));
				}
				xmlDocument = new XmlDocument();
				xmlDocument.Schemas.Add(schema);
			}
			catch (XmlSchemaException innerException)
			{
				throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: Unable to use the schema provided for xml: " + xmlName, innerException);
			}
			try
			{
				xmlDocument.Load(xmlStream);
				xmlDocument.Validate(new ValidationEventHandler(this.ValidationHandler));
			}
			catch (Exception innerException2)
			{
				throw new XsdValidatorException("ToolsCommon!XsdValidator::ValidateXsd: There was a problem validating the XML file " + xmlName, innerException2);
			}
			if (!this._fileIsValid)
			{
				throw new XsdValidatorException(string.Format(CultureInfo.InvariantCulture, "ToolsCommon!XsdValidator::ValidateXsd: Validation of {0} failed", new object[]
				{
					xmlName
				}));
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B40C File Offset: 0x0000960C
		private void ValidationHandler(object sender, ValidationEventArgs args)
		{
			string format = string.Format(CultureInfo.InvariantCulture, "\nToolsCommon!XsdValidator::ValidateXsd: XML Validation {0}: {1}", new object[]
			{
				args.Severity,
				args.Message
			});
			if (args.Severity == XmlSeverityType.Error)
			{
				if (this._logger != null)
				{
					this._logger.LogError(format, new object[0]);
				}
				this._fileIsValid = false;
				return;
			}
			if (this._logger != null)
			{
				this._logger.LogWarning(format, new object[0]);
			}
		}

		// Token: 0x0400011B RID: 283
		private bool _fileIsValid = true;

		// Token: 0x0400011C RID: 284
		private IULogger _logger;
	}
}
