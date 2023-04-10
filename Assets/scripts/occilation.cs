using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class occilation : MonoBehaviour
{
    Vector3 startposition;
    [SerializeField] Vector3 movementvector;
    float movementfactor;     

    [SerializeField] float period = 8f ; 

    void Start()
    {
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}

        float cycles = Time.time / period ;
        const float tau = Mathf.PI * 2 ;

        float rawsin = Mathf.Sin(cycles * tau);
        movementfactor = (rawsin + 1f)/2f;

        Vector3 offset = movementvector * movementfactor;

        transform.position = startposition + offset ;
    }
}
