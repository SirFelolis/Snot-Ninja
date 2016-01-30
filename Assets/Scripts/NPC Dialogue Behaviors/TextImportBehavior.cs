using UnityEngine;
using System.Collections;

public class TextImportBehavior : MonoBehaviour
{

    public TextAsset textFile;
    public string[] textLines;

    void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
    }
}
