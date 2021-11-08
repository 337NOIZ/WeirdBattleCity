
using UnityEngine;

public sealed class Player : Character
{
    public static Player instance { get; private set; }

    [SerializeField] private ImageFillAmountController experiencePointBar = null;

    [Space]

    [SerializeField] private LayerMask layerMask = default;

    [Space]

    [SerializeField] private Transform _cameraPivot = null;

    [SerializeField] private Transform cameraFollower_Right_Pivot = null;

    [SerializeField] private Transform cameraFollower_Right = null;

    [SerializeField] private Transform cameraFollower_Front_Pivot = null;

    [SerializeField] private Transform cameraFollower_Front = null;

    [SerializeField] private Transform cameraFollower = null;

    [Space]

    [SerializeField] private Transform aimTarget_Muzzle_Crosshair = null;

    [Space]

    [SerializeField] private PlayerMovement _movement = null;

    [Space]

    [SerializeField] private PlayerInventory _inventory = null;

    public Transform cameraPivot { get { return _cameraPivot; } }

    public PlayerMovement playerMovement { get { return _movement; } }

    public PlayerInventory playerInventory { get { return _inventory; } }

    public override CharacterType characterType { get { return CharacterType.player; } }

    public override CharacterCode characterCode { get { return CharacterCode.player; } }

    public PlayerData playerData { get; private set; }

    public PlayerInfo playerInfo { get; private set; }

    private RaycastHit raycastHit;

    protected override void Awake()
    {
        instance = this;

        base.Awake();
    }

    public override void Initialize()
    {
        playerData = GameMaster.instance.gameData.levelData.playerData;

        base.Initialize();

        playerInfo = GameMaster.instance.gameInfo.levelInfo.playerInfo;

        characterInfo = playerInfo.characterInfo;

        Caching();

        GainHealthPoint(healthPoint_Max);

        playerInfo = GameMaster.instance.gameInfo.levelInfo.playerInfo;

        playerMovement.Initialize(this);

        playerInventory.Initialize(this);
    }

    protected override void LevelUp(CharacterInfo_LevelUpData characterInfo_LevelUpData)
    {
        base.LevelUp(characterInfo_LevelUpData);

        Caching();
    }

    private void Update()
    {
        Vector3 end = cameraFollower_Right_Pivot.position + cameraFollower_Right_Pivot.position - _cameraPivot.position;

        if (Physics.Linecast(_cameraPivot.position, end, out raycastHit, layerMask) == true)
        {
            cameraFollower_Right.localPosition = -(end - raycastHit.point);
        }

        else
        {
            cameraFollower_Right.localPosition = Vector3.zero;
        }

        //Debug.DrawLine(_cameraPivot.position, end, Color.red);

        if (Physics.Linecast(cameraFollower_Right.position, cameraFollower_Front_Pivot.position, out raycastHit, layerMask) == true)
        {
            cameraFollower_Front.position = raycastHit.point;
        }

        else
        {
            cameraFollower_Front.localPosition = Vector3.zero;
        }

        //Debug.DrawLine(cameraFollower_Right.position, cameraFollower_Front_Pivot.position, Color.blue);

        Ray ray = new Ray(cameraFollower.position, cameraFollower.forward);

        if (Physics.Raycast(ray, out raycastHit, 1000f, layerMask) == true)
        {
            aimTarget_Muzzle_Crosshair.position = raycastHit.point;
        }

        else
        {
            aimTarget_Muzzle_Crosshair.position = ray.origin + ray.direction * 1000f;
        }

        //Debug.DrawRay(cameraFollower.position, cameraFollower.forward * 1000f, Color.red);
    }

    protected override void Dead()
    {
        Debug.Log("YOU DIED");
    }
}