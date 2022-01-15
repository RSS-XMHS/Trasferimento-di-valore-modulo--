using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CopyDataStruct.Class1;

namespace 窗体传值Demo
{
    public partial class Sender : Form
    {
        public Sender()
        {
            InitializeComponent();
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]

        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]

        private static extern int FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004A;
        
        private void button1_Click(object sender, EventArgs e)
        {
           int hWnd = FindWindow(null, @"消息接受者");

            if (hWnd == 0)

            {

                MessageBox.Show("555，未找到消息接受者！");

            }

            else

            {

                byte[] sarr = System.Text.Encoding.Default.GetBytes(textBox1.Text);

                int len = sarr.Length;

                COPYDATASTRUCT cds;

                cds.dwData = (IntPtr)Convert.ToInt16(0);//可以是任意值
                cds.cbData = len + 1;//指定lpData内存区域的字节数

                cds.lpData = textBox1.Text;//发送给目标窗口所在进程的数据

                SendMessage(hWnd, WM_COPYDATA, 0, ref cds);

            }
        }
         protected override void DefWndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case WM_COPYDATA:

                    COPYDATASTRUCT cds = new COPYDATASTRUCT();

                    Type t = cds.GetType();

                    cds = (COPYDATASTRUCT)m.GetLParam(t);

                 //   string strResult = cds.dwData.ToString() + ":" +cds.lpData; 
                    string strResult =cds.lpData; 

                    listBox1.Items.Add(strResult);

                    break;

                default:

                    base.DefWndProc(ref m);

                    break;

            }

        }
    }
}
