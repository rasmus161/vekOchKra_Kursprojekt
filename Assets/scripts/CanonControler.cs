using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanonControler : MonoBehaviour
{
    public Text forceText;
    public Text angleText;
    private Vector2 start;
    private Vector2 current;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // UpdateUI();
    }

    void OnMouseUp()
    {
        current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ApplyImpulse();
        //  ClearUI();
    }
    /*
        void UpdateUI()
        {
            Vector2 direction = (current - start).normalized;
            float distance = Vector2.Distance(start, current);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (forceText != null)
            {
                forceText.text = $"Force: {distance * 10f} N";
            }

            if (angleText != null)
            {
                angleText.text = $"Angle: {angle}°";
            }
        }
    */

    

    void ApplyImpulse()
    {
        Vector2 direction = (current - start).normalized;
        float distance = Vector2.Distance(start, current);
        rb.AddForce(direction * distance * 10f, ForceMode2D.Impulse);
    }



}