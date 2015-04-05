using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Data;
using System.Windows.Forms;

namespace StringToByte
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringToByte();
            //TestSubstring();
            //TestWriteFile();
            //TestStringFormat();
            //TestDateTime();
            //TestDecimal();
            //TestPost();
            //TestGetString();
            //TestIP();
            //TestDataTable();
            TestMessageBox();
        }

        private static void TestMessageBox()
        {
            int accountBalance=66;
            DialogResult r1= MessageBox.Show(string.Format("Account Balance: {0}", accountBalance), "Attention !", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
            if (r1==DialogResult.OK)
            {
                MessageBox.Show("OK", "Attention:", MessageBoxButtons.OK,MessageBoxIcon.Stop,MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                MessageBox.Show("Cancel", "Attention:", MessageBoxButtons.RetryCancel);
            }
        }

        private static void TestDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("name", typeof(string)),
                                         new DataColumn("sex", typeof(string)),
                                         new DataColumn("score", typeof(int)) });
            dt.Rows.Add(new object[] { "张三", "男", 1 });
            dt.Rows.Add(new object[] { "张三", "男", 4 });
            dt.Rows.Add(new object[] { "李四", "男", 100 });
            dt.Rows.Add(new object[] { "李四", "女", 90 });
            dt.Rows.Add(new object[] { "王五", "女", 77 });
            dt.Rows.Add(new object[] { "test", "female", 100 });
            DataTable dtResult = dt.Clone();
            DataTable dtName = dt.DefaultView.ToTable(true, "name", "sex");
            for (int i = 0; i < dtName.Rows.Count; i++)
            {
                DataRow[] rows = dt.Select("name='" + dtName.Rows[i][0] + "' and sex='" + dtName.Rows[i][1] + "'");
                //temp用来存储筛选出来的数据
                DataTable temp = dtResult.Clone();
                foreach (DataRow row in rows)
                {
                    temp.Rows.Add(row.ItemArray);
                }

                DataRow dr = dtResult.NewRow();
                for (int j = 0; j < temp.Columns.Count; j++)
                {
                    dr[j] = temp.Rows[0][j].ToString();
                    dr[temp.Columns.Count-1] = temp.Compute("sum(score)", ""); 
                }
                dtResult.Rows.Add(dr);
            }
        }

        private static void TestIP()
        {
            try
            {
                string hostname = Dns.GetHostName();
                IPHostEntry ipHostEntry = Dns.GetHostEntry(hostname);
                for (int i = 0; i < ipHostEntry.AddressList.Length; i++)
                {
                    if (ipHostEntry.AddressList[i].AddressFamily==AddressFamily.InterNetwork)
                    {
                        Console.WriteLine(ipHostEntry.AddressList[i].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void TestGetString()
        {
            Encoding encoding=Encoding.Default;
            string testA = "20150313105400";
            byte[] byteB = encoding.GetBytes(testA);
            int count = Convert.ToInt32(encoding.GetString(byteB, byteB.Length - 7, 6));
            Console.WriteLine(count);
        }

        private static void TestPost()
        {
            string uri = "https://www.baidu.com/s";
            //string requestPara = "{\"DdjgID\":\"100100\",\"SfzdID\":\"100100\",\"ypfl\":\"0000000001\",\"pzwh\":\"\",\"ypmc\":\"\"}";
            string requestPara = "{\"cl\":\"3\",\"tn\":\"baidutop10\",\"fr\":\"top1000\",\"wd\":\"国有林场改革\",\"rsv_idx\":\"2\"}";
            string returnVal = PostData(requestPara, uri);
        }

        public static string PostData(string requestPara, string uri)
        {
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

        private static void TestDecimal()
        {
            decimal testA = -1.254M;
            int targetLength = 10;
            int isNegative = 0;
            int addSize = targetLength - 3;
            if (isNegative==1)
            {
                addSize = targetLength - 4;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < addSize; i++)
            {
                sb.Append('0');
            }
            sb.Append(".00");
            string newNumber = testA.ToString(sb.ToString());
            Console.WriteLine(newNumber);
        }

        private static void TestDateTime()
        {
            DateTime dt = DateTime.Now;
            DateTime dt2 = DateTime.Parse("2015-02-01 10:21:30");
            string dts = "20150307/123456/";
            dt = DateTime.ParseExact(dts, "yyyyMMdd/HHmmss/", null);
            Console.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static void TestStringFormat()
        {
            decimal testA = 12.34567M;
            string ret = string.Format(new AccNumberFormat(), "{0:T}", testA);
            string ret2 = testA.ToString("F");
            int newint=string.Compare(ret, ret2);
        }

        public static void StringToByte()
        {
            string space = "2546657942";
            byte[] spaceInByte=new byte[10];
            Encoding coding = Encoding.GetEncoding("GB2312");
            space=VerifyCodeLength(space,10);
            int t = coding.GetBytes(space,0,10,spaceInByte,0);

            space="12345";
            space = VerifyCodeLength(space, 10);
            t = coding.GetBytes(space,0,10,spaceInByte,0);

            string space1 = "12";
            string space2 = "2345";
            space1 = VerifyCodeLength(space1, 5);
            space2 = VerifyCodeLength(space2, 5);
            StringBuilder sb = new StringBuilder(space1 + space2);
            t = coding.GetBytes(sb.ToString(), 0, sb.Length, spaceInByte, 0);

            DateTime dt = DateTime.Now;
            string tmpStr = dt.ToString("yyyyMMdd/HHmmss/");
            tmpStr = VerifyCodeLength(tmpStr, 16);
            byte[] dtByte = coding.GetBytes(tmpStr);

            Console.ReadLine();
        }

        /// <summary>
        /// 验证字符长度是否溢出
        /// </summary>
        /// <param name="strVerify"> 需要验证的代码 </param>
        /// <param name="byteLength"> 目标长度 </param>
        /// <returns></returns>
        public static string VerifyCodeLength(string strVerify, int byteLength)
        {
            int strVerifyLength = strVerify.Length;
            if (strVerifyLength == byteLength)
            {
                return strVerify;
            }
            else if (strVerifyLength < byteLength)
            {
                StringBuilder sb = new StringBuilder(strVerify);
                sb.Append(' ', byteLength - strVerifyLength);
                return sb.ToString();
            }
            else
            {
                throw new ArgumentOutOfRangeException("超出范围!");
            }
        }

        public  static void TestSubstring()
        {
            DateTime dt = DateTime.Now;
            string tmp = dt.ToString("yyyyMMdd/HHmmss/");
            Console.WriteLine(tmp.Substring(0, 8));
            Console.WriteLine(tmp.Substring(9, 6));
            string testSplit = "Just||Dance||In|| ||2015||";
            string[] strSplit = testSplit.Split(new string[]{"||"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void TestWriteFile()
        {
            string fileName = "WriteTest.txt";
            string filePath = @"D:\";
            string A = "2015-03-04";
            string C = "2015-03-05";
            string B = "2015-03-06";
            string D = "2015-03-07";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("A:{0}", A).Append('/');
            sb.AppendFormat("B:{0}", B).AppendLine();
            sb.AppendFormat("C;{0}", C).Append('/');
            sb.AppendFormat("D:{0}", D).AppendLine();

            FileStream fs = new FileStream(filePath + fileName, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(sb.ToString());
                sw.Flush();
            }

            //using (StreamWriter sw= new StreamWriter(fs2))
            //{
            //    sw.WriteLine(B);
            //    sw.Flush();
            //}
        }

    }

    public class AccNumberFormat : IFormatProvider, ICustomFormatter
    {
        private const int ACC_LENGTH = 12;
        public object GetFormat(Type formatType)
        {
            if (formatType==typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string fmt,object arg,IFormatProvider formatProvider)
        {
            //if (arg.GetType()!=typeof(Int64))
            //{
            //    try
            //    {
            //        return HandleOtherFormats(fmt, arg);
            //    }
            //    catch (FormatException e)
            //    {
            //        throw new FormatException(string.Format("TheFormatOf'{0}'isInvalid", fmt), e);
            //    }
            //}

            string ufmt = fmt.ToUpper(CultureInfo.InvariantCulture);
            if (!(ufmt=="H"||ufmt=="I"||ufmt=="T"))
            {
                try
                {
                    return this.HandleOtherFormats(fmt, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(string.Format("The Format of '{0}' is invalid.", fmt), e);
                }
            }

            string result = arg.ToString();
            string[] splitStr = result.Split('.');
            splitStr[1] = splitStr[1].Substring(0, 2);
            if (int.Parse(result.Split('.')[1].Substring(2,1))>=5)
            {
                splitStr[1] = (int.Parse(splitStr[1]) + 1).ToString();
            }
            return splitStr[0] + '.' + splitStr[1];

            //if (result.Length<ACC_LENGTH)
            //{
            //    result = result.Substring(ACC_LENGTH, '0');
            //}

            //if (result.Length>ACC_LENGTH)
            //{
            //    result = result.Substring(0, ACC_LENGTH);
            //}

            //if (ufmt=="I")
            //{
            //    return result;
            //}
            //else
            //{
            //    return result.Substring(0, 5) + "-" + result.Substring(5, 3) + "-" + result.Substring(8);
            //}
        }

        private string HandleOtherFormats(string fmt, object arg)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(fmt, CultureInfo.CurrentCulture);
            }
            else if (arg!=null)
            {
                return arg.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
