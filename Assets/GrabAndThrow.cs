using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    public Transform grabDetect;
    public Transform holdPoint;
    public float rayDist = 2.0f;
    private GameObject grabbedObject;
    public float throwForce = 20.0f;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(grabDetect.position, transform.forward, out hit, rayDist))
        {
            if (hit.collider.gameObject.CompareTag("Enemy") && Input.GetMouseButtonDown(1))  // Click derecho para agarrar
            {
                if (grabbedObject == null)
                {
                    grabbedObject = hit.collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.GetComponent<Collider>().enabled = false;
                    grabbedObject.transform.position = holdPoint.position;
                    grabbedObject.transform.parent = holdPoint;
                }
            }
        }

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = holdPoint.position;
        }

        // Modificado para usar la tecla Espacio en lugar del click izquierdo del mouse
        if (Input.GetKeyDown(KeyCode.Space) && grabbedObject != null)  // Tecla Espacio para lanzar
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Collider>().enabled = true;  // Reactiva el collider
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;

            Enemy enemyComponent = grabbedObject.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.MarkAsThrown(); // Marca como lanzado si es un enemigo
            }
            else
            {
                Debug.LogWarning("Lanzado objeto no enemigo: " + grabbedObject.name);
            }

            Debug.Log("Lanzado: " + grabbedObject.name);
            grabbedObject = null;
        }
    }
}
