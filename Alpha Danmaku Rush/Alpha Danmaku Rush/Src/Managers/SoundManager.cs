using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

public class SoundManager
{
    private ContentManager contentManager;
    private Dictionary<string, SoundEffect> soundEffects;
    private List<Song> levelMusic;
    private Song menuMusic;

    public SoundManager(ContentManager contentManager)
    {
        this.contentManager = contentManager;
        soundEffects = new Dictionary<string, SoundEffect>();
        levelMusic = new List<Song>();
        LoadContent();
    }

    private void LoadContent()
    {
        // 加载音乐
        menuMusic = contentManager.Load<Song>("Music/MenuMusic");
        levelMusic.Add(contentManager.Load<Song>("Music/LevelMusic1"));
        levelMusic.Add(contentManager.Load<Song>("Music/LevelMusic2"));
        // 加载更多关卡音乐...

        // 加载音效
        soundEffects["BulletFire"] = contentManager.Load<SoundEffect>("Sounds/BulletFire");
        soundEffects["EnemyHit"] = contentManager.Load<SoundEffect>("Sounds/EnemyHit");
        soundEffects["PlayerHit"] = contentManager.Load<SoundEffect>("Sounds/PlayerHit");
        // 加载更多音效...
    }

    public void PlayMenuMusic()
    {
        MediaPlayer.Play(menuMusic);
        MediaPlayer.IsRepeating = true;
    }

    public void PlayLevelMusic()
    {
        var random = new Random();
        int index = random.Next(levelMusic.Count);
        MediaPlayer.Play(levelMusic[index]);
        MediaPlayer.IsRepeating = true;
    }

    public void PlaySoundEffect(string effectName)
    {
        if (soundEffects.ContainsKey(effectName))
        {
            soundEffects[effectName].Play();
        }
    }

    // 你可以添加更多方法，比如停止音乐、调整音量等
}

