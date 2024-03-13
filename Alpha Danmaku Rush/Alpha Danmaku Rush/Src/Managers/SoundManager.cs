namespace Alpha_Danmaku_Rush.Src.Managers;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

public class SoundManager
{
    private ContentManager content;
    private Dictionary<string, SoundEffect> soundEffects;
    private Dictionary<string, Song> songs;

    public SoundManager(ContentManager content)
    {
        this.content = content;
        soundEffects = new Dictionary<string, SoundEffect>();
        songs = new Dictionary<string, Song>();
    }

    public void LoadSoundEffect(string name)
    {
        if (!soundEffects.ContainsKey(name))
        {
            soundEffects[name] = content.Load<SoundEffect>(name);
        }
    }

    public void PlaySoundEffect(string name)
    {
        if (soundEffects.ContainsKey(name))
        {
            soundEffects[name].Play();
        }
    }

    public void LoadSong(string name)
    {
        if (!songs.ContainsKey(name))
        {
            songs[name] = content.Load<Song>(name);
        }
    }

    public void PlaySong(string name, bool isRepeating = false)
    {
        if (songs.ContainsKey(name))
        {
            MediaPlayer.IsRepeating = isRepeating;
            MediaPlayer.Play(songs[name]);
        }
    }

    public void StopMusic()
    {
        MediaPlayer.Stop();
    }

    // Additional methods to pause, resume music, adjust volume, etc., can be added here.
}
