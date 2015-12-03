using UnityEngine;
using System.Collections;

public class CreateNode : MonoBehaviour
{
    static public GameObject MakeNode(string name, bool anchor, GameObject pref)
    {
        GameObject newNode = Instantiate(pref);
        newNode.name = name;

        if (!pref.GetComponent<Node>())
            newNode.AddComponent<Node>();

        newNode.GetComponent<Node>().isAnchor = anchor;

        return newNode;
    }
    static public GameObject MakeNode(string name, bool anchor, GameObject pref, Vector3 location)
    {
        GameObject newNode = (GameObject)Instantiate(pref, location, new Quaternion(0, 0, 0, 0));
        newNode.name = name;
        if (!pref.GetComponent<Node>())
            newNode.AddComponent<Node>();

        newNode.GetComponent<Node>().isAnchor = anchor;

        return newNode;
    }
    static public GameObject MakeNode(bool anchor, GameObject pref)
    {
        GameObject newNode = Instantiate(pref);

        if (!pref.GetComponent<Node>())
            newNode.AddComponent<Node>();

        newNode.GetComponent<Node>().isAnchor = anchor;

        return newNode;
    }
    static public GameObject MakeNode(bool anchor, GameObject pref, Vector3 location)
    {
        GameObject newNode = (GameObject)Instantiate(pref, location, new Quaternion(0, 0, 0, 0));
        if (!pref.GetComponent<Node>())
            newNode.AddComponent<Node>();

        newNode.GetComponent<Node>().isAnchor = anchor;

        return newNode;
    }
}
