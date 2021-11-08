
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Space]

    [SerializeField] private GameObject _spots = null;

    protected List<Transform> spots;

    public int spawnCount { get; set; } = 0;

    protected virtual void Awake()
    {
        spots = new List<Transform>(_spots.GetComponentsInChildren<Transform>().ToList());

        spots.RemoveAt(0);
    }
}