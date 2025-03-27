using UnityEngine;

public class EvenReciver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public EventEmmiter emisor;
    void Start()
    {
        emisor.eventoUnity.AddListener(funcion1);
        emisor.eventoNormal += funcion2;

        emisor.eventoNormalParam += funcion2Param;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void funcion1()
    {
        Debug.Log("Evento unity recibido");

    }
    public void funcion2()
    {
        
        Debug.Log("Evento normal recibido");

    }
    public void funcion2Param(float param)
    {
        Debug.Log("Evento normal recibido " + param);

    }
}
