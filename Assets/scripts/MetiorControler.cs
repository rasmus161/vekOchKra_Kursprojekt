using UnityEngine;

public class MetiorController : MonoBehaviour
{

    // Array av vägpunkter
    public Transform[] waypoints; 

    // Hastighet 
    public float speed = 2f; 

    // startposition
    public Vector2 StartPosition; 
    private int index;

    void Start()
    {
        // Sätt objektets startposition till den anpassade startpositionen
        transform.position = StartPosition;

        ChooseNextWaypoint();
    }

    void Update()
    {
        // Flytta objektet mot den aktuella vägpunktens position
        transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);

        // Kontrollera om objektet har nått den aktuella vägpunktens position
        if (Vector2.Distance(transform.position, waypoints[index].position) < 1)
        {
            ChooseNextWaypoint();
        }
    }

    void ChooseNextWaypoint()
    {
        // Väljer slumpmässigt en vägpunkt som nästa destination
        index = Random.Range(0, waypoints.Length);
    }
}
