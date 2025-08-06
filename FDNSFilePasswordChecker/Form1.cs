using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace FDNSFilePasswordChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            CenterSubmitButton();
        }

        /// <summary>
        /// フォームのリサイズイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterSubmitButton();
        }

        /// <summary>
        /// 実行ボタンを中央に配置する
        /// </summary>
        private void CenterSubmitButton()
        {
            // 実行ボタンを水平方向の中央に配置
            int centerX = (this.ClientSize.Width - btnSubmit.Width) / 2;
            btnSubmit.Location = new Point(centerX, btnSubmit.Location.Y);
        }

        /// <summary>
        /// 実行ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // 入力値の検証
                if (string.IsNullOrWhiteSpace(txtSourceFolder.Text))
                {
                    MessageBox.Show("選択フォルダを指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOutputFolder.Text))
                {
                    MessageBox.Show("ログ出力先を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // フォルダの存在確認
                if (!Directory.Exists(txtSourceFolder.Text))
                {
                    MessageBox.Show("指定された選択フォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(txtOutputFolder.Text))
                {
                    MessageBox.Show("指定されたログ出力先が存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // exeファイルのパスを設定
                string exePath1 = Path.Combine(Application.StartupPath, "fpc_1_3_0", "FPCCmd.exe");
                string exePath2 = Path.Combine(Application.StartupPath, "fpc_1_3_0", "FPCNOCmd.exe");
                
                // exeファイルの存在確認
                if (!File.Exists(exePath1))
                {
                    MessageBox.Show($"実行ファイルが見つかりません: {exePath1}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(exePath2))
                {
                    MessageBox.Show($"実行ファイルが見つかりません: {exePath2}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // プロセス開始情報を設定
                ProcessStartInfo startInfo1 = new ProcessStartInfo();
                startInfo1.FileName = "cmd.exe";
                startInfo1.WorkingDirectory = Path.Combine(Application.StartupPath, "fpc_1_3_0");  // fpc_1_3_0フォルダを作業ディレクトリに設定
                startInfo1.Arguments = $"/c \"cd /d {Path.Combine(Application.StartupPath, "fpc_1_3_0")} && FPCCmd.exe /l:\"{txtOutputFolder.Text}\\FPCCmd.log\" /c:\"{txtSourceFolder.Text}\" & PAUSE\"";
                startInfo1.UseShellExecute = true;  // シェル実行を有効化
                startInfo1.CreateNoWindow = false;  // ウィンドウを表示
                
                ProcessStartInfo startInfo2 = new ProcessStartInfo();
                startInfo2.FileName = "cmd.exe";
                startInfo2.WorkingDirectory = Path.Combine(Application.StartupPath, "fpc_1_3_0");  // fpc_1_3_0フォルダを作業ディレクトリに設定
                startInfo2.Arguments = $"/c \"cd /d {Path.Combine(Application.StartupPath, "fpc_1_3_0")} && FPCNOCmd.exe -l \"{txtOutputFolder.Text}\\FPCNOCmd.log\" -c \"{txtSourceFolder.Text}\" & PAUSE\"";
                startInfo2.UseShellExecute = true;  // シェル実行を有効化
                startInfo2.CreateNoWindow = false;  // ウィンドウを表示
                
                // チェックボックスの状態に応じて引数を追加
                if (chkExcludePasswordFolders.Checked)
                {
                    startInfo1.Arguments += " --exclude-password";
                    startInfo2.Arguments += " --exclude-password";
                }

                // 両方のプロセスを同時に開始
                using (Process process1 = new Process())
                using (Process process2 = new Process())
                {
                    process1.StartInfo = startInfo1;
                    process2.StartInfo = startInfo2;
                    
                    // 同時並行で実行
                    process1.Start();
                    process2.Start();
                    
                    // 成功メッセージを表示
                    string message = "FPCCmd.exeとFPCNOCmd.exeを新しいウィンドウで開始しました！";
                    if (chkExcludePasswordFolders.Checked)
                    {
                        message += "\n(パスワード付きファイルは除外されています)";
                    }
                    
                    MessageBox.Show(message, "実行", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"実行中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 選択フォルダの参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "選択フォルダを指定してください";
                folderDialog.ShowNewFolderButton = false;
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFolder.Text = folderDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// ログ出力先の参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "ログ出力先フォルダを指定してください";
                folderDialog.ShowNewFolderButton = true;
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
