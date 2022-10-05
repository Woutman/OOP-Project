using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Unit
{
    public bool IsActive = false;

    // Update is called once per frame
    protected override void Update()
    {
        CheckSurroundings();
        // Reset if no targers are nearby.
        if (targets.Count == 0 && HasTarget)
        {
            IsActive = false;
            HasTarget = false;
            Agent.ResetPath();
        }
        // Police will chase Killers if activated by civilian.
        else if (targets.Count > 0 && IsActive)
        {
            HasTarget = true;
            GameObject target = FindNearest(targets);
            MoveTo(target);
        }

        // Wander around randomly when nothing else to do.
        if (!HasTarget && !Agent.hasPath)
        {
            MoveRandomly();
        }
    }

    // Check for killers and add them to list of targets.
    protected override void CheckSurroundings()
    {
        targets.Clear();
        foreach (var killer in allKillers)
        {
            if (killer == null)
            {
                continue;
            }

            if (Vector3.Distance(transform.position, killer.transform.position) < DetectionRadius)
            {
                targets.Add(killer);
            }
        }
    }

    // Police kill killers.
    protected override void ResolveConflict(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killer"))
        {
            allKillers.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
