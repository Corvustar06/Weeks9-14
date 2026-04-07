using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieDespawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //holds the location of the background and whether or not the zombies are inside
    //I ended up not needing this script
    public Tilemap bg;
    public bool inBounds = true;
    public bool hasEntered = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
	}
}
