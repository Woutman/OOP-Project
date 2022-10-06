using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Base class for all units. Handles default movement.
// Requires NavMeshAgent to move around the scene.
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : MonoBehaviour
{
    // ENCAPSULATION
    private float _moveRadius = 25.0f;
    public float MoveRadius 
    {   get { return _moveRadius; }
        set 
        { 
            if (value > 0) { _moveRadius = value; }
            else { Debug.LogError("Value can't be negative."); } 
        }
    }
    // ENCAPSULATION
    private float _speed = 5.0f;
    public float Speed
    {
        get { return _speed;  }
        set
        {
            if (value > 0) { _speed = value; }
            else { Debug.LogError("Value can't be negative."); }
        }
    }
    // ENCAPSULATION
    private float _detectionRadius = 10.0f;
    protected float DetectionRadius 
    { 
        get { return _detectionRadius; }
        set 
        { 
            if (value > 0) { _detectionRadius = value; }
            else { Debug.LogError("Value can't be negative."); } 
        }
    }

    protected bool HasTarget = false;

    protected NavMeshAgent Agent;

    protected List<GameObject> allCivilians = new List<GameObject>();
    protected List<GameObject> allPolice = new List<GameObject>();
    protected List<GameObject> allKillers = new List<GameObject>();
    protected List<GameObject> threats = new List<GameObject>();
    protected List<GameObject> targets = new List<GameObject>();


    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.acceleration = 99999;
        Agent.angularSpeed = 99999;
        Agent.speed = _speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        allCivilians.AddRange(GameObject.FindGameObjectsWithTag("Civilian"));
        allPolice.AddRange(GameObject.FindGameObjectsWithTag("Police"));
        allKillers.AddRange(GameObject.FindGameObjectsWithTag("Killer"));
    }

    // Update is called once per frame
    protected abstract void Update();

    // Move towards a random point within range
    protected void MoveRandomly()
    {
        Vector3 newPos = Random.insideUnitSphere * _moveRadius;
        newPos.y = 1;
        Agent.SetDestination(newPos);
    }

    protected abstract void CheckSurroundings();
    // Check surroundings for threats and targets.

    // Find agent that is nearest from list of agents.
    protected GameObject FindNearest(List<GameObject> agents)
    {
        Vector3 curPos = transform.position;
        GameObject closestAgent = null;
        float closestDistance = 9999;

        foreach (GameObject agent in agents)
        {
            float distance = Vector3.Distance(curPos, agent.transform.position);
            if (distance < closestDistance)
            {
                closestAgent = agent;
                closestDistance = distance;
            }
        }

        return closestAgent;
    }

    // POLYMORPHISM
    // Find weighted center of agents in list and move away from that point.
    protected void MoveFrom(List<GameObject> agents)
    {
        float sumWeights = 0;
        var centerPos = new Vector3(0, 0, 0);
        foreach (GameObject agent in agents)
        {
            var distance = Vector3.Distance(transform.position, agent.transform.position);
            centerPos += (agent.transform.position - transform.position) * distance;
            sumWeights += distance;
        }
        centerPos /= agents.Count;

        var targetPos = transform.position - centerPos;

        Agent.SetDestination(targetPos);
    }

    // POLYMORPHISM
    // Move away from specified game object.
    protected void MoveFrom(GameObject agent)
    {
        var targetPos = transform.position - (agent.transform.position - transform.position);
        Agent.SetDestination(targetPos);
    }

    // Move towards specified game object.
    protected void MoveTo(GameObject target)
    {
        Agent.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ABSTRACTION
        ResolveConflict(collision);
    }

    protected abstract void ResolveConflict(Collision collision);
    // Resolve conflict when two agents run into each other.

}
