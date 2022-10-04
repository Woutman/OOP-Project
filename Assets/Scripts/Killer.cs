using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Unit
{
    // Update is called once per frame
    protected override void Update()
    {
        CheckSurroundings();
        if (threats.Count == 0 && targets.Count == 0 && HasTarget)
        {
            HasTarget = false;
            Agent.ResetPath();
        }
        else if (threats.Count > 0)
        {
            HasTarget = true;
            MoveFrom(threats);
        }
        else if (targets.Count > 0)
        {
            HasTarget = true;
            GameObject target = FindNearest(targets);
            MoveTo(target);
        }

        if (!HasTarget && !Agent.hasPath)
        {
            MoveRandomly();
        }
    }

    // 
    protected override void CheckSurroundings()
    {
        targets.Clear();
        threats.Clear();
        foreach (var police in allPolice)
        {
            if (police == null)
            {
                break;
            }

            if (Vector3.Distance(transform.position, police.transform.position) < DetectionRadius)
            {
                threats.Add(police);
            }
        }

        foreach (var civilian in allCivilians)
        {
            if (civilian == null)
            {
                break;
            }

            if (Vector3.Distance(transform.position, civilian.transform.position) < DetectionRadius)
            {
                targets.Add(civilian);
            }
        }
    }

    protected override void ResolveConflict(Collision collision)
    {
        if (collision.gameObject.CompareTag("Civilian"))
        {
            Destroy(collision.gameObject);
        }
    }
}
