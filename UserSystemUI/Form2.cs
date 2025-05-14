using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
namespace UserSystemUI
{
    public partial class 注册界面 : Form
    {
        // 导入login函数
        [DllImport("UserSystemCore.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool login(string username, string password);

        // 导入addUser函数
        [DllImport("UserSystemCore.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void addUser(string username, string password);

        // 导入removeUser函数
        [DllImport("UserSystemCore.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void removeUser(string username);

        // 导入updatePassword函数
        [DllImport("UserSystemCore.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void updatePassword(string username, string newPassword);
        public 注册界面()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //注册Click
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text;
            string password = txtNewPassword.Text;
            string password1= txtNewPassword1.Text;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
                MessageBox.Show("用户名和密码不能为空!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (password != password1)
            {
                MessageBox.Show("两次输入密码不一致！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool is_exist = login(username, password);
            if (IsUserExists(username))//文件和AVL中均存在
            {
                MessageBox.Show("用户名已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

           addUser(username,password);//写入AVL树,(同时写入文件)
            MessageBox.Show("注册成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }
        //检查文件中用户是否已存在
        private bool IsUserExists(string username)
        {
            if(!File.Exists("users.txt"))  return false;
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines) { 
                string[] parts = line.Split(',');
                if (parts[0] == username) return true;
            }
            return false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
