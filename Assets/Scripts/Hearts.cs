using UnityEngine;

public class Hearts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Animator heartController;
    SpriteRenderer sr;
    public SpriteRenderer zombie;
    void Start()
    {
        heartController = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if zombie is over heart
        //heart is destroyed
        if (zombie != null)
        {
            if (zombie.bounds.Contains(transform.position))
            {
                heartController.SetBool("IsDestroyed", true);
            }
        }
		
	}

    void Destroy(){
        sr.enabled = false;
    }

}
