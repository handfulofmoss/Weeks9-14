using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float speed = 5;
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
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
        }

        //updates the movement when the InputAction is called based on the WASD inputs
        movement = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);

    }
}
