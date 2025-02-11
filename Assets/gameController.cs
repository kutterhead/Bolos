using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bola;
    public Transform puntero;


    public GameObject[] bolosObjects;

    bool[] checkBolos = new bool[] { false, false, false, false, false, false, false, false, false, false };


    public int caidos = 0;

    void Start()
    {
        // bola.GetComponent<Rigidbody>().AddForce(puntero.forward*10,ForceMode.Impulse);
        //bola.GetComponent<Rigidbody>().AddForce(puntero.forward * 10, ForceMode.VelocityChange);

        caidos = 0;
        Invoke("dispara", 1f);

        bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        //compruebaBolos();
        Invoke("compruebaBolos", 5f);
    }
    public void dispara()
    {
        bola.GetComponent<Rigidbody>().linearVelocity = puntero.forward * 10;

    }


    public void compruebaBolos(){

        Debug.Log("Comprobando bolos");
        caidos = 0;
        for (int i = 0; i < bolosObjects.Length;i++)
        {
            if (bolosObjects[i].transform.eulerAngles.x>10f || bolosObjects[i].transform.eulerAngles.z > 10f)
            {
                checkBolos[i] = true;//si ha ca�do
                caidos++;
            }
            else
            {
                checkBolos[i] = false;//no ha ca�do
            }
            //Debug.Log("bolo ca�do " + i + ": " + checkBolos[i]);
        }
        Debug.Log("Han ca�do " + caidos + " bolos.");
        Debug.Log("En pie " + (bolosObjects.Length - caidos) + " bolos.");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
