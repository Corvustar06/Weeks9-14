using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	public float speed = 5;
	public Vector2 movement;
	Animator mushroomController;
	SpriteRenderer sr;
	bool isMoving = false;
	bool forward = false;

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
		Debug.Log("Attack!" + context.phase);
	}

	public void OnPrev(InputAction.CallbackContext context)
	{
		Debug.Log("Prev"+context.phase);
	}

	public void OnNext(InputAction.CallbackContext context)
	{
		Debug.Log("Next"+context.phase);
	}

}
