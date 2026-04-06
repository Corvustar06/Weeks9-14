using UnityEngine;
using UnityEngine.Events;

public class Zombies : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Animator zombieController;
    public float speed;
    public SpriteRenderer[] hearts;

    void Start()
    {
        zombieController = GetComponent<Animator>();
        zombieController.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
