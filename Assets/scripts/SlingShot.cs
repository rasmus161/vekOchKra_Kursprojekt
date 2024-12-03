using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform slingshotOrigin;
    public float maxDragDistance = 1f;
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
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseDrag();
        }
    }

    void StartDrag()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPos.z = -2;
        currentProjectile = Instantiate(projectilePrefab, slingshotOrigin.position, Quaternion.identity);
        isDragging = true;
    }

    void ContinueDrag()
    {
        Vector3 currentDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentDragPos.z = -2;
        Vector3 dragVector = currentDragPos - dragStartPos;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);
        currentProjectile.transform.position = slingshotOrigin.position + dragVector;
    }

    void ReleaseDrag()
    {
        Vector3 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        releasePos.z = -2;
        Vector3 dragVector = releasePos - dragStartPos;
        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);
        Vector3 force = -dragVector * 3f; // Adjust the multiplier for desired force
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        isDragging = false;
    }
}
