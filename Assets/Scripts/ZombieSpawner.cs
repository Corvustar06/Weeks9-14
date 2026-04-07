using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float time=0;
    float timer = 6;
    float timerMax=6;

    int ranMax = 60;
    int ranNum;
    int zomType = 1;

    public GameObject zom, spawnedZom;
    public List<GameObject> zombies, hearts;
    public GameObject heart, spawnedHeart;
    public GameObject bombHitBox,swordHitBox, bulletHitBox;
    public GameObject bomb, spawnedBomb;
    public GameObject player, bulletSpawn;
    public float[] yCoords = { 0.78f, -4.35f, -2.64f, -0.92f, 2.69f };

	public GameObject bullet, spawnedBullet;
	public List<GameObject> bullets;

	public ZombieDespawner despawner;
    public Zombies zomScript;
    public Bombs bombScript;

    public SpriteRenderer heartRenderer;

    public float bombTimer;
    public bool timerActive = false;

	void Start()
    {
        spawnHearts();
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


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


		if (timer <= 0){
            spawnZom();
            timer = timerMax;
        }

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

        if(timerActive){
            bombTimer -= Time.deltaTime;
            if (bombTimer < 0)
            {
                timerActive = false;
            }
        }
        if (bombScript != null)
        {
            if (bombScript.sploding)
            {
                bombDamage();
                bombScript.sploding = false;
            }
        }

        bulletDamage();

        for(int i=0; i< bullets.Count; i++){
            GameObject activeBullet = bullets[i];
            MoveBullet bulletScript = activeBullet.GetComponent<MoveBullet>();
            if (bulletScript.transform.position.x > 10)
            {
                bullets.Remove(activeBullet);
                Destroy(activeBullet);
            }
        }
        
	}

    //random spawns zombies from a random number that changes over time

    void spawnZom(){
        Vector3 spawnLoc;
        spawnLoc.z = 0;
        spawnLoc.x = 18;
        float[] rows = { -4.34f, -2.58f, -0.82f, 0.94f, 2.7f };
        spawnLoc.y = rows[Random.Range(0, 5)];
        spawnedZom = Instantiate(zom, spawnLoc, transform.rotation);

        zomScript = spawnedZom.GetComponent<Zombies>();

		zomScript.zombieController = spawnedZom.GetComponent<Animator>();
		zomScript.zombieController.SetBool("IsMoving", true);

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
        }
        else
        {
            zomScript.health = 20;
            zomScript.speed = 0.5f;
            zomScript.type = 3;
        }

		zombies.Add(spawnedZom);
    }

    void spawnHearts(){
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
        for(int i =0; i<zombies.Count;i++){
            GameObject activeZom = zombies[i];
			zomScript = activeZom.GetComponent<Zombies>();

            SpriteRenderer hitRenderer = bombHitBox.GetComponent<SpriteRenderer>();

				
					if (hitRenderer.bounds.Contains(zomScript.transform.position))
					{
                        zomScript.health -= 15;
                        zomScript.zombieController.SetTrigger("IsHit");
					}
				
			
		}
    }

    public void spawnBomb(){
        if (!timerActive)
        {
            spawnedBomb = Instantiate(bomb, player.transform.position, transform.rotation);
            bombScript = spawnedBomb.GetComponent<Bombs>();
            bombHitBox = bombScript.hitBox;
            bombTimer = 5;
            timerActive = true;
        }
    }

    public void swordDamage(){
        
		for (int i = 0; i < zombies.Count; i++)
		{
			GameObject activeZom = zombies[i];
			zomScript = activeZom.GetComponent<Zombies>();

			SpriteRenderer hitRenderer = swordHitBox.GetComponent<SpriteRenderer>();


			if (hitRenderer.bounds.Contains(zomScript.transform.position))
			{
				zomScript.health -= 5;
				zomScript.zombieController.SetTrigger("IsHit");

			}


		}
	}

	public void spawnBullet()
	{
		spawnedBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.Euler(0,0,-90));
		MoveBullet bulletScript = spawnedBullet.GetComponent<MoveBullet>();
		bulletScript.sr = spawnedBullet.GetComponent<SpriteRenderer>();
        bulletHitBox = bulletScript.hitBox;
		bullets.Add(spawnedBullet);
	}

    public void bulletDamage(){
        for(int i = 0;i < zombies.Count;i++){
            GameObject activeZom = zombies[i];
            zomScript = activeZom.GetComponent<Zombies>();
            

            for (int j = 0; j < bullets.Count; j++)
            {
                GameObject activeBullet = bullets[j];
                MoveBullet bulletScript = activeBullet.GetComponent<MoveBullet>();
				SpriteRenderer hitRenderer = bulletHitBox.GetComponent<SpriteRenderer>();

				if (hitRenderer.bounds.Contains(zomScript.transform.position))
                {
                    zomScript.health -= 5;
                    zomScript.zombieController.SetTrigger("IsHit");
                    bullets.Remove(activeBullet);
                    Destroy(activeBullet);
                }
            }
        }
    }

	public void attack(int weapon){
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
