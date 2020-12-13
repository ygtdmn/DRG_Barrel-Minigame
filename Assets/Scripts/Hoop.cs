using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] private float zRange = 8;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float speed;
    private int direction = 1;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        var localPosition = transform.localPosition;
        
        if (localPosition.z < -zRange + 0.5f)
        {
            direction *= -1;
            if (Random.Range(0, 10) < 1)
                speed = Random.Range(minSpeed, maxSpeed);
            localPosition.z = -zRange + 0.5f;
            transform.localPosition = localPosition;
        }
        else if (localPosition.z > zRange - 0.5f)
        {
            direction *= -1;
            localPosition.z = zRange - 0.5f;
            transform.localPosition = localPosition;
        }
        else
        {
            localPosition.z += Time.deltaTime * speed * direction;
            transform.localPosition = localPosition;
        }
    }
}