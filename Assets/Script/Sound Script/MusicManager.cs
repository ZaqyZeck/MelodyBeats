using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    //public static MusicManager instance;
    //[SerializeField] private MusicLibrary musicLibrary;
    //[SerializeField] private AudioSource audioSource;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    //public void PlaySound(string soundName)
    //{
    //    audioSource.PlayOneShot(musicLibrary.GetClipFromName(soundName));
    //}
    //public void StopSound()
    //{
    //    audioSource.Stop();
    //}

    [SerializeField] private Slider musicSlider;

    public float volumeValue;
    private const string VolumeKey = "MusicVolume";
    [SerializeField] private AudioSource mainMenuMusic;
    public void SetVolumeValue()
    {
        volumeValue = musicSlider.value;
        SetMainMenuVolume();
        SaveVolumeValue();
    }

    public void SetMainMenuVolume()
    {
        mainMenuMusic.volume = volumeValue;
    }
    public void SaveVolumeValue()
    {
        PlayerPrefs.SetFloat(VolumeKey, volumeValue);
        PlayerPrefs.Save();
        //float x = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateMusicSlider()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicSlider.value = 1.0f; // default volume
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
            PlayerPrefs.Save();
        }
    }
}
