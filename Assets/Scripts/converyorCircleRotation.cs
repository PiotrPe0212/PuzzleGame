using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class converyorCircleRotation : MonoBehaviour
{
    public int multiplier=1;
    private Quaternion rotation;
    private Vector3 _rotationStep;
   
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        _rotationStep -= new Vector3(0, 0, Time.deltaTime*multiplier);
        rotation = Quaternion.Euler(_rotationStep);
        gameObject.transform.rotation = rotation;
       
    }
}
