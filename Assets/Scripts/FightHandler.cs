using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    public static GameObject GetNearestTarget(Vector3 position, Collider2D[] colliders) 
    {
        Collider2D result = null;
        var distance = Mathf.Infinity;

        foreach(Collider2D collider in colliders)
        {
            var currentDistance = Vector3.Distance(position, collider.transform.position);

            if(currentDistance < distance)
            {
                result = collider;
                distance = currentDistance;
            }
        }

        return (result != null) ? result.gameObject : null;
    }
    
    public static void Fight(Vector2 point, float radius, int layerMask, float damage, bool allTargets = false)
    {
        var colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);

        if(!allTargets)
        {
            GameObject obj = GetNearestTarget(point, colliders);
            if(obj != null && obj.GetComponent<Enemy>())
            {
                obj.GetComponent<Enemy>().AddDamage(-damage);
            }
            return;
        }

        foreach(Collider2D hit in colliders) 
        {
            if(hit.GetComponent<Enemy>())
            {
                hit.GetComponent<Enemy>().AddDamage(-damage);
            }
        }
    }
}
