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
        
        if (Force.magnitude < StaticFriction * physics.mass * 9.82f && physics.velocity.magnitude < 0.1f)
        {
    
            FrictionForce = -Force.normalized * StaticFriction * physics.mass * 9.82f;

    
            physics.velocity = Vector2.zero;
        }
        else
        {
            
            FrictionForce = -physics.velocity.normalized * KineticFriction * physics.mass * 9.82f;
        }

        
        FrictionForce = Vector2.Dot(FrictionForce, tangentDirection.normalized) * tangentDirection;

        
        Vector2 ResultingForce = FrictionForce + Force;

        
        physics.AddForce(ResultingForce);
    }
    }

