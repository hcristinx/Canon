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
    void Start()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _fire = _inputActionMap.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (_fire.triggered && Time.time >= _nextFireTime)
        {
            Debug.Log("Fire");
            Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            _nextFireTime = Time.time + fireRate;
            disparoSound.Play();
        }
    }
}
