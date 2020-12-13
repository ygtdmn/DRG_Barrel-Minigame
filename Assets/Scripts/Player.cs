using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody barrel;

    [SerializeField] private float force = 6;
    [SerializeField] private float verticalForce = 1.5f;
    [SerializeField] private float minTorque = 6;
    [SerializeField] private float maxTorque = 10;

    private Camera mainCamera;
    
    private Vector3 barrelStartPos;
    private Quaternion barrelStartRotation;


    private bool reset = false;
    private bool kick = false;

    public bool playMissSound = true;
    
    private void Start()
    {
        mainCamera = Camera.main;
        barrelStartPos = barrel.transform.position;
        barrelStartRotation = barrel.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && barrel.velocity.magnitude <= 0.1f)
        {
            kick = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        if (kick)
        {
            var relativeVector = transform.forward;
            relativeVector.y = verticalForce;
            barrel.AddRelativeForce(relativeVector * force);
            var sign = mainCamera.transform.rotation.x < 0 ? 1 : -1;
            barrel.AddTorque(Vector3.forward * (Random.Range(minTorque, maxTorque) * sign));
            GameManager.Instance.kickCount++;
            Audio.Instance.PlayHitSound();
            kick = false;
        }

        if (reset)
        {
            barrel.transform.position = barrelStartPos;
            barrel.transform.rotation = barrelStartRotation;
            barrel.velocity = Vector3.zero;
            barrel.angularVelocity = Vector3.zero;
            playMissSound = true;
            reset = false;
        }

        barrel.AddForce(Physics.gravity * barrel.mass);
    }

    public void ResetPlayerPosition()
    {
        GetComponentInParent<SC_FPSController>().ResetPosition();
    }

    public void ResetBarrelPosition()
    {
        reset = true;
    }
}