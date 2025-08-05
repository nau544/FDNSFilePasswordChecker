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

                // バッチファイルのパスを設定
                string batchPath = Path.Combine(Application.StartupPath, "fpc_1_3_0", "00.パスワードファイルチェッカー.bat");
                
                // バッチファイルの存在確認
                if (!File.Exists(batchPath))
                {
                    MessageBox.Show($"バッチファイルが見つかりません: {batchPath}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // プロセス開始情報を設定
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = batchPath;  // バッチファイルを直接実行
                startInfo.WorkingDirectory = Path.Combine(Application.StartupPath, "fpc_1_3_0");  // fpc_1_3_0フォルダを作業ディレクトリに設定
                
                // 引数を適切にエスケープして設定
                string arguments = $"\"{txtSourceFolder.Text}\" \"{txtOutputFolder.Text}\"";
                
                // チェックボックスの状態に応じて引数を追加
                if (chkExcludePasswordFolders.Checked)
                {
                    arguments += " --exclude-password";
                }
                
                startInfo.Arguments = arguments;
                startInfo.UseShellExecute = false;  // シェル実行を無効化
                startInfo.RedirectStandardOutput = true;  // 標準出力をリダイレクト
                startInfo.RedirectStandardError = true;   // 標準エラーをリダイレクト
                startInfo.CreateNoWindow = false;  // ウィンドウを表示
                
                // バッチファイルを実行
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    
                    // プロセスの終了を待機（エラー確認のため）
                    process.WaitForExit();
                    
                    // エラー内容を確認
                    string error = process.StandardError.ReadToEnd();
                    string output = process.StandardOutput.ReadToEnd();
                    
                    if (!string.IsNullOrEmpty(error))
                    {
                        string errorMessage = "実行中にエラーが発生しました:\n\n";
                        errorMessage += $"バッチファイル:\n{error}";
                        MessageBox.Show(errorMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // 成功メッセージを表示
                        string message = "00.パスワードファイルチェッカー.batを開始しました！";
                        if (chkExcludePasswordFolders.Checked)
                        {
                            message += "\n(パスワード付きファイルは除外されています)";
                        }
                        
                        MessageBox.Show(message, "実行", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
