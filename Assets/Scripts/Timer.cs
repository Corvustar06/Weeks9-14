using TMPro;
using UnityEngine;


//This script is for the timer at the top of the scene
public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //holds the displayed values
    float min = 0;
    float sec = 0;
    float time=0;
    string digit = "";
    TMP_Text timer;
    void Start()
    {
    // so the text in the inspector can be changed
        timer = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    //tracks how long the game has been running
        time += Time.deltaTime;
        //how many seconds are left over
        sec = time % 60;
        //how many minutes have passed excluding the seconds
        min = (time - sec) / 60;
        //displayed 0s for aesthetic reasons
        if(sec==0){
            digit = "00";
        }
        else if(sec<10){
            digit = "0";
        }
        else{
            digit = "";
        }
        //changes the text in the scene
        timer.text = (int)min + ":" +digit+ (int)sec;

    }
}
