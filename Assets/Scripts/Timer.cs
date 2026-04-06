using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float min = 0;
    float sec = 0;
    float time=0;
    string digit = "";
    TMP_Text timer;
    void Start()
    {
        timer = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        sec = time % 60;
        min = (time - sec) / 60;
        if(sec==0){
            digit = "00";
        }
        else if(sec<10){
            digit = "0";
        }
        else{
            digit = "";
        }

        timer.text = (int)min + ":" +digit+ (int)sec;

    }
}
