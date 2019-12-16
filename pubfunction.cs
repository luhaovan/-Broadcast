using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Drawing;

//using MSScriptControl;
using System.Text.RegularExpressions;
namespace BroadCast
{
    public class pubfunction
    {
        public string GetContentFromUrl(string URL)
        {
            string strBuff = "";
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                Stream respStream = httpResp.GetResponseStream();
                StreamReader respStreamReader = new StreamReader(respStream, System.Text.Encoding.Default);
                //byteRead = respStreamReader.Read(cbuffer, 0, 256);
                string strline = respStreamReader.ReadLine();
                while (strline != null)
                {
                    strBuff = strBuff + strline;
                    strline = respStreamReader.ReadLine();
                }
                respStream.Close();
                if (strline != "" && strline != null)
                    strBuff = strBuff + strline;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return strBuff;
        }


        public void DownLoadFromUrl(string url, string filename)
        {
            try
            {
                string filepath = getModulePath() + "\\mapfile\\" + filename;
                if (File.Exists(filepath))
                    return;
                url = url + "?filename=" + getOXCode(filename);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                Stream respStream = httpResp.GetResponseStream();
                FileStream fs = new FileStream(getModulePath() + "\\mapfile\\" + filename, FileMode.Create);
                byte[] cbuffer = new byte[256];
                int byteRead = 0;
                byteRead = respStream.Read(cbuffer, 0, 256);
                if (byteRead > 0)
                    fs.Write(cbuffer, 0, byteRead);
                while (byteRead > 0)
                {
                    byteRead = respStream.Read(cbuffer, 0, 256);
                    fs.Write(cbuffer, 0, byteRead);
                }
                fs.Close();
                respStream.Close();
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }
        public void DownLoadFromUrl2(string url, string filename)
        {
            try
            {
                string filepath = getModulePath() + "\\images\\" + filename;
                if (File.Exists(filepath))
                    return;
                url = url + "?filename=" + getOXCode(filename);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                Stream respStream = httpResp.GetResponseStream();
                FileStream fs = new FileStream(getModulePath() + "\\images\\" + filename, FileMode.Create);
                byte[] cbuffer = new byte[256];
                int byteRead = 0;
                byteRead = respStream.Read(cbuffer, 0, 256);
                if (byteRead > 0)
                    fs.Write(cbuffer, 0, byteRead);
                while (byteRead > 0)
                {
                    byteRead = respStream.Read(cbuffer, 0, 256);
                    fs.Write(cbuffer, 0, byteRead);
                }
                fs.Close();
                respStream.Close();
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }
        public void UploadFileByUrl(string url, string filename)
        {
            string strcontent = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "text/html; charset=GBK";// "multipart/form-data;";
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;
                Stream stream = request.GetRequestStream();
                FileStream fs = new FileStream(filename, FileMode.Open);
                byte[] byts = new byte[1024];
                int nRead = 0;

                nRead = fs.Read(byts, 0, 1024);
                if (nRead > 0)
                    stream.Write(byts, 0, nRead);
                while (nRead > 0)
                {
                    nRead = fs.Read(byts, 0, 1024);
                    if (nRead > 0)
                        stream.Write(byts, 0, nRead);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamres = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                string line = "";

                line = streamres.ReadLine();
                if (line != null)
                {
                    strcontent = strcontent + line;
                    line = streamres.ReadLine();
                }
                fs.Close();
                stream.Close();
                streamres.Close();
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            MessageBox.Show(strcontent);
        }
        public void memset(byte[] byts, int val)
        {
            for (int i = 0; i < byts.Length; i++)
            {
                byts[i] = 0;
            }
        }
        public string UploadStrByUrl(string URL, string strcontent)
        {
            string strBuff = "";
            try
            {
                int byteRead = 0;
                char[] cbuffer = new char[256];
                //MessageBox.Show("length=");
                //string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(URL);
                //HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                httpReq.Referer = "";
                //httpReq.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //httpReq.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                //httpReq.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                //httpReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                httpReq.ContentType = "text/html; charset=GBK";// "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.Default;//.UTF8;//
                httpReq.Method = "post";
                httpReq.KeepAlive = true;
                httpReq.Credentials = CredentialCache.DefaultCredentials;
                //MessageBox.Show("length=");
                using (Stream stream = httpReq.GetRequestStream())
                {
                    //MessageBox.Show("length=");
                    byte[] byteArray = encoding.GetBytes(strcontent);//System.Text.Encoding.ASCII.GetBytes(strcontent);
                    //MessageBox.Show("length=" + byteArray.Length);
                    stream.Write(byteArray, 0, byteArray.Length);
                }
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                Stream respStream = httpResp.GetResponseStream();
                StreamReader respStreamReader = new StreamReader(respStream, System.Text.Encoding.Default);
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
                while (byteRead != 0)
                {
                    string strResp = new string(cbuffer, 0, byteRead);
                    strBuff = strBuff + strResp;
                    byteRead = respStreamReader.Read(cbuffer, 0, 256);
                }
                respStream.Close();
                //stream.Write
                //byte[] con=strcontent.get
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return strBuff;
        }
        public string getItemValue(string strcontent, string strtag)
        {
            string strvalue = "";
            string strnode = "<" + strtag + ">";
            string strnode2 = "</" + strtag + ">";
            if (strcontent.IndexOf(strnode) >= 0)
            {
                int ibeg = strcontent.IndexOf(strnode) + strnode.Length;

                strcontent = strcontent.Substring(ibeg);
                int iend = strcontent.IndexOf(strnode2);
                strvalue = strcontent.Substring(0, iend);
                //strcontent.
                //MessageBox.Show(strvalue);
            }
            return strvalue;
        }
        public string getModulePath()
        {
            string strpath = "";
            strpath = System.Environment.CurrentDirectory;
            //MessageBox.Show("current path :" + strpath);
            //  bin/debug
            return strpath;
        }
        public ArrayList splitStrToArray2(string strcontent, string split)
        {
            ArrayList alst = new ArrayList();
            int iloc = strcontent.IndexOf(split);
            if (iloc < 0)
                alst.Add(strcontent);
            while (iloc > 0)
            {
                String stritem = strcontent.Substring(0, iloc);
                alst.Add(stritem);
                int ibeg = iloc + split.Length;
                strcontent = strcontent.Substring(ibeg, strcontent.Length - ibeg);
                //MessageBox.Show(stritem);
                iloc = strcontent.IndexOf(split);
            }
            alst.Add(strcontent);
            return alst;
        }
        public ArrayList splitStrToArray(string strcontent, string split)
        {
            ArrayList alst = new ArrayList();
            string[] sArray = Regex.Split(strcontent, split, RegexOptions.IgnoreCase);
            for (int i = 0; i < sArray.Length; i++)
                alst.Add(sArray[i]);
            return alst;
        }
        public string getdatetime()
        {
            DateTime date = DateTime.Now;
            string format = "yyyy-MM-dd HH:mm:ss";
            string strvalue = date.ToString(format, DateTimeFormatInfo.InvariantInfo);

            return strvalue;
        }
        public string getdate()
        {
            //string strvalue = "";
            DateTime date = DateTime.Now;
            string format = "yyyy-MM-dd";
            string strvalue = date.ToString(format, DateTimeFormatInfo.InvariantInfo);
            return strvalue;
        }
        public string getchdate()
        {
            DateTime date = DateTime.Now;
            string format = "yyyy年MM月dd日";
            string strvalue = date.ToString(format, DateTimeFormatInfo.InvariantInfo);
            string dt = date.DayOfWeek.ToString();
            string week = dt;
            switch (dt)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
            }
            return strvalue + "  " + week;
        }
        public int getJSeconds(string strdate)
        {
            //DateTime date1 = DateTime.Parse("1970-01-01 08:00:00");
            //DateTime date2=DateTime.Parse(strdate);
            int ivalue = getBetSeconds("1970-01-01 08:00:00", strdate);

            return ivalue;
        }
        public int getBetSeconds(string strdate1, string strdate2)
        {
            DateTime date1 = DateTime.Parse(strdate1);
            DateTime date2 = DateTime.Parse(strdate2);
            TimeSpan ts = date2.Subtract(date1);
            int ivalue = (int)ts.TotalSeconds;

            //date1.
            //string nformat = "";
            return ivalue;
        }
        public string getNewDateTime(string strdate, int iseconds)
        {
            string strvalue = "";
            DateTime date = DateTime.Parse(strdate);
            string format = "yyyy-MM-dd HH:mm:ss";
            string strvalue1 = date.ToString(format, DateTimeFormatInfo.InvariantInfo);

            date = date.AddSeconds(iseconds);

            strvalue = date.ToString(format, DateTimeFormatInfo.InvariantInfo);
            //MessageBox.Show(strvalue1 + "," + strvalue+","+iseconds);
            return strvalue;
        }
        public string GetStandStr(string strvalue, int ilen)
        {
            string str = strvalue;
            int ibeg = strvalue.Length;
            for (int i = ibeg; i < ilen; i++)
            {
                str = " " + str;
            }
            return str;
        }
        public Color GetColorByIndex(int iIndex)
        {
            Color rgb = Color.FromName("#FF0000");
            switch (iIndex)
            {
                case 0:
                    rgb = RGB(0xFF, 0x00, 0x00);
                    break;
                case 1:
                    rgb = RGB(215, 215, 0);

                    break;
                case 2:
                    rgb = RGB(0x8B, 0x87, 0x73);
                    break;
                case 3:
                    rgb = RGB(0x00, 0x80, 0x40);
                    break;
                case 4:
                    rgb = RGB(0x80, 0x00, 0x80);
                    break;
                case 5:
                    rgb = RGB(0x33, 0x5C, 0x30);
                    break;
                case 6:
                    rgb = RGB(0x2F, 0x7A, 0x8F);
                    break;
                case 7:
                    rgb = RGB(0x9B, 0x56, 0x0B);
                    break;
                case 8:
                    rgb = RGB(0xDE, 0xBB, 0x95);
                    break;
                case 9:
                    rgb = RGB(0x0C, 0x1E, 0x2C);
                    break;
                default:
                    break;

            }
            return rgb;
        }
        public Color RGB(int ir, int ig, int ib)
        {
            return Color.FromArgb(ir, ig, ib);
        }
        public Rectangle SetRect(int x1, int y1, int x2, int y2)
        {
            Rectangle rect;
            int iwidth = x2 - x1;
            int iheight = y2 - y1;
            int x = x1;
            int y = y1;
            if (iwidth < 0)
            {
                iwidth = -iwidth;
                x = x2;
            }
            if (iheight < 0)
            {
                iheight = -iheight;
                y = y2;
            }
            rect = new Rectangle(x, y, iwidth, iheight);

            return rect;

        }
        public Rectangle SetRect(int x1, int y1, int x2, int y2, int offex)
        {
            Rectangle rect;
            int iwidth = x2 - x1;
            int iheight = y2 - y1;
            int x = x1;
            int y = y1;
            if (iwidth < 0)
            {
                iwidth = -iwidth;
                x = x2;
            }
            if (iheight < 0)
            {
                iheight = -iheight;
                y = y2;
            }
            rect = new Rectangle(x - offex, y - offex, iwidth + offex * 2, iheight + offex * 2);

            return rect;

        }
        public Rectangle SetRect(Point pnt1, Point pnt2)
        {
            Rectangle rect;
            int x1 = pnt1.X;
            int x2 = pnt2.X;
            int y1 = pnt1.Y;
            int y2 = pnt2.Y;
            rect = SetRect(x1, y1, x2, y2);

            return rect;

        }
        public bool PtInRect(Rectangle rect, Point pnt)
        {
            bool bln = false;
            if (pnt.X > rect.Left &&
                pnt.Y > rect.Top &&
                pnt.X < rect.Right &&
                pnt.Y < rect.Bottom)
                bln = true;
            return bln;
        }
        public string getDateTimeBySeconds(int iseconds)
        {
            return getNewDateTime("1970-01-01 08:00:00", iseconds);
        }
        public int getMiusPlus(int x)
        {
            int ivalue = 1;
            if (x < 0)
                ivalue = -1;
            return ivalue;

        }
        public void OnMoveForm(System.Windows.Forms.Form frm, Rectangle rect)
        {
            frm.SetBounds(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public Point GetCenterPoint(Point[] p)
        {
            Point ptCenter = new Point(0, 0);
            int i, j;
            double ai, atmp = 0, xtmp = 0, ytmp = 0;
            if (p == null)
                throw new ArgumentNullException("获取多边形中心点坐标时传入的参数为空。");
            if (p.Length == 1)
                return p[0];
            if ((p.Length == 2) || (p.Length == 3 && p[0] == p[2]))
                return new Point((p[1].X + p[0].X) / 2, (p[1].Y + p[0].Y) / 2);
            int n = p.Length;
            for (i = n - 1, j = 0; j < n; i = j, j++)
            {
                //MessageBox.Show("i=" + i + ",j=" + j);
                ai = p[i].X * p[j].Y - p[j].X * p[i].Y;
                atmp += ai;
                xtmp += (p[j].X + p[i].X) * ai;
                ytmp += (p[j].Y + p[i].Y) * ai;
            }
            if (atmp != 0)
            {
                ptCenter.X = Convert.ToInt32(xtmp / (3 * atmp));
                ptCenter.Y = Convert.ToInt32(ytmp / (3 * atmp));
            }
            return ptCenter;
        }
        public double getDistance(Point pnt1, Point pnt2)
        {
            double dist = Math.Sqrt((pnt1.X - pnt2.X) * (pnt1.X - pnt2.X) + (pnt1.Y - pnt2.Y) * (pnt1.Y - pnt2.Y));
            return dist;
        }
        public string getListString(ArrayList strlist)
        {
            string strcontent = "";
            for (int i = 0; i < strlist.Count; i++)
            {
                string str = (string)strlist[i];
                strcontent = strcontent + str + "";
            }
            return strcontent;
        }
        public bool blnExistIsArrayList(ArrayList strlist, string val)
        {
            bool bln = false;
            for (int i = 0; i < strlist.Count; i++)
            {
                string str = (string)strlist[i];
                if (str == val)
                    bln = true;
            }
            return bln;

        }
        public string getHashTableVal(Hashtable ht, string strname)
        {
            string str = "";
            if (ht[strname] != null)
                str = ht[strname].ToString();
            return str;
        }
        public string getFileTitle(string filepath)
        {
            string strtitle = "";
            filepath = filepath.Replace("\\", "/");
            ArrayList alst = splitStrToArray(filepath, "/");
            if (alst.Count > 0)
            {
                strtitle = (string)alst[alst.Count - 1];
            }
            return strtitle;
        }
        public string getOXCode(string str)
        {
            StringBuilder sb = new StringBuilder();
            Encoding encode = Encoding.GetEncoding("GB2312");
            if (str == null)
                return null;
            byte[] arr = encode.GetBytes(str);
            foreach (byte b in arr)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            //string str = sb.ToString();

            return sb.ToString();
        }

        public void CreateFolder(string foldername)
        {
            string strpath = getModulePath() + "\\" + foldername;
            if (false == System.IO.Directory.Exists(strpath))
            {
                System.IO.Directory.CreateDirectory(strpath);
            }
        }
    }

}
