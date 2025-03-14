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

        // HANDLE ALL CHARACTER MOVEMENT
        playerLocomotionManager.HandleAllMovement();
    }
}
