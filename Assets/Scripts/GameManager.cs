using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int point;
    public int kickCount;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}