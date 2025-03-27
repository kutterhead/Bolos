using System;

using UnityEngine;

using UnityEngine.Events;


public class EventEmmiter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent eventoUnity;

    public event Action eventoNormal;
    public event Action<float> eventoNormalParam;

    void Start()
    {
        Debug.Log(eventoUnity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            eventoUnity?.Invoke();
            eventoNormalParam?.Invoke(3f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            eventoNormal?.Invoke();
        }

    }
}
