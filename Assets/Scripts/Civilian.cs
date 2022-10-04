using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : Unit
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
        else if (targets.Count > 0 && threats.Count > 0)
        {
            HasTarget = true;
            GameObject target = FindNearest(targets);
            MoveTo(target);
        }
        else if (threats.Count > 0)
        {
            HasTarget = true;
            MoveFrom(threats);
        }

        if (!HasTarget && !Agent.hasPath)
        {
            MoveRandomly();
        }
    }

    protected override void CheckSurroundings()
    {
        targets.Clear();
        threats.Clear();
        foreach (var killer in allKillers)
        {
            if (killer == null)
            {
                break;
            }

            if (Vector3.Distance(transform.position, killer.transform.position) < DetectionRadius)
            {
                threats.Add(killer);
            }
        }

        foreach (var police in allPolice)
        {
            if (police == null)
            {
                break;
            }

            if (Vector3.Distance(transform.position, police.transform.position) < DetectionRadius)
            {
                targets.Add(police);
            }
        }
    }

    protected override void ResolveConflict(Collision collision)
    {
        if (collision.gameObject.CompareTag("Police") && threats.Count > 0)
        {
            collision.gameObject.GetComponent<Police>().IsActive = true;
        }
    }
}
