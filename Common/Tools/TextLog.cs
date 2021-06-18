using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Tools
{
    public class TextLog
    {
        DateTime startTime = DateTime.Now;
        TextBoxBase textBox;
        public TextLog(TextBoxBase textBox)
        {
            this.textBox = textBox;
        }
        private delegate void SetTextToRichTextBox(object sText);

        
        private void SetRichBoxText(object sText)
        {
            //textBox.Text = sText.ToString() + "\r\n" + textBox.Text;
            if(textBox.Text.Length > (int.MaxValue/2))
            {
                textBox.Text = textBox.Text.Substring(0, int.MaxValue / 4);
            }
            textBox.Text = DateTime.Now.ToString() + " : " + (sText.ToString()) + Environment.NewLine + textBox.Text;
        }
        public void ShowLog(String text)
        {
            if (textBox.IsHandleCreated)
            {
                textBox.Invoke(new SetTextToRichTextBox(SetRichBoxText), text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}
