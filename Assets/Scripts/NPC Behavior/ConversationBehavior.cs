using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationBehavior : MonoBehaviour
{
    public string[] dialogue;
    public Text text;
    public Image portrait;
    public GameObject textPanel;
    public Sprite subjectImage;

    private bool canTalk = false;

    private int index = 0; // How far are we into the dialogue?
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetAxis("Vertical") > 0.1 && !canTalk)
            {
                Time.timeScale = 0;
                canTalk = true;
                index = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
        }
    }

	void Update()
	{
        textPanel.SetActive(canTalk);
        if (dialogue.Length > 0 && canTalk)
        {
            OnDialogue();
        }
    }

    void OnDialogue()
    {
        portrait.sprite = subjectImage;
        text.text = dialogue[index];
        if (Input.GetButtonUp("Fire2"))
        {
            if (index < (dialogue.Length - 1))
            {
                index++;
            }
            else
            {
                Time.timeScale = 1;
                canTalk = false;
            }
        }
    }
}
