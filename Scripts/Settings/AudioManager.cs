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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateVolumes();
    }

    private void Update()
    {
        UpdateVolumes();
    }

    public void PlayBGM(int index)
    {
        if (index >= 0 && index < BGMClips.Length)
        {
            BGMSource.clip = BGMClips[index];
            BGMSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (index >= 0 && index < SFXClips.Length)
        {
            SFXSource.PlayOneShot(SFXClips[index]);
        }
    }

    public void PlayChar(int index)
    {
        if (index >= 0 && index < CharClips.Length)
        {
            SFXSource.PlayOneShot(CharClips[index]);
        }
    }

    public void PauseBGM()
    {
        BGMSource.Pause();
    }

    public void ResumeBGM()
    {
        BGMSource.UnPause();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
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
