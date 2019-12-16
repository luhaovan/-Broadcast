using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using BroadCast;
using System.Collections;
using System.Threading;
using System.Linq;
using System.Collections.Generic;


namespace BroadCast
{
    public partial class MainForm : MaterialForm
    {
        #region Ini
        pubfunction pubfun;
        ArrayList userinfo;
        string userid;
        string username;
        
        List<List<string>> array = new List<List<string>>();
        #endregion

        private readonly MaterialSkinManager materialSkinManager;
        public MainForm()
        {
            InitializeComponent();
            lblTime.Text = getWebTime().ToString();
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            pubfun = new pubfunction();
            userinfo = new ArrayList();
            onLoadUserInfo();
            // Add dummy data to the listview
            //seedListView();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
            Thread onload = new Thread(onLoadTable);
            onload.Start();
        }

        #region LoadTable & AddList

        private void onLoadTable()
        {
            if (userlistView != null)
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                pubfun = new pubfunction();

                Thread thread = new Thread(onLoadUserInfo);
                thread.Start();

                userlistView.ColumnAdd("序号", 70, HorizontalAlignment.Center);
                userlistView.ColumnAdd("用户编号", 90, HorizontalAlignment.Left);
                userlistView.ColumnAdd("用户名称", 90, HorizontalAlignment.Left);
                userlistView.ColumnAdd("信标IP", 90, HorizontalAlignment.Left);
                userlistView.ColumnAdd("部门", 90, HorizontalAlignment.Left);
                userlistView.ColumnAdd("职务", 90, HorizontalAlignment.Left);
                userlistView.ColumnAdd("性别", 70, HorizontalAlignment.Left);
                userlistView.ColumnAdd("手机号", 180, HorizontalAlignment.Left);

                //searchlistView.ColumnAdd("序号", 30, HorizontalAlignment.Center);
                //listQuery.ColumnAdd("空", 0, HorizontalAlignment.Center);
                listQuery.ColumnAdd("序号", 70, HorizontalAlignment.Center);
                listQuery.ColumnAdd("用户编号", 90, HorizontalAlignment.Left);
                listQuery.ColumnAdd("用户名称", 90, HorizontalAlignment.Left);
                listQuery.ColumnAdd("信标IP", 90, HorizontalAlignment.Left);
                listQuery.ColumnAdd("部门", 90, HorizontalAlignment.Left);
                listQuery.ColumnAdd("职务", 90, HorizontalAlignment.Left);
                listQuery.ColumnAdd("性别", 70, HorizontalAlignment.Left);
                listQuery.ColumnAdd("手机号", 180, HorizontalAlignment.Left);
                listQuery.setHasCheckBox(false);
            }
        }

        private void onLoadUserInfo()
        {
            string strurl = common.strBsUrl + "/remote/ReaddataSlv?funname=UserInfo";
            string content = pubfun.GetContentFromUrl(strurl);
            common.pUserList.Clear();
            ArrayList alst = pubfun.splitStrToArray2(content, "</item>");
            for (int i = 0; i < alst.Count; i++)
            {
                string str = alst[i] as string;
                string userno = pubfun.getItemValue(str, "userno");
                if (userno != "")
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("userno", userno);
                    ht.Add("username", pubfun.getItemValue(str, "username"));
                    ht.Add("usersex", pubfun.getItemValue(str, "usersex"));
                    ht.Add("telno", pubfun.getItemValue(str, "telno"));
                    userinfo.Add(userno);
                    common.pUserList.Add(ht);
                }
            }
            userlistView.Items.Clear();
            for (int i = 0; i < common.pUserList.Count; i++)
            {
                Hashtable ht = common.pUserList[i] as Hashtable;
                userlistView.InsertItem(i, (i + 1) + "");
                userlistView.SetItemText(i, 1, (string)ht["userno"]);
                userlistView.SetItemText(i, 2, (string)ht["username"]);
                userlistView.SetItemText(i, 6, (string)ht["usersex"]);
                userlistView.SetItemText(i, 7, (string)ht["telno"]);
            }
        }
        #endregion

        #region TransValue
        public string StringName
        {
            get { return username; }
        }

        public string StringId
        {
            get { return userid; }
        }

        public List<System.Collections.Generic.List<string>> ArraryStaff
        {
            get { return array; }
        }
        #endregion

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

        private void userlistView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            userid = this.userlistView.FocusedItem.SubItems[1].Text;
            username = this.userlistView.FocusedItem.SubItems[2].Text;
            Frm_Notification test = new Frm_Notification(this);
            test.ShowDialog();
            //notification.ShowDialog();
        }

        private void subItem1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userid = this.userlistView.FocusedItem.SubItems[1].Text;
            username = this.userlistView.FocusedItem.SubItems[2].Text;
            Frm_Notification test = new Frm_Notification(this);
            test.ShowDialog();
        }
        
        private void btnFresh_Click(object sender, EventArgs e)
        {
            Thread onload = new Thread(onLoadUserInfo);
            onload.Start();
        }

        #region Tab_2
        private void btnSend_Click(object sender, EventArgs e)
        {
            array.Clear();
            if (listQuery.Items.Count > 0)
            {
                for (int nColOrder = 0; nColOrder < listQuery.Items.Count; nColOrder++)
                {
                    userid = this.listQuery.Items[nColOrder].SubItems[1].Text;
                    username = this.listQuery.Items[nColOrder].SubItems[2].Text;
                    addStaff(userid, username);
                }
                //if (array.Count == 2)
                //MessageBox.Show(array[0][0] + array[1][0]);
                //if (array.Count == 4)
                //MessageBox.Show(array[0][0] + array[1][0] + array[2][0]+ array[3][0]);

                // 更改排列方式
                Frm_Notification test = new Frm_Notification(this);
                test.ShowDialog();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Thread searchthread = new Thread(new ThreadStart(CompressAll));
            searchthread.IsBackground = true;
            searchthread.Start();
        }
        #endregion

        private void addStaff(string userid, string username)
        {
            #region TestMode

            List<string> item = new List<string>(new string[] { userid + username });
            array.Add(item);
            #endregion
        }

        #region SearchProcess
        private delegate void DelegateSearchList(bool _isOk, string userid);
        private void SearchList(bool _isOk, string userid)
        {
            if (_isOk && userid == "All")
            {
                MessageBox.Show("All");
                listQuery.Items.Clear();
                foreach (ListViewItem item in userlistView.Items)
                    listQuery.Items.Add((ListViewItem)item.Clone());
            }
            if (_isOk && userid != "All")
            {
                listQuery.Items.Clear();
                for (int index = 0; index < userlistView.Items.Count; index++)
                {
                    ListViewItem foundItem = userlistView.FindItemWithText(userid, true, index, true);
                    if (foundItem != null)
                    {
                        //MessageBox.Show(foundItem.Index.ToString());
                        listQuery.Items.Add((ListViewItem)foundItem.Clone());
                        index = foundItem.Index;
                    }
                }
            }
        }

        private void CompressAll()
        {
            // 判断是否需要Invoke，多线程时需要
            if (this.InvokeRequired)
            {
                // 通过委托调用写主线程控件的程序，传递参数放在object数组中
                this.Invoke(new DelegateSearchList(SearchList),
                                new object[] { true, cmbStaffId.Text.ToString() });
            }
            else
            {
                // 如果不需要委托调用，则直接调用
                this.SearchList(true, cmbStaffId.Text.ToString());
            }
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblTime.Text = dt.ToString("yy-MM-dd HH:MM:ss");
            //lblTime.Text = getWebTime().ToString();
        }
    }
}
