using UnityEngine;

/** Player abstract behavior script
*/

public abstract class AbstractBehavior : MonoBehaviour
{
    public Buttons[] inputButtons;
    public MonoBehaviour[] disableScripts;

    protected InputState _inputState;
    protected Rigidbody2D _rb2d;
    protected CollisionState _collisionState;



    protected virtual void Awake()
    {
        _inputState = GetComponent<InputState>();
        _rb2d = GetComponent<Rigidbody2D>();
        _collisionState = GetComponent<CollisionState>();
    }

    protected virtual void ToggleScripts(bool value)
    {
        foreach(var script in disableScripts)
        {
            script.enabled = value;
        }
    }
}
