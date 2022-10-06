using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Police : Unit
{
    public bool IsActive = false;

    // POLYMORPHISM
    protected override void Update()
    {
        // ABSTRACTION
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

    // POLYMORPHISM
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

    // POLYMORPHISM
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
