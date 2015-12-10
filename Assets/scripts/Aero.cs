using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aero : MonoBehaviour
{
    public Vector3 WindVelocity;
    public float p;
    public float cdrag;


    static public void CalAero(ref Dictionary<Vector2, GameObject> nodes, float amount)
    {
        Aero aeroObject = FindObjectOfType<Aero>();
        if (aeroObject != null && aeroObject.GetComponent<Aero>().WindVelocity != Vector3.zero)
        {
            for (int row = 0; row < amount; row++)
            {
                for (int colom = 0; colom < amount; colom++)
                {
                    if (nodes.ContainsKey(new Vector2(row + 1, colom)) && nodes.ContainsKey(new Vector2(row + 1, colom + 1)))
                    {
                        GameObject p1 = nodes[new Vector2(row, colom)];
                        GameObject p2 = nodes[new Vector2(row + 1, colom)];
                        GameObject p3 = nodes[new Vector2(row + 1, colom + 1)];
                        if (p1 != null && p2 != null && p3 != null)
                        {
                            Vector3 Vsurface = (p1.GetComponent<Node>().vel + p2.GetComponent<Node>().vel + p3.GetComponent<Node>().vel) / 3;

                            Vector3 V = Vsurface - aeroObject.WindVelocity;

                            Vector3 crossProduct = Vector3.Cross(p2.transform.position - p1.transform.position, p3.transform.position - p1.transform.position);
                            Vector3 N = crossProduct / Vector3.Magnitude(crossProduct);

                            float ao = .5f * Vector3.Magnitude(crossProduct);
                            float a = ao * Vector3.Dot(V, N) / Vector3.Magnitude(V);

                            Vector3 faero = -.5f * (aeroObject.p * (Vector3.Magnitude(V) * Vector3.Magnitude(V)) * aeroObject.cdrag * a) * N;

                            p1.GetComponent<Node>().frc = p2.GetComponent<Node>().airfrc = p3.GetComponent<Node>().frc = faero / 3;
                        }
                    }
                }
            }
            for (int row = 0; row < amount; row++)
            {
                for (int colom = 0; colom < amount; colom++)
                {
                    if (nodes.ContainsKey(new Vector2(row , colom+1)) && nodes.ContainsKey(new Vector2(row + 1, colom + 1)))
                    {
                        GameObject p1 = nodes[new Vector2(row, colom)];
                        GameObject p2 = nodes[new Vector2(row, colom+1)];
                        GameObject p3 = nodes[new Vector2(row + 1, colom + 1)];
                        if (p1 != null && p2 != null && p3 != null)
                        {
                            Vector3 Vsurface = (p1.GetComponent<Node>().vel + p2.GetComponent<Node>().vel + p3.GetComponent<Node>().vel) / 3;

                            Vector3 V = Vsurface - aeroObject.WindVelocity;

                            Vector3 crossProduct = Vector3.Cross(p2.transform.position - p1.transform.position, p3.transform.position - p1.transform.position);
                            Vector3 N = crossProduct / Vector3.Magnitude(crossProduct);

                            float ao = .5f * Vector3.Magnitude(crossProduct);
                            float a = ao * Vector3.Dot(V, N) / Vector3.Magnitude(V);



                            Vector3 faero = -.5f * (aeroObject.p * (Vector3.Magnitude(V) * Vector3.Magnitude(V)) * aeroObject.cdrag * a) * N;

                            p1.GetComponent<Node>().frc = p2.GetComponent<Node>().airfrc = p3.GetComponent<Node>().frc = faero / 3;
                        }
                    }
                }
            }
        }
    }
}


