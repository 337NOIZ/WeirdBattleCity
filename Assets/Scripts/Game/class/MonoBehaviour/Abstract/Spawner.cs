
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Space]

    [SerializeField] private Transform _spots_Transform = null;

    protected List<Transform> _spots;

    public int spawnCount { get; set; } = 0;

    protected virtual void Awake()
    {
        _spots = _spots_Transform.GetComponentsInChildren<Transform>().ToList();

        _spots.RemoveAt(0);
    }
}