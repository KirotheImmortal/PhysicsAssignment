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

   public bool isStatic = false;

  public  float mass =1;

    void Awake()
    {

        vel = acl = frc = mom = Vector3.zero;
        mass += .00001f;
    }

    void Update()
    {
        if (!isStatic)
        {
           // frc += CalUniformGravity(GetComponent<Node>());

           CalNewAcceleration(gameObject.GetComponent<Node>());
           //frc = Vector3.zero;
            //frc += CalUniformGravity(this.GetComponent<Node>());
           // print(this + "/\t" + vel + "/\t" + acl + "/\t" + frc);
            gameObject.transform.position = CalNewPosition(gameObject.GetComponent<Node>());
            
        }
        //frc = Vector3.zero;
    }

    static public Vector3 CalNewAcceleration(Node a)
    {
        return a.acl = (1f / a.mass) * a.frc;
    }

    static public Vector3 CalNewVelocity(Node a)
    {
        return a.vel += (CalNewAcceleration(a) * Time.deltaTime);
    }

    static public Vector3 CalNewPosition(Node a)
    {
        return a.transform.position + CalNewVelocity(a) * Time.deltaTime;
    }

    static public Vector3 CalUniformGravity(Node a)
    {
        return Mathf.Abs(a.mass) * new Vector3(0f, -9.8f, 0f) * (Time.deltaTime * Time.deltaTime);
    }
}
