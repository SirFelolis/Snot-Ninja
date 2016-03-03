using UnityEngine;
using System.Collections;

public class EnemyFocusAnchor : MonoBehaviour
{
    public GameObject player;
    public float smooth;
    public float focusRadius;
    public bool focus;

    private Vector2 enemyPos = new Vector2();

    void FixedUpdate()
    {

        if (player == null)
            return;

        if (Vector3.Distance(FindClosestObject("Enemy").transform.position, player.transform.position) < focusRadius)
        {
            enemyPos = FindClosestObject("Enemy").transform.position;
            focus = true;
        }
        else
            focus = false;

        var pos = transform.position;
        if (focus)
        {
            pos.x = Mathf.Lerp(pos.x, enemyPos.x, smooth);
            pos.y = Mathf.Lerp(pos.y, enemyPos.y, smooth);
        }
        else
        {
            pos.x = Mathf.Lerp(pos.x, player.transform.position.x, smooth * 5);
            pos.y = Mathf.Lerp(pos.y, player.transform.position.y, smooth * 5);
        }

        transform.position = pos;
    }

    GameObject FindClosestObject(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, focusRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15.0f);
    }

}
