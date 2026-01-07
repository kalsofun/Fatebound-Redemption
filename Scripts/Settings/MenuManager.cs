using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private float fadeDuration = 3f;
    private Color fadeColor = Color.black;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator StartMenu()
    {
        isTransitioning = true;

        Image fadeImage = CreateFadeCanvas(1f);
        yield return Fade(fadeImage, 0f);
        Destroy(fadeImage.canvas.gameObject);

        isTransitioning = false;
    }

    public void LoadScene(string sceneName)
    {
        if (isTransitioning) 
        {
            Debug.Log("Scene transition already in progress.");
            return;
        }
        StartCoroutine(SceneTransition(sceneName));
    }

    public void QuitGame()
    {
        if (isTransitioning) 
        {
            Debug.Log("Scene transition already in progress.");
            return;
        }
        StartCoroutine(QuitTransition());
    }

    private IEnumerator SceneTransition(string sceneName)
    {
        isTransitioning = true;

        Image fadeImage = CreateFadeCanvas(0f); // Create fade canvas
        yield return Fade(fadeImage, 1f); // Fade Out

        Debug.Log("Load Scene: " + sceneName);
        SceneManager.LoadScene(sceneName);

        yield return null; // Wait one frame so scene fully loads
        yield return Fade(fadeImage, 0f); // Fade In
        Destroy(fadeImage.canvas.gameObject); // Destroy canvas

        isTransitioning = false;
    }

    private IEnumerator QuitTransition()
    {
        isTransitioning = true;

        Image fadeImage = CreateFadeCanvas(0f);
        yield return Fade(fadeImage, 1f);
        
        Debug.Log("Quit Game.");
        Application.Quit();
    }

    private Image CreateFadeCanvas(float alpha)
    {
        GameObject canvasGO = new GameObject("FadeCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject imageGO = new GameObject("FadeImage");
        imageGO.transform.SetParent(canvasGO.transform, false);

        Image image = imageGO.AddComponent<Image>();
        image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

        RectTransform rt = image.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;


        DontDestroyOnLoad(canvas);
        return image;
    }

    private IEnumerator Fade(Image image, float targetAlpha)
    {
        float startAlpha = image.color.a;
        float time = 0f;

        Debug.Log("Scene Fading...");
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetAlpha(image, alpha);
            yield return null;
        }

        Debug.Log("Scene Faded.");
        SetAlpha(image, targetAlpha);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}