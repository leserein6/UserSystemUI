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
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
        }

        //登录Button_Click后
        //首先search用户是否存在，然后匹配用户名与密码是否相符，最后跳转到相应的登录界面
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password= txtPassword.Text;
            if(string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool is_log_in=login(username, password);

            if(ValidateUser(username,password)&&is_log_in)
            {
                MessageBox.Show("登录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
     
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        //用户登录验证函数
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //跳转到注册界面
        private void button2_Click(object sender, EventArgs e)
        {
            注册界面 registerForm = new 注册界面();
            registerForm.Show();
            this.Hide();//隐藏当前登录界面
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 revisePaForm = new Form3();
            revisePaForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 eraseUForm=new Form5(); 
            eraseUForm.Show();
            this.Hide();
        }
    }
}

