using UnityEngine;

public enum SFX { Countdown, Fail, GameOver, GameOverControl, Reward }

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sfxClips;
    [SerializeField]
    GameObject sfxChannel;
    [SerializeField]
    GameObject bgmChannel;

    AudioSource[] sfxers;
    AudioSource bgmer;

    int channel;

    void Awake()
    {
        sfxers = sfxChannel.GetComponents<AudioSource>();
        bgmer = bgmChannel.GetComponent<AudioSource>();
        channel = 0;
    }

    public void PlaySfx(SFX sfx)
    {
        channel = (channel + 1) % sfxers.Length;
        sfxers[channel].clip = sfxClips[(int)sfx];
        sfxers[channel].Play();
    }

    public void PlayBgm()
    {
        bgmer.Play();
    }

    public void StopBgm()
    {
        bgmer.Stop();
    }

    public void PauseBgm()
    {
        bgmer.Pause();
    }
}
