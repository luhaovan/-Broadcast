using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BroadCast
{
    public partial class Frm_Notification : MaterialForm
    {
        MainForm mainfrm = new MainForm();
        private readonly MaterialSkinManager materialSkinManager;

        public Frm_Notification()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;

            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
        }

        public Frm_Notification(MainForm mainFrm)
            : this()
        {
            this.mainfrm = mainFrm;
            lbl_BasicInfo.Text = mainfrm.StringId + "号   " + mainfrm.StringName;
            //strdate = mainfrm.StringDate;

        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (rtx_Text.Text.Trim().Length > 0)
            {
                FileStream fs = new FileStream("C:\\Users\\Administrator.USER-20190712MM\\Desktop\\log.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                string strdate = getWebTime().ToString();
                List<List<string>> array1 = mainfrm.ArraryStaff;
                for (int i = 0; i < array1.Count; i++)
                {
                    //MessageBox.Show(i.ToString());
                    sw.WriteLine(strdate + "\t To:" + array1[i][0] + "    " + this.rtx_Text.Text.Trim());//开始写入值 
                }
                sw.Close();
                fs.Close();
                MessageBox.Show("已发送");
            }
            else
            {
                MessageBox.Show("请输入内容");
            }
        }
        #region GetWebTime

        public DateTime getWebTime()
        {
            // default ntp server
            const string ntpServer = "ntp1.aliyun.com";

            // NTP message size - 16 bytes of the digest (RFC 2030)
            byte[] ntpData = new byte[48];
            // Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; // LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
            foreach (var item in addresses)
            {
                Debug.WriteLine("IP:" + item);
            }

            // The UDP port number assigned to NTP is 123
            IPEndPoint ipEndPoint = new IPEndPoint(addresses[0], 123);

            // NTP uses UDP
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Connect(ipEndPoint);
            // Stops code hang if NTP is blocked
            socket.ReceiveTimeout = 3000;
            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            // Offset to get to the "Transmit Timestamp" field (time at which the reply 
            // departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;
            // Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            // Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            // Convert From big-endian to little-endian
            intPart = swapEndian(intPart);
            fractPart = swapEndian(fractPart);
            ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000UL);

            // UTC time
            DateTime webTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds);
            // Local time
            return webTime.ToLocalTime();
        }

        private uint swapEndian(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
            ((x & 0x0000ff00) << 8) +
            ((x & 0x00ff0000) >> 8) +
            ((x & 0xff000000) >> 24));
        }
        #endregion
    }
}
