using UnityEngine;

public class Swords : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    public SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //triggers the beginning of the sword swinging animation
    public void startHit(){
        animator.SetBool("buttonPressed", true);
        sr.enabled = true;
    }

    //stops the sword swinging animation
    public void endHit()
    {
        animator.SetBool("buttonPressed", false);
        sr.enabled = false;
    }
}
