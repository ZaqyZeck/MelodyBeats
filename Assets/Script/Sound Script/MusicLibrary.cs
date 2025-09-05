using UnityEngine;

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] soundEffects;

    public AudioClip GetClipFromName(string name)
    {
        foreach (var effect in soundEffects)
        {
            if (effect.trackName  == name)
            {
                return effect.clip;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct MusicTrack
{
    public string trackName;
    public AudioClip clip;
}
