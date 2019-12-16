using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//using System.ComponentModel;
namespace BroadCast
{
    public enum ToolType { ScaleNode, MsgNode, StationNode, MeasureNode, LabelNode, NorthNode, None };
    public static class common
    {
        public static ArrayList pLocNodeList = new ArrayList();
        public static ArrayList pStationList = new ArrayList();
        public static ArrayList pAssetList = new ArrayList();
        public static ArrayList pTraceNodeList = new ArrayList();
        public static ArrayList pUserList = new ArrayList();
        public static ArrayList pMapList = new ArrayList();
        public static double dbmapscale = 16.14;//2.43;//22;//1.78;
        //public static string imgpath = "images/view.jpg";
        public static string imgpath = "mapfile/view.jpg";
        public static string mapname = "SM广场A区一楼";
        public static string strOriUrl = "http://39.98.183.234:88/m2m/"; //"http://39.108.122.50:88/m2m/";
        public static string strBsUrl = "http://39.98.183.234:88/m2m/";// "http://39.108.122.50:88/m2m/";//"http://27.154.230.22:81/m2m/";
        //code128 barcode = new code128();
    }
}
