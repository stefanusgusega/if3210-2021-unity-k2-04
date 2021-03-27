using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    public LayerMask p_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    public ParticleSystem p_ExplosionParticles;         // Reference to the particles that will play on explosion.
    public float p_MaxDamage = 50f;                    // The amount of damage done if the explosion is centred on a tank.
    public float p_ExplosionForce = 500f;              // The amount of force added to a plane at the centre of the explosion.
    public float p_MaxLifeTime = 20f;                    // The time in seconds before the shell is removed.
    public float p_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.


    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        // Destroy(gameObject, p_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, p_ExplosionRadius, p_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(p_ExplosionForce, transform.position, p_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);

            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);
        }

        // Unparent the particles from the shell.
        p_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        p_ExplosionParticles.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        Destroy(p_ExplosionParticles.gameObject, p_ExplosionParticles.duration);

        // Destroy the shell.
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (p_ExplosionRadius - explosionDistance) / p_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * p_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
