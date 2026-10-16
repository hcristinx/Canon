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

        // Buscar el enemigo en el objeto impactado o en sus padres.
        EnemyCtrl enemigo =
    collision.gameObject.GetComponentInParent<EnemyCtrl>();

Debug.Log("Objeto impactado: " + collision.gameObject.name);

Debug.Log("Ruta del objeto: " +
    GetRutaJerarquia(collision.transform));

if (enemigo != null)
{
    Debug.Log("EnemyCtrl encontrado en: " + enemigo.gameObject.name);

    Destroy(enemigo.gameObject);
}
else
{
    Debug.LogWarning(
        "No se encontró EnemyCtrl en la jerarquía del objeto impactado."
    );
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