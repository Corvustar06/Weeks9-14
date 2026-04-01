using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieDespawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap bg;
    public bool inBounds = true;
    public bool hasEntered = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int zombro = Vector3Int.FloorToInt(transform.position);

		if(bg.cellBounds.Contains(zombro)){
			hasEntered = true;
		}

        if(hasEntered){
			if (!bg.cellBounds.Contains(zombro))
			{
				inBounds = false;
                Debug.Log("Zombro has left the play area");
                Destroy(gameObject);
			}
		}
	}
}
