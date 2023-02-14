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
    public partial class MDIContainer : Form
    {
        public MDIContainer()
        {
            InitializeComponent();
        }

        private void nouveauToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyForm childform = new MyForm();
            childform.MdiParent = this;
            childform.Show();
        }

        private void ouvrirFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "Text Files (*.txt) |*.txt| Rich Text Format (*.rtf) |*.rtf|" + "All Files (*.*)| *.*";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                if(fileName.Length != 0)
                {
                    try
                    {
                        MyForm childform = new MyForm();
                        childform.OpenFile(openFileDialog1.FileName);
                        childform.MdiParent = this;
                        childform.Show();
                    }
                    catch
                    {
                        MessageBox.Show(String.Format("" +
                            "{0} is not " + "a valid file", fileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void enregistrerFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files (*.txt) |*.txt|RTF Files (*.rtf)|*.rtf|All Files(*.*)|*.*";
            saveFileDialog1.AddExtension = true;
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form activeChildForm = this.ActiveMdiChild;
                if(activeChildForm != null)
                {
                    RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                    if(RichtxtEditor != null)
                    {
                        string extension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                        if (extension.ToLower() == ".txt") /*saveFileDialog.FilterIndex == 1 */
                            RichtxtEditor.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        else if (extension.ToLower() == ".rtf")
                            RichtxtEditor.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
    
                    }
                }
            }
        }

        private void texteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if(activeChildForm == null)
            {
                RichTextBox RichTxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if(RichTxtEditor != null)
                {
                    DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Text);

                    if(RichTxtEditor.CanPaste(myFormat))
                    {
                        RichTxtEditor.Paste(myFormat);  
                    }
                }
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Insert Image";
            openFileDialog1.DefaultExt = "bmp";
            openFileDialog1.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|PNG Files|*.png";
            openFileDialog1.FilterIndex = 1;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(openFileDialog1.FileName == "")
                {
                    return;
                }
                try
                {
                    Form activeChildForm = this.ActiveMdiChild;
                    if(activeChildForm != null)
                    {
                        RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;   
                        if(RichtxtEditor != null)
                        {
                            string theImagePath1 = openFileDialog1.FileName;
                            Image img;
                            img = Image.FromFile(theImagePath1);    
                            Clipboard.SetDataObject(img);
                            DataFormats.Format df;
                            df = DataFormats.GetFormat(DataFormats.Bitmap);

                            if(RichtxtEditor.CanPaste(df))
                            {
                                RichtxtEditor.Paste(df);
                            }
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("Unable to insert image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                }
            }
        }

        private void annulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (RichtxtEditor.CanUndo)
                    {
                        activeChildForm.SuspendLayout();
                        RichtxtEditor.Undo();
                        activeChildForm.ResumeLayout();
                    }
                }
            }
        }

        private void refaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (RichtxtEditor.CanRedo)
                    {
                        activeChildForm.SuspendLayout();
                        RichtxtEditor.Redo();
                        activeChildForm.ResumeLayout();
                    }
                }
            }
        }

        private void marquerLeTexteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {

                        if (RichtxtEditor.SelectionLength > 0)
                        {
                            RichtxtEditor.SelectionBackColor = colorDialog1.Color;
                            RichtxtEditor.DeselectAll();
                        }
                    }
                }
            }
        }

        private void couleurAvantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        RichtxtEditor.SelectionColor = colorDialog1.Color;
                        RichtxtEditor.DeselectAll();
                    }
                }
            }
        }

        private void couleurActuelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        RichtxtEditor.ForeColor = colorDialog1.Color;
                    }
                }
            }
        }

        private void policeActuelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        RichtxtEditor.Font = fontDialog1.Font;
                    }
                }
            }
        }

        private void couperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (RichtxtEditor.SelectionLength > 0)
                    {
                        RichtxtEditor.Cut();
                    }
                }
            }
        }

        private void copierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                    if (RichtxtEditor.SelectionLength > 0)
                    {
                        RichtxtEditor.Copy();
                    }
                }
            }
        }

        private void collerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChildForm = this.ActiveMdiChild;
            if (activeChildForm != null)
            {
                RichTextBox RichtxtEditor = activeChildForm.ActiveControl as RichTextBox;
                if (RichtxtEditor != null)
                {
                   RichtxtEditor.Paste();
                }
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
            //MessageBox.Show("Voulez-vous vraiment quitter l'application", "Erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
