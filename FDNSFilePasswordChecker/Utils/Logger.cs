using System;
using System.IO;
using System.Text;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// ログ機能を提供するクラス
    /// </summary>
    public static class Logger
    {
        private static readonly object lockObject = new object();
        private static string logFilePath;

        /// <summary>
        /// ログファイルのパスを設定する
        /// </summary>
        /// <param name="path">ログファイルのパス</param>
        public static void SetLogFilePath(string path)
        {
            logFilePath = path;
        }

        /// <summary>
        /// 情報ログを記録する
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// 警告ログを記録する
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        /// <summary>
        /// エラーログを記録する
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        /// <summary>
        /// デバッグログを記録する
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void LogDebug(string message)
        {
            WriteLog("DEBUG", message);
        }

        /// <summary>
        /// ログを書き込む
        /// </summary>
        /// <param name="level">ログレベル</param>
        /// <param name="message">ログメッセージ</param>
        private static void WriteLog(string level, string message)
        {
            lock (lockObject)
            {
                try
                {
                    var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    
                    // コンソールに出力
                    Console.WriteLine(logEntry);
                    
                    // ログファイルに出力（設定されている場合）
                    if (!string.IsNullOrEmpty(logFilePath))
                    {
                        string logDir = Path.GetDirectoryName(logFilePath);
                        if (!Directory.Exists(logDir))
                        {
                            Directory.CreateDirectory(logDir);
                        }
                        
                        File.AppendAllText(logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                    }
                }
                catch (Exception ex)
                {
                    // ログ書き込みエラーは無視する（無限ループを防ぐため）
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] ログ書き込みエラー: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// ログファイルをクリアする
        /// </summary>
        public static void ClearLog()
        {
            lock (lockObject)
            {
                try
                {
                    if (!string.IsNullOrEmpty(logFilePath) && File.Exists(logFilePath))
                    {
                        File.Delete(logFilePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ログファイルクリアエラー: {ex.Message}");
                }
            }
        }
    }
}
