using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
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

    void Awake()
    {
        if (goA != null && goA != null && A == null && B == null)
        {
            A = goA.GetComponent<Node>();
            B = goB.GetComponent<Node>();
            RestLength = Vector3.Distance(goA.transform.position, goB.transform.position);
        }
       

    }

    void Update()
    {
        if (goA != null && goA != null && A == null && B == null)
        {
            A = goA.GetComponent<Node>();
            B = goB.GetComponent<Node>();
        }

        if( RestLength== 0)
            RestLength = Vector3.Distance(goA.transform.position, goB.transform.position);

        Debug.DrawLine(goA.transform.position, goB.transform.position);

        Vector3 middle;
        
        middle.x = (goA.transform.position.x + goB.transform.position.x)/2;
        middle.y = (goA.transform.position.y + goB.transform.position.y)/2;
        middle.z = (goA.transform.position.z + goB.transform.position.z)/2;

        gameObject.transform.position = middle;

        CalForce();
    }
    
    public void CalForce()
    {
        //Computes the reletive distance from the two nodes
        Vector3 vectorDistance = goB.transform.position - goA.transform.position;
        //gets a 1D Distance of the two nodes
        float floatDistance = Vector3.Magnitude(vectorDistance);
        Vector3 e = Vector3.Normalize(vectorDistance);
        float sForce;

        Vector3 aForce, bForce;

        float aVel =  Vector3.Magnitude(MultiplyVectors(e,A.vel));
        float bVel = Vector3.Magnitude(MultiplyVectors(e, B.vel));


        sForce = -SpringConstant * (RestLength - floatDistance) - DampingFactor * (aVel - bVel);

        aForce = sForce * e;
        bForce = -1 * aForce;

        A.frc = aForce + Node.CalUniformGravity(A);
        B.frc = bForce + Node.CalUniformGravity(B);

    }

    Vector3 MultiplyVectors(Vector3 a , Vector3 b)
    {
        return new Vector3((a.x * b.x), (a.y * b.y), (a.z * b.z));
    }


    static public void MakeSpring(GameObject A, GameObject B, GameObject springPrefab)
    {
        GameObject spring = Instantiate(springPrefab);
        spring.GetComponent<Spring>().goA = A;
        spring.GetComponent<Spring>().goB = B;
    }

}
