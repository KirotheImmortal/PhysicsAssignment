using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    Slider WindX;
    [SerializeField]
    Slider WindY;  
    [SerializeField]
    Slider WindZ;

    [SerializeField]
    Slider SpringConst;

    [SerializeField]
    Slider DampingForce;


    void Update()
    {
        Aero air = FindObjectOfType<Aero>();
        if (air != null)
        {
            if (WindX.gameObject.active == false)
                WindX.gameObject.active = WindY.gameObject.active = WindZ.gameObject.active = true;

            air.WindVelocity.x = WindX.value;
            air.WindVelocity.y = WindY.value;
            air.WindVelocity.z = WindZ.value;
        }

        CreateCloth sp = FindObjectOfType<CreateCloth>();

        if(sp != null)
        {
            sp.b = DampingForce.value;
            sp.k = SpringConst.value;
        }

        else
        {
            WindX.gameObject.active = WindY.gameObject.active = WindZ.gameObject.active = false;
        }

    }

  

}
