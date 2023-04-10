using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainthrust = 1000f;
    [SerializeField] float rotatethrust = 100f;
    [SerializeField] AudioClip boostsound ;

    [SerializeField] ParticleSystem mainparticles;
    [SerializeField] ParticleSystem rightparticles;
    [SerializeField] ParticleSystem leftparticles;

    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
         audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        processthrust();
        processrotation();
    } 

    void processthrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            startthrusting();
        }
        else
        {
            stopthrusting();
        }
    }
    
    void startthrusting()
    {
        rb.AddRelativeForce(Vector3.up*mainthrust*Time.deltaTime);
        if(!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(boostsound);
        }
        if(!mainparticles.isPlaying)
        {
            mainparticles.Play();
        }
    }

    void stopthrusting()
    {
        audiosource.Stop();
        mainparticles.Stop();
    }

    void processrotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotateleft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotateright();
        }
        else
        {
            stoprotating();
        }
    }

    void rotateleft()
    {
        applyrotation(rotatethrust);
        leftparticles.Stop();
        if(!rightparticles.isPlaying)
        {
            rightparticles.Play();
        }
    }

    void rotateright()
    {
        applyrotation(-rotatethrust);
        rightparticles.Stop();
        if(!leftparticles.isPlaying)
        {
            leftparticles.Play();
        }
    }

    void stoprotating()
    {
        rightparticles.Stop();
        leftparticles.Stop();
    }

    void  applyrotation(float rotationthisframe)
    {
        rb.freezeRotation = true ;  // freezing the rotations so that we can logically rotate the body instead and without alowing the physics system to do its work
        transform.Rotate(Vector3.forward*rotationthisframe*Time.deltaTime);
        rb.freezeRotation = false ;     //now we are just unfreezing it 
    }

}
