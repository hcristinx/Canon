using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DerrotaCtrl : MonoBehaviour
{
    public GameObject panelDerrota; // Panel rojo que dice que perdiste

    private bool perdio = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (panelDerrota != null)
        {
            panelDerrota.SetActive(false);
        }
    }

    void Update()
    {
        // Cuando ya perdiste, con R se reinicia el nivel
        if (perdio && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si un enemigo toca al jugador, pierde
        if (!perdio && collision.gameObject.CompareTag("Enemy"))
        {
            Perder();
        }
    }

    void Perder()
    {
        perdio = true;

        if (panelDerrota != null)
        {
            panelDerrota.SetActive(true);
        }

        // Detener el juego y que ya no se pueda mover ni disparar
        Time.timeScale = 0f;

        GetComponent<MovementCtrl>().enabled = false;

        foreach (CanonCtrl canon in FindObjectsByType<CanonCtrl>(FindObjectsSortMode.None))
        {
            canon.enabled = false;
        }
    }
}
