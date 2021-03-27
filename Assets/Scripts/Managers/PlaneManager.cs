using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaneManager
{
    public Color p_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform p_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector] public int p_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string p_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector] public GameObject p_Instance;         // A reference to the instance of the tank when it is created.

    private PlaneMovement p_Movement;                        // Reference to tank's movement script, used to disable and enable control.
    private PlaneShooting p_Shooting;                        // Reference to tank's shooting script, used to disable and enable control.


    public void Setup()
    {
        // Get references to the components.
        p_Movement = p_Instance.GetComponent<PlaneMovement>();
        p_Shooting = p_Instance.GetComponent<PlaneShooting>();

        // Set the player numbers to be consistent across the scripts.
        p_Movement.p_PlayerNumber = p_PlayerNumber;
        p_Shooting.p_PlayerNumber = p_PlayerNumber;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        p_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(p_PlayerColor) + ">PLAYER " + p_PlayerNumber + "</color>";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = p_Instance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = p_PlayerColor;
        }
    }

    // Used at the start of each round to put the tank into it's default state.
    public void Reset()
    {
        p_Instance.transform.position = p_SpawnPoint.position;
        p_Instance.transform.rotation = p_SpawnPoint.rotation;

        p_Instance.SetActive(false);
        p_Instance.SetActive(true);
    }

}
