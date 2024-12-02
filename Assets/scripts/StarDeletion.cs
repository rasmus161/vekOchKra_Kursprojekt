using UnityEngine;

public class StarDeletion : MonoBehaviour
{
    
    void Start()
    {
        if (collision.collider.CompareTag("player"))
        {
            Destroy(collision.gameObject);
        }

    
    void Update()
    {
    }
        
}
