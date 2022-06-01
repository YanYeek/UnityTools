using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace YanYeek
{
    public class Feishu
    {
        public static List<string> GetFields(Feishu.SheetData sheet, int endColumn, int startRow)
        {
            var cells = sheet.data.valueRange.values;
            var fields = new List<string>(endColumn);
            for (int i = 0; i < endColumn; i++)
            {
                try
                {
                    fields.Add(cells[startRow - 1][i].ToString().Split('/')[0]);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning($"sheetID = {sheet.title} name = {cells[startRow][3]} column = {i}");
                    throw e;
                }
            }
            return fields;
        }
        public static HttpClient httpClient = new HttpClient();
        public static async Task<SheetData> GetSheet(string workbook, string sheet, string range = "")
        {
            SheetData data = null;
            var key = await GetKey();

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://open.feishu.cn/open-apis/sheets/v2/spreadsheets/{workbook}/values/{sheet}{range}?valueRenderOption=ToString"))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {key}");
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<SheetData>(json);
            }
            return data;
        }

        public static async Task<List<Feishu.SheetData>> GetSheets(string wbName, int len, int startSheet)
        {
            var wbInfo = await Feishu.GetWorkbookInfo(wbName);
            var sheets = new List<Feishu.SheetData>();
            startSheet--;
            for (int i = 0; i < len; i++)
            {
                var sheetID = wbInfo.Item1[startSheet + i];
                var sheet = await Feishu.GetSheet(wbName, sheetID);
                sheet.title = wbInfo.Item2[sheetID];
                sheets.Add(sheet);
            }
            return sheets;
        }

        public static async Task<Tuple<List<string>, Dictionary<string, string>>> GetWorkbookInfo(string workbook)
        {
            var list = new List<string>();
            var dict = new Dictionary<string, string>();

            var key = await GetKey();
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://open.feishu.cn/open-apis/sheets/v2/spreadsheets/{workbook}/metainfo?extFields=protectedRange&user_id_type=open_id"))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {key}");
                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                // UnityEngine.Debug.Log(json);
                var pattern = "(?<=\"sheetId\":\").*?\",\"title\":\".*?(?=\")";
                foreach (var e in Regex.Matches(json, pattern))
                {
                    var value = e.ToString().Replace("\",\"title\":\"", ",").Split(',');
                    list.Add(value[0]);
                    dict.Add(value[0], value[1]);
                }
            }
            return Tuple.Create<List<string>, Dictionary<string, string>>(list, dict);
        }
        private static async Task<string> GetKey()
        {
            var key = "";
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://open.feishu.cn/open-apis/auth/v3/tenant_access_token/internal"))
            {
                request.Content = new StringContent($"{{\"app_id\":\"{app_id}\",\"app_secret\":\"{app_secret}\"}}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");

                var response = await httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                key = Regex.Match(json, "(?<=\"tenant_access_token\":\").*?(?=\")").Value;
            }

            return key;
        }

        public class ValueRange
        {
            public string majorDimension { get; set; }
            public string range { get; set; }
            public int revision { get; set; }
            public List<List<object>> values { get; set; }
        }

        public class Data
        {
            public int revision { get; set; }
            public string spreadsheetToken { get; set; }
            public ValueRange valueRange { get; set; }
        }

        public class SheetData
        {
            public int code { get; set; }
            public Data data { get; set; }
            public string msg { get; set; }
            public string title { get; set; }
        }

        private const string app_id = "cli_a15553d262b9d00e";
        private const string app_secret = "7DOuG7BL7L6VuUyfNIMJOlr3JnpVJ7B4";
    }
}
