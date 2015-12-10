using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{

   //public Vector3 pos; //Position
   public Vector3 vel; //Velocity
   public Vector3 acl; //Acceleration
   public Vector3 mom; //Momentum
   public Vector3 frc; // Force

   public Vector3 airfrc;

   public  float mass =1;

   public bool isAnchor = false;

  

    void Awake()
    {

        vel = acl = frc = mom = Vector3.zero;
        mass += .0000001f;
    }
    void Update()
    {   
        //if(Vector3.Magnitude(frc)!= 0)
        //Spring.MoveNode(this);
    }




   
    //static public GameObject MakeNode(bool anchor, PrimitiveType type)
    //{
    //    GameObject newNode = GameObject.CreatePrimitive(type);
    //    new 
    //    return 
    //}



}
