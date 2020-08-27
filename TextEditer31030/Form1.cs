using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31030 {
    public partial class テキストエディタ : Form {

        //現在編集中のファイル名
        private string editFilePath = "";//Camel形式（⇔Pascal形式）

        public テキストエディタ()
        {
            InitializeComponent();
        }

        //終了
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //×ボタン
        private void テキストエディタ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtTextArea.Modified) {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) {
                    SaveToolStripMenuItem_Click(sender, e);
                } else if (result == DialogResult.No) {
                } else if (result == DialogResult.Cancel) {
                    return;
                }
            }
        }
        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Modified) {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) {
                    SaveToolStripMenuItem_Click(sender, e);
                } else if (result == DialogResult.No) {
                } else if (result == DialogResult.Cancel) {
                    return;
                }
            }
            rtTextArea.Clear();
            editFilePath = "";
            this.Text = "";
        }

        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Modified) {
                DialogResult result = MessageBox.Show("ファイルを保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) {
                    SaveToolStripMenuItem_Click(sender, e);
                } else if (result == DialogResult.No) {
                } else if (result == DialogResult.Cancel) {
                    return;
                }
            }
            if (ofdFileOpen.ShowDialog() == DialogResult.OK) {
                rtTextArea.LoadFile(ofdFileOpen.FileName,RichTextBoxStreamType.RichText);
                editFilePath = ofdFileOpen.FileName;
                this.Text = editFilePath;
            }
        }

        //ファイル名を指定しデータを保存
        private void FileSave(string editFileName)
        {
            rtTextArea.SaveFile(editFileName, RichTextBoxStreamType.RichText);
        }

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
                this.Text = sfdFileSave.FileName;
                editFilePath = sfdFileSave.FileName;
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(editFilePath))
            {
                FileSave(editFilePath);
            } else {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }

        private void cheakEnabled()
        {
            if (rtTextArea.CanUndo) {
                UndoToolStripMenuItem.Enabled = true;
            } else {
                UndoToolStripMenuItem.Enabled = false;
            }
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
            if (rtTextArea.CanRedo) {
                RedoToolStripMenuItem.Enabled = true;
            } else {
                UndoToolStripMenuItem.Enabled = false;
            }
        }

        //やり直し
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
            if (rtTextArea.CanRedo) {
                RedoToolStripMenuItem.Enabled = true;
            } else {
                RedoToolStripMenuItem.Enabled = false;
            }
        }

        //テキストに変化があったとき
        private void rtTextArea_TextChanged(object sender, EventArgs e)
        {
            cheakEnabled();
        }

        //編集ボタン
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataFormats.Format myformat = DataFormats.GetFormat(DataFormats.Rtf);
            //undo
            UndoToolStripMenuItem.Enabled = rtTextArea.CanUndo;
            //redo
            RedoToolStripMenuItem.Enabled = rtTextArea.CanRedo;
            //cut
            CutToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            //copy
            CopyToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            //paste
            PasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Rtf);
            //delete
            DeleteToolStripMenuItem.Enabled = (rtTextArea.SelectionLength > 0);
            
        }


        //カット
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //ペースト
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //カラー
        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cdColor.ShowDialog() == DialogResult.OK) {
                rtTextArea.SelectionColor = cdColor.Color;
            }
        }

        //フォント
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fdFont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.SelectionFont = fdFont.Font;
            }
        }

    }
}
