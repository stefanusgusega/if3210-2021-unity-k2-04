using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public int p_PlayerNumber = 1;
    public float p_Speed = 10f;
    public float p_TurnSpeed = 180f;

    private string p_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string p_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody p_Rigidbody;              // Reference used to move the tank.
    private float p_MovementInputValue;         // The current value of the movement input.
    private float p_TurnInputValue;             // The current value of the turn input.

    private void Awake()
    {
        p_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        p_Rigidbody.isKinematic = false;

        // Also reset the input values.
        p_MovementInputValue = 0f;
        p_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        p_Rigidbody.isKinematic = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // The axes names are based on player number.
            p_MovementAxisName = "Vertical" + p_PlayerNumber;
            p_TurnAxisName = "Horizontal" + p_PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        // Store the value of both input axes.
            p_MovementInputValue = Input.GetAxis(p_MovementAxisName);
            p_TurnInputValue = Input.GetAxis(p_TurnAxisName);
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * p_MovementInputValue * p_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        p_Rigidbody.MovePosition(p_Rigidbody.position + movement);
    }

    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = p_TurnInputValue * p_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        p_Rigidbody.MoveRotation(p_Rigidbody.rotation * turnRotation);
    }
}
