using UnityEngine;

public class DetectorFueraPista : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public gameController controller;

    private void OnTriggerExit(Collider other)
    {

        controller.finalRonda = true;
        Debug.Log("Ha salido de pista: " + other.gameObject.tag);
    }

}
