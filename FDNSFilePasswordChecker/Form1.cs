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
            
            // ボタンのマウスイベントハンドラーを設定（ホバー時にカーソルをポインターに変更）
            SetupButtonHoverEffects();
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
        /// ボタンのホバー効果を設定する
        /// </summary>
        private void SetupButtonHoverEffects()
        {
            // 実行ボタンのホバー効果
            btnSubmit.MouseEnter += (sender, e) => { btnSubmit.Cursor = Cursors.Hand; };
            btnSubmit.MouseLeave += (sender, e) => { btnSubmit.Cursor = Cursors.Default; };

            // 参照ボタンのホバー効果
            btnBrowseSource.MouseEnter += (sender, e) => { btnBrowseSource.Cursor = Cursors.Hand; };
            btnBrowseSource.MouseLeave += (sender, e) => { btnBrowseSource.Cursor = Cursors.Default; };

            btnBrowseOutput.MouseEnter += (sender, e) => { btnBrowseOutput.Cursor = Cursors.Hand; };
            btnBrowseOutput.MouseLeave += (sender, e) => { btnBrowseOutput.Cursor = Cursors.Default; };

            btnBrowseOutput2.MouseEnter += (sender, e) => { btnBrowseOutput2.Cursor = Cursors.Hand; };
            btnBrowseOutput2.MouseLeave += (sender, e) => { btnBrowseOutput2.Cursor = Cursors.Default; };

            btnBrowseCopyDestination.MouseEnter += (sender, e) => { btnBrowseCopyDestination.Cursor = Cursors.Hand; };
            btnBrowseCopyDestination.MouseLeave += (sender, e) => { btnBrowseCopyDestination.Cursor = Cursors.Default; };
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
                    MessageBox.Show("ログ出力先1（office）を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOutputFolder2.Text))
                {
                    MessageBox.Show("ログ出力先2（PDF）を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCopyDestination.Text))
                {
                    MessageBox.Show("複写先フォルダを指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // フォルダの存在確認
                if (!Directory.Exists(txtSourceFolder.Text))
                {
                    MessageBox.Show("指定された選択フォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ログ出力先1はファイルパスなので、親ディレクトリの存在確認を行う
                string outputDir1 = Path.GetDirectoryName(txtOutputFolder.Text);
                if (!Directory.Exists(outputDir1))
                {
                    MessageBox.Show("指定されたログ出力先1（office）の保存先フォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ログ出力先2はファイルパスなので、親ディレクトリの存在確認を行う
                string outputDir2 = Path.GetDirectoryName(txtOutputFolder2.Text);
                if (!Directory.Exists(outputDir2))
                {
                    MessageBox.Show("指定されたログ出力先2（PDF）の保存先フォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(txtCopyDestination.Text))
                {
                    MessageBox.Show("指定された複写先フォルダが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                startInfo1.UseShellExecute = true;  // シェル実行を有効化
                startInfo1.CreateNoWindow = false;  // ウィンドウを表示
                
                ProcessStartInfo startInfo2 = new ProcessStartInfo();
                startInfo2.FileName = "cmd.exe";
                startInfo2.WorkingDirectory = Path.Combine(Application.StartupPath, "fpc_1_3_0");  // fpc_1_3_0フォルダを作業ディレクトリに設定
                startInfo2.UseShellExecute = true;  // シェル実行を有効化
                startInfo2.CreateNoWindow = false;  // ウィンドウを表示
                
                // チェックボックスの状態に応じて引数を追加
                string options1 = "";  // FPCCmd.exe用のオプション
                string options2 = "";  // FPCNOCmd.exe用のオプション

                // パスワード付きフォルダを除外するオプション
                if (chkExcludePasswordFolders.Checked)
                {
                    options1 += " --exclude-password";
                    options2 += " --exclude-password";
                }

                // 結果を出力しないオプション (USE_SM)
                if (chkNoOutput.Checked)
                {
                    options1 += " /sm";
                    options2 += " --sm";
                }

                // コメントを出力しないオプション (USE_NC)
                if (chkNoComment.Checked)
                {
                    options1 += " /nc";
                    options2 += " --nc";
                }

                // 出力結果の文字をダブルクォーテーションで囲むオプション (USE_DQ)
                if (chkDoubleQuote.Checked)
                {
                    options1 += " /dq";
                    options2 += " --dq";
                }

                // オプションを引数に追加
                startInfo1.Arguments = $"/c \"cd /d {Path.Combine(Application.StartupPath, "fpc_1_3_0")} && FPCCmd.exe /l:\"{txtOutputFolder.Text}\" /c:\"{txtSourceFolder.Text}\"{options1} & PAUSE\"";
                startInfo2.Arguments = $"/c \"cd /d {Path.Combine(Application.StartupPath, "fpc_1_3_0")} && FPCNOCmd.exe -l \"{txtOutputFolder2.Text}\" -c \"{txtSourceFolder.Text}\"{options2} & PAUSE\"";

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
                        message += "\n(複写元フォルダからパスワード付きファイルが削除されます)";
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
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "ログ出力先ファイルを指定してください";
                saveDialog.Filter = "ログファイル (*.log)|*.log|テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
                saveDialog.DefaultExt = "log";
                saveDialog.FileName = "FPCCmd.log";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = saveDialog.FileName;
                }
            }
        }

        /// <summary>
        /// ログ出力先2（PDF）の参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseOutput2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "ログ出力先2（PDF）ファイルを指定してください";
                saveDialog.Filter = "ログファイル (*.log)|*.log|テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
                saveDialog.DefaultExt = "log";
                saveDialog.FileName = "FPCNOCmd.log";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder2.Text = saveDialog.FileName;
                }
            }
        }

        /// <summary>
        /// 複写先フォルダの参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseCopyDestination_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "複写先フォルダを指定してください";
                folderDialog.ShowNewFolderButton = true;
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCopyDestination.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
