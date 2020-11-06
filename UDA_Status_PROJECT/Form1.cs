﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UDA_Status_PROJECT
{
    public partial class View1 : Form
    {
        private string Myval;
        public string check1;
        public string MyVal
        {
            get { return Myval; }
            set { Myval = value; }
        }
        public View1()
        {
            InitializeComponent();
            button1.Visible = false;
        }

        private void CHOOSE_UDA_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var fr1 = new View2(Myval);
            fr1.Closed += (s, args) => this.Close();
            fr1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            if (radioButton1.Checked == true)
            {
                MyVal = "1";
                button1.Enabled = true;
            }
            if (radioButton2.Checked == true)
            {
                MyVal = "2";
            }
            if (radioButton3.Checked == true)
            {
                MyVal = "3";
            }
            if (radioButton4.Checked == true)
            {
                MyVal = "4";
            }
            if (radioButton5.Checked == true)
            {
                MyVal = "5";
            }
        }
    }
}
