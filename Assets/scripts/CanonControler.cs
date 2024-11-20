using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanonControler : MonoBehaviour
{
    // Variables to store mouse positions and dragging state
    private Vector3 mouseStartPosition;
    private Vector3 mouseEndPosition;
    private bool isDragging = false;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // References to UI Text elements for displaying force and angle
    public Text forceText;
    public Text angleText;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse input for dragging and releasing
        MouseInput();
    }

    void MouseInput()
    {
        // Check if the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            // Record the start position of the mouse in world coordinates
            mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseStartPosition.z = 0;
            isDragging = true;
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Record the end position of the mouse in world coordinates
            mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseEndPosition.z = 0;
            // Apply the impulse force based on the drag distance and direction
            ApplyImp();
            isDragging = false;
        }

        // If the mouse is being dragged, update the visual feedback
        if (isDragging)
        {
            // Get the current mouse position in world coordinates
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            // Show visual feedback for the force and angle
            ShowVisualFeedback(mouseStartPosition, currentMousePosition);
        }
    }

    void ApplyImp()
    {
        // Calculate the direction and distance of the drag
        Vector2 direction = (mouseEndPosition - mouseStartPosition).normalized;
        float distance = Vector2.Distance(mouseStartPosition, mouseEndPosition);
        // Calculate the force magnitude based on the drag distance
        float forceMagnitude = distance * 10f; // Adjust the multiplier as needed
        // Apply the impulse force to the Rigidbody2D component
        rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
    }

    void ShowVisualFeedback(Vector3 start, Vector3 current)
    {
        // Calculate the direction and distance of the drag
        Vector2 direction = (current - start).normalized;
        float distance = Vector2.Distance(start, current);
        // Calculate the angle of the drag in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update UI Text elements with force magnitude and angle
        if (forceText != null)
        {
            forceText.text = $"Force: {distance * 10f} N";
        }

        if (angleText != null)
        {
            angleText.text = $"Angle: {angle}°";
        }
    }
}
