
using UnityEngine;

public sealed class Player : Character
{
    public static Player instance { get; private set; }

    [Space]

    [SerializeField] private Transform _cameraPivot = null;

    [Space]

    [SerializeField] private Transform _cameraFollower = null;

    [Space]

    [SerializeField] private Transform _aimTarget_Crosshair = null;

    [Space]

    [SerializeField] private Movement _movement = null;

    [Space]

    [SerializeField] private Inventory _inventory = null;

    public Transform cameraPivot { get { return _cameraPivot; } }

    public Transform cameraFollower { get { return _cameraFollower; } }

    public Transform aimTarget_Crosshair { get { return _aimTarget_Crosshair; } }

    public Movement movement { get { return _movement; } }

    public Inventory inventory { get { return _inventory; } }

    public override CharacterType characterType { get { return CharacterType.friendly; } }

    public override CharacterCode characterCode { get { return CharacterCode.player; } }

    public PlayerData playerData { get; private set; }

    public PlayerInfo playerInfo { get; private set; }

    private RaycastHit raycastHit;

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }
    public void Initialize(PlayerInfo playerInfo)
    {
        Initialize(playerInfo.characterInfo);

        playerData = GameManager.instance.playerData;

        this.playerInfo = playerInfo;
        
        movement.Initialize(this);
        
        inventory.Initialize(this);
    }
    private void LateUpdate()
    {
        if (Physics.Raycast(cameraFollower.position, cameraFollower.forward, out raycastHit, 1000f) == true)
        {
            aimTarget_Crosshair.position = raycastHit.point;
        }
        else
        {
            aimTarget_Crosshair.position = cameraFollower.forward * 1000f;
        }
    }
    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }
}