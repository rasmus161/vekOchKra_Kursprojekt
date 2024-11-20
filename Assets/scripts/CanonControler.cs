using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanonControler : MonoBehaviour
{
    private Vector3 mouseStartPosition;
    private Vector3 mouseEndPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;

    // References to UI Text elements
    public Text forceText;
    public Text angleText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseStartPosition.z = 0;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseEndPosition.z = 0;
            ApplyImp();
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            ShowVisualFeedback(mouseStartPosition, currentMousePosition);
        }
    }

    void ApplyImp()
    {
        Vector2 direction = (mouseEndPosition - mouseStartPosition).normalized;
        float distance = Vector2.Distance(mouseStartPosition, mouseEndPosition);
        float forceMagnitude = distance * 10f; // Adjust the multiplier as needed
        rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
    }

    void ShowVisualFeedback(Vector3 start, Vector3 current)
    {
        Vector2 direction = (current - start).normalized;
        float distance = Vector2.Distance(start, current);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update UI Text elements with force magnitude and angle
        forceText.text = $"Force: {distance * 10f} N";
        angleText.text = $"Angle: {angle}°";
    }
}
