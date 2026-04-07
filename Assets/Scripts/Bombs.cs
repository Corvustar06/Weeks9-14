using UnityEngine;
using UnityEngine.Events;

public class Bombs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer sr;
    public bool sploding=false;
    public bool isMoving = true;
    public Animator animator;
    public Vector2 velocity;
    public Vector2 acceleration;
    public GameObject hitBox;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += velocity.x;
        if (velocity.x > 0)
        {
            velocity.x += acceleration.x;
        }
        else{
            velocity.x = 0;
            if(isMoving){
                isMoving = false;
                animator.SetBool("isSploding", true);
            }
        }
        transform.position = pos;
    }

    public void damage(){
        sploding = true;
    }

    public void destroy(){
        animator.SetBool("isSploding", false);
        sploding = false;
        sr.enabled = false;
    }
}
