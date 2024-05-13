using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float speed = 5.0f;          // Velocidad de movimiento normal
    public float dashSpeed = 20.0f;     // Velocidad de dash
    public float dashDuration = 0.2f;   // Duración del dash en segundos
    public float rotationSpeed = 360f;  // Velocidad de rotación en grados por segundo
    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Manejar el input para dash
        if (Input.GetMouseButtonDown(0) && !isDashing) // Click izquierdo del mouse
        {
            StartCoroutine(PerformDash(movement));
        }

        if (!isDashing && movement.magnitude > 0.1f)
        {
            // Movimiento normal
            Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator PerformDash(Vector3 direction)
    {
        float startTime = Time.time;
        isDashing = true;

        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        isDashing = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destruye el objeto enemigo
        }
    }

}
