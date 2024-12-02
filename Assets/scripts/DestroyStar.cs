using UnityEngine;

public class DestroyStar : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
    {
        
        Destroy(gameObject);
    }
 

}