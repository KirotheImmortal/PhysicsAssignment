using UnityEngine;
using System.Collections.Generic;

public class SpringHelper : MonoBehaviour {

    List<Spring> springs = new List<Spring>();
	// Use this for initialization
	void Start () {
        springs.AddRange(FindObjectsOfType<Spring>());
	
	}

    // Update is called once per frame
    void Update()
    {
        foreach (Spring s in springs)
        {
            s.DampingFactor = b;
            s.SpringConstant = k;
        }
    }
    public float k;
    public float b;
    
}
