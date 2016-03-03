using UnityEngine;
using System.Collections;

/** Camera movement behavior script
*/


public class FollowPrediction : MonoBehaviour
{
    public GameObject target; // Target has to have a rigidbody for the script to work
    public float smooth = 0.2f;

    private string _targetName;
    private Rigidbody2D _Trb2d;

    void Start()
    {
        _targetName = target.name;
        _Trb2d = target.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameObject.Find(_targetName) != null)
        {
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, target.transform.position.x + _Trb2d.velocity.x / 4.0f, smooth),
                Mathf.Lerp(transform.position.y, target.transform.position.y, smooth),
                transform.position.z);
        }
    }
}
