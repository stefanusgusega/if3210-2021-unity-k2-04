using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneShooting : MonoBehaviour
{
    public int p_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody p_Shell;                   // Prefab of the shell.
    public Transform p_FireTransform;           // A child of the tank where the shells are spawned.
    public float p_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    public float p_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    public float p_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.
    public float period = 20;
    public float next = 0;

    private string p_FireButton;                // The input axis that is used for launching shells.
    private float p_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float p_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool p_Fired;                       // Whether or not the shell has been launched with this button press.

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        p_CurrentLaunchForce = p_MinLaunchForce;
    }


    private void Start()
    {
        // The fire axis is based on the player number.
        p_FireButton = "Fire" + p_PlayerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        p_ChargeSpeed = (p_MaxLaunchForce - p_MinLaunchForce) / p_MaxChargeTime;
    }


    private void Update()
    {

        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (p_CurrentLaunchForce >= p_MaxLaunchForce && !p_Fired)
        {
            // ... use the max force and launch the shell.
            p_CurrentLaunchForce = p_MaxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(p_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            p_Fired = false;
            p_CurrentLaunchForce = p_MinLaunchForce;
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(p_FireButton) && !p_Fired)
        {
            // Increment the launch force and update the slider.
            p_CurrentLaunchForce += p_ChargeSpeed * Time.deltaTime;
        }
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        else if (Input.GetButtonUp(p_FireButton) && !p_Fired)
        {
            // ... launch the shell.
            Fire();
        }
    }


    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        p_Fired = true;
        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
           (Rigidbody) Instantiate(p_Shell, p_FireTransform.position, p_FireTransform.rotation);
        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = p_CurrentLaunchForce * p_FireTransform.forward;

        // Reset the launch force.  This is a precaution in case of missing button events.
        p_CurrentLaunchForce = p_MinLaunchForce;
    }
}
