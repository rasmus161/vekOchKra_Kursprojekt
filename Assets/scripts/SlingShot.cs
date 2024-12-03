using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform slingshotOrigin;
    public float maxDragDistance = 1f;
    public LineRenderer lineRenderer;
    private Vector3 dragStartPos;
    private GameObject currentProjectile;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            ContinueDrag();
            UpdateTrajectory();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseDrag();
            lineRenderer.positionCount = 0; // Clear the trajectory line
        }
    }

    void StartDrag()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPos.z = 0;
        currentProjectile = Instantiate(projectilePrefab, slingshotOrigin.position, Quaternion.identity);
        isDragging = true;
    }

    void ContinueDrag()
    {
        Vector3 currentDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentDragPos.z = 0;
        Vector3 dragVector = currentDragPos - dragStartPos;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);
        currentProjectile.transform.position = slingshotOrigin.position + dragVector;
    }

    void ReleaseDrag()
    {
        Vector3 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        releasePos.z = 0;
        Vector3 dragVector = releasePos - dragStartPos;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);
        Vector3 force = -dragVector * 5f; // Adjust the multiplier for desired force
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        isDragging = false;
    }

    void UpdateTrajectory()
    {
        Vector3 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        releasePos.z = 0;
        Vector3 dragVector = releasePos - dragStartPos;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);
        Vector3 force = -dragVector * 3f; // Adjust the multiplier for desired force

        // Calculate the trajectory
        int numPoints = 50;
        Vector3[] positions = new Vector3[numPoints];
        Vector3 startingPos = currentProjectile.transform.position;
        Vector3 velocity = force / currentProjectile.GetComponent<Rigidbody2D>().mass;
        float timeStep = 0.1f;

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            positions[i] = startingPos + velocity * t;
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(positions);
    }
}
