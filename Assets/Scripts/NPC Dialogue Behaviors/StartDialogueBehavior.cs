using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartDialogueBehavior : MonoBehaviour
{
    [Header("Portrait & Name")]
    public Sprite sourceImage;
    public string nameTag = "Tell a dev to put a name here";

    [Header("Dialogue")]
    public TextAsset textFile;
    public string[] textLines;

    public int lineIndex;
    public int endlineIndex;

    public DialogueBoxBehavior textManager;

    public bool autoScroll = false;
    public float autoScrollSpeed = 1;

    [Header("Typing Sound")]
    public AudioClip typingSounds;

    void Start()
    {
        textManager = FindObjectOfType<DialogueBoxBehavior>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endlineIndex == 0)
        {
            endlineIndex = textLines.Length - 1;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textManager.ReloadScript(textFile);
            textManager.lineIndex = this.lineIndex;
            textManager.endlineIndex = this.endlineIndex;
            textManager.portrait.sprite = this.sourceImage;
            textManager.nameText.text = nameTag;
            textManager.autoScroll = this.autoScroll;
            textManager.autoScrollSpeed = this.autoScrollSpeed;
            if (typingSounds != null)
                textManager.typingSounds = this.typingSounds;

            textManager.EnableDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textManager.DisableDialogue();
        }
    }
}
