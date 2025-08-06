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
        private async void btnSubmit_Click(object sender, EventArgs e)
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
                    
                    // プロセスの完了を待つ
                    process1.WaitForExit();
                    process2.WaitForExit();
                    
                    // PRファイルの自動コピーを実行
                    await CopyPasswordProtectedFilesAsync();
                    
                    // 成功メッセージを表示
                    string message = "FPCCmd.exeとFPCNOCmd.exeの実行が完了しました！";
                    message += "\nPRファイルの自動コピーも完了しました。";
                    
                    // 削除機能が有効な場合のメッセージを追加
                    if (chkDeleteSourceFiles.Checked)
                    {
                        message += "\n複写元フォルダからのPRファイル削除も完了しました。";
                    }
                    
                    MessageBox.Show(message, "実行完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// パスワード保護されたファイル（PR）を複写先フォルダにコピーする
        /// </summary>
        private async Task CopyPasswordProtectedFilesAsync()
        {
            try
            {
                // 削除対象のファイルリストを保持
                List<string> filesToDelete = new List<string>();
                
                // ログファイルからPRファイルを抽出してコピー
                if (File.Exists(txtOutputFolder.Text))
                {
                    await ProcessLogFileAsync(txtOutputFolder.Text, filesToDelete);
                }
                
                if (File.Exists(txtOutputFolder2.Text))
                {
                    await ProcessLogFileAsync(txtOutputFolder2.Text, filesToDelete);
                }
                
                // チェックボックスがチェックされている場合、複写元フォルダからPRファイルを削除
                if (chkDeleteSourceFiles.Checked && filesToDelete.Count > 0)
                {
                    await DeleteSourceFilesAsync(filesToDelete);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PRファイルのコピー中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ログファイルを解析してPRファイルをコピーする
        /// </summary>
        /// <param name="logFilePath">ログファイルのパス</param>
        /// <param name="filesToDelete">削除対象のファイルリスト（出力パラメータ）</param>
        private async Task ProcessLogFileAsync(string logFilePath, List<string> filesToDelete)
        {
            try
            {
                using (StreamReader reader = new StreamReader(logFilePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        // CSV形式の行を解析
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            string status = parts[1].Trim();
                            string filePath = parts[2].Trim();
                            
                            // PRステータスのファイルのみ処理
                            if (status == "PR" && File.Exists(filePath))
                            {
                                await CopyFileWithRelativePathAsync(filePath);
                                filesToDelete.Add(filePath); // コピーしたファイルを削除リストに追加
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ログファイルが読めない場合は警告のみ表示
                Console.WriteLine($"ログファイル読み込みエラー: {ex.Message}");
            }
        }

        /// <summary>
        /// ファイルを相対パスを保持して複写先フォルダにコピーする
        /// </summary>
        /// <param name="sourceFilePath">コピー元ファイルのフルパス</param>
        private async Task CopyFileWithRelativePathAsync(string sourceFilePath)
        {
            try
            {
                // 選択フォルダからの相対パスを取得（.NET Framework互換実装）
                string relativePath = GetRelativePath(txtSourceFolder.Text, sourceFilePath);
                
                // 複写先のフルパスを構築
                string destinationPath = Path.Combine(txtCopyDestination.Text, relativePath);
                
                // ディレクトリが存在しない場合は作成
                string destinationDir = Path.GetDirectoryName(destinationPath);
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }
                
                // ファイルをコピー（上書きする）
                await Task.Run(() => File.Copy(sourceFilePath, destinationPath, true));
                
                Console.WriteLine($"コピー完了: {relativePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ファイルコピーエラー ({sourceFilePath}): {ex.Message}");
            }
        }

        /// <summary>
        /// 複写元フォルダから指定されたファイルを削除する
        /// </summary>
        /// <param name="filesToDelete">削除対象のファイルパスリスト</param>
        private async Task DeleteSourceFilesAsync(List<string> filesToDelete)
        {
            try
            {
                foreach (string filePath in filesToDelete)
                {
                    string relativePath = GetRelativePath(txtSourceFolder.Text, filePath);
                    string fullPathToDelete = Path.Combine(txtSourceFolder.Text, relativePath);

                    if (File.Exists(fullPathToDelete))
                    {
                        await Task.Run(() => File.Delete(fullPathToDelete));
                        Console.WriteLine($"削除完了: {relativePath}");
                    }
                    else
                    {
                        Console.WriteLine($"削除対象ファイルが見つかりません: {relativePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"複写元フォルダからのファイル削除中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// .NET Framework互換の相対パス取得メソッド
        /// </summary>
        /// <param name="basePath">基準パス</param>
        /// <param name="fullPath">フルパス</param>
        /// <returns>相対パス</returns>
        private string GetRelativePath(string basePath, string fullPath)
        {
            try
            {
                // パスを正規化
                string normalizedBasePath = Path.GetFullPath(basePath);
                string normalizedFullPath = Path.GetFullPath(fullPath);
                
                // 基準パスがフルパスに含まれているかチェック
                if (normalizedFullPath.StartsWith(normalizedBasePath, StringComparison.OrdinalIgnoreCase))
                {
                    // 基準パス部分を除去して相対パスを取得
                    string relativePath = normalizedFullPath.Substring(normalizedBasePath.Length);
                    
                    // 先頭のディレクトリセパレータを除去
                    if (relativePath.StartsWith(Path.DirectorySeparatorChar.ToString()) || 
                        relativePath.StartsWith(Path.AltDirectorySeparatorChar.ToString()))
                    {
                        relativePath = relativePath.Substring(1);
                    }
                    
                    return relativePath;
                }
                else
                {
                    // 基準パス外のファイルの場合はファイル名のみ返す
                    return Path.GetFileName(fullPath);
                }
            }
            catch
            {
                // エラーの場合はファイル名のみ返す
                return Path.GetFileName(fullPath);
            }
        }
    }
}
