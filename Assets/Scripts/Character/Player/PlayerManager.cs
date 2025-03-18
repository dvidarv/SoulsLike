using UnityEngine;

public class PlayerManager : CharacterManager
{
    PlayerLocomotionManager playerLocomotionManager;
    protected override void Awake()
    {
        base.Awake();

        //CODE ONLY FOR THE PLAYER

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }
    protected override void Update()
    {
        base.Update();

        // IF WE ARE NOT THE OWNER OF THIS GAME OBJECT, WE DO NOT CONTROL OR EDIT IT
        if (!IsOwner)
        {
            return;
        }

        // HANDLE ALL CHARACTER MOVEMENT
        playerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //IF THIS IS THE PLAYER OBJECT  OWNED BY THIS CLIENT
        if (IsOwner)
        {
            PlayerCamera.instance.playerManager = this;
        }
    }
}
