using UnityEngine;

public class DestroyBombHitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float timer;
    public GameObject hitBox;
    void Start()
    {
        hitBox = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer-= Time.deltaTime;
        if(timer < 0 ){
            Destroy(hitBox);
        }
    }
}
