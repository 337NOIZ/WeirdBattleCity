
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Events;

public enum AttackBoxType
{
    BoxCast,

    SphereCast,

    LineCast,
}

public sealed class AttackBox : MonoBehaviour
{
    [SerializeField] private AttackBoxType _attackBoxType = default;

    [SerializeField] private bool _castAll = false;

    [SerializeField] private Vector3 _scale = Vector3.one;

    [SerializeField] private float _radius = 1f;

    [SerializeField] private bool _drawGizmo = false;

    [SerializeField] private Color _gizmoColor = new Color(1f, 0f, 0f, 0.1f);

    private Character _attacker;

    UnityAction<HitBox> _actionOnHit;

    private bool _allowNullHitBox = false;

    private Vector3 _tranformPosition_Previous;

    private RaycastHit _raycastHit;

    public RaycastHit raycastHit { get => _raycastHit; }

    private RaycastHit[] _raycastHits;

    public RaycastHit[] raycastHits { get => _raycastHits; }

    private List<Character> _victims = new List<Character>();

    private void OnDrawGizmosSelected()
    {
        if (_drawGizmo)
        {
            if (_attackBoxType != AttackBoxType.LineCast && _attackBoxType != AttackBoxType.LineCast)
            {
                Gizmos.color = _gizmoColor;

                Gizmos.matrix = transform.localToWorldMatrix;

                switch (_attackBoxType)
                {
                    case AttackBoxType.BoxCast:

                        Gizmos.DrawCube(Vector3.zero, _scale);

                        break;

                    case AttackBoxType.SphereCast:

                        Gizmos.DrawSphere(Vector3.zero, _radius);

                        break;
                }
            }
        }
    }

    public void Initialize(Character attacker)
    {
        _attacker = attacker;
    }

    public void StartTrailCasting(UnityAction<HitBox> actionOnHit, bool allowNullHitBox)
    {
        if (_trailCasting == null)
        {
            _actionOnHit = actionOnHit;

            _allowNullHitBox = allowNullHitBox;

            _trailCasting = _TrailCasting();

            StartCoroutine(_trailCasting);
        }
    }

    public void StopTrailCasting()
    {
        if (_trailCasting != null)
        {
            StopCoroutine(_trailCasting);

            _victims.Clear();

            _trailCasting = null;
        }
    }

    private IEnumerator _trailCasting = null;

    private IEnumerator _TrailCasting()
    {
        var attackableLayers = _attacker.attackableLayers;

        while (true)
        {
            _tranformPosition_Previous = transform.position;

            yield return null;

            bool isHit = false;

            if (_castAll == false)
            {
                switch (_attackBoxType)
                {
                    case AttackBoxType.BoxCast:

                        isHit = PhysicsWizard.BoxCast(_tranformPosition_Previous, transform.position, transform.rotation, _scale, attackableLayers, out _raycastHit);

                        break;

                    case AttackBoxType.LineCast:

                        isHit = PhysicsWizard.LineCast(_tranformPosition_Previous, transform.position, _radius, attackableLayers, out _raycastHit);

                        break;

                    case AttackBoxType.SphereCast:

                        isHit = PhysicsWizard.SphereCast(_tranformPosition_Previous, transform.position, _radius, attackableLayers, out _raycastHit);

                        break;

                    default:

                        break;
                }

                if (isHit == true)
                {
                    OnHit(_raycastHit);
                }
            }

            else
            {
                switch (_attackBoxType)
                {
                    case AttackBoxType.BoxCast:

                        isHit = PhysicsWizard.BoxCastAll(_tranformPosition_Previous, transform.position, transform.rotation, _scale, attackableLayers, out _raycastHits);

                        break;

                    case AttackBoxType.LineCast:

                        isHit = PhysicsWizard.LineCastAll(_tranformPosition_Previous, transform.position, _radius, attackableLayers, out _raycastHits);

                        break;

                    case AttackBoxType.SphereCast:

                        isHit = PhysicsWizard.SphereCastAll(_tranformPosition_Previous, transform.position, _radius, attackableLayers, out _raycastHits);

                        break;

                    default:

                        break;
                }

                if (isHit == true)
                {
                    int index_Max = _raycastHits.Length;

                    for (int index = 0; index < index_Max; ++index)
                    {
                        OnHit(_raycastHits[index]);
                    }
                }
            }
        }
    }

    public void Check(UnityAction<HitBox> actionOnHit)
    {
        _actionOnHit = actionOnHit;

        var attackableLayers = _attacker.attackableLayers;

        bool isHit = false;

        if (_castAll == false)
        {
            switch (_attackBoxType)
            {
                case AttackBoxType.BoxCast:

                    isHit = PhysicsWizard.BoxCast(transform.position, transform.rotation, _scale, attackableLayers, out _raycastHit);

                    break;

                case AttackBoxType.SphereCast:

                    isHit = PhysicsWizard.SphereCast(transform.position, _radius, attackableLayers, out _raycastHit);

                    break;

                default:

                    break;
            }

            if (isHit == true)
            {
                OnHit(_raycastHit);
            }
        }

        else
        {
            switch (_attackBoxType)
            {
                case AttackBoxType.BoxCast:

                    isHit = PhysicsWizard.BoxCastAll(transform.position, transform.rotation, _scale, attackableLayers, out _raycastHits);

                    break;

                case AttackBoxType.SphereCast:

                    isHit = PhysicsWizard.SphereCastAll(transform.position, _radius, attackableLayers, out _raycastHits);

                    break;

                default:

                    break;
            }

            if (isHit == true)
            {
                int index_Max = _raycastHits.Length;

                for (int index = 0; index < index_Max; ++index)
                {
                    OnHit(_raycastHits[index]);
                }
            }
        }

        _victims.Clear();
    }

    private void OnHit(RaycastHit raycastHit)
    {
        var hitbox = raycastHit.collider.GetComponent<HitBox>();

        if (hitbox != null)
        {
            if (_victims.Contains(hitbox.character) != true)
            {
                _victims.Add(hitbox.character);

                _actionOnHit.Invoke(hitbox);
            }
        }

        else if(_allowNullHitBox == true)
        {
            _actionOnHit.Invoke(hitbox);
        }
    }
}