using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

#region Enum
/// <summary>
/// An enumeration that holds every sound effect in the game.
/// </summary>
public enum SoundFile
{
    // Put the name of the file (make sure it's inside Resources) without the extension, ex. Fire.wav -> Fire
}
# endregion

public class SoundManager : SingletonBehavior<SoundManager>
{
    const string AUDIO_FILE_LOCATION = "Audio";

    float volume = 1f;

    /// <summary>
    /// A collection of all of the sound effects in the game.
    /// </summary>
    private Dictionary<SoundFile, AudioClip> SoundEffects
    { get; set; }

    /// <summary>
    /// Source for the sound effects
    /// </summary>
    private static AudioSource SoundEffectSource
    { get; set; }

    /// <summary>
    /// Source for the BGM
    /// </summary>
    private static AudioSource BGMSource
    { get; set; }

    protected override void Init()
    {
        SoundEffects = new Dictionary<SoundFile, AudioClip>();

        //Create a temporary dictionary that loads all of the Audio files from a specific location.
        //Key = name of file, Value = file itself.
        Dictionary<string, AudioClip> clips = Resources.LoadAll<AudioClip>(AUDIO_FILE_LOCATION).ToDictionary(t => t.name);

        //Iterates through the loaded sound files and adds them to the Enum to AudioClip dictionary.
        foreach (KeyValuePair<string, AudioClip> c in clips)
        {
            SoundEffects.Add((SoundFile)Enum.Parse(typeof(SoundFile), c.Key, true), c.Value);
        }

        //Creates a single sound effect source. Can play every sound in the game through this unless you want to have different effects
        //such as different pitch/volume for different sources.
        if (SoundEffectSource == null)
        {
            Debug.Log("Creating SoundEffectSource");
            SoundEffectSource = new GameObject("SoundEffectSource", typeof(AudioSource)).GetComponent<AudioSource>();
            SoundEffectSource.volume = 1.3f;
            DontDestroyOnLoad(SoundEffectSource.gameObject);
        }

        //Creates a single background music source.
        if (BGMSource == null)
        {
            Debug.Log("Creating BGMSource");
            BGMSource = new GameObject("BGMSource", typeof(AudioSource)).GetComponent<AudioSource>();
            BGMSource.volume = .4f;
            BGMSource.loop = true;
            DontDestroyOnLoad(BGMSource.gameObject);
        }
    }

    /// <summary>
    /// Updates the audio manager
    /// </summary>
    public void Update()
    {
        
    }

    /// <summary>
    /// Plays a single sound effect
    /// </summary>
    /// <param name="sound">The sound effect we want to play</param>
    public void DoPlayOneShot(SoundFile[] sounds, Vector3? location = null, float volumeScale = 1)
    {
        if (location == null)
            location = Vector3.zero;
        AudioSource.PlayClipAtPoint(SoundEffects[sounds[UnityEngine.Random.Range(0, sounds.Length)]], (Vector3)location, volumeScale * volume);
    }

    /// <summary>
    /// Changes the BGM to something else
    /// </summary>
    /// <param name="sound">The bgm sound we want to play</param>
    public void ChangeBGM(SoundFile sound)
    {
        BGMSource.clip = SoundEffects[sound];
        BGMSource.Play();
    }
}