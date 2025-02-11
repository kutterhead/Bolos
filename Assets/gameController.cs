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

    
    int puntos = 0;
    int ronda = 0;


    public Vector3[] posiciones;

    void Start()
    {
        // bola.GetComponent<Rigidbody>().AddForce(puntero.forward*10,ForceMode.Impulse);
        //bola.GetComponent<Rigidbody>().AddForce(puntero.forward * 10, ForceMode.VelocityChange);

        caidos = 0;
        puntos = 0;

        bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        //compruebaBolos();
        System.Array.Resize(ref posiciones, bolosObjects.Length);

        for (int i =0; i < bolosObjects.Length; i++ )
        {
            posiciones[i] = bolosObjects[i].transform.position;

        }


    }

    public void resetBolos()
    {
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            bolosObjects[i].transform.position = posiciones[i];
            bolosObjects[i].transform.eulerAngles = Vector3.zero;

            //bolosObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            bolosObjects[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            bolosObjects[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
        Debug.Log("Resetea bolos");
    }




    public void dispara()
    {

        bola.transform.position = puntero.position;
        bola.GetComponent<Rigidbody>().linearVelocity = puntero.forward * 20;

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
                puntos++;
            }
            else
            {
                checkBolos[i] = false;//no ha ca�do
            }
            //Debug.Log("bolo ca�do " + i + ": " + checkBolos[i]);
        }

        if (caidos == bolosObjects.Length)
        {
            //premio pleno!
            Debug.Log("Has hecho pleno!!! " + caidos + " bolos.");
            puntos += 10;
        }
        else
        {

        Debug.Log("Han caído " + caidos + " bolos.");

        Debug.Log("En pie " + (bolosObjects.Length - caidos) + " bolos.");
        }

        Invoke("resetBolos",1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Invoke("dispara", 1f);

            dispara();
            Invoke("compruebaBolos", 5f);

        }
    }
}
