using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private bool wasThrown = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        // Comprueba si ha sido lanzado y si se ha detenido
        if (wasThrown && rb.velocity.magnitude < 0.1f)
        {
            Destroy(gameObject); // Destruye el objeto inmediatamente sin retardo
        }
    }

    public void MarkAsThrown()
    {
        wasThrown = true; // Marca al enemigo como lanzado
    }
}
