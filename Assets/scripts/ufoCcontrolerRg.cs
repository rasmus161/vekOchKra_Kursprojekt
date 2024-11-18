using UnityEngine;

public class UfoController : MonoBehaviour
{
   
    public float speed = 4f; // Set the speed of the UFO
    public float rotationSpeed = 150f; // set  rotation
    public float mass = 20; // Predetermined mass

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the UFO
        rb = GetComponent<Rigidbody2D>();

        // Set the mass of the Rigidbody2D
        rb.mass = mass;

        // Set the UFO's starting position to (0, 0) + zindex
        transform.position = new Vector3(0.0f, 0.0f, -2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player rotation
        float rotationInput = Input.GetAxis("Horizontal");
        float thrustInput = Input.GetAxis("Vertical");

        // Rotate the UFO
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime;

        // Apply force to the Rigidbody2D component for movement
        Vector2 thrust = transform.up * thrustInput * speed;
        rb.AddForce(thrust);
    }
}
