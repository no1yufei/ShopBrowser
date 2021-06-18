using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Tools
{
     public class TextLogHelper: System.IO.TextWriter
    {
        static TextLogHelper instance;
        public static TextLogHelper Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new TextLogHelper();
                }
                return instance;
            }
        }

        TextBoxBase txtBox;
        TextBoxBase mainTxtBox;
        delegate void VoidAction();
        public override void WriteLine(string value)
        {
            //base.Write(value);//still output to Console
            VoidAction action = delegate {
                txtBox.Text = DateTime.Now.ToString() + ":" + (value.ToString()) + Environment.NewLine + txtBox.Text;
            };
            if (!txtBox.IsHandleCreated && txtBox != mainTxtBox)
            {
                txtBox = mainTxtBox;
            }
            if (txtBox.IsHandleCreated)
            {
                txtBox.BeginInvoke(action);
            }
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }

        public void Initialize(TextBoxBase txtBox)
        {
            if (null == mainTxtBox)
            {
                mainTxtBox = txtBox;
                this.txtBox = txtBox;
            }
        }
        public void SetConsolOut(TextBoxBase txtBox)
        {
            this.txtBox = txtBox;
        }

    }
    public class TextBoxWriter : System.IO.TextWriter
    {
        TextBoxBase txtBox;
        delegate void VoidAction();

        public TextBoxWriter(RichTextBox box)
        {
            txtBox = box; //transfer the enternal TextBox in
        }
        public override void WriteLine(string value)
        {
            //base.Write(value);//still output to Console
            VoidAction action = delegate {
                txtBox.Text = DateTime.Now.ToString() + ":" + (value.ToString()) + Environment.NewLine + txtBox.Text;
            };
            if (txtBox.IsHandleCreated)
            {
                txtBox.BeginInvoke(action);
            }

        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
