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

namespace UDA_Status_PROJECT
{
    public partial class View2 : Form
    {
        public string UDA_index;
        public int status_UDA_changed;
        public ArrayList txts = new ArrayList() { "IDLE" };
        public ArrayList labels = new ArrayList() { "IDLE" };
        public ArrayList colors = new ArrayList() { Color.Black };
       
        public View2(string x)
        {
            txts.Add("IDLE");
            labels.Add("IDLE");
            colors.Add(Color.DarkGreen);

            txts.Add("GROUP_SENT");
            labels.Add("REACH_UDA");
            colors.Add(Color.DarkRed);

            txts.Add("ABORT");
            labels.Add("REACH_UDA");
            colors.Add(Color.DarkOrange);

            txts.Add("RESUME");
            labels.Add("REACHING_UDA");
            colors.Add(Color.DarkRed);

            txts.Add("PAUSE");
            labels.Add("READY");
            colors.Add(Color.DarkOrchid);

            txts.Add("START");
            labels.Add("NOT IMPLEMENTED");
            colors.Add(Color.Blue);

            txts.Add("STARTED");
            labels.Add("STARTED");
            colors.Add(Color.Green);

            txts.Add("PAUSE");
            labels.Add("PAUSED");
            colors.Add(Color.DarkOrange);

            txts.Add("PAUSED");
            labels.Add("PAUSED");
            colors.Add(Color.Brown);

            txts.Add("RESUME");
            labels.Add("RESUMED");
            colors.Add(Color.DarkCyan);

            txts.Add("ABORT");
            labels.Add("ABORTED");
            colors.Add(Color.Brown);

            txts.Add("ABORTED");
            labels.Add("ABORTED");
            colors.Add(Color.Brown);

            txts.Add("RESTART");
            labels.Add("ABORTED");
            colors.Add(Color.Red);

            txts.Add("WAIT_DATA");
            labels.Add("PAUSED");
            colors.Add(Color.DarkOrange);

            txts.Add("DATA_SENT");
            labels.Add("NOT IMPLEMENTED");
            colors.Add(Color.Brown);

            txts.Add("COMPLETED");
            labels.Add("");
            colors.Add(Color.DarkCyan);

            txts.Add("FINISHED");
            labels.Add("R");
            colors.Add(Color.Brown);

            txts.Add("FINALIZED");
            labels.Add("");
            colors.Add(Color.Brown);

            Business_Logic BL = new Business_Logic(this,x);
            InitializeComponent();
            label3.Visible = false;
            label4.Visible = false;
            UDA_index = x;
        }

        public void Status_Changed(string k, int i)
        {
            this.BeginInvoke((Action)delegate ()
            {
                label3.Visible = true;

                if (i == 2)
                {
                    label4.Visible = true;
                }
                setSelection(Int32.Parse(k));
            });

        }
        public void app_Status_Changed(string k, int i)
        {
            this.BeginInvoke((Action)delegate ()
            {
                label3.Visible = true;

                if (i == 2)
                {
                    label4.Visible = true;
                }
                app_setSelection(Int32.Parse(k));
            });

        }

        private void setSelection(int k)
        {
            if (k >= 0)
            {
                label3.ForeColor = (Color)colors[k];
                // label4.ForeColor = (Color)colors[k];
                label3.Text = (string)txts[k];
               // label4.Text = (string)labels[k];
            }
            else if (k == -1)
            {
                // label3.ForeColor = (Color)colors[k];
                label4.ForeColor = Color.Black; // (Color)colors[k];
                // label3.Text = (string)txts[k];
                label4.Text = "IDLE"; //(string)labels[k];
            }
          
        }
        private void app_setSelection(int k)
        {
            if (k >= 0)
            {
                label4.ForeColor = (Color)colors[k];
               label4.Text = (string)labels[k];
            }
            else if (k == -1)
            {
                label4.ForeColor = Color.Black; // (Color)colors[k];
                label4.Text = "INIT"; //(string)labels[k];
            }

        }

        private void BACK_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fr2 = new View1();
            fr2.Closed += (s, args) => this.Close();
            fr2.Show();
        }

        private void View2_Load(object sender, EventArgs e)
        {
            string nome;
            nome = string.Concat("UDA ", UDA_index);
            this.Text = nome;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public async void button1_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=7";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
                //app_Status_Changed(uda_change, 2);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=9";
            try
            {
                string uda_change= await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
                //app_Status_Changed(uda_change, 2);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=2";
            try
            {
                string app_change=await UDA_server_communication.Server_Request(put);
                app_Status_Changed(app_change, 2);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=4";
            try
            {
                string app_change = await UDA_server_communication.Server_Request(put);
                app_Status_Changed(app_change, 2);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=8";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }


        private async void button7_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=11";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=5";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                app_Status_Changed(uda_change, 2);
                //Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button6_Click_2(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=10";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private void button6_Click_3(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=10";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=13";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/app/put/?i=" + UDA_index + "&k=15&data=StringaSaving123.php";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=18";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=16";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=14";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            string put = "https://www.sagosoft.it/_API_/cpim/luda/www/luda_20210111_1500//api/uda/put/?i=" + UDA_index + "&k=12";
            try
            {
                string uda_change = await UDA_server_communication.Server_Request(put);
                Status_Changed(uda_change, 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error", ex);
            }
        }
    }
}
