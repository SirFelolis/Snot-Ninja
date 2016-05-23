using UnityEngine;
using System.Collections;

public enum States
{
    Alert,
    Caution,
    Patrol,
};

public class EnemyFSM : MonoBehaviour
{
    public States states;

    private EnemySight enemyPlayerDetection;

    [SerializeField]
    private SpriteRenderer rend;

    void Awake()
    {
        enemyPlayerDetection = GetComponentInChildren<EnemySight>();
    }

    void FixedUpdate()
    {

        if (enemyPlayerDetection.PlayerInSight)
        {
            states = States.Alert;
        }
        else if (states != States.Patrol)
        {
            states = States.Caution;
            StartCoroutine(CautionCounter(10.0f));
        }

        switch (states)
        {
            case States.Patrol:
                rend.color = Color.Lerp(rend.color, new Color(1, 1, 1, 0.5f), 0.2f);
                break;

            case States.Caution:
                rend.color = Color.Lerp(rend.color, new Color(1, 1, 0.60f, 0.5f), 0.2f);
                break;

            case States.Alert:
                rend.color = Color.Lerp(rend.color, new Color(1, 0, 0, 0.5f), 0.2f);
                break;

        }
    }

    IEnumerator CautionCounter(float time)
    {
        yield return new WaitForSeconds(time);
        states = States.Patrol;
    }
}
