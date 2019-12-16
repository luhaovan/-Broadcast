using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;

namespace BroadCast
{
    class   ListViewEx : ListView
    {
        public int columncount;
        public bool blnHasCheckBox = false;
        public ListViewEx()
        {
            InitListView();
            this.columncount = 0;
            blnHasCheckBox = false;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        }
        public void InitListView()
        {
            ImageList imageList = new ImageList();//
            imageList.ImageSize = new System.Drawing.Size(1, 22);
            this.View = System.Windows.Forms.View.Details;
            this.GridLines = true;  //显示网格线
            this.FullRowSelect = true;  //显示全行
            this.MultiSelect = false;  //设置只能单选
            this.OwnerDraw = true;
            this.SmallImageList = imageList;
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(listview_DrawColumnHeader);
            this.DrawItem += new DrawListViewItemEventHandler(listview_DrawItem);
            this.DrawSubItem += new DrawListViewSubItemEventHandler(listview_DrawSubItem);
        }
        public void setHasCheckBox(bool bln)
        {
            this.blnHasCheckBox = bln;
            this.CheckBoxes = true;
        }
        public void ColumnAdd(string text, int iwidth, HorizontalAlignment TextAlign)
        {
            ColumnHeader ch = new ColumnHeader();
            ch.Text = text;
            ch.Width = iwidth;
            ch.TextAlign = TextAlign;
            this.Columns.Add(ch);
        }
        public void InsertItem(int iIndex, string str)
        {
            ListViewItem item1 = new ListViewItem(str, iIndex);
            //item1.CheckBox
            item1.Text = str;
            for (int i = 0; i < Columns.Count - 1; i++)
            {
                item1.SubItems.Add("");
            }
            Items.Add(item1);
        }
        public void SetItemText(int iIndex, int isub, string str)
        {
            if (isub < Columns.Count)
                this.Items[iIndex].SubItems[isub].Text = str;
              
        }
        private void listview_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            int tColumnCount;
            Rectangle tRect = new Rectangle();
            Point tPoint = new Point();
            Font tFont = new Font("宋体", 11, FontStyle.Regular);
            SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(15, 104, 153));
            SolidBrush tFtontBrush;
            tFtontBrush = new SolidBrush(System.Drawing.SystemColors.GradientActiveCaption);
            if (this.Columns.Count == 0)
                return;
            tColumnCount = this.Columns.Count;
            this.columncount = this.Columns.Count;
            tRect.Y = 0;
            tRect.Height = e.Bounds.Height - 1;

