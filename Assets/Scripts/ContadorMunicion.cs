using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph;

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
        _fire = _inputActionMap.FindAction("Jump");
        _reload = _inputActionMap.FindAction("Crouch");
    }

 void Update()
    {
        if(_fire.triggered && municionActual > 0)
        {
            municionActual--;
            municionesText.text = municionActual.ToString() + "/20";
        }

        if (_reload.triggered && municionReserva > 0)
        {
            municionReserva = municionReserva - (capacidadCargador-municionActual);
            municionActual = capacidadCargador;
            municionesText.text = municionActual.ToString() + "/20";
            municionesText.color = Color.black;
        }

        if(municionActual == 0)
        {
            municionesText.color = Color.red;
        }
    }

    public void ActualizarTexto()
    {
        
    }
}
