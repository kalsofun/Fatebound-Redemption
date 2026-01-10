using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    private PlayerMovement pm;
    private Rigidbody2D rb;
    [SerializeField] private bool NoPlayer = false;
    private TextMeshProUGUI _text;
    private Coroutine _typingCoroutine;
    private bool _isTyping = false;

    [System.Serializable]
    public struct CharInfo
    {
        public string name;
        public GameObject icon;
        public int speak;
    }
    [SerializeField] CharInfo[] charInfos = { new CharInfo { name = "苡慧", icon = null, speak = 0 }, };
    [SerializeField] TextMeshProUGUI CharNameDisp;

    [System.Serializable]
    public struct TextEffect
    {
        public string fullDialogue;
        public float textSpeed; //30f
        public float frequency; //30f
        public float amplitude; //10f
        public int CharHeadNum;
    }
    [SerializeField] public TextEffect[] textEffects =
    {
        new TextEffect
        {
            fullDialogue = "若想念是凋謝的雲，乘著風也飄向你，降落在你耳邊低語",
            textSpeed = 30f,
            frequency = 0f,
            amplitude = 0f,
            CharHeadNum = 0
        },
    };
    
    [SerializeField] GameObject[] EndPingItems;

    int line = -1;
    int maxline;

    private void OnEnable()
    {
        DialogueManager.Instance.DialogueBox.SetActive(true);
        if (!NoPlayer)
        {
            pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            pm.canMove = false;
            rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
        }
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
        maxline = textEffects.Length;
        Play();
    }

    private void Update()
    {
        if (!PauseManager.Instance.isPaused)
        {
            var enter = Keyboard.current.enterKey;
            if (enter != null && enter.wasPressedThisFrame)
            {
                if (_isTyping)
                {
                    FinishCurrentLine();
                }
                else if (line < maxline - 1)
                {
                    Play();
                }
                else
                {
                    DialogueManager.Instance.DialogueBox.SetActive(false);
                    if (EndPingItems != null)
                        foreach (GameObject item in EndPingItems)
                        {
                            item.SetActive(true);
                            Debug.Log("Item Available: " + item);
                        }
                    if (pm != null) pm.canMove = true;
                    for (int i = 0; i < charInfos.Length; i++)
                        if (charInfos[i].icon != null)
                            charInfos[i].icon.SetActive(false);
                    line = -1;
                    gameObject.SetActive(false);
                }
            }
        }
        TextShake();
    }

    private void Play()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
            _isTyping = false;
        }
        _typingCoroutine = StartCoroutine(Type());
    }

    private IEnumerator Type()
    {
        _isTyping = true;
        _text.text = "";
        line++;
        if (CharNameDisp != null) CharNameDisp.text = charInfos[textEffects[line].CharHeadNum].name;
        for (int i = 0; i < charInfos.Length; i++)
            if (charInfos[i].icon != null)
                charInfos[i].icon.SetActive(i == textEffects[line].CharHeadNum);
        foreach (var c in textEffects[line].fullDialogue)
        {
            _text.text += c;
            if (charInfos[textEffects[line].CharHeadNum].speak >= 0)
                AudioManager.Instance.PlayChar(charInfos[textEffects[line].CharHeadNum].speak);
            yield return new WaitForSeconds(1/textEffects[line].textSpeed);
        }
        _isTyping = false;
        _typingCoroutine = null;
    }

    private void FinishCurrentLine()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        _text.text = textEffects[line].fullDialogue;
        _isTyping = false;
    }

    private void TextShake()
    {
        if (_text == null) return;
        if (line < 0 || line >= textEffects.Length) return;
        if (_text.textInfo.characterCount == 0) return;

        _text.ForceMeshUpdate();
        var cachedMeshInfo = _text.textInfo.CopyMeshInfoVertexData();

        for (var i = 0; i < _text.textInfo.characterCount; i++) if (_text.textInfo.characterInfo[i].isVisible)
        {
            var charInfo = _text.textInfo.characterInfo[i];
            var matIndex = charInfo.materialReferenceIndex;
            var vertexIndex = charInfo.vertexIndex;

            var source = cachedMeshInfo[matIndex].vertices;
            var destination = _text.textInfo.meshInfo[matIndex].vertices;

            var phase = i * 0.5f;

            var freq = textEffects[line].frequency;
            var amp = textEffects[line].amplitude;

            var x = Mathf.PerlinNoise(Time.time * freq, phase) - 0.5f;
            var y = Mathf.PerlinNoise(phase, Time.time * freq) - 0.5f;

            var offset = new Vector3(x, y) * amp;

            destination[vertexIndex + 0] = source[vertexIndex + 0] + offset;
            destination[vertexIndex + 1] = source[vertexIndex + 1] + offset;
            destination[vertexIndex + 2] = source[vertexIndex + 2] + offset;
            destination[vertexIndex + 3] = source[vertexIndex + 3] + offset;
        }

        for (var i = 0; i < _text.textInfo.meshInfo.Length; i++)
        {
            var meshInfo = _text.textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            _text.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}