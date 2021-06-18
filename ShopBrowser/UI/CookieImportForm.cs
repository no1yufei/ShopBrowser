using Newtonsoft.Json;
using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat.UI
{
    public partial class CookieImportForm : Form
    {
        public String SCookie = "";
        public Guid SPC_CDS;
        public CookieImportForm()
        {
            InitializeComponent();
            //formatCBox.DataSource = StoreRegionMap.GetRegionList();
        }

        private void sureBtn_Click(object sender, EventArgs e)
        {
            try
            {
                    string s = System.IO.File.ReadAllText(filePathTbox.Text);
                List<Cookie> cookies = JsonConvert.DeserializeObject<List<Cookie>>(s);
                
                Regex r = new Regex(@".*[0-9].*");//正则
               
                foreach (Cookie c in cookies)
                {

                    //if (c.Name.StartsWith("_") ||  r.IsMatch(c.Name) )
                    if (r.IsMatch(c.Name))
                    {
                        continue;
                    }
                    if(c.Name.Contains("SPC_CDS"))
                    {
                        SPC_CDS = new Guid(c.Value);
                        continue;
                    }
                    SCookie += c.Name + "=" + c.Value + ";";
                }
            }
           catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.DialogResult = DialogResult.OK;
            //Close();
        }

        private void fileOpenBtn_Click(object sender, EventArgs e)
        {
           if(DialogResult.OK ==  openFileDialog1.ShowDialog())
                {
                filePathTbox.Text = openFileDialog1.FileName;

            }
        }
    }
}
