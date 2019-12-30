using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spring.Context;
using Spring.Context.Support;
using Stock.Core.Base;
using Stock.Core.Log;
using Stock.EastMoney;
using Stock.Tools.Utility;

namespace StockControl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Log4NetHelper.WriteLog("程序启动");
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDoWork doWork = ServiceFactory.GetServiceImp<IDoWork>(ServiceModule.SvcModule.EastMoneyMain);
            if (doWork != null)
            {
                doWork.DoWork();
            }
            // IDoWork doWork = new EastMoneyMain();

        }
    }
}
