using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private float _civiliansToSpawn;
    private float _policeToSpawn;
    private float _killersToSpawn;

    [SerializeField] private GameObject _civilianPrefab;
    [SerializeField] private GameObject _policePrefab;
    [SerializeField] private GameObject _killerPrefab;

    [SerializeField] private Slider _civilianSlider;
    [SerializeField] private Slider _policeSlider;
    [SerializeField] private Slider _killerSlider;

    private readonly float _boundary = 50;

    private readonly List<GameObject> _allUnits = new();

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
        _civiliansToSpawn = _civilianSlider.value;
        _policeToSpawn = _policeSlider.value;
        _killersToSpawn = _killerSlider.value;

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
