using UnityEngine;
using UnityEngine.UI;
public class Slingshot : MonoBehaviour
{
    // Public variables to be set in the Unity Inspector
    public GameObject projectilePrefab; // Prefab of the projectile to be launched
    public Transform slingshotOrigin; // The origin point of the slingshot
    public float maxDragDistance = 1f; // Maximum distance the projectile can be dragged
    public LineRenderer lineRenderer; // LineRenderer component to visualize the trajectory
    public Text powerText; // UI Text component to display power
    public Text angleText; // UI Text component to display angle

    // Private variables for internal use
    private Vector3 dragStartPosition; // Starting position of the drag
    private GameObject currentProjectile; // The current projectile being dragged
    private bool isDragging = false; // Flag to check if the player is currently dragging

    void Start()
    {
        powerText.text = "Power: 0";
        angleText.text = "Angle: 0";
    }
    void Update()
    {
        // Check for mouse input to start, continue, or release the drag
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(); // Start dragging when the mouse button is pressed
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            ContinueDrag(); // Continue dragging while the mouse button is held down
            UpdateTrajectory(); // Update the trajectory visualization
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseDrag(); // Release the drag when the mouse button is released
            lineRenderer.positionCount = 0; // Clear the trajectory line
        }
    }

    void StartDrag()
    {
        // Convert mouse position to world position and set the drag start position
        dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPosition.z = 0;

        // Instantiate the projectile at the slingshot origin
        currentProjectile = Instantiate(projectilePrefab, slingshotOrigin.position, Quaternion.identity);
        isDragging = true; // Set the dragging flag to true
    }

    void ContinueDrag()
    {
        // Convert current mouse position to world position
        Vector3 daragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        daragStartPosition.z = 0;

        // Calculate the drag vector and clamp its magnitude
        Vector3 dragVector = daragStartPosition - dragStartPosition;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);

        // Update the projectile's position based on the drag vector
        currentProjectile.transform.position = slingshotOrigin.position + dragVector;

        // Calculate power and angle
        float power = dragVector.magnitude;
        float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;

        // Update UI elements
        powerText.text = "Power: " + power.ToString("F2");
        angleText.text = "Angle: " + angle.ToString("F2") + "°";
    }

    void ReleaseDrag()
    {
        // Convert release mouse position to world position
        Vector3 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        releasePosition.z = 0;

        // Calculate the drag vector and clamp its magnitude
        Vector3 dragVector = releasePosition - dragStartPosition;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);

        // Calculate the force to be applied to the projectile
        Vector3 force = -dragVector * 3f; // Adjust the multiplier for desired force

        // Apply the force to the projectile's Rigidbody2D component
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        isDragging = false; // Reset the dragging flag
    }

    void UpdateTrajectory()
    {
        // Convert current mouse position to world position
        Vector3 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        releasePosition.z = 0;

        // Calculate the drag vector and clamp its magnitude
        Vector3 dragVector = releasePosition - dragStartPosition;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);

        // Calculate the force to be applied to the projectile
        Vector3 force = -dragVector * 3f; // Adjust the multiplier for desired force

        // Calculate the trajectory
        int numPoints = 20; // Number of points in the trajectory line
        Vector3[] positions = new Vector3[numPoints]; // Array to store the positions
        Vector3 startingPos = currentProjectile.transform.position; // Starting position of the projectile
        Vector3 velocity = force / currentProjectile.GetComponent<Rigidbody2D>().mass; // Initial velocity of the projectile
        float distance = 0.2f; // distance betwen dots

        // Calculate the positions along the trajectory
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * distance;
            positions[i] = startingPos + velocity * t;
        }

        // Update the LineRenderer with the calculated positions
        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);
    }
}
