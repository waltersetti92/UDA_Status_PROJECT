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
using System.Timers;
using System.Windows;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Timer = System.Timers.Timer;

namespace UDA_Status_PROJECT
{
    class Business_Logic
    {
        private View2 view2;
        public string save_status;
        private static System.Timers.Timer aTimer;
        public string UDA_index1;
        public int counter_timer;
        public Business_Logic(View2 form,string x)
        {
            view2 = form;
            counter_timer = 0;
            UDA_index1 = x;
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += new ElapsedEventHandler(New_Status_UDA);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }
        // Tramite il contatore, io prendo lo stato della UDA dalla classe UDA_server_communication
        // Quindi se il contatore è zero (appena apro l'eseguibile) avrò lo stato della mia uda al tempo zero
        // Poi al tempo 1 etc, la funzione fa una comparazione tra gli stati dell'UDA e quello iniziale per vedere se cambiato
        // appena cambia il contatore viene risettato a 0 ed il ciclo ricomincia.
        public async void New_Status_UDA(object source, ElapsedEventArgs e)
        {  
            string get_status_uda = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20200901_0900//api/uda/get/?i=" + UDA_index1;  // url per ottenere lo stato dell'UDA       
            try
            {
                string uda_status = await UDA_server_communication.Server_Request(get_status_uda); //stato dell'UDA ottenuto con la classe UDA_server_communication
                if (counter_timer == 0) // salvo lo stato dell'UDA al tempo t=0 e la prima volta che cambia
                {
                    save_status = uda_status;
                    view2.Status_Changed(uda_status, 1); // mostro attraverso la form2 il cambio di stato dell'UDA
                    string put_server= Url_Put(uda_status); // creo la stringa per il put al server che notifica il cambio di stato dell'UDA
                    await UDA_server_communication.Server_Request(put_server); // qui mando al server il comando di put per cambiare il suo stato
                    view2.Status_Changed(uda_status,2); // una volta che il comando è stato mandato, mostro con la form 2 il cambio di stato del server
                    counter_timer++;
                }
                else //verifico che lo stato corrente sia diverso dallo stato salvato
                {
                    if (!string.Equals(uda_status, save_status))
                    {
                        counter_timer = 0;
                        view2.Status_Changed(uda_status, 1);
                        string put_server= Url_Put(uda_status);
                        await UDA_server_communication.Server_Request(put_server);
                        view2.Status_Changed(uda_status, 2);
                    }
                }               
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
                aTimer.Stop();
            }

        }

        // Questo modulo serve per costruire la stringa di comando per il server (il put),
        // sulla base dell'indice (i) dell'UDA.
        public string Url_Put(string k)
        {
            int ik = Int32.Parse(k);
            if (ik >= 0 && ik < 8)
                return "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20200901_0900//api/uda/put/?i=" + UDA_index1 + "&k=" + ik.ToString();
            else
                return "";
        }

    }

}
