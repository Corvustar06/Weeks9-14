using UnityEngine;

public class DestroyBombHitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //intended to remove the hitboxes that are spawned with the bombs, although I'm not sure that it works
    public float timer;
    public GameObject hitBox;
    void Start()
    {
        hitBox = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    //meant to destroy the hitbox after its been in the scene for long enough
        timer-= Time.deltaTime;
        if(timer < 0 ){
            Destroy(hitBox);
        }
    }
}
