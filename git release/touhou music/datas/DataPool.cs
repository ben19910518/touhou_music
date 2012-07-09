using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace touhou_music.datas
{   
    class DataPool
    {
        public static string server = "58.221.49.107";//"127.0.0.1"; //
       //public static string server = ".";
    //    private static string database = "touhou music";
    //    private static string uid = "sa";
   //     private static string password = "";
        public static string conString = "Data Source="+server+";Initial Catalog=touhou music;User ID=thviewer";
        public static string conkamiro = "Data Source=" + server + ";Initial Catalog=AnimeData;User ID=thviewer";
        //10.2.10.5,1433
        public static string currentID = "";
        public static string currentMD5Password = "";
        public static string currentAutho = "";
        public static Form1 Form1;
        public static Form3 Form3;
        //密码: 字母+数字 5到18位
        public static string passwordReg = "^[A-Za-z0-9]{4,19}$";

        //用户名：字母中文下划线开头,后面内容小写大写中文数字下划线
        public static string nameReg = "^[a-zA-Z\xa0-\xff_][0-9a-zA-Z\xa0-\xff_]{3,19}$"; 
    }

}
