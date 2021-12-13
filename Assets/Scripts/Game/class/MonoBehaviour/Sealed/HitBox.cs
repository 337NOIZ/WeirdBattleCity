
using UnityEngine;

public sealed class HitBox : MonoBehaviour
{
    [SerializeField] private HitBoxJudge _hitBoxJudge = default;

    public HitBoxJudge hitBoxJudge { get => _hitBoxJudge; }

    public Character character { get; private set; }

    public void Awaken(Character character)
    {
        this.character = character;
    }
}