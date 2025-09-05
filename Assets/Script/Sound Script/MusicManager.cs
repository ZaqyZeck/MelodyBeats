using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(musicLibrary.GetClipFromName(soundName));
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}
