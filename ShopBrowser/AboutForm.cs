using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            versionLabel.Text = "版本号" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            pubDateLabel.Text = "发行时间"+ System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyy年MM月dd日");
        }
    }
}
