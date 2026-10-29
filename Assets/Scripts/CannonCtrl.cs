using UnityEngine;
using UnityEngine.InputSystem;

public class CanonCtrl : MonoBehaviour
{

    public AudioSource disparoSound;
    public Transform spawnPoint;
    public GameObject bullet;
    public InputActionAsset inputActionAsset;
    public float fireRate = 0.5f;
    private InputActionMap _inputActionMap;
    private InputAction _fire;
    private float _nextFireTime;

    private ContadorMunicion contador; // Contador de munición de la primera parte
    private bool gastaMunicion; // Solo el arma que tiene el contador gasta las balas

    void Start()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _fire = _inputActionMap.FindAction("Attack");

        contador = GetComponent<ContadorMunicion>();
        gastaMunicion = contador != null;

        if (contador == null)
        {
            contador = FindFirstObjectByType<ContadorMunicion>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_fire.triggered && Time.time >= _nextFireTime)
        {
            // Solo se puede disparar si hay munición
            if (contador != null && contador.municionActual <= 0)
            {
                return;
            }

            if (gastaMunicion)
            {
                contador.GastarBala();
            }

            Debug.Log("Fire");

            // Si el arma tiene la línea de PistolLine, la bala sale desde su origen y en su dirección
            PistolLine linea = GetComponent<PistolLine>();
            Transform origen = spawnPoint;
            if (linea != null && linea.lineOrigin != null)
            {
                origen = linea.lineOrigin;
            }

            GameObject nuevaBala = Instantiate(bullet, origen.position, origen.rotation);

            // Que la bala no choque con el arma que la disparó (ni con quien la carga)
            Collider colBala = nuevaBala.GetComponent<Collider>();
            if (colBala != null)
            {
                foreach (Collider c in transform.root.GetComponentsInChildren<Collider>())
                {
                    Physics.IgnoreCollision(colBala, c);
                }
            }
            _nextFireTime = Time.time + fireRate;
            disparoSound.Play();
        }
    }
}
