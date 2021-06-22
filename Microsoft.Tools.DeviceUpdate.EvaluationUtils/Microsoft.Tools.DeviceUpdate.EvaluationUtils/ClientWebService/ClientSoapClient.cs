using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000063 RID: 99
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class ClientSoapClient : ClientBase<ClientSoap>, ClientSoap
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000998C File Offset: 0x00007B8C
		public ClientSoapClient()
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009994 File Offset: 0x00007B94
		public ClientSoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000999D File Offset: 0x00007B9D
		public ClientSoapClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000099A7 File Offset: 0x00007BA7
		public ClientSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000099B1 File Offset: 0x00007BB1
		public ClientSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000099BB File Offset: 0x00007BBB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetConfigResponse ClientSoap.GetConfig(GetConfigRequest request)
		{
			return base.Channel.GetConfig(request);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000099CC File Offset: 0x00007BCC
		public Config GetConfig(string protocolVersion)
		{
			return ((ClientSoap)this).GetConfig(new GetConfigRequest
			{
				Body = new GetConfigRequestBody(),
				Body = 
				{
					protocolVersion = protocolVersion
				}
			}).Body.GetConfigResult;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009A07 File Offset: 0x00007C07
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetCookieResponse ClientSoap.GetCookie(GetCookieRequest request)
		{
			return base.Channel.GetCookie(request);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009A18 File Offset: 0x00007C18
		public Cookie GetCookie(AuthorizationCookie[] authCookies, Cookie oldCookie, DateTime lastChange, DateTime currentTime, string protocolVersion)
		{
			return ((ClientSoap)this).GetCookie(new GetCookieRequest
			{
				Body = new GetCookieRequestBody(),
				Body = 
				{
					authCookies = authCookies,
					oldCookie = oldCookie,
					lastChange = lastChange,
					currentTime = currentTime,
					protocolVersion = protocolVersion
				}
			}).Body.GetCookieResult;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009A85 File Offset: 0x00007C85
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		RegisterComputerResponse ClientSoap.RegisterComputer(RegisterComputerRequest request)
		{
			return base.Channel.RegisterComputer(request);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009A94 File Offset: 0x00007C94
		public void RegisterComputer(Cookie cookie, ComputerInfo computerInfo)
		{
			((ClientSoap)this).RegisterComputer(new RegisterComputerRequest
			{
				Body = new RegisterComputerRequestBody(),
				Body = 
				{
					cookie = cookie,
					computerInfo = computerInfo
				}
			});
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009AD2 File Offset: 0x00007CD2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		StartCategoryScanResponse ClientSoap.StartCategoryScan(StartCategoryScanRequest request)
		{
			return base.Channel.StartCategoryScan(request);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public ArrayOfGuid StartCategoryScan(CategoryRelationship[] requestedCategories, out ArrayOfGuid requestedCategoryIdsInError)
		{
			StartCategoryScanResponse startCategoryScanResponse = ((ClientSoap)this).StartCategoryScan(new StartCategoryScanRequest
			{
				Body = new StartCategoryScanRequestBody(),
				Body = 
				{
					requestedCategories = requestedCategories
				}
			});
			requestedCategoryIdsInError = startCategoryScanResponse.Body.requestedCategoryIdsInError;
			return startCategoryScanResponse.Body.preferredCategoryIds;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009B2A File Offset: 0x00007D2A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		SyncUpdatesResponse ClientSoap.SyncUpdates(SyncUpdatesRequest request)
		{
			return base.Channel.SyncUpdates(request);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009B38 File Offset: 0x00007D38
		public SyncInfo SyncUpdates(Cookie cookie, SyncUpdateParameters parameters)
		{
			return ((ClientSoap)this).SyncUpdates(new SyncUpdatesRequest
			{
				Body = new SyncUpdatesRequestBody(),
				Body = 
				{
					cookie = cookie,
					parameters = parameters
				}
			}).Body.SyncUpdatesResult;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009B7F File Offset: 0x00007D7F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		SyncPrinterCatalogResponse ClientSoap.SyncPrinterCatalog(SyncPrinterCatalogRequest request)
		{
			return base.Channel.SyncPrinterCatalog(request);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009B90 File Offset: 0x00007D90
		public SyncInfo SyncPrinterCatalog(Cookie cookie, ArrayOfInt installedNonLeafUpdateIDs, ArrayOfInt printerUpdateIDs)
		{
			return ((ClientSoap)this).SyncPrinterCatalog(new SyncPrinterCatalogRequest
			{
				Body = new SyncPrinterCatalogRequestBody(),
				Body = 
				{
					cookie = cookie,
					installedNonLeafUpdateIDs = installedNonLeafUpdateIDs,
					printerUpdateIDs = printerUpdateIDs
				}
			}).Body.SyncPrinterCatalogResult;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00009BE3 File Offset: 0x00007DE3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		RefreshCacheResponse ClientSoap.RefreshCache(RefreshCacheRequest request)
		{
			return base.Channel.RefreshCache(request);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public RefreshCacheResult[] RefreshCache(Cookie cookie, UpdateIdentity[] globalIDs)
		{
			return ((ClientSoap)this).RefreshCache(new RefreshCacheRequest
			{
				Body = new RefreshCacheRequestBody(),
				Body = 
				{
					cookie = cookie,
					globalIDs = globalIDs
				}
			}).Body.RefreshCacheResult;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009C3B File Offset: 0x00007E3B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetExtendedUpdateInfoResponse ClientSoap.GetExtendedUpdateInfo(GetExtendedUpdateInfoRequest request)
		{
			return base.Channel.GetExtendedUpdateInfo(request);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00009C4C File Offset: 0x00007E4C
		public ExtendedUpdateInfo GetExtendedUpdateInfo(Cookie cookie, ArrayOfInt revisionIDs, XmlUpdateFragmentType[] infoTypes, ArrayOfString locales)
		{
			return ((ClientSoap)this).GetExtendedUpdateInfo(new GetExtendedUpdateInfoRequest
			{
				Body = new GetExtendedUpdateInfoRequestBody(),
				Body = 
				{
					cookie = cookie,
					revisionIDs = revisionIDs,
					infoTypes = infoTypes,
					locales = locales
				}
			}).Body.GetExtendedUpdateInfoResult;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009CAC File Offset: 0x00007EAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetExtendedUpdateInfo2Response ClientSoap.GetExtendedUpdateInfo2(GetExtendedUpdateInfo2Request request)
		{
			return base.Channel.GetExtendedUpdateInfo2(request);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009CBC File Offset: 0x00007EBC
		public ExtendedUpdateInfo2 GetExtendedUpdateInfo2(Cookie cookie, UpdateIdentity[] updateIDs, XmlUpdateFragmentType[] infoTypes, ArrayOfString locales)
		{
			return ((ClientSoap)this).GetExtendedUpdateInfo2(new GetExtendedUpdateInfo2Request
			{
				Body = new GetExtendedUpdateInfo2RequestBody(),
				Body = 
				{
					cookie = cookie,
					updateIDs = updateIDs,
					infoTypes = infoTypes,
					locales = locales
				}
			}).Body.GetExtendedUpdateInfo2Result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009D1C File Offset: 0x00007F1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetFileLocationsResponse ClientSoap.GetFileLocations(GetFileLocationsRequest request)
		{
			return base.Channel.GetFileLocations(request);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009D2C File Offset: 0x00007F2C
		public GetFileLocationsResults GetFileLocations(Cookie cookie, ArrayOfBase64Binary fileDigests)
		{
			return ((ClientSoap)this).GetFileLocations(new GetFileLocationsRequest
			{
				Body = new GetFileLocationsRequestBody(),
				Body = 
				{
					cookie = cookie,
					fileDigests = fileDigests
				}
			}).Body.GetFileLocationsResult;
		}
	}
}
