using UnityEngine;

public class GravityForce : MonoBehaviour
{
    public float G;
    Rigidbody2D planet1Physics;
    Rigidbody2D Rock2Physics;
    GameObject Rock2;
    
        void Start()
    {
        planet1Physics = GetComponent<Rigidbody2D>();

        Rock2 = GameObject.Find("Rock2");

        Rock2Physics = Rock2.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float r = Vector2.Distance(transform.position, Rock2.transform.position);

        float F = G * ((Rock2Physics.mass * planet1Physics.mass) / (r *r));

        Vector2 Direction = transform.position - Rock2.transform.position;

        Direction.Normalize();

        Vector2 Force = Direction * F;

        Rock2Physics.AddForce(Force);
    }

}
