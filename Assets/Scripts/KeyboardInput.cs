using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class KeyboardInput : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	public float speed = 5;
	public Vector2 movement;
	Animator mushroomController;
	SpriteRenderer sr;
	bool isMoving = false;
	bool forward = false;
	public int currentSlot = 0;
	public UnityEvent<int> OnSwapWeapon,OnAttacking;
	

	void Start()
    {
        mushroomController = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += (Vector3)movement * speed * Time.deltaTime;

		if (movement.x != 0 || movement.y != 0)
		{
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}

		mushroomController.SetBool("isMoving", isMoving);
		forward = movement.x > 0 || movement.y > 0;
		mushroomController.SetBool("forward", forward);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			OnAttacking.Invoke(currentSlot);
		}
	}

	public void OnPrev(InputAction.CallbackContext context)
	{
		if(context.performed){
			
			if (currentSlot == 0)
			{
				currentSlot = 2;
			}
			else
			{
				currentSlot -= 1;
			}

			OnSwapWeapon.Invoke(currentSlot);
		}
		
	}

	public void OnNext(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			
			if (currentSlot == 2)
			{
				currentSlot = 0;
			}
			else
			{
				currentSlot += 1;
			}
			OnSwapWeapon.Invoke(currentSlot);
		}
	}

}
