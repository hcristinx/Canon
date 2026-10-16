using UnityEngine;
using UnityEngine.InputSystem;

public class MovementCtrl : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputActionMap _inputActionMap;
    private InputAction _move;
    private InputAction _look;

    public float speed = 5f;
    public float sensibilidad = 2f;

    public Transform camara;

    private float rotacionVertical = 0f;

    void Start()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");

        _move = _inputActionMap.FindAction("Move");
        _look = _inputActionMap.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Leer el movimiento del teclado
        Vector2 movimiento = _move.ReadValue<Vector2>();

        // Convertir el movimiento 2D en una dirección 3D
        Vector3 direccion = new Vector3(
            movimiento.x,
            0f,
            movimiento.y
        );

        // Mover al jugador
        transform.Translate(direccion * speed * Time.deltaTime);

        // Leer el movimiento del mouse
        Vector2 mouse = _look.ReadValue<Vector2>();

        // Girar al jugador horizontalmente
        transform.Rotate(Vector3.up * mouse.x * sensibilidad);

        // Controlar la rotación vertical de la cámara
        rotacionVertical -= mouse.y * sensibilidad;

        rotacionVertical = Mathf.Clamp(
            rotacionVertical,
            -70f,
            70f
        );

        camara.localRotation = Quaternion.Euler(
            rotacionVertical,
            0f,
            0f
        );
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}