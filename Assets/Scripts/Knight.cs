using NUnit.Framework;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public AudioSource SFX;
    public AudioSource Sword;
    public AudioClip[] footsteps;
    private AudioSource footstep;
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
        footstep = GetComponent<AudioSource>();
        footstep.clip = footsteps[Random.Range(0, footsteps.Length)];
        footstep.Play();
    }

    public void Slice()
    {
        Sword.Play();
    }
}
