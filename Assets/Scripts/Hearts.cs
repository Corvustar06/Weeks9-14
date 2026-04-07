using UnityEditor.Animations;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator heartController;
    SpriteRenderer sr;
    public SpriteRenderer zombie;
    //bool heartBroken = false;
    void Start()
    {
		heartController = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {

    }

    //triggers the heart breaking animation
    public void startBreak(){
		
		//heartBroken = true;
		heartController.SetBool("IsDestroyed", true);
        heartController.SetTrigger("Destroy");
	}

    //hides the heart from the scene
    void Destroy(){
        sr.enabled = false;
    }

}
