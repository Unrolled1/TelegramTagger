using System;
using System.IO;

namespace TelegramTags
{
    public static class Paths
    {
        public static string DataFolder =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "TelegramTags");

        public static string HashtagsFile =>
            Path.Combine(DataFolder, "hashtags.json");

        public static void EnsureDataFolder()
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
        }
    }
}