using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tilemap;
    public Tile[] tiles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);
        Vector3 pos = tilemap.GetCellCenterWorld(cellPos);
        // Debug.Log(mousePos + " " + cellPos);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tilemap.GetTile(cellPos)==tiles[i])
                {
                    Debug.Log("This is stone");
                    break;
                }
            }
        }
    }
}
