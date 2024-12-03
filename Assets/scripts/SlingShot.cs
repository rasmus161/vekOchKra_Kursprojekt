using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform slingshotOrigin; 
    public float maxDragDistance = 1f; 
    public LineRenderer lineRenderer; 
    public Text powerText; 
    public Text angleText; 

    private Vector3 dragStartPosition; 
    private GameObject currentProjectile; 
    private bool isDragging = false; 

    void Start()
    {
        // Set initial text values
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
            UpdateTrajectory(); // Update the trajectory line while dragging
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
        dragStartPosition.z = 0; // Ensure the z-coordinate is 0 for 2D

        // Instantiate the projectile at the slingshot origin
        currentProjectile = Instantiate(projectilePrefab, slingshotOrigin.position, Quaternion.identity);
        isDragging = true; 
    }

    void ContinueDrag()
    {
        // Calculate the drag vector
        Vector3 dragVector = CalculateDragVector();

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
        // Calculate the force to be applied to the projectile
        Vector3 force = CalculateForce();

        // Apply the force to the projectile's Rigidbody2D component
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        isDragging = false; 
    }

    
void UpdateTrajectory()
{
    // Number of points to simulate in the trajectory
    int numPoints = 25;
    
    float distance = 0.1f;

    // Calculate the initial velocity based on the drag vector
    Vector3 initialVelocity = CalculateForce() / currentProjectile.GetComponent<Rigidbody2D>().mass;

    // Array to store the trajectory points
    Vector3[] trajectoryPoints = new Vector3[numPoints];

    // Initial position of the projectile
    Vector3 currentPosition = slingshotOrigin.position;

    // Simulate the trajectory
    for (int i = 0; i < numPoints; i++)
    {
        // Calculate the position at this point in time
        trajectoryPoints[i] = currentPosition;

        // Update the position based on the velocity and time step
        currentPosition += initialVelocity * distance;
    }

    // Update the LineRenderer with the trajectory points
    lineRenderer.positionCount = numPoints;
    lineRenderer.SetPositions(trajectoryPoints);
}
    Vector3 CalculateDragVector()
    {
        // Convert current mouse position to world position
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        // Calculate the drag vector and clamp its magnitude
        Vector3 dragVector = currentMousePosition - dragStartPosition;
        return Vector3.ClampMagnitude(dragVector, maxDragDistance);
    }

    Vector3 CalculateForce()
    {
        // Calculate the drag vector
        Vector3 dragVector = CalculateDragVector();

        // Calculate the force to be applied to the projectile
        return -dragVector * 3f;
    }
}
