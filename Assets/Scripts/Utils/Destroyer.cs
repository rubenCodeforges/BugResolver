using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float delay = 3f;
    
    void Start()
    {
        Destroy(gameObject, delay);
    }
    
}
