using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private AudioClip[] BGMClips;
    [SerializeField] private AudioClip[] SFXClips;
    [SerializeField] private AudioClip[] CharClips;

    [Range(0f, 1f)] public float MasterVol = 1f;
    [Range(0f, 1f)] public float BGMVol = 1f;
    [Range(0f, 1f)] public float SFXVol = 1f;

    Coroutine BGMFadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start() => UpdateVolumes();

    private void Update() => UpdateVolumes();

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= BGMClips.Length) return;
        BGMSource.clip = BGMClips[index];
        BGMSource.Play();
        Debug.Log("Played BGM: " + index);
    }

    public void PlaySFX(int index)
    {
        if (index < 0 && index >= SFXClips.Length) return;
        SFXSource.PlayOneShot(SFXClips[index]);
        Debug.Log("Played SFX: " + index);
    }

    public void PlayChar(int index)
    {
        if (index >= 0 && index < CharClips.Length) SFXSource.PlayOneShot(CharClips[index]);
    }

    public void PauseBGM()
    {
        BGMSource.Pause();
        Debug.Log("BGM Paused.");
    }

    public void ResumeBGM()
    {
        BGMSource.UnPause();
        Debug.Log("BGM Resumed.");
    }

    public void FadeInBGM(int Index, float duration)
    {
        if (BGMFadeCoroutine != null)
            StopCoroutine(BGMFadeCoroutine);
        BGMSource.volume = 0f;
        PlayBGM(Index);
        Debug.Log("Fading in BGM...");
        BGMFadeCoroutine = StartCoroutine(FadeInBGMCoroutine(duration));
    }

    public void FadeOutBGM(float duration)
    {
        if (BGMFadeCoroutine != null)
            StopCoroutine(BGMFadeCoroutine);
        Debug.Log("Fading out BGM...");
        BGMFadeCoroutine = StartCoroutine(FadeOutBGMCoroutine(duration));
    }

    private IEnumerator FadeInBGMCoroutine(float duration)
    {
        var targetVolume = MasterVol * BGMVol;

        if (!BGMSource.isPlaying)
            BGMSource.Play();

        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            BGMSource.volume = Mathf.Lerp(0f, targetVolume, time / duration);
            yield return null;
        }

        BGMSource.volume = targetVolume;
        Debug.Log("BGM Faded in.");
    }

    private IEnumerator FadeOutBGMCoroutine(float duration)
    {
        var startVolume = BGMSource.volume;

        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            BGMSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        BGMSource.volume = 0f;
        Debug.Log("BGM Faded out.");
        StopBGM();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
        Debug.Log("BGM Stopped.");
    }

    public void SetMasterVolume(float volume)
    {
        MasterVol = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetBGMVolume(float volume)
    {
        BGMVol = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        SFXVol = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        BGMSource.volume = MasterVol * BGMVol;
        SFXSource.volume = MasterVol * SFXVol;
    }
}
