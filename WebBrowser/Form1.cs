using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebBrowser
{
    [PermissionSet(SecurityAction.Demand,Name="FullTrust")]
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.textBox1.Text);
            this.webBrowser1.ObjectForScripting = this;
        }

        public string InvokeFormMethod(string message)
        {
            MessageBox.Show(message);
            return "This is The End";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Text = this.webBrowser1.Url.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string uri = "http://www.ycjw.zjut.edu.cn/logon.aspx";
            ////JObject requestPara = JObject.Parse("{\"DdjgID\":\"100100\",\"SfzdID\":\"100100\",\"ypfl\":\"0000000001\",\"pzwh\":\"\",\"ypmc\":\"\"}");
            //string requestPara = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=dDwtMTU2MDM2OTk5Nzt0PDtsPGk8MT47PjtsPHQ8O2w8aTwzPjtpPDEzPjs%2BO2w8dDw7bDxpPDE%2BO2k8Mz47aTw1PjtpPDc%2BO2k8OT47aTwxMT47aTwxMz47aTwxNT47aTwxNz47PjtsPHQ8cDxwPGw8QmFja0ltYWdlVXJsOz47bDxodHRwOi8vd3d3Lnljancuemp1dC5lZHUuY24vL2ltYWdlcy9iZy5naWY7Pj47Pjs7Pjt0PHA8cDxsPEJhY2tJbWFnZVVybDs%2BO2w8aHR0cDovL3d3dy55Y2p3LnpqdXQuZWR1LmNuLy9pbWFnZXMvYmcxLmdpZjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8QmFja0ltYWdlVXJsOz47bDxodHRwOi8vd3d3Lnljancuemp1dC5lZHUuY24vL2ltYWdlcy9iZzEuZ2lmOz4%2BOz47Oz47dDxwPHA8bDxCYWNrSW1hZ2VVcmw7PjtsPGh0dHA6Ly93d3cueWNqdy56anV0LmVkdS5jbi8vaW1hZ2VzL2JnMS5naWY7Pj47Pjs7Pjt0PHA8cDxsPEJhY2tJbWFnZVVybDs%2BO2w8aHR0cDovL3d3dy55Y2p3LnpqdXQuZWR1LmNuLy9pbWFnZXMvYmcxLmdpZjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8QmFja0ltYWdlVXJsOz47bDxodHRwOi8vd3d3Lnljancuemp1dC5lZHUuY24vL2ltYWdlcy9iZzEuZ2lmOz4%2BOz47Oz47dDxwPHA8bDxCYWNrSW1hZ2VVcmw7PjtsPGh0dHA6Ly93d3cueWNqdy56anV0LmVkdS5jbi8vaW1hZ2VzL2JnMS5naWY7Pj47Pjs7Pjt0PHA8cDxsPEJhY2tJbWFnZVVybDs%2BO2w8aHR0cDovL3d3dy55Y2p3LnpqdXQuZWR1LmNuLy9pbWFnZXMvYmcxLmdpZjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8QmFja0ltYWdlVXJsOz47bDxodHRwOi8vd3d3Lnljancuemp1dC5lZHUuY24vL2ltYWdlcy9iZzEuZ2lmOz4%2BOz47Oz47Pj47dDx0PDt0PGk8Mz47QDwtLeeUqOaIt%2Bexu%2BWeiy0tO%2BaVmeW4iDvlrabnlJ87PjtAPC0t55So5oi357G75Z6LLS075pWZ5biIO%2BWtpueUnzs%2BPjs%2BOzs%2BOz4%2BOz4%2BO2w8SW1nX0RMOz4%2Bqmizg8nuU1ebhUFzNA%2Fqu71sECk%3D&Cbo_LX=%D1%A7%C9%FA&Txt_UserName=201010800314&Txt_Password=921021&Img_DL.x=26&Img_DL.y=3";
            //string returnVal = PostData(requestPara.ToString(), uri);
            //this.webBrowser1.DocumentText = returnVal;

            MessageBox.Show("test", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return;
        }

        public static string PostData(string requestPara,string uri)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            WebRequest hr = HttpWebRequest.Create(uri);

            byte[] buf = Encoding.GetEncoding("unicode").GetBytes(requestPara);
            hr.ContentType = "application/x-www-form-urlencoded";
            hr.ContentLength = buf.Length;
            hr.Method = "POST";

            Stream requestStream = hr.GetRequestStream();
            requestStream.Write(buf, 0, buf.Length);
            requestStream.Close();

            WebResponse response = hr.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string returnVal = reader.ReadToEnd();
            reader.Close();
            response.Close();

            return returnVal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BLL_Kufang bllKufang = new BLL_Kufang();
            bsKufang.DataSource = bllKufang.GetKufangInfo();
            comboBox1.DisplayMember = "kufangname";
            comboBox1.ValueMember = "localcode";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.label1.Text = this.comboBox1.SelectedItem + "         " + this.comboBox1.SelectedValue;
        }
    }
}
