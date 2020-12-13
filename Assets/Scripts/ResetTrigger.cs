using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player;

        if (other.CompareTag("Player"))
        {
           (player = other.GetComponentInChildren<Player>()).ResetPlayerPosition();
        }
        else
        {
            (player = FindObjectOfType<Player>()).ResetBarrelPosition();
        }

        if (player.playMissSound)
        {
            Audio.Instance.PlayMissSound();
        }
        
    }
}