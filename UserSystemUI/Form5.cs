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
using System.IO;
namespace UserSystemUI
{
    public partial class Form5 : Form
    {
       
        // 导入removeUser函数
        [DllImport("UserSystemCore.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void removeUser(string username);

       

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uname = username.Text;
            string upass = userpassword.Text;
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upass))
            {
                MessageBox.Show("用户名和密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateUser(uname, upass))
            {
                MessageBox.Show("用户名不存在或密码错误！注销失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 弹窗提示用户是否确认注销
            DialogResult result = MessageBox.Show("确认注销？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            // 如果用户点击“确认”
            if (result == DialogResult.OK)
            {
                // 调用 C++ DLL 中的 removeUser 函数
                removeUser(uname);

                // 提示注销成功
                MessageBox.Show("用户注销成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }
        private bool ValidateUser(string username, string password)
        {
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts[0] == username && parts[1] == password)
                {

                    return true;
                }
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
