using UnityEngine;

public class MetiorController : MonoBehaviour
{
    // Array av vägpunkter
    public Transform[] Waypoints;

    public float speed = 2.1f;

    // startposition
    public Vector2 StartPosition;

    // vilken waypoint objektet färdas mot
    private int index;

    void Start()
    {
        // Sätt objektets startposition till en slumpmässig position på skärmen
        SetRandomStartPosition();

        // Sätt objektets startposition till den anpassade startpositionen
        transform.position = StartPosition;

        ChooseNextWaypoint();
    }

    void Update()
    {
        // Flytta objektet mot den aktuella vägpunktens position
        transform.position = Vector2.MoveTowards(transform.position, Waypoints[index].position, speed * Time.deltaTime);

        // Kontrollera om objektet har nått den aktuella vägpunktens position
        if (Vector2.Distance(transform.position, Waypoints[index].position) < 1)
        {
            ChooseNextWaypoint();
        }
    }

    // Väljer slumpmässigt en vägpunkt som nästa destination baserat på hur många waypoints det finns i arrayen
    void ChooseNextWaypoint()
    {
        index = Random.Range(0, Waypoints.Length);
    }


    // Sätter en slumpmässig startposition inom skärmens gränser
    void SetRandomStartPosition()
    {  // random  postion 0 till senens dimentioner
        float screenX = Random.Range(0f, Screen.width);
        float screenY = Random.Range(0f, Screen.height);

        // Konvertera skärmens koordinater till världens koordinater, ser till att de hålls inom spelområdet
        Vector2 Wordposition = Camera.main.ScreenToWorldPoint(new Vector2(screenX, screenY));
        StartPosition = Wordposition;
    }
}
