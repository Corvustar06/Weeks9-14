using UnityEngine;

public class weaponSwap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //The visual position of the red selection outline
    //I learned that the size of the screen ruins this really quickly
    public float[] xPos = { 779.4f, 850.7f, 918.5f };
    public float yPos = 359;
    public int chosenWeapon=0;
    public RectTransform sr;

    void Start()
    {
        //the image in the UI
        sr= GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //changes which weapon is active and which one is being higlighted
    public void swapWeapon(int index){
        chosenWeapon = index;
        Vector2 pos;
        pos.x = xPos[chosenWeapon];
        pos.y = yPos;
        sr.transform.position = pos;
    }
}
