using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateCloth : MonoBehaviour
{

    public int amount;
    public float distanceAppart;
    [SerializeField]
    GameObject nodePref;
    [SerializeField]
    GameObject springPref;

    public bool LockCorners = true;

    public List<Vector2> lockedNodesPosition = new List<Vector2>();

    List<Spring> springs = new List<Spring>();
    //List<Node> nodes = new List<Node>();
    Dictionary<Vector2, GameObject> objectNodes = new Dictionary<Vector2, GameObject>();


    void Awake()
    {
        for(int row = 0; row < amount; row++)
        {
           for(int colom = 0; colom <amount; colom++)
           {
               GameObject node = (GameObject)GameObject.Instantiate(nodePref, new Vector3(colom * distanceAppart, row * distanceAppart), gameObject.transform.rotation);
               node.GetComponent<Node>().isAnchor = false;

               objectNodes.Add(new Vector2(row,colom), node);
           }
        }
        if(LockCorners)
        {
            lockedNodesPosition.Add(new Vector2(0, 0));
            lockedNodesPosition.Add(new Vector2(0, amount-1));
            lockedNodesPosition.Add(new Vector2(amount-1, 0));
            lockedNodesPosition.Add(new Vector2(amount-1, amount-1));
        }

        foreach(Vector2 node in lockedNodesPosition)
        {
            if (objectNodes.ContainsKey(node))
            {
                print(node);
                objectNodes[node].GetComponent<Node>().isAnchor = true;
            }
        }

        for (int row = 0; row < amount; row++)
        {
            for (int colom = 0; colom < amount; colom++)
            {

                if(objectNodes.ContainsKey(new Vector2(row+1, colom)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row+1, colom )], springPref);
                }
                if(objectNodes.ContainsKey(new Vector2(row, colom+1)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row, colom+1) ], springPref);
                }
                if (objectNodes.ContainsKey(new Vector2(row+1, colom + 1)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row+1, colom + 1)], springPref);
                }
                if (objectNodes.ContainsKey(new Vector2(row - 1, colom + 1)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row - 1, colom + 1)], springPref);
                }
              
                //if(row != amount -1)
                //{
                //    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row, colom+1)], springPref);
                //}
                //if (colom != amount - 1)
                //{
                //    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row, colom + 1)], springPref);
                //}
            }
        }
        

    }

}
