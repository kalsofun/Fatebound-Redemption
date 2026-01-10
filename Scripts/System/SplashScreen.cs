using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Image SplashScreenImage;
    [SerializeField] float fadeDuration = 2.5f;
    [SerializeField] float waitDuration = 2f;
    GameObject MenuBGM;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("Splash Screen.");
        StartCoroutine(Splash());
    }

    void Update()
    {
        MenuBGM = GameObject.Find("BGMStart");
        if (MenuBGM != null)
        {
            if (MenuBGM.GetComponent<WaitSecondsPing>() != null)
                Destroy(MenuBGM);
            else
                MenuBGM.GetComponent<WaitSecondsPing>().StopAllCoroutines();
        }
    }

    private IEnumerator Splash()
    {
        yield return Fade(SplashScreenImage, 1f);
        yield return new WaitForSeconds(waitDuration);
        yield return Fade(SplashScreenImage, 0f);
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator Fade(Image image, float targetAlpha)
    {
        float startAlpha = image.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetAlpha(image, alpha);
            yield return null;
        }

        SetAlpha(image, targetAlpha);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}
