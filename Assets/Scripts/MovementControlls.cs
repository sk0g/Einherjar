using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControlls : MonoBehaviour 
{
    [Range(0.1f, 40.0f)]
    public float speed = 5.0f;
    private Vector3 velocity;

    void Awake() 
    {
    }

    void Start() 
    {
        StartCoroutine(HandleMovement());
    }

    void Update() 
    {
    }

    // Checks and applies any input made by the player
    // Current Controll schema is just moving towards direction 
    // of the mouse
    IEnumerator HandleMovement()
    {
        bool running = true;
        
        while(running && this.isActiveAndEnabled)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = (this.transform.position - Camera.main.transform.position).magnitude;
            
            // convert to world coordinates 
            // basically creates a 2D plane at the player's coordinates that face the camera
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            // grab direction from player to mouse world coordinate on plane
            Vector3 direction = (mousePosition - this.transform.position).normalized;

            // figure out the angle of the direction to rotate player towards in the X axis
            // + 90, as player is already in a 90 degree rotation 
            float angle = (Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg) + 90;
            
            //X axis rotation using EULER => Quaternion rotation method
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

            // Horizontal = A, D keys, returns a value from 0-1
            // Vertical = W, S keys, returns a value from 0-1
            velocity.x = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
            velocity.z = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;

            this.transform.position += this.transform.rotation * velocity;

            // return nothing
            yield return null;
        }

        // coroutine return end;
        yield break;
    }
}

