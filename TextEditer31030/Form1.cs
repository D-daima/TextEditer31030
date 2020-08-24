using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
            //アプリケーション終了
            Application.Exit();
        }

        //新規作成
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Clear();
            editFilePath = "";
            this.Text = editFilePath;
        }

        //開く
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdFileOpen.ShowDialog() == DialogResult.OK) {
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false)) {
                    rtTextArea.Text = sr.ReadToEnd();
                    editFilePath = ofdFileOpen.FileName;
                    this.Text = editFilePath;
                }
            }
        }

        //ファイル名を指定しデータを保存
        private void FileSave(string editFileName)
        {
            using (StreamWriter sw = new StreamWriter(editFileName, false, Encoding.GetEncoding("utf-8"))) {
                sw.WriteLine(rtTextArea.Text);
            }
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

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo)
            {
                rtTextArea.Undo();
                rtTextArea.ClearUndo();
            }
        }
    }
}
