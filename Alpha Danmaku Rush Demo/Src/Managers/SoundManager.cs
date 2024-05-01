using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers;

public class SoundManager
{
    private ContentManager _content;
    private Dictionary<string, SoundEffect> _soundEffects;
    private Song _backgroundMusic;
    private bool _isMusicPlaying;

    public SoundManager(ContentManager content)
    {
        _content = content;
        _soundEffects = new Dictionary<string, SoundEffect>();
    }

    public void LoadSound(string key, string assetName)
    {
        _soundEffects[key] = _content.Load<SoundEffect>(assetName);
    }

    public void PlaySound(string key)
    {
        if (_soundEffects.ContainsKey(key))
        {
            _soundEffects[key].Play();
        }
    }

    public void LoadBackgroundMusic(string assetName)
    {
        _backgroundMusic = _content.Load<Song>(assetName);
    }

    public void PlayBackgroundMusic()
    {
        if (!_isMusicPlaying)
        {
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.IsRepeating = true;
            _isMusicPlaying = true;
        }
    }

    public void StopBackgroundMusic()
    {
        MediaPlayer.Stop();
        _isMusicPlaying = false;
    }
}
