using KobApp.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;
using System.IO;
using System.Net.Http;
using KobApplication;
using ModernHttpClient;

namespace KobApp.APIServices
{
	public class ApiServices
	{
		public enum ApiReadWay { NormalJSon, HugeJson };
		public async Task<LoginResult> LoginAsync(string username, string password)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoLogin.aspx?Username=" + username + "&password=" + password);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Responce : " + response.Content);
				LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(response.Content);
				System.Diagnostics.Debug.WriteLine("Token : " + loginResult.Token != null ? loginResult.Token : "Null");
				return loginResult;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("LoginAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<LoginResult> LoginCardAsync(string CardCert)
		{
			try
			{
				String url = Config.APIEndPoint + "JSonKyoLogin.aspx?CardCert=" + CardCert;
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Responce : " + response.Content);
				LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(response.Content);
				System.Diagnostics.Debug.WriteLine("LoginCardAsync : " + loginResult.Token != null ? loginResult.Token : "Null");
				return loginResult;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("d Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<UsersModel>> SearchAsync(string name, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoSearchAnag_ByName.aspx?NAME=" + name + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				List<UsersModel> usersList = JsonConvert.DeserializeObject<List<UsersModel>>(response.Content);
				return usersList;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("SearchAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TicketBlockModel>> GetTicketBlocksAsync(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoSearchTicketBlocks.aspx?TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				List<TicketBlockModel> usersList = JsonConvert.DeserializeObject<List<TicketBlockModel>>(response.Content);
				return usersList;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTicketBlocksAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TicketBlockModel>> AddTicketBlocksAsync(string id, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoAddSearchTicketBlocks.aspx?ID=" + id + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				List<TicketBlockModel> usersList = JsonConvert.DeserializeObject<List<TicketBlockModel>>(response.Content);
				return usersList;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("AddTicketBlocksAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TicketModel>> GetTicketsAsync(UsersModel userModel, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoSearchAnagDetail_ByName.aspx?NAME=" + userModel.NOME + "&SURNAME=" + userModel.COGNOME + "&BIRTHDATE=" + string.Format("{0:dd/MM/yyyy}", userModel.DATA_NASCITA) + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<TicketModel> ticketModel = JsonConvert.DeserializeObject<List<TicketModel>>(response.Content);
				return ticketModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTicketsAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		//http://{IP_ADDRESS}/inspections/JSonKyoSearchAnagDetail_ByID2.aspx?ID=11908&TOKEN=caa6506739432bb3c790e1c550f393734e450a7e
		public async Task<List<UsersModel>> GetTicketsByBarcodeAsync2(string BarcodeID, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoSearchAnagDetail_ByID2.aspx?ID=" + BarcodeID + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<UsersModel> usersModel = JsonConvert.DeserializeObject<List<UsersModel>>(response.Content);
				return usersModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTicketsByBarcodeAsync2 Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		//http://{IP_ADDRESS}/inspections/JSonKyoSearchAnagDetail_ByID.aspx?ID=11908&TOKEN=caa6506739432bb3c790e1c550f393734e450a7e
		public async Task<List<TicketModel>> GetTicketsByBarcodeAsync(string BarcodeID, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoSearchAnagDetail_ByID.aspx?ID=" + BarcodeID + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<TicketModel> ticketModel = JsonConvert.DeserializeObject<List<TicketModel>>(response.Content);
				return ticketModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTicketsByBarcodeAsync Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<InspectorsModel>> GetInspectors(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoInspectors.aspx?TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<InspectorsModel> ticketModel = JsonConvert.DeserializeObject<List<InspectorsModel>>(response.Content);
				return ticketModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetInspectors Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<ActivityModel>> GetActivityModelData(DateTime To, DateTime From, string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoMyActivity.aspx?TO=" + string.Format("{0:yyyy-MM-dd}", To) + "&FROM=" + string.Format("{0:yyyy-MM-dd}", From) + "&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<ActivityModel> activityModel = JsonConvert.DeserializeObject<List<ActivityModel>>(response.Content);
				return activityModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Get Activity Model Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<AreasModel>> GetAreas(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoAreas&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<AreasModel> model = JsonConvert.DeserializeObject<List<AreasModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetAreas Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<LinesModel>> GetLines(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoLinee&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<LinesModel> model = JsonConvert.DeserializeObject<List<LinesModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetLines Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<GeoCountriesModel>> GetGeoCountries(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoGeoCountries&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<GeoCountriesModel> model = JsonConvert.DeserializeObject<List<GeoCountriesModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetGeoCountries Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<GeoComuniITModel>> GetGeoComuniIT(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoGeoComuniIT&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<GeoComuniITModel> model = JsonConvert.DeserializeObject<List<GeoComuniITModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetGeoComuniIT Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<GeoProvinceITModel>> GetGeoProvinceIT(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoGeoProvinceIT&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<GeoProvinceITModel> model = JsonConvert.DeserializeObject<List<GeoProvinceITModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetAreas Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<MotiviSanzioniModel>> GetMotiviSanzioni(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoMotiviSanzioni&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<MotiviSanzioniModel> model = JsonConvert.DeserializeObject<List<MotiviSanzioniModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetMotiviSanzioni Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TipoDocumentoModel>> GetTipoDocumento(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoTipoDocumento&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<TipoDocumentoModel> model = JsonConvert.DeserializeObject<List<TipoDocumentoModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTipoDocumento Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TipoTitoloEvasoModel>> GetTipoTitoloEvaso(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?CLASS=KyoTipoTitoloEvaso&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<TipoTitoloEvasoModel> model = JsonConvert.DeserializeObject<List<TipoTitoloEvasoModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTipoTitoloEvaso Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<StopsModel>> GetStops(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoStops&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				//System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<StopsModel> model = JsonConvert.DeserializeObject<List<StopsModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetStops Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<CalendarDatesModel>> GetCalendarDates(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoCalendarDates&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				//System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<CalendarDatesModel> model = JsonConvert.DeserializeObject<List<CalendarDatesModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetCalendarDates Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<RoutesModel>> GetRoutes(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoRoutes&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				//System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<RoutesModel> model = JsonConvert.DeserializeObject<List<RoutesModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetRoutes Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<TripsModel>> GetTrips(string Token)
		{
			try
			{
				var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoTrips&TOKEN=" + Token);
				var request = new RestRequest(Method.GET);
				IRestResponse response = await client.Execute(request);
				//System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
				List<TripsModel> model = JsonConvert.DeserializeObject<List<TripsModel>>(response.Content);
				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetTrips Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

		public async Task<List<StopTimesModel>> GetStopTimes(string Token, ApiReadWay apiReadWay)
		{
			try
			{
				if (apiReadWay == ApiReadWay.NormalJSon)
				{
					var client = new RestClient(Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoStopTimes&TOKEN=" + Token);

					var request = new RestRequest(Method.GET);
					IRestResponse response = await client.Execute(request);
					//System.Diagnostics.Debug.WriteLine("Response : " + response.Content);
					List<StopTimesModel> model = JsonConvert.DeserializeObject<List<StopTimesModel>>(response.Content);
					return model;
				}
				else
				{
					var messageHandler = new NativeMessageHandler();

					String url = Config.APIEndPoint + "JSonKyoClasses.aspx?COMPRESS=Yes&CLASS=KyoStopTimes&TOKEN=" + Token;
					using (var stream = await new HttpClient(messageHandler).GetStreamAsync(url))
					{
						using (var sr = new StreamReader(stream))
						{
							using (var jr = new JsonTextReader(sr))
							{
								return new JsonSerializer().Deserialize<List<StopTimesModel>>(jr);
							}
						}
					}
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetStopTimes Exception : " + pException.Message + "\nStackTrace : " + pException.StackTrace);
				throw pException;
			}
			return null;
		}

	}
}
