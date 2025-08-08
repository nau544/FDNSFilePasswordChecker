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
using System.Configuration;
using System.Text.RegularExpressions;

namespace FDNSFilePasswordChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// フォームの初期化を行う
        /// </summary>
        private void InitializeForm()
        {
            this.Resize += Form1_Resize;
            CenterSubmitButton();
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
            int centerX = (this.ClientSize.Width - btnSubmit.Width) / 2;
            btnSubmit.Location = new Point(centerX, btnSubmit.Location.Y);
        }

        /// <summary>
        /// ボタンのホバー効果を設定する
        /// </summary>
        private void SetupButtonHoverEffects()
        {
            SetupButtonHoverEffect(btnSubmit);
            SetupButtonHoverEffect(btnBrowseSource);
            SetupButtonHoverEffect(btnBrowseOutput);
            SetupButtonHoverEffect(btnBrowseOutput2);
            SetupButtonHoverEffect(btnBrowseCopyDestination);
        }

        /// <summary>
        /// 個別のボタンにホバー効果を設定する
        /// </summary>
        /// <param name="button">設定対象のボタン</param>
        private void SetupButtonHoverEffect(Button button)
        {
            button.MouseEnter += (sender, e) => { button.Cursor = Cursors.Hand; };
            button.MouseLeave += (sender, e) => { button.Cursor = Cursors.Default; };
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
                // ログファイルの設定
                Logger.SetLogFilePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FDNSFilePasswordChecker.log"));
                Logger.LogInfo("アプリケーション実行を開始します");
                
                if (!ValidateInputs())
                {
                    Logger.LogWarning("入力値の検証でエラーが発生しました");
                    return;
                }

                if (!await ExecuteProcesses())
                {
                    Logger.LogError("プロセス実行でエラーが発生しました");
                    return;
                }

                await ProcessFiles();
                ShowSuccessMessage();
                
                Logger.LogInfo("アプリケーション実行が正常に完了しました");
            }
            catch (Exception ex)
            {
                Logger.LogError($"実行中にエラーが発生しました: {ex.Message}");
                MessageBox.Show($"実行中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <returns>成功した場合はtrue</returns>
        private async Task<bool> ExecuteProcesses()
        {
            string workingDirectory = AppConfigurationManager.GetWorkingDirectory();
            var processExecutor = new ProcessExecutor(workingDirectory);
            
            if (!processExecutor.ValidateConfiguration())
            {
                return false;
            }

            var (options1, options2) = processExecutor.BuildOptions(
                chkNoOutput.Checked, 
                chkNoComment.Checked, 
                chkDoubleQuote.Checked
            );

            await processExecutor.ExecuteProcessesAsync(
                txtSourceFolder.Text,
                txtOutputFolder.Text,
                txtOutputFolder2.Text,
                options1,
                options2
            );

            return true;
        }

        /// <summary>
        /// ファイル処理を実行する
        /// </summary>
        private async Task ProcessFiles()
        {
            var fileProcessor = new FileProcessor(txtSourceFolder.Text, txtCopyDestination.Text);
            var logFiles = new List<string> { txtOutputFolder.Text, txtOutputFolder2.Text };
            await fileProcessor.ProcessPasswordProtectedFilesAsync(logFiles, chkDeleteSourceFiles.Checked);
        }

        
        /// <summary>
        /// 入力値の検証を行う
        /// </summary>
        /// <returns>検証が成功した場合はtrue</returns>
        private bool ValidateInputs()
        {
            return InputValidator.ValidateAllInputs(
                txtSourceFolder.Text,
                txtOutputFolder.Text,
                txtOutputFolder2.Text,
                txtCopyDestination.Text
            );
        }



        /// <summary>
        /// 成功メッセージを表示する
        /// </summary>
        private void ShowSuccessMessage()
        {
            var message = new StringBuilder();
            message.AppendLine("FPCCmd.exeとFPCNOCmd.exeの実行が完了しました！");
            message.AppendLine("PRファイルの自動処理も完了しました。");
            
            if (chkDeleteSourceFiles.Checked)
            {
                message.AppendLine("選択フォルダへのコピーも完了しました。");
            }
            else
            {
                message.AppendLine("選択フォルダからのファイル移動も完了しました。");
            }
            
            MessageBox.Show(message.ToString(), "実行完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 選択フォルダの参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog("選択フォルダを指定してください", txtSourceFolder, false);
        }

        /// <summary>
        /// ログ出力先の参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            ShowSaveFileDialog("ログ出力先ファイルを指定してください", "FPCCmd.log", txtOutputFolder);
        }

        /// <summary>
        /// ログ出力先2（PDF）の参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseOutput2_Click(object sender, EventArgs e)
        {
            ShowSaveFileDialog("ログ出力先2（PDF）ファイルを指定してください", "FPCNOCmd.log", txtOutputFolder2);
        }

        /// <summary>
        /// 複写先フォルダの参照ボタンのクリックイベントハンドラー
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void btnBrowseCopyDestination_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog("パスワード付きファイルの移動先フォルダを指定してください", txtCopyDestination, true);
        }

        /// <summary>
        /// フォルダブラウザダイアログを表示する
        /// </summary>
        /// <param name="description">説明</param>
        /// <param name="textBox">結果を設定するテキストボックス</param>
        /// <param name="showNewFolderButton">新規フォルダボタンを表示するかどうか</param>
        private void ShowFolderBrowserDialog(string description, TextBox textBox, bool showNewFolderButton)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = description;
                folderDialog.ShowNewFolderButton = showNewFolderButton;
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// ファイル保存ダイアログを表示する
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="defaultFileName">デフォルトファイル名</param>
        /// <param name="textBox">結果を設定するテキストボックス</param>
        private void ShowSaveFileDialog(string title, string defaultFileName, TextBox textBox)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = title;
                saveDialog.Filter = "ログファイル (*.log)|*.log|テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
                saveDialog.DefaultExt = "log";
                saveDialog.FileName = defaultFileName;
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = saveDialog.FileName;
                }
            }
        }
    }
}
