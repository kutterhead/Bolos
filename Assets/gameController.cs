using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    [SerializeField]
    int action = 0;

    public Scrollbar barraPotencia;
    public Scrollbar barraDireccion;
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
        bola.GetComponent<Rigidbody>().linearVelocity = puntero.forward * 30 * barraPotencia.size;

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
            action++;
            if (action==1)
            {
                StartCoroutine("mueveBarraPotencia");
            }

            if (action > 2)
            {

                action = 0;
            }
           

        }
    }

    IEnumerator mueveBarraPotencia()
    {
        float tiempo = 0;
        //float tiempo2 = 0;
        while (true)
        {


            /*       
               tiempo += Time.deltaTime;

               barraPotencia.value = (Mathf.Sin(tiempo * 2)+1)/2;
            */

            tiempo += Time.deltaTime;

            if (tiempo < 1)
            {
                barraPotencia.size = tiempo;

            }
            else
            {
                barraPotencia.size = 2 - tiempo;
                if (tiempo >= 2)
                {

                    tiempo = 0;
                }

            }

            if (action==2)
            {
                dispara();
                Invoke("compruebaBolos", 5f);
                break;
            }

            yield return null;
        }
    }


}
