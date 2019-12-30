using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Stock.Core.Base;
using Stock.Core.Log;
using Stock.Tools.Utility;

namespace StockSpider
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            //绑定委托
            LogUtility.LogAction = PrintLog2View;
            InitializeComponent();
        }

        private void ClickEastMoney(object sender, RoutedEventArgs e)
        {
            try
            {
                IDoWork doWork = ServiceFactory.GetServiceImp<IDoWork>(ServiceModule.SvcModule.EastMoneyMain);
                if (doWork != null)
                {
                    Task.Factory.StartNew(doWork.DoWork);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogAction(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 输出日志到控制界面
        /// </summary>
        /// <param name="strLog">日志内容</param>
        private void PrintLog2View(string strLog)
        {
            try
            {
                Log4NetHelper.WriteLog(strLog);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    string log = DateTime.Now + " " + strLog + "\r\n";
                    LogBox.Text = log + LogBox.Text;
                }));
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                System.GC.Collect();
            }
        }

        private void ClickStart(object sender, RoutedEventArgs e)
        {

        }

    }
}
