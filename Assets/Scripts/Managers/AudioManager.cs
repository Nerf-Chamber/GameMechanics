using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameSounds
{
    SoundOne,
    SoundTwo,
    SoundThree
}

[Serializable]
public class SoundEntry
{
    public GameSounds sound;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<SoundEntry> sounds = new List<SoundEntry>();
    private Dictionary<GameSounds, AudioClip> clipList = new Dictionary<GameSounds, AudioClip>();

    private void Awake()
    {
        Instance = this;

        foreach (SoundEntry entry in sounds)
        {
            if (entry.clip == null)
            {
                Debug.LogWarning($"AudioClip buit per {entry.sound}");
                continue;
            }
            if (!clipList.TryAdd(entry.sound, entry.clip)) Debug.LogWarning($"So duplicat: {entry.sound}");
        }
    }

    public void PlaySound(AudioSource aus, GameSounds sound)
    {
        if (clipList.TryGetValue(sound, out AudioClip clip))
        {
            aus.clip = clip;
            aus.Play();
        }
        else { Debug.LogWarning($"{sound} no està registrat en aquesta escena"); }
    }

    public void PlayExternalSound(AudioClip clip, AudioSource aus)
    {
        aus.clip = clip;
        aus.Play();
    }
}