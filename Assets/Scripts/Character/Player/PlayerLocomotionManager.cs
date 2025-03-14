using Unity.Burst;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager playerManager;

    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;

    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float rotationSpeed = 15;

    protected override void Awake()
    {
        base.Awake();

        playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        // GROUNDED MOVEMENT
        HandleGroundedMovement();
        HandleRotation();
        // AERIAL MOVEMENT
    }

    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;

        // CLAMP THE MOVEMENTS
    }

    private void HandleGroundedMovement()
    {
        GetVerticalAndHorizontalInputs();
        
        // OUR MOVEMENT DIRECTION IS BASED ON CAMERA PERSPECTIVE AND MOVEMENT INPUT
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        if(PlayerInputManager.instance.moveAmount > 0.5f)
        {
            // MOVE AT RUNNING SPEED
            playerManager.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);

        }
        else if(PlayerInputManager.instance.moveAmount <= .5f)
        {
            // MOVE AT WALKING SPEED
            playerManager.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime); 
        }
    }

    private void HandleRotation()
    {
        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0f;

        if(targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
