using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bola;
    public GameObject bolaFake;
    public Transform puntoInicio;


    public Transform puntero;

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

    public Text textoInfo;
    public TMPro.TextMeshProUGUI infoTmPro;

    public Material boloEspecialMaterial;

    bool bolosDetenidos = false;

    void Start()
    {
        puntuacionGeneral = 0;
        // bola.GetComponent<Rigidbody>().AddForce(puntero.forward*10,ForceMode.Impulse);
        //bola.GetComponent<Rigidbody>().AddForce(puntero.forward * 10, ForceMode.VelocityChange);
        bola.SetActive(false);
        bolaFake.SetActive(true);
        caidos = 0;
        puntos = 0;

        bolosObjects = GameObject.FindGameObjectsWithTag("bolo");
        //compruebaBolos();
        System.Array.Resize(ref posiciones, bolosObjects.Length);

        for (int i =0; i < bolosObjects.Length; i++ )
        {
            posiciones[i] = bolosObjects[i].transform.position;

        }


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
            }
            else if (action == 2)
            {
                StartCoroutine("mueveBarraAngulo");

            }
            else if (action == 3)
            {

                StartCoroutine("mueveBarraPotencia");
            }
            else if (action == 4)
            {
                dispara();
                StartCoroutine("comprobarVelocidadBolos");
            }
            else if(bolosDetenidos)
            {
                resetBolos();
                action = 0;
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
        
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            bolosObjects[i].transform.position = posiciones[i];
            bolosObjects[i].transform.eulerAngles = Vector3.zero;

            //bolosObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            bolosObjects[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            bolosObjects[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
        Debug.Log("Resetea bolos");

        bola.SetActive(false);
        bolaFake.SetActive(true);
        puntero.transform.position = puntoInicio.position;
        puntero.transform.rotation = puntoInicio.rotation;
    }

    void escogeRojos()
    {
        int contadorEspeciales = 0;
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            if (bolosObjects.Length / 2 <= Random.Range(0, bolosObjects.Length))
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
        Invoke("compruebaBolos", 10f);
    }


    public void compruebaBolos()
    {

        Debug.Log("Comprobando bolos");
        caidos = 0;
        for (int i = 0; i < bolosObjects.Length; i++)
        {
            if (bolosObjects[i].transform.eulerAngles.x > 10f || bolosObjects[i].transform.eulerAngles.z > 10f)
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
    IEnumerator comprobarVelocidadBolos()
    {
        bolosDetenidos = false;
        int numBolosDetenidos = 0;               
        while (true)
        {
            for (int i = 0; i < bolosObjects.Length; i++)
            {
                if (Mathf.Abs(bolosObjects[i].GetComponent<Rigidbody>().angularVelocity.y) < 0.0001f)
                {
                    numBolosDetenidos++;
                    bolosDetenidos = true;
                    
                }
                
               // bolosObjects[i].transform.eulerAngles = Vector3.zero;
                //bolosObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //bolosObjects[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                //bolosObjects[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }
            if (numBolosDetenidos == bolosObjects.Length)
            {
                break;
            }
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
