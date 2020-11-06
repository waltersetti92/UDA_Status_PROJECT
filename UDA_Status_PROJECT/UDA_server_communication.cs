using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Collections;
using System.Windows;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UDA_Status_PROJECT
{
    class UDA_server_communication
    {
        public UDA_server_communication()
        {
        }
        // Con questo modulo, eseguo la comunicazione con il server e salvo lo stato corrente
        // a prescindere che sia quello dell'UDA o del server.
        public async static Task<string> Server_Request(string url)
        {
            try
            {
                WebRequest server = HttpWebRequest.Create(url);
                var response = server.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = await reader.ReadToEndAsync();
                    JObject json_parsed = JObject.Parse(result);
                    string current_status = (string)json_parsed["status"];
                    return current_status;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }
    }
    
}
