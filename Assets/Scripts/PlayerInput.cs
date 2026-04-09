using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        //updates the movement when the InputAction is called based on the WASD inputs
        movement = context.ReadValue<Vector2>();
    }
}
