using UnityEngine;
using UnityEngine.Events;

public class Zombies : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //the zombies specific stats and animator
    public Animator zombieController;
    public float speed, health;
    public bool inHeart = false;
    public int type;
    public GameObject zom;
    public SpriteRenderer zombRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
   }
   
   //destroys itself when triggered
   public void destroyer(){
        Destroy(zom);
   }
}
