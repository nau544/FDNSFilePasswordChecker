using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// プロセス実行に関する機能を提供するクラス
    /// </summary>
    public class ProcessExecutor
    {
        private readonly string workingDirectory;
        private readonly string fpccmdPath;
        private readonly string fpcnocmdPath;

        public ProcessExecutor(string workingDirectory)
        {
            this.workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            this.fpccmdPath = Path.Combine(workingDirectory, "FPCCmd.exe");
            this.fpcnocmdPath = Path.Combine(workingDirectory, "FPCNOCmd.exe");
        }

        /// <summary>
        /// 設定を検証する
        /// </summary>
        /// <returns>設定が有効な場合はtrue</returns>
        public bool ValidateConfiguration()
        {
            Logger.LogInfo("設定の検証を開始します");
            
            var validationResults = new[]
            {
                ValidateDirectory(workingDirectory, "作業ディレクトリ"),
                ValidateFile(fpccmdPath, "FPCCmd.exe"),
                ValidateFile(fpcnocmdPath, "FPCNOCmd.exe")
            };

            bool isValid = Array.TrueForAll(validationResults, result => result);
            
            if (isValid)
            {
                Logger.LogInfo("設定の検証が完了しました - すべて正常です");
            }
            else
            {
                Logger.LogError("設定の検証でエラーが発生しました");
            }
            
            return isValid;
        }

        /// <summary>
        /// ディレクトリの存在を検証する
        /// </summary>
        /// <param name="path">検証するパス</param>
        /// <param name="name">表示名</param>
        /// <returns>存在する場合はtrue</returns>
        private bool ValidateDirectory(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Logger.LogError($"{name}が見つかりません: {path}");
                MessageBox.Show($"{name}が見つかりません: {path}", "設定エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Logger.LogInfo($"{name}の検証完了: {path}");
            return true;
        }

        /// <summary>
        /// ファイルの存在を検証する
        /// </summary>
        /// <param name="path">検証するパス</param>
        /// <param name="name">表示名</param>
        /// <returns>存在する場合はtrue</returns>
        private bool ValidateFile(string path, string name)
        {
            if (!File.Exists(path))
            {
                Logger.LogError($"{name}が見つかりません: {path}");
                MessageBox.Show($"{name}が見つかりません: {path}", "設定エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Logger.LogInfo($"{name}の検証完了: {path}");
            return true;
        }

        /// <summary>
        /// 両方のプロセスを並列実行する
        /// </summary>
        /// <param name="sourceFolder">選択フォルダ</param>
        /// <param name="outputFolder1">ログ出力先1（office）</param>
        /// <param name="outputFolder2">ログ出力先2（PDF）</param>
        /// <param name="options1">FPCCmd.exe用のオプション</param>
        /// <param name="options2">FPCNOCmd.exe用のオプション</param>
        public async Task ExecuteProcessesAsync(string sourceFolder, string outputFolder1, string outputFolder2, string options1, string options2)
        {
            try
            {
                Logger.LogInfo("プロセス実行を開始します");
                
                var startInfo1 = CreateProcessStartInfo(outputFolder1, sourceFolder, options1, "FPCCmd.exe");
                var startInfo2 = CreateProcessStartInfo(outputFolder2, sourceFolder, options2, "FPCNOCmd.exe");

                // 両方のプロセスを同時に開始
                using (var process1 = new Process { StartInfo = startInfo1 })
                using (var process2 = new Process { StartInfo = startInfo2 })
                {
                    Logger.LogInfo("FPCCmd.exeとFPCNOCmd.exeを並列実行します");
                    
                    // 同時並行で実行
                    process1.Start();
                    process2.Start();
                    
                    Logger.LogInfo("プロセスの完了を待機中...");
                    
                    // プロセスの完了を待つ
                    await Task.Run(() => process1.WaitForExit());
                    await Task.Run(() => process2.WaitForExit());
                    
                    Logger.LogInfo("プロセス実行が完了しました");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"プロセス実行中にエラーが発生しました: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// プロセス開始情報を作成する
        /// </summary>
        /// <param name="outputFolder">出力フォルダ</param>
        /// <param name="sourceFolder">ソースフォルダ</param>
        /// <param name="options">オプション</param>
        /// <param name="executableName">実行ファイル名</param>
        /// <returns>プロセス開始情報</returns>
        private ProcessStartInfo CreateProcessStartInfo(string outputFolder, string sourceFolder, string options, string executableName)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WorkingDirectory = workingDirectory,
                UseShellExecute = true,
                CreateNoWindow = false
            };

            string arguments = executableName == "FPCCmd.exe" 
                ? $"/c \"cd /d {workingDirectory} && FPCCmd.exe /l:\"{outputFolder}\" /c:\"{sourceFolder}\"{options} & PAUSE\""
                : $"/c \"cd /d {workingDirectory} && FPCNOCmd.exe -l \"{outputFolder}\" -c \"{sourceFolder}\"{options} & PAUSE\"";

            startInfo.Arguments = arguments;
            return startInfo;
        }

        /// <summary>
        /// オプション文字列を生成する
        /// </summary>
        /// <param name="noOutput">結果を出力しない</param>
        /// <param name="noComment">コメントを出力しない</param>
        /// <param name="doubleQuote">出力結果の文字をダブルクォーテーションで囲む</param>
        /// <returns>FPCCmd.exe用とFPCNOCmd.exe用のオプション文字列のタプル</returns>
        public (string options1, string options2) BuildOptions(bool noOutput, bool noComment, bool doubleQuote)
        {
            var options1 = new System.Text.StringBuilder();
            var options2 = new System.Text.StringBuilder();

            // 結果を出力しないオプション (USE_SM)
            if (noOutput)
            {
                options1.Append(" /sm");
                options2.Append(" --sm");
            }

            // コメントを出力しないオプション (USE_NC)
            if (noComment)
            {
                options1.Append(" /nc");
                options2.Append(" --nc");
            }

            // 出力結果の文字をダブルクォーテーションで囲むオプション (USE_DQ)
            if (doubleQuote)
            {
                options1.Append(" /dq");
                options2.Append(" --dq");
            }

            return (options1.ToString(), options2.ToString());
        }
    }
}
