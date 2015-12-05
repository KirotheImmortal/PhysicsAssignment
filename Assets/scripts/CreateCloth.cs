using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateCloth : MonoBehaviour
{
    public int k;
    public int b;
    public int amount;
    public float distanceAppart;
    [SerializeField]
    GameObject nodePref;
    [SerializeField]
    GameObject springPref;

    public enum BorderLock { NoLock, LockCorners, LockTop, LockWalls, LockLeft, LockRight }
    public BorderLock _Lock = BorderLock.LockCorners;
    public BorderLock Lock
    {
        get { return _Lock; }
        set {

            _Lock = value;
        }
    }

    public bool Horizontal = false;


    public List<Vector2> lockedNodesPosition = new List<Vector2>();

    // List<Spring> springs = new List<Spring>();
    //List<Node> nodes = new List<Node>();
    Dictionary<Vector2, GameObject> objectNodes = new Dictionary<Vector2, GameObject>();


    void Awake()
    {
        for (int row = 0; row < amount; row++)
        {
            for (int colom = 0; colom < amount; colom++)
            {
                GameObject node;
                if (!Horizontal)
                    node = (GameObject)Instantiate(nodePref, new Vector3(colom * distanceAppart, row * distanceAppart), gameObject.transform.rotation);
                else
                    node = (GameObject)Instantiate(nodePref, new Vector3(colom * distanceAppart, 0, row * distanceAppart), gameObject.transform.rotation);

                node.GetComponent<Node>().isAnchor = false;
                node.transform.SetParent(gameObject.transform);

                objectNodes.Add(new Vector2(row, colom), node);
            }
        }
        switch (Lock)
        {

            case BorderLock.LockCorners:

                lockedNodesPosition.Add(new Vector2(0, 0));
                lockedNodesPosition.Add(new Vector2(0, amount - 1));
                lockedNodesPosition.Add(new Vector2(amount - 1, 0));
                lockedNodesPosition.Add(new Vector2(amount - 1, amount - 1));
                break;
            case BorderLock.LockTop:

                for (int i = 0; i < amount; i++)
                {
                    lockedNodesPosition.Add(new Vector2(amount - 1, i));
                }


                break;

            case BorderLock.LockWalls:
                
                    for (int i = 0; i < amount; i++)
                    {
                        lockedNodesPosition.Add(new Vector2(amount - 1, i));
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        lockedNodesPosition.Add(new Vector2(0, i));
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        lockedNodesPosition.Add(new Vector2(i, amount - 1));
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        lockedNodesPosition.Add(new Vector2(i, 0));
                    }
                    break;

            case BorderLock.LockLeft:
                for (int i = 0; i < amount; i++)
                {
                    lockedNodesPosition.Add(new Vector2(i, 0));
                }
                break;
            case BorderLock.LockRight:
                for (int i = 0; i < amount; i++)
                {
                    lockedNodesPosition.Add(new Vector2(i, amount-1));
                }
                break;

        }
        foreach (Vector2 node in lockedNodesPosition)
        {
            if (objectNodes.ContainsKey(node))
            {
                //   print(node);
                objectNodes[node].name += "(LOCKED)";
                objectNodes[node].GetComponent<Node>().isAnchor = true;
                objectNodes[node].GetComponent<Renderer>().enabled = true;
            }
        }

        for (int row = 0; row < amount; row++)
        {
            for (int colom = 0; colom < amount; colom++)
            {

                if (objectNodes.ContainsKey(new Vector2(row + 1, colom)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row + 1, colom)], springPref);
                }
                if (objectNodes.ContainsKey(new Vector2(row, colom + 1)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row, colom + 1)], springPref);
                }
                if (objectNodes.ContainsKey(new Vector2(row + 1, colom + 1)))
                {
                    Spring.MakeSpring(objectNodes[new Vector2(row, colom)], objectNodes[new Vector2(row + 1, colom + 1)], springPref);
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
