using JetBrains.Annotations;
using UnityEngine;

public class Friction : MonoBehaviour
{
    public float StaticFriction;
    public float KineticFriction;
    Rigidbody2D physics;
    Vector2 FrictionForce;

    public GameObject planet1;
    public Vector2 planetPosition;

    void Start()
    {
       physics = GetComponent<Rigidbody2D>();
       

    }

    [System.Obsolete]
    public void ApplyForce(Vector2 Force)
    {
        float FMax = StaticFriction * physics.mass * 9.82f;
        Vector2 directionToPlanet = (planetPosition - (Vector2)transform.position).normalized;
        Vector2 tangentDirection = new Vector2(-directionToPlanet.y, directionToPlanet.x);
        
        if (Force.magnitude < FMax && physics.velocity.magnitude < 0.1f)
        {
            // Ställ in friktionskraften så att den motverkar den applicerade kraften
            FrictionForce = -Force.normalized * FMax;

            // Stoppa objektets rörelse helt
            physics.velocity = Vector2.zero;
        }
        else
        {
            // Om objektet rör sig
            // Beräkna friktionskraften baserat på kinetisk friktion
            FrictionForce = -physics.velocity.normalized * KineticFriction * physics.mass * 9.82f;
        }

        // Friktionen ska appliceras i tangentvektorns riktning
        FrictionForce = Vector2.Dot(FrictionForce, tangentDirection.normalized) * tangentDirection;

        // Beräkna den resulterande kraften genom att lägga ihop friktionskraften och den applicerade kraften
        Vector2 ResultingForce = FrictionForce + Force;

        // Applicera den resulterande kraften på objektet
        physics.AddForce(ResultingForce);
    }
    }

