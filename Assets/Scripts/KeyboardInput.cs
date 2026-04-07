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
	public UnityEvent OnSwordHit, OnGunHit;
	

	void Start()
    {
        mushroomController = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		//makes the player moves based on the playerinput chaning the movement variable
		transform.position += (Vector3)movement * speed * Time.deltaTime;

		//checks in the player is still or moving
		if (movement.x != 0 || movement.y != 0)
		{
			isMoving = true;
		}
		else
		{
			isMoving = false;
		}

		//whichever it is, it sets the paramater in the animator to be the same
		mushroomController.SetBool("isMoving", isMoving);
		//if they are moving forward or back / up or down
		forward = movement.x > 0 || movement.y > 0;
		//sets the same in the animator
		mushroomController.SetBool("forward", forward);
	}

	//makes the player move
	public void OnMove(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
	// so it's only triggered once
		if (context.performed)
		{
			//triggers a function in the zombieSpawner script
			OnAttacking.Invoke(currentSlot);
			//checks if the gun or the sword are being used so that it can make them visible in the scene
			if (currentSlot == 2)
			{
				OnSwordHit.Invoke();
			}
			else if (currentSlot == 1)
			{
				OnGunHit.Invoke();
			}
		}
	}

	public void OnPrev(InputAction.CallbackContext context)
	{
		if(context.performed){
			//makes sure that their slot switching loops between 0,1, and 2 so we know which weapon is being used
			if (currentSlot == 0)
			{
				currentSlot = 2;
			}
			else
			{
				currentSlot -= 1;
			}
			//sends this choice into another script to be referenced
			OnSwapWeapon.Invoke(currentSlot);
		}
		
	}

	public void OnNext(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			//makes sure that their slot switching loops between 0,1, and 2 so we know which weapon is being used

			if (currentSlot == 2)
			{
				currentSlot = 0;
			}
			else
			{
				currentSlot += 1;
			}
			//sends this choice into another script to be referenced
			OnSwapWeapon.Invoke(currentSlot);
		}
	}

}
