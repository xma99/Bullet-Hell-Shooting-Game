using System.Text.Json;

namespace Alpha_Danmaku_Rush.Src.UI;

using System;
using System.IO;
using System.Text.Json;

public class Settings
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public DifficultyLevel GameDifficulty { get; set; }
    public float Volume { get; set; }

    private static readonly string SettingsFilePath = "Save/settings.json";

    public Settings()
    {
        // 默认设置
        GameDifficulty = DifficultyLevel.Medium;
        Volume = 0.5f; // 假设音量大小在0（静音）到1（最大）之间
    }

    public void SaveSettings()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(SettingsFilePath, jsonString);
    }

    public static Settings LoadSettings()
    {
        if (File.Exists(SettingsFilePath))
        {
            string jsonString = File.ReadAllText(SettingsFilePath);
            return JsonSerializer.Deserialize<Settings>(jsonString);
        }
        return new Settings();
    }

    public void ChangeSettings(DifficultyLevel difficulty, float volume)
    {
        GameDifficulty = difficulty;
        Volume = volume;
        SaveSettings();
    }
}
