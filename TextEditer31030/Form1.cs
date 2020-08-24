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

        //名前を付けて保存
        private void SaveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter sw = new StreamWriter(sfdFileSave.FileName, false, Encoding.GetEncoding("utf-8"))) 
                {
                    sw.WriteLine(rtTextArea.Text);
                    editFilePath = sfdFileSave.FileName;
                    this.Text = editFilePath;
                }
            }
        }

        //上書き保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(editFilePath))
            {
                using (StreamWriter sw = new StreamWriter(editFilePath, false, Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                }
            } else {
                SaveNameToolStripMenuItem_Click(sender, e);
            }
        }
    }
}
