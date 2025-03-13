using UnityEngine;

public class boloSoundController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void OnCollisionEnter(Collision collision)
    {

        GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
        GetComponent<AudioSource>().Play();
    }
}
