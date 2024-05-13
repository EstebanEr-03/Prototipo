using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject canvasToActivate; // Referencia al canvas que se activará
    public float life = 9; // Tiempo de vida de la bala antes de autodestruirse

    void Awake()
    {
        Destroy(gameObject, life); // Destruye la bala después de un tiempo 'life' si no colisiona
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisiona tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destruye la bala inmediatamente tras colisionar con un enemigo
        }
        // Si no es un enemigo, puedes decidir no hacer nada o manejar otros casos específicos aquí
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasToActivate.SetActive(true); // Activa el canvas especificado
            Destroy(collision.gameObject); // Destruye el enemigo

        }
    }
}
