using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;

    public float alturaSalto = 2f;
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    private InputAction jumpAction;
    private Rigidbody rb;
    private bool estaEnSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        jumpAction = inputActionAsset.FindActionMap("Player").FindAction("Jump");
        jumpAction.Enable();
    }

    void OnDisable()
    {
        if (jumpAction != null)
            jumpAction.Disable();
    }

    void Update()
    {
        estaEnSuelo = Physics.CheckSphere(
            puntoSuelo.position,
            radioSuelo,
            capaSuelo
        );

        if (jumpAction.WasPressedThisFrame() && estaEnSuelo)
        {
            float fuerzaSalto = Mathf.Sqrt(2f * alturaSalto * Mathf.Abs(Physics.gravity.y));

            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.VelocityChange);
        }
    }
}