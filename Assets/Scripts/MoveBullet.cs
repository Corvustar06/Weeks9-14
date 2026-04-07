using Unity.Hierarchy;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public SpriteRenderer sr;
    public GameObject hitBox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += speed;
        transform.position = pos;
    }
}
