using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using touhou_music.datas;
using System.Diagnostics;
using System.IO;

namespace touhou_music
{
    public partial class Form2 : Form
    {
        private string tempgname;
        private string tempaname;
        private string tempsname;
        private string temparranger;
        private string templyric;
        private string tempvocal;
        private string tempstyle;
        private string temptime;
        private string temporigin;
        private string sqlstr;

        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.ShowDialog();



            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                string sql = "select count(scode) from gas ";
                int ssum = Convert.ToUInt16(sqladp.ExecuteScalar(sql));
                toolStripStatusLabel1.Text = "目前歌曲数：" + ssum;
                sql = "select count(username) from [user] ";
                int peoplesum = Convert.ToUInt16(sqladp.ExecuteScalar(sql));
                toolStripStatusLabel3.Text = "注册人数：" + peoplesum;
                sql = "select autho from [user] where username = '" + DataPool.currentID + "'";

                string autho = sqladp.ExecuteScalar(sql) as string;
                DataPool.currentAutho = autho;

                if (autho == "3")
                {
                    添加原曲ToolStripMenuItem.Enabled = false;
                    添加ToolStripMenuItem.Enabled = false;
                    删除ToolStripMenuItem.Enabled = false;
                    修改ToolStripMenuItem.Enabled = false;
                }
                else if (autho == "2")
                {
                    删除ToolStripMenuItem.Enabled = false;
                    修改ToolStripMenuItem.Enabled = false;
                }

                DataSet ds;
                sqladp.getDataSet("SELECT origin FROM ori",out ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
                }

                comboBox1.SelectedIndex = 0;
            }
            
