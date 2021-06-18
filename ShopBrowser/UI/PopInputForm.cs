using ShopeeChat.SysData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopeeChat.UI
{
    public partial class PopInputForm : Form
    {
        public String Code = "";
        public StoreRegion Region;
        public PopInputForm()
        {
            InitializeComponent();
            regionCBox.DataSource = StoreRegionMap.GetRegionList();
        }

        private void sureBtn_Click(object sender, EventArgs e)
        {
            Code = inputTbox1.Text;
            Region = regionCBox.SelectedItem as StoreRegion;
            //this.DialogResult = DialogResult.OK;
            //Close();
        }
    }
}
