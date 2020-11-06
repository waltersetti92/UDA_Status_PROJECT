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

namespace UDA_Status_PROJECT
{
    public partial class View2 : Form
    {
        public string UDA_index;
        public int status_UDA_changed;
        public ArrayList txts = new ArrayList() { "IDLE" };
        public ArrayList labels = new ArrayList() { "IDLE" };
        public ArrayList colors = new ArrayList() { Color.DarkGreen };
        public View2(string x)
        {
            txts.Add("START");
            labels.Add("STARTED");
            colors.Add(Color.DarkGreen);

            txts.Add("ABORT");
            labels.Add("ABORTED");
            colors.Add(Color.DarkRed);

            txts.Add("PAUSE");
            labels.Add("PAUSED");
            colors.Add(Color.DarkOrange);

            txts.Add("RESUME");
            labels.Add("RESUMED");
            colors.Add(Color.Brown);

            txts.Add("FINALIZED");
            labels.Add("COMPLETED");
            colors.Add(Color.DarkOrchid);

            txts.Add("FINISHED");
            labels.Add("FINALIZED");
            colors.Add(Color.DarkCyan);

            txts.Add("NOT IMPLEMENTED");
            labels.Add("FINISHED");
            colors.Add(Color.Purple);

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

        private void setSelection(int k)
        {
            label3.ForeColor = (Color)colors[k];
            label4.ForeColor = (Color)colors[k];
            label3.Text = (string)txts[k];
            label4.Text = (string)labels[k];
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
    }
}
