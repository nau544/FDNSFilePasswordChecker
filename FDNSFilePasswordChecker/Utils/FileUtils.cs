using System;
using System.IO;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// ファイル操作のユーティリティクラス
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 相対パスを取得する
        /// </summary>
        /// <param name="basePath">基準パス</param>
        /// <param name="fullPath">フルパス</param>
        /// <returns>相対パス</returns>
        public static string GetRelativePath(string basePath, string fullPath)
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

        /// <summary>
        /// ディレクトリが存在しない場合は作成する
        /// </summary>
        /// <param name="path">ディレクトリパス</param>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// ファイルを安全にコピーする
        /// </summary>
        /// <param name="sourcePath">コピー元パス</param>
        /// <param name="destinationPath">コピー先パス</param>
        /// <param name="overwrite">上書きするかどうか</param>
        /// <returns>成功した場合はtrue</returns>
        public static bool SafeCopyFile(string sourcePath, string destinationPath, bool overwrite = true)
        {
            try
            {
                // ディレクトリが存在しない場合は作成
                string destinationDir = Path.GetDirectoryName(destinationPath);
                EnsureDirectoryExists(destinationDir);
                
                // ファイルをコピー
                File.Copy(sourcePath, destinationPath, overwrite);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError($"ファイルコピーエラー ({sourcePath}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// ファイルを安全に削除する
        /// </summary>
        /// <param name="filePath">削除するファイルのパス</param>
        /// <returns>成功した場合はtrue</returns>
        public static bool SafeDeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError($"ファイル削除エラー ({filePath}): {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// ファイルサイズを人間が読みやすい形式で取得する
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ファイルサイズの文字列表現</returns>
        public static string GetFileSizeString(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return "0 B";

                long bytes = new FileInfo(filePath).Length;
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                double size = bytes;

                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size /= 1024;
                }

                return $"{size:0.##} {sizes[order]}";
            }
            catch
            {
                return "0 B";
            }
        }
    }
}
