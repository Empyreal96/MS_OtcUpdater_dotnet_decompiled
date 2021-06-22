using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000039 RID: 57
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", ConfigurationName = "ClientWebService.ClientSoap")]
	public interface ClientSoap
	{
		// Token: 0x060002A2 RID: 674
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/GetConfig", ReplyAction = "*")]
		GetConfigResponse GetConfig(GetConfigRequest request);

		// Token: 0x060002A3 RID: 675
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/GetCookie", ReplyAction = "*")]
		GetCookieResponse GetCookie(GetCookieRequest request);

		// Token: 0x060002A4 RID: 676
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/RegisterComputer", ReplyAction = "*")]
		RegisterComputerResponse RegisterComputer(RegisterComputerRequest request);

		// Token: 0x060002A5 RID: 677
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/StartCategoryScan", ReplyAction = "*")]
		StartCategoryScanResponse StartCategoryScan(StartCategoryScanRequest request);

		// Token: 0x060002A6 RID: 678
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/SyncUpdates", ReplyAction = "*")]
		SyncUpdatesResponse SyncUpdates(SyncUpdatesRequest request);

		// Token: 0x060002A7 RID: 679
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/SyncPrinterCatalog", ReplyAction = "*")]
		SyncPrinterCatalogResponse SyncPrinterCatalog(SyncPrinterCatalogRequest request);

		// Token: 0x060002A8 RID: 680
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/RefreshCache", ReplyAction = "*")]
		RefreshCacheResponse RefreshCache(RefreshCacheRequest request);

		// Token: 0x060002A9 RID: 681
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/GetExtendedUpdateInfo", ReplyAction = "*")]
		GetExtendedUpdateInfoResponse GetExtendedUpdateInfo(GetExtendedUpdateInfoRequest request);

		// Token: 0x060002AA RID: 682
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/GetExtendedUpdateInfo2", ReplyAction = "*")]
		GetExtendedUpdateInfo2Response GetExtendedUpdateInfo2(GetExtendedUpdateInfo2Request request);

		// Token: 0x060002AB RID: 683
		[OperationContract(Action = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService/GetFileLocations", ReplyAction = "*")]
		GetFileLocationsResponse GetFileLocations(GetFileLocationsRequest request);
	}
}
