
using UnityEngine;

public class UfoController : MonoBehaviour
{

    //  set the speed of the ufo
     float speed = 4;
     


    // Update is called once per frame
    void Update()
    {
         // Get input from the player horizontal and vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

// creates a new vector for movement based on the input
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
// For each frame, add the vector's speed to the position vector
        transform.Translate(movement * speed * Time.deltaTime);
    }


    // Start is called before the first frame update
    void Start () {
	
	}
	
	
}