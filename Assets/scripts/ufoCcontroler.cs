using UnityEngine;

public class UfoController : MonoBehaviour
{
    // Set the speed of the UFO
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the UFO's starting position to (0, 0)
        transform.position = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player horizontal and vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Creates a new vector for movement based on the input
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // For each frame, add the vector's speed to the position vector
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
