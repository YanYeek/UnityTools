using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace YanYeek
{
    public static class Net
    {
        public static T Request<T>(string url, string method, string contentType, string body, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = contentType;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, encoding);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Request(string url, string contentType, string body, Encoding encoding, string method = "GET")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = contentType;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, encoding);
            string json = reader.ReadToEnd();
            return json;
        }
    }
}