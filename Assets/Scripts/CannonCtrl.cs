using UnityEngine;
using UnityEngine.InputSystem;

public class CanonCtrl : MonoBehaviour
{
    public int capacity = 10;    
    public int currentBullets = 0;
    public AudioSource disparoSound;
    public Transform spawnPoint;
    public GameObject bullet;
    public InputActionAsset inputActionAsset;
    public float fireRate = 0.5f;
    private InputActionMap _inputActionMap;
    private InputAction _fire;
    private InputAction _reload;
    private float _nextFireTime;
    void Start()
    {
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _fire = _inputActionMap.FindAction("Jump");
        _reload = _inputActionMap.FindAction("Crouch");

        currentBullets = capacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fire.triggered && currentBullets > 0 && Time.time >= _nextFireTime)
        {
            Debug.Log("Fire");
            currentBullets--;
            Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
            _nextFireTime = Time.time + fireRate;
            disparoSound.Play();
        }
        if(_reload.triggered)
        {
            Debug.Log("Reaload");
            currentBullets = capacity;
        }
    }
}
