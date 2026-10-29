using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCtrl : MonoBehaviour
{
    private Rigidbody rb;
    private bool yaImpacto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;

        // Sin gravedad para que siga recta la línea de la pistola
        rb.useGravity = false;

        rb.AddForce(transform.forward * 500f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Evitar procesar más de una colisión.
        if (yaImpacto)
        {
            return;
        }

        yaImpacto = true;

        Debug.Log("La bala chocó con: " + collision.gameObject.name);

        Debug.Log("Ruta del objeto: " +
            GetRutaJerarquia(collision.transform));

        // Si la bala impacta a un objeto con tag Enemy, se elimina
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemigo eliminado: " + collision.gameObject.name);

            Destroy(collision.gameObject);
        }

        // Destruir la bala.
        Destroy(gameObject);
    }
    string GetRutaJerarquia(Transform objeto)
{
    string ruta = objeto.name;

    while (objeto.parent != null)
    {
        objeto = objeto.parent;
        ruta = objeto.name + "/" + ruta;
    }

    return ruta;
}
}