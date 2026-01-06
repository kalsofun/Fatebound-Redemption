using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct AudioInfo
    {
        public enum AudioType{ BGM, SFX }
        public AudioType Type;
        public int Index;
        public bool FadeIn;
        public float FadeInSeconds;
    }
    [SerializeField] AudioInfo[] audioInfos;
    [SerializeField] bool FadeOutBGM = false;
    [SerializeField] float FadeOutSeconds;

    void Start()
    {
        if (audioInfos.Length > 0)
        {
            foreach (var audioInfo in audioInfos)
            {
                if (audioInfo.Type == AudioInfo.AudioType.SFX)
                {
                    AudioManager.Instance.PlaySFX(audioInfo.Index);
                }
                else if (audioInfo.Type == AudioInfo.AudioType.BGM)
                {
                    if (audioInfo.FadeIn)
                    {
                        AudioManager.Instance.FadeInBGM(audioInfo.Index, audioInfo.FadeInSeconds);
                    }
                    else
                    {
                        AudioManager.Instance.PlayBGM(audioInfo.Index);
                    }
                }
            }
            this.gameObject.SetActive(false);
        }
        else if (FadeOutBGM)
        {
            AudioManager.Instance.FadeOutBGM(FadeOutSeconds);
            this.gameObject.SetActive(false);
        }
    }
}
