using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using System.Xml;
using static MinShengQingGou.MinDelegate;

namespace MinShengQingGou
{
    public partial class Form1 : Form
    {
        byte[] data;
        public Form1()
        {
            InitializeComponent();
        }
#     region 委托方式...
        string str = DateTime.Now.ToString("dddd");
        public delegate bool WedDelegate(string str);
        string WedUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057258&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=";
        string ThuUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057259&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=";
        string FriUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057260&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=";
        string SatUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057261&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=";
        string SunUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057262&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=";
        //string QtUrl = "周一至周二无活动";

        private bool Wednesday(string str)
        {
            return this.str == "星期三";
        }

        private bool Thursday(string str)
        {
            return this.str == "星期四";
        }

        private bool Friday(string str)
        {
            return this.str == "星期五";
        }

        private bool Satday(string str)
        {
            return this.str == "星期六";
        }

        private bool Sunday(string str)
        {
            return this.str == "星期日";
        }

        private bool Qtday(string str)
        {
            return this.str == "星期一二";
        }


        private void GetListDele(string Url, WedDelegate wed)
        {
            if (wed.Invoke(str))
            {
                string qmUrl = Url+ textBox3.Text + "&userKey=" + textBox2.Text;
                richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
            }
        }
        #endregion
        //窗口事件
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        //点击获取图片事件
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            {
                //ThreadStart threadStart = () => this.codeImage(textBox2, textBox3);
                //Action action = new Action(() => this.AouteRobbing1(textBox2, textBox3));
                //ThreadWith(threadStart, action);//另开一个线程按顺序执行两个事件，不卡界面
                //以下为上面三步的简写
                //ThreadWith(() => this.codeImage(textBox2, textBox3), () => this.AouteRobbing1(textBox2, textBox3));
            }

            {
                //ThreadStart threadStart = () => this.codeImage(textBox2, textBox3);
                //Thread thread = new Thread(threadStart);
                //thread.Start();//启动线程
                //thread.Join();//等待线程完成后再执行下一步，如何没有此步，会报错。
                //AouteRobbing1(textBox2, textBox3);
            }

            //异步委托
            try
            {
                {
                    //个人定义委托
                    //Imagedelegate imagedelegate = new Imagedelegate(codeImage);//实例化委托，个人定义
                    //AsyncCallback asyncCallback = new AsyncCallback(ar => AouteRobbing1(textBox2, textBox3));//此步可以直接简化到下步中。
                    //imagedelegate.BeginInvoke(textBox2,textBox3, ar => AouteRobbing1(textBox2, textBox3), null);
                }

                {
                    //利用系统封装直接委托
                    Action action = () => codeImage(textBox2, textBox3);//系统自定义委托
                    action.BeginInvoke(ar => AouteRobbing1(textBox2, textBox3), null);
                }

                
                

                //codeImage(textBox2,textBox3);//获取图片
                //AouteRobbing1(textBox2, textBox3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        //异步回调，获取验证码——验证码填充至控件按顺序执行，即便网络卡壳，界面也不会卡死
        private void ThreadWith(ThreadStart threadStart,Action action)
        {
            ThreadStart startNew = new ThreadStart(
                () =>
                {
                    threadStart.Invoke();
                    action.Invoke();
                }
                );
            Thread thread = new Thread(startNew);
            thread.Start();

        }

        //点击抢购事件
        private void button1_Click(object sender, EventArgs e)
        {
            //抢购
            //QmCondition(textBox2, textBox3);
            //委托方法
            //for (int i = 0; i < 3; i++)
            {
                //System.Threading.Thread.Sleep(1000); 隔1秒触发一次
                 //WedDelegate wed = new WedDelegate(Wednesday);//周三
                GetListDele(WedUrl, str => Wednesday(str));
                //WedDelegate thu = new WedDelegate(Thursday); ;//周四
                GetListDele(ThuUrl, str => Thursday(str));
                //new Task(() => GetListDele(ThuUrl, str => Thursday(str))).Start();//启用多线程，不卡界面
                WedDelegate fri = new WedDelegate(Friday);//周五
                GetListDele(FriUrl, fri);
                //new Task(() => GetListDele(FriUrl, str => Friday(str))).Start();
                WedDelegate sat = new WedDelegate(Satday);
                GetListDele(SatUrl, sat);
                WedDelegate sun = new WedDelegate(Sunday);
                GetListDele(SunUrl, sun);
                //WedDelegate qt = new WedDelegate(Qtday);
                //GetListDele(QtUrl, qt);
            }



        }
        #region 普通方法
        public delegate void Imagedelegate(TextBox parJj, TextBox codeText);
        
        //形参1是个人码JJ，2是文本验证码控件
        public void codeImage(TextBox parJj,TextBox codeText)
        {
            string picUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/jcaptcha.img?userKey=" + parJj.Text; //验证码地址
           

            string cooikes = string.Empty;
            Image image = Image.FromStream(RuoKuaiHttp.RuoKuaiHttpGet.GetStream(picUrl, out cooikes));
            //byte[] data;
            //把Image转换为byte
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                ms.Position = 0;
                data = new byte[ms.Length];
                ms.Read(data, 0, Convert.ToInt32(ms.Length));

                pictureBox1.Image = Image.FromStream(ms); //显示下载的图片，实际程序中应该不会有pictureBox控件
                ms.Flush();
            }
        }