        //    clerkNameText.Text = "当前管理员 " + DataPool.currentID;
       //     statusStrip1.Items.Insert(1, new ToolStripSeparator());
        }

        //private void connectSQL()
        //{
        //    conn = new SqlConnection(DataPool.conString);

        //    conn.Open();
        //}

        //private void closeSQL()
        //{
        //    conn.Close();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
           // pictureBox2.Image = null;
            tempgname = textBox1.Text.Replace("'", "''");
            tempaname = textBox2.Text.Replace("'", "''");
            tempsname = textBox3.Text.Replace("'", "''");
            temparranger = textBox5.Text.Replace("'", "''");
            templyric = textBox4.Text.Replace("'", "''");
            tempvocal = textBox9.Text.Replace("'", "''");
            tempstyle = textBox8.Text.Replace("'", "''");
            temptime = textBox7.Text;
            temporigin = comboBox1.Text;



            sqlstr = "select [group].gname as 社团名,album.aname as 专辑名,[time] as 首发展会,track as 音轨号,sname as 曲目名,arranger as 编曲,lyric as 作词,vocal as 歌手,style as 风格,origin as 原曲,adduser as 添加人员 from album,gas,[group],ori,song where gas.gcode=[group].gcode and gas.acode=album.acode and gas.scode=song.scode and song.oricode=ori.oricode";
            if (tempgname != string.Empty)
                sqlstr = sqlstr + " and [group].gname like '%" + tempgname + "%'";
            if (tempaname != string.Empty)
                sqlstr = sqlstr + " and album.aname like '%" + tempaname + "%'";
            if (tempsname != string.Empty)
                sqlstr = sqlstr + " and song.sname like '%" + tempsname + "%'";
            if (temparranger != string.Empty)
                sqlstr = sqlstr + " and arranger like '%" + temparranger + "%'";
            if (templyric != string.Empty)
                sqlstr = sqlstr + " and lyric like '%" + templyric + "%'";
            if (tempvocal != string.Empty)
                sqlstr = sqlstr + " and vocal like '%" + tempvocal + "%'";
            if (tempstyle != string.Empty)
                sqlstr = sqlstr + " and style like '%" + tempstyle + "%'";
            if (temptime != string.Empty)
                sqlstr = sqlstr + " and time like '%" + temptime + "%'";
            if (temporigin != string.Empty)
                sqlstr = sqlstr + " and origin='" + temporigin + "'";

            DataSet ds1;
            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                sqladp.getDataSet(sqlstr, out ds1, "detail");
            }
         //   Trace.WriteLine(ds.Tables[0].Rows.Count);

            dataGridView1.DataSource = ds1.Tables[0];

        //   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         //   {
              //  dataGridView1.
                //comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
          //  }

        //    comboBox1.SelectedIndex = 0;
     /*       byte[] imagebytes = null;

            connectSQL();

            SqlCommand com = new SqlCommand("select cover from album where aname='"+tempaname+"'", conn);
                        //" + tempaname + "
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {

                imagebytes = (byte[])dr.GetValue(0);

            }

            dr.Close();

            com.Clone();

            if (imagebytes != null)
            {
                MemoryStream ms = new MemoryStream(imagebytes);

                Bitmap bmpt = new Bitmap(ms);
                //Image image = Image.FromStream(ms, true);
                dr.Close();
                closeSQL();
                pictureBox2.Image = bmpt;
            }*/

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // if (comboBox1.Text == "少女綺想曲 ～ Dream Battle")
            //     pictureBox1.Image = touhou_music.Properties.Resources.reimu;

            #region Select Picture
            switch (comboBox1.Text)
            {
                case "少女綺想曲 ～ Dream Battle": pictureBox1.Image = touhou_music.Properties.Resources.Reimu; break;
                case "天狗の手帖 ～ Mysterious Note": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "風の循環 ～ Wind Tour": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "恋色マスタースパーク": pictureBox1.Image = touhou_music.Properties.Resources.Marisa; break;
                case "天狗が見ている ～ Black Eyes": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "東の国の眠らない夜": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "レトロスペクティブ京都": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "風神少女": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "おてんば恋娘": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "ほおずきみたいに紅い魂": pictureBox1.Image = touhou_music.Properties.Resources.Rumia; break;
                case "妖魔夜行": pictureBox1.Image = touhou_music.Properties.Resources.Rumia; break;
                case "ルーネイトエルフ": pictureBox1.Image = touhou_music.Properties.Resources.Fairy; break;
                case "上海紅茶館 ～ Chinese Tea": pictureBox1.Image = touhou_music.Properties.Resources.Meiling; break;
                case "明治十七年の上海アリス": pictureBox1.Image = touhou_music.Properties.Resources.Meiling; break;
                case "ヴワル魔法図書館": pictureBox1.Image = touhou_music.Properties.Resources.Devil; break;
                case "ラクトガール ～ 少女密室": pictureBox1.Image = touhou_music.Properties.Resources.Patchouli; break;
                case "メイドと血の懐中時計": pictureBox1.Image = touhou_music.Properties.Resources.Sakuya; break;
                case "月時計 ～ ルナ·ダイアル": pictureBox1.Image = touhou_music.Properties.Resources.Sakuya; break;
                case "ツェペシュの幼き末裔": pictureBox1.Image = touhou_music.Properties.Resources.Remilia; break;
                case "亡き王女の為のセプテット": pictureBox1.Image = touhou_music.Properties.Resources.Remilia; break;
                case "魔法少女達の百年祭": pictureBox1.Image = touhou_music.Properties.Resources.Patchouli; break;
                case "U.N.オーエンは彼女なのか？": pictureBox1.Image = touhou_music.Properties.Resources.Flandre; break;

                case "無何有の郷 ～ Deep Mountain": pictureBox1.Image = touhou_music.Properties.Resources.Letty; break;
                case "クリスタライズシルバー": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "遠野幻想物語": pictureBox1.Image = touhou_music.Properties.Resources.Chen; break;
                case "ティアオイエツォン(withered leaf)": pictureBox1.Image = touhou_music.Properties.Resources.Chen; break;
                case "ブクレシュティの人形師": pictureBox1.Image = touhou_music.Properties.Resources.Alice; break;
                case "人形裁判 ～ 人の形弄びし少女": pictureBox1.Image = touhou_music.Properties.Resources.Alice; break;
                case "天空の花の都": pictureBox1.Image = touhou_music.Properties.Resources.Lilywhite; break;
                case "幽霊楽団 ～ Phantom Ensemble": pictureBox1.Image = touhou_music.Properties.Resources.Lunasa; break;
                case "東方妖々夢 ～ Ancient Temple": pictureBox1.Image = touhou_music.Properties.Resources.Youmu; break;
                case "広有射怪鳥事 ～ Till When?": pictureBox1.Image = touhou_music.Properties.Resources.Youmu; break;
                case "アルティメットトゥルース": pictureBox1.Image = touhou_music.Properties.Resources.Yuyuko; break;
                case "幽雅に咲かせ、墨染の桜 ～ Border of Life": pictureBox1.Image = touhou_music.Properties.Resources.Yuyuko; break;
                case "ボーダーオブライフ": pictureBox1.Image = touhou_music.Properties.Resources.Yuyuko; break;
                case "妖々跋扈": pictureBox1.Image = touhou_music.Properties.Resources.Chen; break;
                case "少女幻葬 ～ Necro-Fantasy": pictureBox1.Image = touhou_music.Properties.Resources.Ran; break;
                case "妖々跋扈 ～ Who done it!": pictureBox1.Image = touhou_music.Properties.Resources.Ran; break;
                case "ネクロファンタジア": pictureBox1.Image = touhou_music.Properties.Resources.Yukari; break;

                case "Demystify Feast": pictureBox1.Image = touhou_music.Properties.Resources.Suika; break;
                case "夜が降りてくる　～ Evening Star": pictureBox1.Image = touhou_music.Properties.Resources.Suika; break;
                case "御伽の国の鬼が島　～ Missing Power": pictureBox1.Image = touhou_music.Properties.Resources.Suika; break;
                case "酔月": pictureBox1.Image = touhou_music.Properties.Resources.Suika; break;

                case "幻視の夜 ～ Ghostly Eyes": pictureBox1.Image = touhou_music.Properties.Resources.Wriggle; break;
                case "蠢々秋月 ～ Mooned Insect": pictureBox1.Image = touhou_music.Properties.Resources.Wriggle; break;
                case "夜雀の歌声 ～ Night Bird": pictureBox1.Image = touhou_music.Properties.Resources.Mystia; break;
                case "もう歌しか聞こえない": pictureBox1.Image = touhou_music.Properties.Resources.Mystia; break;
                case "懐かしき東方の血 ～ Old World": pictureBox1.Image = touhou_music.Properties.Resources.Keine; break;
                case "プレインエイジア": pictureBox1.Image = touhou_music.Properties.Resources.Keine; break;
                case "シンデレラケージ ～ Kagome-Kagome": pictureBox1.Image = touhou_music.Properties.Resources.Tewi; break;
                case "狂気の瞳 ～ Invisible Full Moon": pictureBox1.Image = touhou_music.Properties.Resources.Udonge; break;
                case "ヴォヤージュ1969": pictureBox1.Image = touhou_music.Properties.Resources.Eirin; break;
                case "千年幻想郷 ～ History of the Moon": pictureBox1.Image = touhou_music.Properties.Resources.Eirin; break;
                case "竹取飛翔 ～ Lunatic Princess": pictureBox1.Image = touhou_music.Properties.Resources.Kaguya; break;
                case "ヴォヤージュ1970": pictureBox1.Image = touhou_music.Properties.Resources.Kaguya; break;
                case "エクステンドアッシュ ～ 蓬莱人": pictureBox1.Image = touhou_music.Properties.Resources.Mokou; break;
                case "月まで届け、不死の煙": pictureBox1.Image = touhou_music.Properties.Resources.Mokou; break;

                case "春色小径 ～ Colorful Path": pictureBox1.Image = touhou_music.Properties.Resources.Reimu; break;
                case "オリエンタルダークフライト": pictureBox1.Image = touhou_music.Properties.Resources.Marisa; break;
                case "フラワリングナイト": pictureBox1.Image = touhou_music.Properties.Resources.Sakuya; break;
                case "おてんば恋娘の冒険": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "もう歌しか聞こえない ～ Flower Mix": pictureBox1.Image = touhou_music.Properties.Resources.Mystia; break;
                case "お宇佐さまの素い幡": pictureBox1.Image = touhou_music.Properties.Resources.Tewi; break;
                case "風神少女 (Short Version)": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "ポイズンボディ ～ Forsaken Doll": pictureBox1.Image = touhou_music.Properties.Resources.Medicine; break;
                case "今昔幻想郷 ～ Flower Land": pictureBox1.Image = touhou_music.Properties.Resources.Yuuka; break;
                case "彼岸帰航 ～ Riverside View": pictureBox1.Image = touhou_music.Properties.Resources.Komachi; break;
                case "六十年目の東方裁判 ～ Fate of Sixty Years": pictureBox1.Image = touhou_music.Properties.Resources.Sikieiki; break;

                case "人恋し神様 ～ Romantic Fall": pictureBox1.Image = touhou_music.Properties.Resources.Sizuha; break;
                case "稲田姫様に叱られるから": pictureBox1.Image = touhou_music.Properties.Resources.Minoriko; break;
                case "厄神様の通り道 ～ Dark Road": pictureBox1.Image = touhou_music.Properties.Resources.Hina; break;
                case "運命のダークサイド": pictureBox1.Image = touhou_music.Properties.Resources.Hina; break;
                case "神々が恋した幻想郷": pictureBox1.Image = touhou_music.Properties.Resources.Nitori; break;
                case "芥川龍之介の河童 ～ Candid Friend": pictureBox1.Image = touhou_music.Properties.Resources.Nitori; break;
                case "フォールオブフォール ～ 秋めく滝": pictureBox1.Image = touhou_music.Properties.Resources.Momizi; break;
                case "妖怪の山 ～ Mysterious Mountain": pictureBox1.Image = touhou_music.Properties.Resources.Ayamof; break;
                case "少女が見た日本の原風景": pictureBox1.Image = touhou_music.Properties.Resources.Sanae; break;
                case "信仰は儚き人間の為に": pictureBox1.Image = touhou_music.Properties.Resources.Sanae; break;
                case "御柱の墓場 ～ Grave of Being": pictureBox1.Image = touhou_music.Properties.Resources.Kanako; break;
                case "神さびた古戦場 ～ Suwa Foughten Field": pictureBox1.Image = touhou_music.Properties.Resources.Kanako; break;
                case "明日ハレの日、ケの昨日": pictureBox1.Image = touhou_music.Properties.Resources.Suwako; break;
                case "ネイティブフェイス": pictureBox1.Image = touhou_music.Properties.Resources.Suwako; break;

                case "黒い海に紅く　～ Legendary Fish": pictureBox1.Image = touhou_music.Properties.Resources.Iku; break;            
                case "有頂天変　～ Wonderful Heaven": pictureBox1.Image = touhou_music.Properties.Resources.Tenshi; break;
                case "幼心地の有頂天": pictureBox1.Image = touhou_music.Properties.Resources.Tenshi; break;

                case "暗闇の風穴": pictureBox1.Image = touhou_music.Properties.Resources.Kisume; break;
                case "封じられた妖怪 ～ Lost Place": pictureBox1.Image = touhou_music.Properties.Resources.Yamame; break;
                case "渡る者の途絶えた橋": pictureBox1.Image = touhou_music.Properties.Resources.Parsee; break;
                case "緑眼のジェラシー": pictureBox1.Image = touhou_music.Properties.Resources.Parsee; break;
                case "旧地獄街道を行く": pictureBox1.Image = touhou_music.Properties.Resources.Yuugi; break;
                case "華のさかづき大江山": pictureBox1.Image = touhou_music.Properties.Resources.Yuugi; break;
                case "ハートフェルトファンシー": pictureBox1.Image = touhou_music.Properties.Resources.Satori_nb; break;
                case "少女さとり ～ 3rd eye": pictureBox1.Image = touhou_music.Properties.Resources.Satori_nb; break;
                case "廃獄ララバイ": pictureBox1.Image = touhou_music.Properties.Resources.Rin_nb; break;
                case "死体旅行 ～ Be of good cheer!": pictureBox1.Image = touhou_music.Properties.Resources.Rin_nb; break;
                case "業火マントル": pictureBox1.Image = touhou_music.Properties.Resources.Rin_nb; break;
                case "霊知の太陽信仰 ～ Nuclear Fusion": pictureBox1.Image = touhou_music.Properties.Resources.Utsuho_nb; break;
                case "ラストリモート": pictureBox1.Image = touhou_music.Properties.Resources.Koishi_nb; break;
                case "ハルトマンの妖怪少女": pictureBox1.Image = touhou_music.Properties.Resources.Koishi_nb; break;

                case "春の湊に": pictureBox1.Image = touhou_music.Properties.Resources.Reimu; break;
                case "小さな小さな賢将": pictureBox1.Image = touhou_music.Properties.Resources.Nazrin_nb; break;
                case "閉ざせし雲の通い路": pictureBox1.Image = touhou_music.Properties.Resources.Kogasa_nb; break;
                case "万年置き傘にご注意を": pictureBox1.Image = touhou_music.Properties.Resources.Kogasa_nb; break;
                case "スカイルーイン": pictureBox1.Image = touhou_music.Properties.Resources.Ichirin_nb; break;
                case "時代親父とハイカラ少女": pictureBox1.Image = touhou_music.Properties.Resources.Ichirin_nb; break;
                case "幽霊客船の時空を越えた旅": pictureBox1.Image = touhou_music.Properties.Resources.Minamitu_nb; break;
                case "キャプテン・ムラサ": pictureBox1.Image = touhou_music.Properties.Resources.Minamitu_nb; break;
                case "魔界地方都市エソテリア": pictureBox1.Image = touhou_music.Properties.Resources.Syou_nb; break;
                case "虎柄の毘沙門天": pictureBox1.Image = touhou_music.Properties.Resources.Syou_nb; break;
                case "法界の火": pictureBox1.Image = touhou_music.Properties.Resources.Byakuren_nb; break;
                case "感情の摩天楼　～ Cosmic Mind": pictureBox1.Image = touhou_music.Properties.Resources.Byakuren_nb; break;
                case "夜空のユーフォーロマンス": pictureBox1.Image = touhou_music.Properties.Resources.Kogasa_nb; break;
                case "平安のエイリアン": pictureBox1.Image = touhou_music.Properties.Resources.Nue_nb; break;


                case "あなたの町の怪事件": pictureBox1.Image = touhou_music.Properties.Resources.Hatate; break;
                case "妖怪モダンコロニー": pictureBox1.Image = touhou_music.Properties.Resources.Hatate; break;
                case "ネメシスの要塞": pictureBox1.Image = touhou_music.Properties.Resources.Hatate; break;
                case "無間の鐘　～ Infinite Nightmare": pictureBox1.Image = touhou_music.Properties.Resources.Hatate; break;
                case "妖怪の山　～ Mysterious Mountain": pictureBox1.Image = touhou_music.Properties.Resources.Aya; break;
                case "可愛い大戦争のリフレーン": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "いたずらに命をかけて": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "年中夢中の好奇心": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "真夜中のフェアリーダンス": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "妖精大戦争　～ Fairy Wars": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "ルーズレイン": pictureBox1.Image = touhou_music.Properties.Resources.Cirno; break;
                case "メイガスナイト": pictureBox1.Image = touhou_music.Properties.Resources.Marisa; break;

                case "死霊の夜桜": pictureBox1.Image = touhou_music.Properties.Resources.Yuyuko; break;
                case "ゴーストリード": pictureBox1.Image = touhou_music.Properties.Resources.Yuyuko; break;
                case "妖怪寺へようこそ": pictureBox1.Image = touhou_music.Properties.Resources.Kyouko; break;
                case "門前の妖怪小娘": pictureBox1.Image = touhou_music.Properties.Resources.Kyouko; break;
                case "素敵な墓場で暮しましょ": pictureBox1.Image = touhou_music.Properties.Resources.Yosika; break;
                case "リジッドパラダイス": pictureBox1.Image = touhou_music.Properties.Resources.Yosika; break;
                case "デザイアドライブ": pictureBox1.Image = touhou_music.Properties.Resources.Seiga; break;
                case "古きユアンシェン": pictureBox1.Image = touhou_music.Properties.Resources.Seiga; break;
                case "夢殿大祀廟": pictureBox1.Image = touhou_music.Properties.Resources.Toziko; break;
                case "大神神話伝": pictureBox1.Image = touhou_music.Properties.Resources.Huto; break;
                case "小さな欲望の星空": pictureBox1.Image = touhou_music.Properties.Resources.Miko; break;
                case "聖徳伝説　～ True Administrator": pictureBox1.Image = touhou_music.Properties.Resources.Miko; break;
                case "妖怪裏参道": pictureBox1.Image = touhou_music.Properties.Resources.Nue_nb; break;
                case "佐渡の二ッ岩": pictureBox1.Image = touhou_music.Properties.Resources.Mamizou; break;


                default: pictureBox1.Image = touhou_music.Properties.Resources.not_found; break;
            }
            #endregion
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 Form6 = new Form6();
            Form6.ShowDialog();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 Form11 = new Form11();
            Form11.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox2.Image = null;
            int row = dataGridView1.CurrentCell.RowIndex;
            string nowalbum = dataGridView1.Rows[row].Cells[1].Value.ToString();
            byte[] imagebytes = null;

            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                //" + tempaname + "
                SqlDataReader dr;
                sqladp.getExecuteReader(out dr,"select cover from album where aname='" + nowalbum.Replace("'", "''") + "'");
                while (dr.Read())
                {

                    imagebytes = (byte[])dr.GetValue(0);

                }

                dr.Close();
            }
            if (imagebytes != null)
            {
                if (imagebytes[0] == 0)
                {
                    pictureBox2.Image = touhou_music.Properties.Resources.x;
                    return;
                }
                    

                MemoryStream ms = new MemoryStream(imagebytes);

                Bitmap bmpt = new Bitmap(ms);
                //Image image = Image.FromStream(ms, true);
                //dr.Close();
                //closeSQL();
                pictureBox2.Image = bmpt;
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Form4 Form4 = new Form4();
                Form4.ShowDialog();
            
        }

        private void 添加原曲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 Form9 = new Form9();
            Form9.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 Form7 = new Form7();
            Form7.ShowDialog();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form12 Form12 = new Form12();
            Form12.ShowDialog();
        }

       






    
    
    
    
    
    
    
    
    
    
    }




}
