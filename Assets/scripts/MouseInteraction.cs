using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MouseInteraction : MonoBehaviour
{
    CreateCloth cc;
    Camera c;
    GameObject go;
    public

    GameObject SelectedNode;

    void Awake()
    {
        cc = FindObjectOfType<CreateCloth>();
        c = gameObject.GetComponent<Camera>();

        if (c.orthographic)
        {
            c.orthographicSize = cc.amount * cc.distanceAppart;
        }



        go = new GameObject(); ///  Creates empty gameobject to be used as cursor in World space
    }


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {// Checks if the mouse wheele is being rolled forward
            //transform.position += transform.forward * 20 * Time.deltaTime;
            c.orthographicSize--;
        }// Moves the camera forward

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {// Checks if the mouse wheele is being rolled backward
           // transform.position -= transform.forward * 20 * Time.deltaTime;  // Moves the camera back
            c.orthographicSize++;
        }

        if (Input.GetMouseButtonDown(0)) // Checks to see if left mouse button is clicked
        {

            CheckForObject(ref cc.objectNodes); // Calls CheckForObject
        }
        if(Input.GetMouseButton(0))
        {
            MoveObject();
           
        }
        else
        {
            SelectedNode = null;
        }

        if (Input.GetMouseButton(1)) // Checks to see if right mouse button is clicked
        {
            //print(Input.mousePosition);
            CutObjects(ref cc.objectNodes,ref cc.springs);// calls check for object

        }


        float dis = c.WorldToScreenPoint(go.transform.position).z; // finds the world to screepoint of go
        go.transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }


    void CutObjects(ref Dictionary<Vector2, GameObject> nodes, ref List<GameObject> springs)
    {
       
        foreach (KeyValuePair<Vector2, GameObject> n in nodes)
        {
            if (n.Value != null)
            {
                float dis = c.WorldToScreenPoint(n.Value.transform.position).z;
                go.transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dis));


                //Checks to see if the X and the Y of the worldspace cursor and the n gameobject is the same
              if (go.transform.position.x < n.Value.transform.position.x + .5f
                    && go.transform.position.y < n.Value.transform.position.y + .5f 
                    && go.transform.position.x > n.Value.transform.position.x - .5f 
                    && go.transform.position.y > n.Value.transform.position.y - .5f)
                {   //Checks to see wich mouse was pressed by the button int passed in
                    Destroy(n.Value); //Destroys the n gameobject
                }
            }

        }
        foreach (GameObject s in springs)
        {
            if (s != null)
            {
                float dis = c.WorldToScreenPoint(s.transform.position).z;
                go.transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dis));

                if (go.transform.position.x < s.transform.position.x + .5f && go.transform.position.y < s.transform.position.y + .5f && go.transform.position.x > s.transform.position.x - .5f && go.transform.position.y > s.transform.position.y - .5f)
                {   //Checks to see wich mouse was pressed by the button int passed in
                    Destroy(s); //Destroys the n gameobject
                }
            }
        }


    }

    void CheckForObject(ref Dictionary<Vector2, GameObject> nodes)
    {
        foreach (KeyValuePair<Vector2, GameObject> n in nodes)
        {

            if (n.Value != null)
            {
                float dis = c.WorldToScreenPoint(n.Value.transform.position).z;
                go.transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dis));

                //Checks to see if the X and the Y of the worldspace cursor and the n gameobject is the same
                if (    go.transform.position.x < n.Value.transform.position.x + .5f 
                    &&  go.transform.position.y < n.Value.transform.position.y + .5f 
                    &&  go.transform.position.x > n.Value.transform.position.x - .5f 
                    &&  go.transform.position.y > n.Value.transform.position.y - .5f)
                {   //Checks to see wich mouse was pressed by the button int passed in
                        SelectedNode = n.Value; // Sets the node to SelectedNode

                }
                //else
                //    SelectedNode = null;
            }
        }

      

    }
    void MoveObject()
    {
        if(SelectedNode != null)
        {
            float dis = c.WorldToScreenPoint(SelectedNode.transform.position).z;
            go.transform.position = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dis));
        //    SelectedNode.GetComponent<Node>().mouseFrc =  ;
        }
    }

}
