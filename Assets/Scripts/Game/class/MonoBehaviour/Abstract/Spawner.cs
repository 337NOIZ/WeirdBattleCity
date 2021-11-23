
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Space]

    [SerializeField] private Transform _spots = null;

    protected List<Transform> spots;

    public int spawnCount { get; set; } = 0;

    protected virtual void Awake()
    {
        spots = _spots.GetComponentsInChildren<Transform>().ToList();

        spots.RemoveAt(0);
    }
}