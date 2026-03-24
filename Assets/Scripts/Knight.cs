using NUnit.Framework;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public AudioSource SFX;
    public AudioSource Sword;
    public AudioSource[] footsteps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Footstep()
    {
        //Debug.Log("Step");
        //SFX.Play();
        footsteps[Random.Range(0, footsteps.Length)].Play();
    }

    public void Slice()
    {
        Sword.Play();
    }
}
