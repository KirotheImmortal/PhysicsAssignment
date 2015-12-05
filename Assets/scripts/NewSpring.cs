using UnityEngine;
using System.Collections;

public class NewSpring : MonoBehaviour
{
    public float SpringConstant = 10; //k.s
    public float DampingFactor = 10;  //k.d
    public float RestLength = 0;
    [SerializeField]
    GameObject _goA;
    [SerializeField]
    GameObject _goB;

    public GameObject goA
    {
        get { return _goA; }
        set
        {
            _goA = value;
            A = value.GetComponent<Node>();
        }
    }
    public GameObject goB
    {
        get { return _goB; }
        set
        {
            _goB = value;
            B = value.GetComponent<Node>();
        }
    }
    Node A, B;



    void CalNewAccel(Node a)
    {
      //  a.acl = (1f / a.mass) * a.frc;
    }
    Vector3 CalNewVel(Node a)
    {
        return a.vel += a.acl * Time.deltaTime;
    }
    void CalNewPos(Node a)
    {
        a.gameObject.transform.position += CalNewVel(a) * Time.deltaTime;
    }
    void CalForce()
    {
        Vector3 e = goB.transform.position - goA.transform.position;
        float l = Vector3.Magnitude(e);
        Vector3 enorm = e / l;
        

        float aVel = Vector3.Magnitude(MultiplyVectors(enorm, A.vel));
        float bVel = Vector3.Magnitude(MultiplyVectors(enorm, B.vel));

        float fs, fd = 0;
        Vector3 fg = new Vector3(0, -9.8f, 0);
        Vector3 Ftotal = Vector3.zero;

        fs = -SpringConstant * (RestLength - l);
        fd = -DampingFactor * (aVel - aVel);


    }
    Vector3 MultiplyVectors(Vector3 a, Vector3 b)
    {
        return new Vector3((a.x * b.x), (a.y * b.y), (a.z * b.z));
    }

}
