using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;

namespace SmartModemReader
{
    public class DslDataReader
    {
        private static HttpClient http = new HttpClient();
        private const string xDslPageAddress = "modals/broadband-modal.lp";
        private readonly string sessionId;
        private readonly string ipAddress;

        public DslDataReader(string sessionId, string ipAddress="192.168.1.1")
        {
            this.sessionId = sessionId;
            this.ipAddress = ipAddress;
        }

        public async Task<DslData> ReadDataAsync()
        {
            var resp = await http.SendAsync(CreateRequest());
            var page = await resp.Content.ReadAsStringAsync();
            return ParsePage(page);
        }

        public async Task<DslData> ReadSampleDataAsync()
        {
            var page = await File.ReadAllTextAsync("SampleResponse.html");
            return ParsePage(page);
        }

        private static DslData ParsePage(string page)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(page);

            var dslData = new DslData();

            dslData.IsUp = html.GetElementbyId("DSL_Status_Id").NextSibling.InnerHtml.Contains("Up");
            dslData.UpTime = html.GetElementbyId("DSL Uptime").InnerHtml.Trim();
            dslData.DslType = html.GetElementbyId("DSL Type").InnerHtml.Trim();
            dslData.DslMode = html.GetElementbyId("DSL Mode").InnerHtml.Trim();
            dslData.MaxLineRateUp = html.GetElementbyId("Maximum Line rate").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloat();
            dslData.MaxLineRateDown = html.GetElementbyId("Maximum Line rate").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloat();
            dslData.LineRateUp = html.GetElementbyId("Line Rate").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloat();
            dslData.LineRateDown = html.GetElementbyId("Line Rate").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloat();
            dslData.Uploaded = html.GetElementbyId("Data Transferred").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloat();
            dslData.Downloaded = html.GetElementbyId("Data Transferred").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloat();
            dslData.OutPowerUp = html.GetElementbyId("Output Power").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloat();
            dslData.OutPowerDown = html.GetElementbyId("Output Power").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloat();
            dslData.AttenuationUp = html.GetElementbyId("Line Attenuation").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloatArray();
            dslData.AttenuationDown = html.GetElementbyId("Line Attenuation").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloatArray();
            dslData.NoiseMarginUp = html.GetElementbyId("Noise Margin").Descendants("i")
                                        .ElementAt(0).NextSibling.InnerHtml.ToFloat();
            dslData.NoiseMarginDown = html.GetElementbyId("Noise Margin").Descendants("i")
                                        .ElementAt(1).NextSibling.InnerHtml.ToFloat();
            return dslData;
        }

        private HttpRequestMessage CreateRequest()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"http://{ipAddress}/{xDslPageAddress}");

            req.Headers.Add("Cookie", $"sessionID={sessionId}");

            return req;
        }
    }
}