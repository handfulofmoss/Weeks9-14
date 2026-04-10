using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //players movement speed
    public float speed = 5;
    //player location
    public Vector2 movement;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //moves the player character
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        //while player is moving, walk animations will be active
        animator.SetBool("isWalking", true);

        //if walking is stopped, player character will swap to idol animation, depending on the last facing direction
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
        }

        //updates the movement when the InputAction is called based on the WASD inputs
        movement = context.ReadValue<Vector2>();
        //bases the player characters current facing direction based on player movement
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);

    }
}
