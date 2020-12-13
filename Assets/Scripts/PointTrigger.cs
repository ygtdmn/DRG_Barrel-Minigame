using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            GameManager.Instance.point += 10;
            Audio.Instance.PlaySuccessSound();
            FindObjectOfType<Player>().playMissSound = false;
        }
    }
}