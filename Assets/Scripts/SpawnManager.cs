using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _civiliansToSpawn;
    [SerializeField] private float _policeToSpawn;
    [SerializeField] private float _killersToSpawn;

    [SerializeField] private GameObject _civilianPrefab;
    [SerializeField] private GameObject _policePrefab;
    [SerializeField] private GameObject _killerPrefab;

    private readonly float _boundary = 50;

    private readonly List<GameObject> _allUnits = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateAgents()
    {
        ResetAgents();
        InstantiateAgents();
    }

    public void ResetAgents()
    {
        foreach (GameObject unit in _allUnits)
        {
            Destroy(unit);
        }
    }

    private void InstantiateAgents()
    {
        for (int i = 0; i < _civiliansToSpawn; i++)
        {
            GameObject unit = Instantiate(_civilianPrefab, GetRandomLocation(), _civilianPrefab.transform.rotation);
            _allUnits.Add(unit);
        }

        for (int i = 0; i < _policeToSpawn; i++)
        {
            GameObject unit = Instantiate(_policePrefab, GetRandomLocation(), _policePrefab.transform.rotation);
            _allUnits.Add(unit);
        }

        for (int i = 0; i < _killersToSpawn; i++)
        {
            GameObject unit = Instantiate(_killerPrefab, GetRandomLocation(), _killerPrefab.transform.rotation);
            _allUnits.Add(unit);
        }
    }

    private Vector3 GetRandomLocation()
    {
        float xPos = Random.Range(-_boundary, _boundary);
        float zPos = Random.Range(-_boundary, _boundary);
        return new Vector3(xPos, 1, zPos);
    }
}