            //AutoScrollMinSize = new Size(500, 500);
            tPoint.Y = 3;
            for (int i = 0; i < tColumnCount; i++)
            {
                if (i == 0)
                {
                    tRect.X = 0;
                    tRect.Width = this.Columns[i].Width;
                }
                else
                {
                    tRect.X += tRect.Width;
                    tRect.X += 1;
                    tRect.Width = this.Columns[i].Width - 1;
                }

                e.Graphics.FillRectangle(tBackBrush, tRect);
                tPoint.X = tRect.X + 3;
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(this.Columns[i].Text, tFont, tFtontBrush, tRect, format);
            }
        }
        void listview_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Point tPoint = new Point();
            SolidBrush tFrontBrush = new SolidBrush(Color.Black);
            Font tFont = new Font("宋体", 9, FontStyle.Regular);
            tPoint.X = e.Bounds.X + 3;
            tPoint.Y = e.Bounds.Y + 2;
            if ((e.State & ListViewItemStates.Selected) != 0)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), e.Bounds);
        }
        void listview_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            //bool blnSelected = false;
            SolidBrush tFrontBrush = new SolidBrush(Color.Black);
            // if (e.Item.Selected)
            // {
            // blnSelected = true;
            //    tFrontBrush = new SolidBrush(Color.Red);
            // }
            Point tPoint = new Point();
            //SolidBrush tFrontBrush = new SolidBrush(Color.Black);
            Font tFont = new Font("宋体", 12, FontStyle.Regular);
            int ih = tFont.Height;
            //tFont.GetHeight
            tPoint.X = e.Bounds.X + 3;
            tPoint.Y = e.Bounds.Y + 2;
            //if (blnSelected)//
            //   e.Graphics.DrawString(e.SubItem.Text, tFont, tFrontBrush, tPoint);
            // else
            if (e.ItemIndex % 2 == 1 && !e.Item.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), e.Bounds);
            }
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            //format.
            //if(e.ColumnIndex==1)
            if (Columns[e.ColumnIndex].TextAlign == HorizontalAlignment.Center)
                format.Alignment = StringAlignment.Center;

            TextFormatFlags textFormatFlags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis;
            if (e.Item.SubItems[0] == e.SubItem && blnHasCheckBox)
            {
                Size sizeCheckbox = CheckBoxRenderer.GetGlyphSize(e.Graphics, e.Item.Checked ?
                    System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                    System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                Point potCheckbox = new Point(e.Bounds.X + 2, e.Bounds.Top + 3);
                if (e.Item.Text != "")
                {
                    Rectangle rectText = new Rectangle(e.Bounds.X + sizeCheckbox.Width + 2, e.Bounds.Y, this.Columns[0].Width - sizeCheckbox.Width - 2, e.Bounds.Height - ih / 2);
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, potCheckbox, rectText, e.Item.Text, tFont, textFormatFlags, false, e.Item.Checked ?
                        System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                }
                else
                {
                    int iBoxwidth = sizeCheckbox.Width;
                    Rectangle rectText = new Rectangle(e.Bounds.X + e.Bounds.Width / 2 - iBoxwidth, e.Bounds.Y, iBoxwidth, e.Bounds.Height);
                    //potCheckbox.X = e.Bounds.X + e.Bounds.Width / 2 - iBoxwidth/2;
                    //potCheckbox.Y = e.Bounds.Y;
                    //CheckBoxRenderer.Set
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, potCheckbox, rectText, e.Item.Text, tFont, textFormatFlags, false, e.Item.Checked ?
                     System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                }

            }
            else
            {

                //this.Columns[e.SubItem.].Text
                //if(e.SubItem.Bounds.Width<1000)
                //Rectangle rct = new Rectangle(e.Bounds.Left, e.Bounds.Top-3, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawString(e.SubItem.Text, tFont, tFrontBrush, e.Bounds, format);
            }

            //else {
            //    e.DrawText(textFormatFlags);
            //}

        }
        public void setRect(Rectangle rect)
        {
            this.SetBounds(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        public void list_MouseDown(object sender, MouseEventArgs e)
        {
            //ListViewItem lvi = this.GetItemAt(e.X, e.Y);
            //ListViewItem.ListViewSubItem lvsi = lvi.GetSubItemAt(e.X, e.Y);
            //Rectangle rect = lvsi.Bounds;
            //txtbox.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
            // txtbox.Text = lvsi.Text;
            // txtbox.Show();
            //Point tPoint = new Point();
            //tPoint.X = e.X;
            //tPoint.Y = e.Y;

        }
        public void listview_selectedchanged(object sender, EventArgs e)
        {
            int length = this.SelectedItems.Count;
            if (length == 0)
                return;
            // Point tPoint = new Point();
            //tPoint.X = e.Bounds.X;
            //int i = FocusedItem.Index;
            // int j = this.SelectedItems[0].Index;
            // Rectangle rect = this.SelectedItems[0].SubItems[j].Bounds;
            // txtbox.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
            // txtbox.Show();
            //Rectangle rect=this.SelectedItems[0].SubItems[0
            //for (int i = 0; i < length; i++)
            //{
            //  string j = (this.SelectedItems[0].Index + 1).ToString();
            // MessageBox.Show(i+","+j);
            // }
        }
    }
}
