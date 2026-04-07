using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultipler : MonoBehaviour
{
    public LocalMultiplayerManager manager;
    public PlayerInput playerInput;
    public Vector2 movementInput;
    public float speed = 5;
    public float health = 20;
    public bool isDead = false;
    public SpriteRenderer sr;

    void Start()
    {
        sr.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0)
        {
            isDead = true;
            
        }

        if (!isDead)
        {

            transform.position += (Vector3)movementInput * speed * Time.deltaTime;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            manager.PlayerAttacking(playerInput);
        }
    }

    public void heal()
    {
        isDead = false;
        health = 20;
    }
}
