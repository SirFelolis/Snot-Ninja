using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationBehavior : MonoBehaviour
{
    public GameObject textBox;

    public Text text;
    
    public string[] dialogue;

    public Image portrait;
    public Sprite subjectImage;

    private bool isActive = false;
    private int index = 0; // How far are we into the dialogue?

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActive)
            {
                isActive = true;
                index = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
        }
    }

	void Update()
	{
        textBox.SetActive(isActive);
        if (dialogue.Length > 0 && isActive)
        {
            OnDialogue();
        }
    }

    void OnDialogue()
    {
        portrait.sprite = subjectImage;
        text.text = dialogue[index];
        if (Input.GetButtonUp("Fire3"))
        {
            if (index < (dialogue.Length - 1))
            {
                index++;
            }
            else
            {
                isActive = false;
            }
        }
    }
}
