using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// ファイル処理に関する機能を提供するクラス
    /// </summary>
    public class FileProcessor
    {
        private readonly string sourceFolder;
        private readonly string destinationFolder;

        public FileProcessor(string sourceFolder, string destinationFolder)
        {
            this.sourceFolder = sourceFolder ?? throw new ArgumentNullException(nameof(sourceFolder));
            this.destinationFolder = destinationFolder ?? throw new ArgumentNullException(nameof(destinationFolder));
        }

        /// <summary>
        /// パスワード保護されたファイル（PR）を移動先フォルダに移動またはコピーする
        /// </summary>
        /// <param name="logFiles">ログファイルのリスト</param>
        /// <param name="isCopyMode">コピーモードかどうか</param>
        public async Task ProcessPasswordProtectedFilesAsync(List<string> logFiles, bool isCopyMode)
        {
            if (logFiles == null || logFiles.Count == 0)
            {
                Logger.LogWarning("ログファイルが指定されていません");
                return;
            }

            try
            {
                Logger.LogInfo("PRファイル処理を開始します");
                var filesToProcess = new List<string>();
                
                // ログファイルからPRファイルを抽出して処理
                foreach (string logFile in logFiles)
                {
                    if (File.Exists(logFile))
                    {
                        await ProcessLogFileAsync(logFile, filesToProcess);
                    }
                    else
                    {
                        Logger.LogWarning($"ログファイルが見つかりません: {logFile}");
                    }
                }
                
                // 処理対象ファイルがある場合のみ処理を実行
                if (filesToProcess.Count > 0)
                {
                    Logger.LogInfo($"{filesToProcess.Count}個のファイルを処理します");
                    if (isCopyMode)
                    {
                        await CopySourceFilesAsync(filesToProcess);
                    }
                    else
                    {
                        await DeleteSourceFilesAsync(filesToProcess);
                    }
                }
                else
                {
                    Logger.LogInfo("処理対象のPRファイルが見つかりませんでした");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"PRファイルの処理中にエラーが発生しました: {ex.Message}");
                MessageBox.Show($"PRファイルの処理中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ログファイルを解析してPRファイルを処理する
        /// </summary>
        /// <param name="logFilePath">ログファイルのパス</param>
        /// <param name="filesToProcess">処理対象のファイルリスト（出力パラメータ）</param>
        private async Task ProcessLogFileAsync(string logFilePath, List<string> filesToProcess)
        {
            try
            {
                Logger.LogInfo($"ログファイルを処理中: {logFilePath}");
                int processedCount = 0;
                
                using (var reader = new StreamReader(logFilePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var filePath = ParseLogLine(line);
                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            await CopyFileWithRelativePathAsync(filePath);
                            filesToProcess.Add(filePath);
                            processedCount++;
                        }
                    }
                }
                
                Logger.LogInfo($"ログファイル処理完了: {processedCount}個のPRファイルを検出");
            }
            catch (Exception ex)
            {
                Logger.LogError($"ログファイル読み込みエラー ({logFilePath}): {ex.Message}");
            }
        }

        /// <summary>
        /// ログ行を解析してファイルパスを抽出する
        /// </summary>
        /// <param name="line">ログ行</param>
        /// <returns>ファイルパス（PRステータスでない場合はnull）</returns>
        private string ParseLogLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            string[] parts = line.Split(',');
            if (parts.Length >= 3)
            {
                string status = parts[1].Trim();
                string filePath = parts[2].Trim();
                
                return status == "PR" ? filePath : null;
            }
            
            return null;
        }

        /// <summary>
        /// ファイルを相対パスを保持して移動先フォルダにコピーする
        /// </summary>
        /// <param name="sourceFilePath">コピー元ファイルのフルパス</param>
        private async Task CopyFileWithRelativePathAsync(string sourceFilePath)
        {
            try
            {
                string relativePath = FileUtils.GetRelativePath(sourceFolder, sourceFilePath);
                string destinationPath = Path.Combine(destinationFolder, relativePath);
                
                // ファイルを安全にコピー
                if (FileUtils.SafeCopyFile(sourceFilePath, destinationPath, true))
                {
                    Logger.LogInfo($"コピー完了: {relativePath}");
                }
                else
                {
                    Logger.LogError($"コピー失敗: {relativePath}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"ファイルコピーエラー ({sourceFilePath}): {ex.Message}");
            }
        }

        /// <summary>
        /// 選択フォルダに指定されたファイルをコピーする
        /// </summary>
        /// <param name="filesToCopy">コピー対象のファイルパスリスト</param>
        private Task CopySourceFilesAsync(List<string> filesToCopy)
        {
            try
            {
                Logger.LogInfo("選択フォルダへのファイルコピーを開始します");
                int copiedCount = 0;
                
                foreach (string filePath in filesToCopy)
                {
                    string relativePath = FileUtils.GetRelativePath(sourceFolder, filePath);
                    string fullPathToCopy = Path.Combine(sourceFolder, relativePath);

                    if (File.Exists(fullPathToCopy))
                    {
                        Logger.LogInfo($"コピー完了（元ファイル保持）: {relativePath}");
                        copiedCount++;
                    }
                    else
                    {
                        Logger.LogWarning($"コピー対象ファイルが見つかりません: {relativePath}");
                    }
                }
                
                Logger.LogInfo($"選択フォルダへのコピー完了: {copiedCount}個のファイル");
            }
            catch (Exception ex)
            {
                Logger.LogError($"選択フォルダへのファイルコピー中にエラーが発生しました: {ex.Message}");
                MessageBox.Show($"選択フォルダへのファイルコピー中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return Task.CompletedTask;
        }

        /// <summary>
        /// 選択フォルダから指定されたファイルを削除する
        /// </summary>
        /// <param name="filesToDelete">削除対象のファイルパスリスト</param>
        private async Task DeleteSourceFilesAsync(List<string> filesToDelete)
        {
            try
            {
                Logger.LogInfo("選択フォルダからのファイル削除を開始します");
                int deletedCount = 0;
                
                foreach (string filePath in filesToDelete)
                {
                    string relativePath = FileUtils.GetRelativePath(sourceFolder, filePath);
                    string fullPathToDelete = Path.Combine(sourceFolder, relativePath);

                    if (File.Exists(fullPathToDelete))
                    {
                        if (FileUtils.SafeDeleteFile(fullPathToDelete))
                        {
                            Logger.LogInfo($"削除完了: {relativePath}");
                            deletedCount++;
                        }
                        else
                        {
                            Logger.LogError($"削除失敗: {relativePath}");
                        }
                    }
                    else
                    {
                        Logger.LogWarning($"削除対象ファイルが見つかりません: {relativePath}");
                    }
                }
                
                Logger.LogInfo($"選択フォルダからの削除完了: {deletedCount}個のファイル");
            }
            catch (Exception ex)
            {
                Logger.LogError($"選択フォルダからのファイル削除中にエラーが発生しました: {ex.Message}");
                MessageBox.Show($"選択フォルダからのファイル削除中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
