using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //tracks how long the scene has been running
    float time=0;
    //the current time between zombies spawning
    float timer = 6;
    //the maximum time between zombies spawning
    float timerMax=6;

    //the maximum random number that can be generated
    int ranMax = 60;
    //the random number that is generated
    int ranNum;
    //the zombie type associated with this random number
    int zomType = 1;


    //The zombie prefab to be spawned and held
    public GameObject zom, spawnedZom;
    //keeping track of hearts and zombies that are active in the scene
    public List<GameObject> zombies, hearts;
    //the heart prefab to be spawned and held
    public GameObject heart, spawnedHeart;
    //the hitboxes of each damage type
    public GameObject bombHitBox,swordHitBox, bulletHitBox;
    //the bomb prefab to be spawned and held
    public GameObject bomb, spawnedBomb;
    //location of the player and where the bullets will be spawned
    public GameObject player, bulletSpawn;
    //the alternate colours for the other zombie types
    public Color col1, col2;
    //the y-coordinates of where each heart spawns when instantiated
    public float[] yCoords = { 0.78f, -4.35f, -2.64f, -0.92f, 2.69f };

    //the bullet prefab to be spawned and held
	public GameObject bullet, spawnedBullet;
    //storing the bullets
	public List<GameObject> bullets;

    //The scripts to hold individual variables and functions for each spawned prefab
	public ZombieDespawner despawner;
    public Zombies zomScript;
    public Bombs bombScript;

    public SpriteRenderer heartRenderer;

    //The time limit between using bombs
    public float bombTimer;
    //whether or not the timer has been triggered
    public bool timerActive = false;
    //which weapon is selected
    public int currentSlot;

	void Start()
    {
        //spawn the hearts initially and never again
        spawnHearts();
        
    }

    // Update is called once per frame
    void Update()
    {
        //keeping track of how long the game has been running
        time += Time.deltaTime;

        //Which zombies is spawned based on a random number between 1 and 100
        ranNum = Random.Range(1, ranMax);
        if (ranNum <= 60)
        {
            zomType = 1;
        }
        else if (ranNum <= 90)
        {
            zomType = 2;
        }
        else if (ranNum <= 100)
        {
            zomType = 3;
        }
        else{
            zomType = 1;
        }

        //how the game gets progressively harder over time. Some making it so harder zombies can spawn and others making the spawn time
        //between zombies spawning
        if (time > 150)
        {
            if (time % 15 < 0.005)
            {
                timerMax /= 1.2f;
            }
        }
        else if (time > 135)
        {
            timerMax = 2;
        }
        else if (time > 105)
        {
            ranMax = 100;
        }
        else if (time > 90)
        {
            timerMax = 3;
        }
        else if (time > 75)
        {
            timerMax = 4;
        }
        else if (time > 60)
        {
            ranMax = 90;
        }
        else if (time > 30)
        {
            timerMax = 5;
        }

        //when the timer is up, spawn a zombie and reset the timer
		if (timer <= 0){
            spawnZom();
            timer = timerMax;
        }

        //the timer counting down
        timer -= Time.deltaTime;

        //checks to despawn zombies that have left the game bounds
        for(int i = 0;i<zombies.Count;i++){
			GameObject activeZom = zombies[i];
			
			despawner = activeZom.GetComponent<ZombieDespawner>();
            zomScript = activeZom.GetComponent<Zombies>();
			Vector3Int zombro = Vector3Int.FloorToInt(activeZom.transform.position);

			if (despawner.bg.cellBounds.Contains(zombro))
			{
				despawner.hasEntered = true;
			}

            //Bottom row zombies weren't despawning for some reason. I wish I could make them work with the rest of the code, but 
            //for now, this is a fix that makes sure they despawn after they've moved far enough.
            if(zombro.x<-15){
                despawner.hasEntered = true;
            }

			if (despawner.hasEntered)
			{
				if (!despawner.bg.cellBounds.Contains(zombro))
				{
					despawner.inBounds = false;
                    
                    zombies.Remove(activeZom);
					Destroy(activeZom);
                    
				}
			}

            if(zomScript.health<=0){
                zombies.Remove(activeZom);
				zomScript.zom = activeZom;
				zomScript.zombieController.SetTrigger("IsKilled");
            }
		}

        //checks if zombies have reached the hearts
        for (int i = 0; i < zombies.Count; i++)
        {
            GameObject activeZom = zombies[i];
            zomScript = activeZom.GetComponent<Zombies>();

			Vector2 pos = zomScript.transform.position;
			pos.x -= zomScript.speed * Time.deltaTime;
			zomScript.transform.position = pos;

			for (int j = 0; j < hearts.Count; j++)
			{
                GameObject activeHeart = hearts[j];
                heartRenderer = activeHeart.GetComponent<SpriteRenderer>();
				if (hearts[j] != null)
				{
					if (heartRenderer.bounds.Contains(zomScript.transform.position))
					{
						if (zomScript.inHeart)
						{
							//do nothing
						}
						else
						{
							hearts[j].GetComponent<Hearts>().startBreak();
							zomScript.inHeart = true;
						}

					}
				}
			}
		}

        //the timer between spawning bombs because infinite bomb use would be too powerful
        if(timerActive){
            bombTimer -= Time.deltaTime;
            if (bombTimer < 0)
            {
                timerActive = false;
            }
        }

        //if the bomb has exploded, hurt the zombies in the area
        if (bombScript != null)
        {
            if (bombScript.sploding)
            {
                bombDamage();
                bombScript.sploding = false;
            }
        }

        //do damage if the bullet hits a zombie
        bulletDamage();

        //despawns bullets that have left the game space
        for(int i=0; i< bullets.Count; i++){
            GameObject activeBullet = bullets[i];
            MoveBullet bulletScript = activeBullet.GetComponent<MoveBullet>();
            if (bulletScript.transform.position.x > 15)
            {
                bullets.Remove(activeBullet);
                Destroy(activeBullet);
            }
        }
        
	}

    void spawnZom(){
        Vector3 spawnLoc;
        spawnLoc.z = 0;
        spawnLoc.x = 18;
        //the y-coordinates where the zombies spawn
        float[] rows = { -4.34f, -2.58f, -0.82f, 0.94f, 2.7f };
        //randomly choose one of the above numbers
        spawnLoc.y = rows[Random.Range(0, 5)];
        //spawn the zombie
        spawnedZom = Instantiate(zom, spawnLoc, transform.rotation);
        
        //mess with the specific zombies variables, animator, and sprite renderer
        zomScript = spawnedZom.GetComponent<Zombies>();
        zomScript.zombRenderer = spawnedZom.GetComponent<SpriteRenderer>();
		zomScript.zombieController = spawnedZom.GetComponent<Animator>();
		zomScript.zombieController.SetBool("IsMoving", true);
        //hold which type of zombie it is
        zomScript.type = zomType;

        //depending on which zombie it is, these are its stats
        if (zomType == 1)
        {
            zomScript.health = 10;
            zomScript.speed = 1;
            zomScript.type = 1;
            
        }
        else if (zomType == 2)
        {
            zomScript.health = 10;
            zomScript.speed = 2;
            zomScript.type = 2;
			zomScript.zombRenderer.color = col1;
		}
        else
        {
            zomScript.health = 20;
            zomScript.speed = 0.5f;
            zomScript.type = 3;
			zomScript.zombRenderer.color = col2;
		}

        //now hold the information for this zombie
		zombies.Add(spawnedZom);
    }

    void spawnHearts(){
    //spawn the hearts only once in the specific positions and keep track of each of them
        for(int i = 0; i<5;i++){
            if(yCoords.Length==5){
                Vector3 spawnLoc;
                spawnLoc.z = 0;
                spawnLoc.x = -11.41f;
                spawnLoc.y = yCoords[i];
                spawnedHeart = Instantiate(heart, spawnLoc, transform.rotation);

                hearts.Add(spawnedHeart);
            }
        }
    }

    public void bombDamage(){
    //checks whether or not each zombie is within the bomb's hitbox
        for(int i =0; i<zombies.Count;i++){
            GameObject activeZom = zombies[i];
			zomScript = activeZom.GetComponent<Zombies>();

            SpriteRenderer hitRenderer = bombHitBox.GetComponent<SpriteRenderer>();

				//if it's in the hit box, do the appropriate damage based on the zombies type
					if (hitRenderer.bounds.Contains(zomScript.transform.position))
					{
                        if (zomScript.type == 3)
                        {
                            zomScript.health -= 5;
                        }
                        else
                        {
                            zomScript.health -= 15;
                        }
                        //do the getting hit animation
                        zomScript.zombieController.SetTrigger("IsHit");
					}
				
			
		}
    }

    public void spawnBomb(){
    //so long as the timer isn't running
        if (!timerActive)
        {
        //spawn the bomb and restart the timer
            spawnedBomb = Instantiate(bomb, player.transform.position, transform.rotation);
            bombScript = spawnedBomb.GetComponent<Bombs>();
            bombHitBox = bombScript.hitBox;
            bombTimer = 5;
            timerActive = true;
        }
    }

    public void swordDamage(){
        //check if each zombie is within the sword's hitbox
		for (int i = 0; i < zombies.Count; i++)
		{
			GameObject activeZom = zombies[i];
			zomScript = activeZom.GetComponent<Zombies>();

			SpriteRenderer hitRenderer = swordHitBox.GetComponent<SpriteRenderer>();

            //if it's in the hitbox, do the appropriate damage
			if (hitRenderer.bounds.Contains(zomScript.transform.position))
			{
                if (zomScript.type == 1)
                {
                    zomScript.health -= 2;
                }
                else
                {
                    zomScript.health -= 5;
                }
                //play the right animation
				zomScript.zombieController.SetTrigger("IsHit");

			}


		}
	}

	public void spawnBullet()
	{
        //spawn the bullet and pull it's hitbox
		spawnedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(0,0,-90));
		MoveBullet bulletScript = spawnedBullet.GetComponent<MoveBullet>();
		bulletScript.sr = spawnedBullet.GetComponent<SpriteRenderer>();
        bulletHitBox = bulletScript.hitBox;
		bullets.Add(spawnedBullet);
	}

    public void bulletDamage(){
    //check if each zombie is within the hitbox
        for(int i = 0;i < zombies.Count;i++){
            GameObject activeZom = zombies[i];
            zomScript = activeZom.GetComponent<Zombies>();
            
            //for each bullet too
            for (int j = 0; j < bullets.Count; j++)
            {
                GameObject activeBullet = bullets[j];
                MoveBullet bulletScript = activeBullet.GetComponent<MoveBullet>();
				SpriteRenderer hitRenderer = bulletHitBox.GetComponent<SpriteRenderer>();
                
                //if the bullet hits the  zombie, deal the appropriate damage and despawn the zombie
				if (hitRenderer.bounds.Contains(zomScript.transform.position))
                {
                    if (zomScript.type == 2)
                    {
                        zomScript.health -= 2;
                    }
                    else
                    {
                        zomScript.health -= 5;
                    }
                    zomScript.zombieController.SetTrigger("IsHit");
                    bullets.Remove(activeBullet);
                    Destroy(activeBullet);
                }
            }
        }
    }

	public void attack(int weapon){
        //based on the current weapon selected, activate that specific weapon
        currentSlot = weapon;
        if (weapon == 0)
        {
            spawnBomb();
        }
        else if (weapon == 1)
        {
            spawnBullet();
        }
        else if (weapon == 2)
        {
			swordDamage();
		}
    }
    
}
