using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    // Remember to assign a value to this variable in the Unity editor!
    // I.e. Drag the camera into the slot in the inspector when you select the plane.
    public GameObject cameraObject;

    public Terrain terrain;

    public TMP_Text scoreText;

    int score = 0;

    // These variables will control how the plane moves
    float forwardSpeed = 12f;
    float xRotationSpeed = 90f;
    float yRotationSpeed = 90f;

    // Speed decrease rate and boost amount
    float speedDecreaseRate = 1f;
    float boostAmount = 5f;
    float minimumSpeed = 2f;

    // Store the starting position of the plane
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Save the initial position of the plane at the start of the game
        startingPosition = transform.position;

        // Initialize the score text
        scoreText.text = "Score: " + score;

        // Ensure the forward speed does not drop below a minimum value
        if (forwardSpeed < minimumSpeed)
        {
            forwardSpeed = minimumSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Get directional input (up, down, left, right)
        float hAxis = Input.GetAxis("Horizontal"); // -1 if left is pressed, 1 if right is pressed, 0 if neither
        float vAxis = Input.GetAxis("Vertical"); // -1 if down is pressed, 1 if up is pressed, 0 if neither

        // Apply the rotation based on the inputs
        Vector3 amountToRotate = new Vector3(0, 0, 0);
        amountToRotate.x = vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed;
        amountToRotate *= Time.deltaTime; // amountToRotate = amountToRotate * Time.deltaTime;
        transform.Rotate(amountToRotate, Space.Self);

        // Deal with colliding with the terrain
        float terrainHeight = terrain.SampleHeight(transform.position);
        if (transform.position.y < terrainHeight)
        {
            forwardSpeed = 0;
        }


        // Make the plane move forward by adding the forward vector to the position.
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        // Position the camera
        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 10f; // Negative forward points in the opposite direction as forward
        cameraPosition += Vector3.up * 8f; // Vector3.up is (0,1,0)
        cameraObject.transform.position = cameraPosition;
        // LookAt is a utility function that rotates a transform so that it looks at a point
        cameraObject.transform.LookAt(transform.position);
    }


    // Unity will tell the function below to run under the following conditions:
    //  1. Two objects with colliders are colliding
    //  2. At least one of the objects' colliders are marked as "Is Trigger"
    //  3. At least one of the objects has a Rigidbody
    public void OnTriggerEnter(Collider other)
    {
        // 'other' is the name of the collider that just collided with the object
        // that this script is attached to (the plane).
        if (other.CompareTag("collectable"))
        {
            // Increase the forward speed when collecting an item
            forwardSpeed += boostAmount;

            // Check to see that it has the tag "collectable". Tags are assigned in the Unity editor.
            score++;

            scoreText.text = "Score: " + score;

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("wall") || other.CompareTag("obstacle") || other.CompareTag("terrain"))
        {
            // Handle the crash: reset the plane to the starting position
            transform.position = startingPosition;


            transform.rotation = Quaternion.identity; // Reset rotation
            forwardSpeed = 12f; // Reset speed to initial value
        }
       
    }
}
