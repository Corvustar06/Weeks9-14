using UnityEngine;
using UnityEngine.Events;

public class Guns : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public Animator animator;
	public SpriteRenderer sr;
	public UnityEvent onGunFire;
	void Start()
    {
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		startHit();
		onGunFire.Invoke();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void startHit()
	{
		animator.SetBool("buttonPressed", true);
		sr.enabled = true;
	}

	public void endHit()
	{
		animator.SetBool("buttonPressed", false);
		onGunFire.Invoke();
		sr.enabled = false;
	}
}
