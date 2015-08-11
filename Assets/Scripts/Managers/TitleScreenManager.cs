using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Application.LoadLevel(1);
        }
    }
}
