using UnityEngine;

public class GravityForce : MonoBehaviour
{
    public float G;
    Rigidbody2D planet1Physics;
    Rigidbody2D Rock2Physics;
    Rigidbody2D Destroyer1Physics;
    GameObject Rock2;
    GameObject Destroyer1;
    
        void Start()
    {
        planet1Physics = GetComponent<Rigidbody2D>();

        Rock2 = GameObject.Find("Rock2");
        Destroyer1 = GameObject.Find("Destroyer 1");

        Rock2Physics = Rock2.GetComponent<Rigidbody2D>();
        Destroyer1Physics = Destroyer1.GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        float r = (Vector2.Distance(transform.position, Rock2.transform.position) );
       
        


        float F = G * ((Rock2Physics.mass * planet1Physics.mass) / (r * r));
        
        if(r < 1)
        {
        return;
        }

        Vector2 Direction = transform.position - Rock2.transform.position;

        Direction.Normalize();

        Vector2 Force = Direction * F;

        Rock2Physics.AddForce(Force);

        
        float rDestroyer1 = (Vector2.Distance(transform.position, Destroyer1.transform.position));

       
        if(rDestroyer1 < 1)
        {
        return;
        }
        
            
            float FDestroyer1 = G * ((Destroyer1Physics.mass * planet1Physics.mass) / (rDestroyer1 * rDestroyer1));
            
            Vector2 directionDestroyer1 = transform.position - Destroyer1.transform.position;
            
            directionDestroyer1.Normalize();
            
            Vector2 forceDestroyer1 = directionDestroyer1 * FDestroyer1;
            
            Destroyer1Physics.AddForce(forceDestroyer1);
        
    }

}
