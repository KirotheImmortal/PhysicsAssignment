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
            RestLength = Vector3.Magnitude(goA.transform.position - goB.transform.position);
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
            RestLength = Vector3.Magnitude(goA.transform.position - goB.transform.position);

        Debug.DrawLine(goA.transform.position, goB.transform.position);

        Vector3 middle;
        
        middle.x = (goA.transform.position.x + goB.transform.position.x)/2;
        middle.y = (goA.transform.position.y + goB.transform.position.y)/2;
        middle.z = (goA.transform.position.z + goB.transform.position.z)/2;

        gameObject.transform.position = middle;

        CalForce();



        CalNewAcceleration(A);
        CalNewAcceleration(B);

        CalNewPosition(A);
        CalNewPosition(B);
    }
    
    public void CalForce()
    {
        //Computes the reletive distance from the two nodes
        Vector3 vectorDistance = goB.transform.position - goA.transform.position;
        //gets a 1D Distance of the two nodes
        float floatDistance = Vector3.Magnitude(vectorDistance);
        Vector3 e = vectorDistance / floatDistance;
        float Fs = 0;
        float Fd = 0;
        Vector3 Fg = new Vector3(0,-9.8f,0);

        Vector3 Ftotal;

        float aVel = Vector3.Magnitude(MultiplyVectors(e, A.vel));
        float bVel = Vector3.Magnitude(MultiplyVectors(e, B.vel));

        //copy paste algo
        //Fg = new vector3(0,-9.8,0)
        //Fs = -k(distance1 - distance2)
        //Fd = -b(velocity1 - velocity2)
        //Ftotal = Fs + Fd + Fg
        // print(aVel + "   " + bVel);
        //Computes the forces
        Fs = -SpringConstant * (RestLength - floatDistance);
        Fd = -DampingFactor * (aVel - bVel);


        ///Total's up the forces
        Ftotal = Fg + (Fs * e) + (Fd * e);        

        ///Gives the node's their new force
        A.frc = Ftotal;
        B.frc = -A.frc;



    }

    Vector3 MultiplyVectors(Vector3 a , Vector3 b)
    {
        return new Vector3((a.x * b.x), (a.y * b.y), (a.z * b.z));
    }


    static public Vector3 CalNewAcceleration(Node a)
    {
        return a.acl = (1f / a.mass) * a.frc;
    }
    static public Vector3 CalNewVelocity(Node a)
    {
        return a.vel += a.acl * Time.deltaTime;
    }
    static public Vector3 CalNewPosition(Node a)
    {
        if (!a.GetComponent<Node>().isAnchor)
            return a.transform.position += CalNewVelocity(a) * Time.deltaTime;

        return a.transform.position;
    }
    //static public Vector3 CalUniformGravity(Node a)
    //{
    //    return Mathf.Abs(a.mass) * new Vector3(0f, -9.8f, 0f) * (Time.deltaTime * Time.deltaTime);
   // }



    static public void MakeSpring(GameObject A, GameObject B, GameObject springPrefab)
    {
        GameObject spring = new GameObject("Spring: " + A.name + "->" + B.name);
        spring.AddComponent<Spring>();
        spring.GetComponent<Spring>().goA = A;
        spring.GetComponent<Spring>().goB = B;
    }
    static public void MakeSpring(GameObject A, GameObject B, GameObject springPrefab, float springConst, float dampFactor)
    {
        GameObject spring = new GameObject("Spring: " + A.name + "->" + B.name);
        spring.AddComponent<Spring>();
        spring.GetComponent<Spring>().goA = A;
        spring.GetComponent<Spring>().goB = B;
        spring.GetComponent<Spring>().SpringConstant = springConst;
        spring.GetComponent<Spring>().DampingFactor = dampFactor;
    }

}
