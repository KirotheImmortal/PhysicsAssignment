using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MouseInteraction : MonoBehaviour
{


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouse = Input.mousePosition;
            
        }

    }

    void CheckForObject(ref Dictionary<Vector2,GameObject> nodes, Vector3 mousePosition)
    {

    }


}