        //形参1为个人码，2转化为文本的验证码,
        public void AouteRobbing1(TextBox parJj,TextBox  codeText)
        {
            var param = new Dictionary<object, object>
            {
                {"username",TxtConfigUserName.Text},
                {"password",TxtConfigPassWord.Text},
                {"typeid",TxtConfigTypeId.Text},
                {"timeout","90"},
                {"softid",TxtConfigSoftId.Text},
                {"softkey",TxtConfigSoftKey.Text}
             };

            Thread t = new Thread(new ThreadStart(delegate
            {
                richTextBox1.BeginInvoke(new EventHandler(delegate
                {
                    richTextBox1.AppendText("正在提交服务器..\r\n");
                }));

                //提交服务器
                string httpResult = RuoKuaiHttp.RuoKuaiHttpPos.Post("http://api.ruokuai.com/create.xml", param, data);
                richTextBox1.BeginInvoke(new EventHandler(delegate
                {
                    //richTextBox1.AppendText(httpResult + "\r\n");
                    //richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                    //richTextBox1.ScrollToCaret();
                }));


                //XML解析
                //richTextBox1.BeginInvoke(new EventHandler(delegate
                //{
                //    richTextBox1.AppendText("\r\nXML解析结果：\r\n");
                //    richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                //    richTextBox1.ScrollToCaret();
                //}));
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(httpResult);
                }
                catch
                {
                    richTextBox1.BeginInvoke(new EventHandler(delegate
                    {
                        richTextBox1.AppendText("返回格式有误\r\n");
                        richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                        richTextBox1.ScrollToCaret();
                    }));
                }
                XmlNode idNode = xmlDoc.SelectSingleNode("Root/Id");
                XmlNode resultNode = xmlDoc.SelectSingleNode("Root/Result");
                XmlNode errorNode = xmlDoc.SelectSingleNode("Root/Error");
                string result = string.Empty;
                string topidid = string.Empty;
                if (resultNode != null && idNode != null)
                {
                    topidid = idNode.InnerText;
                    result = resultNode.InnerText;

                    richTextBox1.BeginInvoke(new EventHandler(delegate
                    {
                        //richTextBox1.AppendText("题目ID：" + topidid + "\r\n");
                        richTextBox1.AppendText("识别结果：" + result + "\r\n");
                        richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                        richTextBox1.ScrollToCaret();
                        //将验证码转化为文本至控件
                        codeText.Text = result;
                        //QmCondition(parJj, codeText);
                    }));
                }
                else if (errorNode != null)
                {
                    richTextBox1.BeginInvoke(new EventHandler(delegate
                    {
                        richTextBox1.AppendText("识别错误：" + errorNode.InnerText + "\r\n");
                        richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                        richTextBox1.ScrollToCaret();
                    }));
                }
                else
                {
                    richTextBox1.BeginInvoke(new EventHandler(delegate
                    {
                        richTextBox1.AppendText("未知问题\r\n");
                        richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
                        richTextBox1.ScrollToCaret();
                    }));
                }
            }));
            t.IsBackground = true;
            t.Start();

        }
        //定义抢码规则，1为个人JJ码，2为验证码
        public void QmCondition(TextBox parJj, TextBox codeText)
        {
            string str = DateTime.Now.ToString("dddd");
            if (codeText.Text != null)
            {
                if (str == "星期三")
                {
                    string qmUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057258&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=" + codeText.Text + "&userKey=" + parJj.Text;
                    richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
                }
                else if (str == "星期四")
                {
                    string qmUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057259&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=" + codeText .Text + "&userKey=" + parJj.Text;
                    richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
                }
                else if (str == "星期五")
                {
                    string qmUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057260&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=" + codeText.Text + "&userKey=" + parJj.Text;
                    richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
                }
                else if (str == "星期六")
                {
                    string qmUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057261&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=" + codeText.Text + "&userKey=" + parJj.Text;
                    richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
                }
                else if (str == "星期日")
                {
                    string qmUrl = "https://prefacty.creditcard.cmbc.com.cn/mmc-main-webapp/main/Order.json?actyId=A20170505897&channelType=activityday&giftId=G201705057262&giftNum=1&groupId=&isCaptcha=true&merchantId=0007137&jcaptchaText=" + codeText.Text + "&userKey=" + parJj.Text;
                    richTextBox1.AppendText("抢购结果：" + RuoKuaiHttp.RuoKuaiHttpGet.SendRequest(qmUrl, Encoding.UTF8) + "\r\n");
                }
                else
                {
                    richTextBox1.AppendText("周一至周二无活动\r\n");
                }

            }
        }
           #endregion 普通方法
        //时间事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString("HH: mm:ss  dddd");//显示系统时间
            //规定时间触发获取验证码事件
            if ((DateTime.Now.Hour == 9 || DateTime.Now.Hour == 14) && DateTime.Now.Minute == 59 && DateTime.Now.Second == 36)
            {
                //利用系统封装直接委托
                Action action = () => codeImage(textBox2, textBox3);//系统自定义委托
                action.BeginInvoke(ar => AouteRobbing1(textBox2, textBox3), null);
            }

            //规定时间触发抢购事件
            if ((DateTime.Now.Hour == 10|| DateTime.Now.Hour == 15 )&& DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
            {
                //new Task(() => QmCondition(textBox2, textBox3)).Start();//使用构造函数启动,此处异步因控件安全原因未实现
                //Task.Factory.StartNew(() => QmCondition(textBox2, textBox3)).Wait();
                //Action action = () => QmCondition(textBox2, textBox3);
                //action.BeginInvoke(null, null);
                QmCondition(textBox2, textBox3);
            }

        }
    }
}
