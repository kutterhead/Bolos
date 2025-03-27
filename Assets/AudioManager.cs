using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioClip[] clip1;
    public AudioSource fuente;
    public gameController gc;





    void Start()
    {
        // fuente = gameObject.AddComponent<AudioSource>();


        gc.EventoSonido1 += reproduceSonido1;
        gc.EventoSonido2 += reproduceSonido2;
        gc.EventoSonido3 += reproduceSonido3;
        gc.EventoSonido4 += reproduceSonido4;
        gc.EventoSonido4 += reproduceSonido5;
    }

    // Update is called once per frame
    void reproduceSonido1()
    {
        fuente.clip = clip1[0];
        fuente.Play();
    }
    void reproduceSonido2()
    {
        fuente.clip = clip1[1];
        fuente.Play();
    }
    void reproduceSonido3()
    {
        fuente.clip = clip1[2];
        fuente.Play();
    }
    void reproduceSonido4()
    {
        fuente.clip = clip1[3];
        fuente.Play();
    }
    void reproduceSonido5()
    {
        fuente.clip = clip1[4];
        fuente.Play();
    }
}
