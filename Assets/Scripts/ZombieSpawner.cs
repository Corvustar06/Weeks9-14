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
    public float[] yCoords = { 0.78f, -4.35f, -2.64f, -0.92f, 2.69f };
    public ZombieDespawner despawner;
    public Zombies zomScript;
    public SpriteRenderer heartRenderer;
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

        for(int i = 0;i<zombies.Count;i++){
			GameObject activeZom = zombies[i];
			despawner = activeZom.GetComponent<ZombieDespawner>();
			Vector3Int zombro = Vector3Int.FloorToInt(activeZom.transform.position);

			if (despawner.bg.cellBounds.Contains(zombro))
			{
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
		}

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
    
}
