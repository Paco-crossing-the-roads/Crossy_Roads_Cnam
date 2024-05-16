using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    private float volume = 1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip, float volumeScale = 1f)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void TriggerOnOffSound()
    {
        float newVolume = volume == 0 ? 1 : 0;
        SetVolume(newVolume);
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        sfxSource.volume = volume;
        musicSource.volume = volume;
    }
    public float GetVolume()
    {
        return volume;
    }

    public void PlayMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic() {
        musicSource.Stop();
    }
}
