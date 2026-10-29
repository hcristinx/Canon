using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ContadorMunicion : MonoBehaviour
{
    public int capacidadCargador = 20;
    public int municionActual = 10;
    public int municionReserva = 50;

    public InputActionAsset inputActionAsset;
    private InputActionMap _inputActionMap;
    private InputAction _fire;
    private InputAction _reload;

    public TextMeshProUGUI municionesText;

    void Start()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _fire = _inputActionMap.FindAction("Attack");
        _reload = _inputActionMap.FindAction("Crouch");

        _fire.Enable();
     _reload.Enable();

        ActualizarTexto();
    }

    void Update()
    {
        if (_reload.triggered && municionActual < capacidadCargador && municionReserva > 0)
        {
            int diferencia = capacidadCargador - municionActual;
            int recarga = Mathf.Min(diferencia, municionReserva);

            municionActual += recarga;
            municionReserva -= recarga;

            ActualizarTexto();
        }

        if (municionActual == 0)
        {
            municionesText.color = Color.red;
        }
        else
        {
            municionesText.color = Color.black;
        }
    }

    // La llama el arma al disparar. Regresa false si ya no hay balas.
    public bool GastarBala()
    {
        if (municionActual <= 0)
        {
            return false;
        }

        municionActual--;
        ActualizarTexto();
        return true;
    }

    public void ActualizarTexto()
    {
        municionesText.text = municionActual + "/" + capacidadCargador;
    }
}