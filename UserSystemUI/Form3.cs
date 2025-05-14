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
  
    public partial class Form3 : Form
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

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = UserName.Text;
            string userpassword = UserPassword.Text;
            string userpassword1= UserPassword1.Text;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(userpassword)||string.IsNullOrEmpty(userpassword1) )
            {
                MessageBox.Show("用户名和密码不能为空！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (!ValidateUser(username, userpassword))
            {
                MessageBox.Show("用户不存在或密码错误！修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (userpassword == userpassword1)
            {
                MessageBox.Show("新密码不能与原密码一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                updatePassword(username, userpassword1);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
