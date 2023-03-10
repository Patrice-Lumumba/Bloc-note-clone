using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
            this.richTextBox1.EnableAutoDragDrop = true;
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.WordWrap = true;
            this.richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            this.richTextBox1.ShortcutsEnabled = true;
        }

        public void OpenFile(string fileName)
        {
            string strExt;
            strExt = System.IO.Path.GetExtension(fileName); 
            strExt = strExt.ToUpper();
            if (strExt == ".RTF")
            {
                richTextBox1.LoadFile(fileName, RichTextBoxStreamType.RichText);
            }

            else if(strExt == ".TXT")
            {
                richTextBox1.LoadFile(fileName, RichTextBoxStreamType.PlainText);
            }

            else
            {
                System.IO.StreamReader txtReader;
                txtReader = new System.IO.StreamReader(fileName);   
                richTextBox1.Text = txtReader.ReadToEnd();
                txtReader.Close();
                txtReader = null;
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 0;
            }

            richTextBox1.Modified = false;
            this.Text = "Editor: " + fileName;
        }

        private void MyForm_Load(object sender, EventArgs e)
        {

        }
    }
}
