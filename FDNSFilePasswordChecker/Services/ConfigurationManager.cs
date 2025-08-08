using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace FDNSFilePasswordChecker
{
    /// <summary>
    /// 設定管理に関する機能を提供するクラス
    /// </summary>
    public static class AppConfigurationManager
    {
        private const string DEFAULT_WORKING_DIRECTORY = "fpc_1_3_0";
        private const string DEFAULT_FPCCMD_PATH = "fpc_1_3_0\\FPCCmd.exe";
        private const string DEFAULT_FPCNOCMD_PATH = "fpc_1_3_0\\FPCNOCmd.exe";

        /// <summary>
        /// アプリケーション設定を取得する
        /// </summary>
        /// <param name="key">設定キー</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns>設定値</returns>
        public static string GetAppSetting(string key, string defaultValue = "")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return defaultValue;
            }

            try
            {
                // まずConfigurationManagerを試す
                string value = System.Configuration.ConfigurationManager.AppSettings[key];
                if (!string.IsNullOrEmpty(value))
                {
                    Logger.LogDebug($"設定値を取得しました: {key} = {value}");
                    return value;
                }
                
                Logger.LogDebug($"設定値が見つからないためデフォルト値を使用: {key} = {defaultValue}");
                return defaultValue;
            }
            catch (System.Reflection.ReflectionTypeLoadException)
            {
                // System.Configurationアセンブリが読み込めない場合
                Logger.LogWarning("System.Configurationアセンブリが読み込めないため、ファイルから直接読み込みます");
                return GetAppSettingFromFile(key, defaultValue);
            }
            catch (Exception ex)
            {
                // その他のエラーの場合
                Logger.LogError($"設定値の取得でエラーが発生しました: {ex.Message}");
                return GetAppSettingFromFile(key, defaultValue);
            }
        }

        /// <summary>
        /// App.configファイルから直接設定を読み込む
        /// </summary>
        /// <param name="key">設定キー</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns>設定値</returns>
        private static string GetAppSettingFromFile(string key, string defaultValue)
        {
            try
            {
                string configPath = GetConfigFilePath();
                if (!File.Exists(configPath))
                {
                    return defaultValue;
                }

                string content = File.ReadAllText(configPath);
                string pattern = $@"<add key=""{Regex.Escape(key)}"" value=""([^""]+)""";
                var match = Regex.Match(content, pattern);
                
                return match.Success ? match.Groups[1].Value : defaultValue;
            }
            catch (Exception)
            {
                // エラーの場合はデフォルト値を返す
                return defaultValue;
            }
        }

        /// <summary>
        /// 設定ファイルのパスを取得する
        /// </summary>
        /// <returns>設定ファイルのパス</returns>
        private static string GetConfigFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config");
        }

        /// <summary>
        /// 作業ディレクトリのパスを取得する
        /// </summary>
        /// <returns>作業ディレクトリのパス</returns>
        public static string GetWorkingDirectory()
        {
            string workingDir = GetAppSetting("WorkingDirectory", DEFAULT_WORKING_DIRECTORY);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, workingDir);
        }

        /// <summary>
        /// FPCCmd.exeのパスを取得する
        /// </summary>
        /// <returns>FPCCmd.exeのパス</returns>
        public static string GetFPCCmdPath()
        {
            string fpccmdPath = GetAppSetting("FPCCmdPath", DEFAULT_FPCCMD_PATH);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fpccmdPath);
        }

        /// <summary>
        /// FPCNOCmd.exeのパスを取得する
        /// </summary>
        /// <returns>FPCNOCmd.exeのパス</returns>
        public static string GetFPCNOCmdPath()
        {
            string fpcnocmdPath = GetAppSetting("FPCNOCmdPath", DEFAULT_FPCNOCMD_PATH);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fpcnocmdPath);
        }
    }
}
