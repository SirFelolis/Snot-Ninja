using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBoxBehavior : MonoBehaviour
{
    [Header("Portrait")]
    public Sprite sourceImage;
    public Image portrait;

    [Header("Dialogue")]
    public GameObject textBox;
    public Text text;

    public TextAsset textFile;
    public string[] textLines;

    public int lineIndex;
    public int endlineIndex;

    public float printSpeed;

    [Header("Name Tag")]
    public Text nameText;

    [Header("Sound")]
    public AudioClip typingSounds;

    private TextPrintSoundBehavior typeSound;

    private bool isActive = false;

    private bool isPrinting = false;
    private bool cancelPrinting = false;


    void Awake()
    {
        typeSound = GetComponent<TextPrintSoundBehavior>();
    }

    void Start()
    {
        portrait.sprite = sourceImage;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endlineIndex == 0)
        {
            endlineIndex = textLines.Length - 1;
        }

        if (isActive)
            EnableDialogue();
        else
            DisableDialogue();
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //text.text = textLines[lineIndex];

        if (Input.GetButtonDown("Fire3"))
        {
            if (!isPrinting)
            {
                lineIndex++;

                if (lineIndex > endlineIndex)
                {
                    DisableDialogue();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[lineIndex]));
                }
            }
            else if (isPrinting && !cancelPrinting)
            {
                cancelPrinting = true;
            }
        }
    }

    private IEnumerator TextScroll(string textLine)
    {
        int letter = 0;
        text.text = "";
        isPrinting = true;
        cancelPrinting = false;
        while (isPrinting && !cancelPrinting && (letter < textLine.Length - 1))
        {
            text.text += textLine[letter];
            letter++;
            StartCoroutine(typeSound.PlayTypingSound(typingSounds));
            yield return new WaitForSeconds(printSpeed);
        }
        text.text = textLine;
        isPrinting = false;
        cancelPrinting = false;
    }

    public void EnableDialogue()
    {
        textBox.SetActive(true);
        isActive = true;

        StartCoroutine(TextScroll(textLines[lineIndex]));
    }

    public void DisableDialogue()
    {
        lineIndex = 0;
        textBox.SetActive(false);
        isActive = false;
        cancelPrinting = true;
    }

    public void ReloadScript(TextAsset textFile)
    {
        if (textFile != null)
        {
            textLines = new string[1];
            textLines = (textFile.text.Split('\n'));
        }
    }
}
