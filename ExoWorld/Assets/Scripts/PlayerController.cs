using UnityEngine;

[RequireComponent(typeof(CharacterController))]  //Dont run if no character controller
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;  //Adjusts player speed
    private float jumpHeight = 1.0f;  // jump height
    private float gravityValue = -9.81f;  //gravity value, decrease once outside spaceship??
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start()
    {
        // get character controller
        controller = GetComponent<CharacterController>();
        //call instance
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //Convert to vector3
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer) //check if player jumped
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
