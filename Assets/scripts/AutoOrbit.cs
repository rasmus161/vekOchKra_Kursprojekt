using UnityEngine;

public class AutoOrbit2D : MonoBehaviour
{
    public Transform planetCenter; // Referens till planetens mittpunkt
    public float orbitSpeed = 5f;  // Hastighet runt planeten

    void Update()
    {
        if (planetCenter == null) return;

        // Beräkna riktningen till planetens centrum
        Vector2 directionToPlanet = (planetCenter.position - transform.position).normalized;

        // Hitta tangentvektorn
        Vector2 tangentDirection = new Vector2(-directionToPlanet.y, directionToPlanet.x);

        // Flytta objektet i tangentens riktning
        transform.position += (Vector3)(tangentDirection * orbitSpeed * Time.deltaTime);
    }
}
