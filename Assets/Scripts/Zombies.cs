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
		Vector2 pos = transform.position;
		pos.x -= speed * Time.deltaTime;
		transform.position = pos;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                if (hearts[i].bounds.Contains(transform.position))
                {
                    hearts[i].GetComponent<Hearts>().startBreak();

                }
            }
        }
    }
}
