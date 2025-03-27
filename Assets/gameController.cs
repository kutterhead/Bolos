using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update
  



    public Action EventoSonido1;
    public Action EventoSonido2;
    public Action EventoSonido3;
    public Action EventoSonido4;
    public Action EventoSonido5;

    public GameObject bola;
    public GameObject bolaFake;
    public Transform puntoInicio;


    public Transform puntero;

    public GameObject prefabBolos;
    public GameObject bolosPadre;
    public GameObject[] bolosObjects;
    bool[] checkBolos = new bool[] { false, false, false, false, false, false, false, false, false, false };
    public bool[] checkBolosEspeciales = new bool[] { false, false, false, false, false, false, false, false, false, false };
    public int caidos = 0;
  
    int puntos = 0;
    int ronda = 0;


    int puntuacionGeneral = 0;

    public Vector3[] posiciones;

    [SerializeField]
    int action = 0;

    public Scrollbar barraPotencia;
    //public Scrollbar barraPosicion;
    //public Scrollbar barraDireccion;
    public Slider sliderPosicion;
    public Slider sliderAngulo;

    //public Text textoInfo;
    public TMPro.TextMeshProUGUI infoTmPro;

    public Material boloEspecialMaterial;

    public bool finalRonda = false;

    void Start()
    {
        puntuacionGeneral = 0;
        // bola.GetComponent<Rigidbody>().AddForce(puntero.forward*10,ForceMode.Impulse);
        //bola.GetComponent<Rigidbody>().AddForce(puntero.forward * 10, ForceMode.VelocityChange);
        bola.SetActive(false);
        bolaFake.SetActive(true);
        caidos = 0;
        puntos = 0;

        //bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        //compruebaBolos();
        /*
        System.Array.Resize(ref posiciones, bolosObjects.Length);

        for (int i =0; i < bolosObjects.Length; i++ )
        {
            posiciones[i] = bolosObjects[i].transform.position;

        }
        */

        escogeRojos();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Invoke("dispara", 1f);
            action++;
            if (action == 1)
            {
                StartCoroutine("mueveBarraPosicion");
                EventoSonido1?.Invoke();

            }
            else if (action == 2)
            {
                StartCoroutine("mueveBarraAngulo");
                EventoSonido2?.Invoke();

            }
            else if (action == 3)
            {

                StartCoroutine("mueveBarraPotencia");
                EventoSonido3?.Invoke();
            }
            else if (action == 4)
            {
                dispara();
                StartCoroutine(comprobarVelocidadBola());
                EventoSonido4?.Invoke();
            }
            else if(finalRonda)
            {
                compruebaBolos();
                resetBolos();
                action = 0;
                EventoSonido5?.Invoke();
            }

            /*if (action > 3)
            {
                action = 0;
            }
           */
        }
    }
    public void resetBolos()
    {
        /* 
         for (int i = 0; i < bolosObjects.Length; i++)
         {
             bolosObjects[i].transform.position = posiciones[i];
             bolosObjects[i].transform.eulerAngles = Vector3.zero;

             //bolosObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
             bolosObjects[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
             bolosObjects[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

         }
        */

        Destroy(bolosPadre);
       
        bolosPadre = Instantiate(prefabBolos);
        
        Invoke("escogeRojos",0.2f);//se invoca con retardo para evitar capturar 
        //public GameObject prefabBolos;

        Debug.Log("Resetea bolos");

        bola.SetActive(false);
        bolaFake.SetActive(true);
        puntero.transform.position = puntoInicio.position;
        puntero.transform.rotation = puntoInicio.rotation;
    }

    void escogeRojos()
    {

        System.Array.Resize(ref bolosObjects,0);

        //return;
        
        bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        System.Array.Resize(ref bolosObjects, 10);


        int contadorEspeciales = 0;
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            if (bolosObjects.Length / 2 <= UnityEngine.Random.Range(0, bolosObjects.Length))
            {
                contadorEspeciales++;
                if (contadorEspeciales > 5)
                {
                    break;
                }


                checkBolosEspeciales[i] = true;
                bolosObjects[i].GetComponentInChildren<MeshRenderer>().material = boloEspecialMaterial;
            }

        }
    }


    public void dispara()
    {
        //Debug.Break();
        bola.SetActive(true);
        bolaFake.SetActive(false);
        bola.transform.position = puntero.position + new Vector3(sliderPosicion.value, 0, 0);
        bola.GetComponent<Rigidbody>().linearVelocity = puntero.forward * 30 * barraPotencia.size;


        //corrutina para comprobacion
        //Invoke("compruebaBolos", 10f);

        StartCoroutine(comprobarVelocidadBola());
    }
    

    public void compruebaBolos()
    {
        System.Array.Resize(ref bolosObjects,0);
        bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        Debug.Log("Comprobando bolos");
        caidos = 0;
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            if (Mathf.Abs(bolosObjects[i].transform.eulerAngles.x) > 10f || Mathf.Abs(bolosObjects[i].transform.eulerAngles.z) > 10f)
            {
                checkBolos[i] = true;//si ha ca�do
                caidos++;

                if (checkBolosEspeciales[i] == false)
                {

                    puntos += 5;
                    Debug.Log("Has derribado bolo especial");
                }
                else
                {
                    puntos++;

                }

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
            Debug.Log("Has hecho pleno!!!");
            puntos += 10;
        }
        else
        {




        }
        Debug.Log("Han caído " + caidos + " bolos.");
        int rest = bolosObjects.Length - caidos;
        Debug.Log("En pie " + (rest) + " bolos.");
        infoTmPro.text = "Fallen: " + caidos + ".";
        infoTmPro.text += "\nRest: " + rest + ".";



        puntuacionGeneral += puntos;
        infoTmPro.text += "\nTotal: " + puntuacionGeneral + ".";
        ronda++;
        if (ronda > 3)
        {

            Debug.Log("gameOver");
        }

        //Invoke("resetBolos",1f);
    }


    #region corrutinas



    IEnumerator comprobarVelocidadBola()
    {
        finalRonda = false;
        //int numfinalRonda = 0;


        yield return new WaitForSeconds(1f);

        float wattchDogTimer = 20f;
        while (!finalRonda)
        {
           
            //Debug.Log("velocidad bola: " + bola.GetComponent<Rigidbody>().linearVelocity.magnitude);
            if (bola.GetComponent<Rigidbody>().linearVelocity.magnitude < 0.1f || wattchDogTimer<=0)
            {             
                finalRonda = true;
                break;
            }



            wattchDogTimer -= Time.deltaTime;
            yield return null;

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

            if (action!=3)
            {
                //ispara();
                //Invoke("compruebaBolos", 5f);
                break;
            }

            yield return null;
        }
    }
    IEnumerator mueveBarraPosicion()
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
                sliderPosicion.value = (tiempo * 2) - 1;

            }
            else
            {
                sliderPosicion.value = 2 - ((tiempo * 2) -1);
                if (tiempo >= 2)
                {

                    tiempo = 0;
                }

            }

            if (action != 1)
            {
                //dispara();
                //Invoke("compruebaBolos", 5f);
                break;
            }

            puntero.position = new Vector3(sliderPosicion.value, puntero.position.y, puntero.position.z);
            //bola.transform.position = puntero.position + new Vector3(sliderPosicion.value, 0, 0);
            //puntero.position = bola.transform.position;

            yield return null;
        }
    }
    IEnumerator mueveBarraAngulo()
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
                sliderAngulo.value = (tiempo * 2) - 1;

            }
            else
            {
                sliderAngulo.value = 2 - ((tiempo * 2) - 1);
                if (tiempo >= 2)
                {

                    tiempo = 0;
                }

            }

            if (action != 2)
            {
              
                
                break;
            }


            puntero.eulerAngles = new Vector3(0, 10 * sliderAngulo.value, 0);
            yield return null;
        }
    }
    #endregion
}
