using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NextClass;
using NextClass.Model;

namespace TestApp
{
    public partial class Form1 : Form
    {
        private const string _db = "CofA_prod";
        private readonly string _sqlIp = @"192.168.0.187\DevIT";
        private readonly string _sqlUser = "sa";
        private readonly string _sqlPass = "b52mkmn5";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

            var dupa = new ConnectionStringModel(_sqlIp, _db, _sqlUser, _sqlPass);
            var ble =dupa.ConnectionString;
            var fly = new MsSql(dupa);
            var emo = fly.IsConnection();

        }
    }
}
