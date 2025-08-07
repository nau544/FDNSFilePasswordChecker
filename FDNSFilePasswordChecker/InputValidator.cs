using System;
using System.IO;
using System.Windows.Forms;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// 入力値の検証を行うクラス
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// すべての入力値を検証する
        /// </summary>
        /// <param name="sourceFolder">選択フォルダ</param>
        /// <param name="outputFolder1">ログ出力先1</param>
        /// <param name="outputFolder2">ログ出力先2</param>
        /// <param name="copyDestination">移動先フォルダ</param>
        /// <returns>検証が成功した場合はtrue</returns>
        public static bool ValidateAllInputs(string sourceFolder, string outputFolder1, string outputFolder2, string copyDestination)
        {
            var validations = new[]
            {
                ValidateSourceFolder(sourceFolder),
                ValidateOutputFolder(outputFolder1, "ログ出力先1（office）"),
                ValidateOutputFolder(outputFolder2, "ログ出力先2（PDF）"),
                ValidateCopyDestination(copyDestination)
            };

            return Array.TrueForAll(validations, result => result);
        }

        /// <summary>
        /// 選択フォルダの検証
        /// </summary>
        /// <param name="sourceFolder">選択フォルダのパス</param>
        /// <returns>検証が成功した場合はtrue</returns>
        public static bool ValidateSourceFolder(string sourceFolder)
        {
            if (string.IsNullOrWhiteSpace(sourceFolder))
            {
                ShowErrorMessage("選択フォルダを指定してください。");
                return false;
            }

            if (!Directory.Exists(sourceFolder))
            {
                ShowErrorMessage("指定された選択フォルダが存在しません。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ログ出力先の検証
        /// </summary>
        /// <param name="outputFolder">ログ出力先のパス</param>
        /// <param name="displayName">表示名</param>
        /// <returns>検証が成功した場合はtrue</returns>
        public static bool ValidateOutputFolder(string outputFolder, string displayName)
        {
            if (string.IsNullOrWhiteSpace(outputFolder))
            {
                ShowErrorMessage($"{displayName}を指定してください。");
                return false;
            }

            string outputDir = Path.GetDirectoryName(outputFolder);
            if (!Directory.Exists(outputDir))
            {
                ShowErrorMessage($"指定された{displayName}の保存先フォルダが存在しません。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 移動先フォルダの検証
        /// </summary>
        /// <param name="copyDestination">移動先フォルダのパス</param>
        /// <returns>検証が成功した場合はtrue</returns>
        public static bool ValidateCopyDestination(string copyDestination)
        {
            if (string.IsNullOrWhiteSpace(copyDestination))
            {
                ShowErrorMessage("パスワード付きファイルの移動先フォルダを指定してください。");
                return false;
            }

            if (!Directory.Exists(copyDestination))
            {
                ShowErrorMessage("指定された移動先フォルダが存在しません。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// エラーメッセージを表示する
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        private static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
