using UnityEngine;

/** Trigger behavior script
*/

public class TriggerBehaviour : MonoBehaviour
{
    public enum TriggerType
    {
        KILL
    }

    public TriggerType type;
    public string targetTag;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (type == TriggerType.KILL)
        {
            if (other.CompareTag(targetTag))
                Destroy(other.gameObject);
        }

    }
}
