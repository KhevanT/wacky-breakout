using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    // Fields

    static bool initialized = false;

    // Audio source object
    static AudioSource audioSource;

    // Audio dictionary
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    // Properties

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    // Methods

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        // Initialise the initialised property :P
        initialized = true;

        // Initialising source
        audioSource = source;

        // Adding all audio clips to dict
        audioClips.Add(AudioClipName.BallBounce,
           Resources.Load<AudioClip>("Audio/Ball Bounce"));
        audioClips.Add(AudioClipName.BlockBreak,
           Resources.Load<AudioClip>("Audio/Block Break"));
        audioClips.Add(AudioClipName.ButtonClick,
           Resources.Load<AudioClip>("Audio/Button Click"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
