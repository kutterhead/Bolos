using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bola;
    public Transform puntero;

    void Start()
    {
        // bola.GetComponent<Rigidbody>().AddForce(puntero.forward*10,ForceMode.Impulse);
        //bola.GetComponent<Rigidbody>().AddForce(puntero.forward * 10, ForceMode.VelocityChange);
        bola.GetComponent<Rigidbody>().velocity = puntero.forward * 10;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
