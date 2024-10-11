using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // For displaying the energy on the UI

public class PlayerController : MonoBehaviour
{
    public int playerEnergy = 100;         // Player starts with 100 energy (percentage)
    public int maxEnergy = 100;            // Maximum energy player can have
    public float energyRegenRate = 2f;     // How fast energy regenerates (in seconds)
    public float energyRegenAmount = 1f;   // Amount of energy to regenerate each interval
    public Text energyText;                // UI text to display energy percentage

    private float energyRegenTimer;        // Timer to track energy regeneration

    void Start()
    {
        energyRegenTimer = energyRegenRate;   // Initialize the energy regeneration timer
    }

    void Update()
    {
        // Update the energy display on the UI
        if (energyText != null)
            energyText.text = "Energy: " + playerEnergy + "%";

        // Automatically regenerate energy over time
        if (playerEnergy < maxEnergy)
        {
            energyRegenTimer -= Time.deltaTime;
            if (energyRegenTimer <= 0f)
            {
                // Increase energy by a set amount, but don't exceed maxEnergy
                playerEnergy = Mathf.Min(playerEnergy + Mathf.FloorToInt(energyRegenAmount), maxEnergy);
                energyRegenTimer = energyRegenRate;  // Reset the timer
            }
        }

        // Check if the mouse is being held down (left-click)
        if (Input.GetMouseButton(0))  // Continuous detection for left-click hold
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f))
            {
                CellScript cell = hit.collider.GetComponent<CellScript>();
                if (cell != null && playerEnergy > 0)
                {
                    // Toggle the cell's state to alive and reduce energy
                    if (cell.alive == false)
                    {
                        cell.alive = true;   // Make the cell alive
                        cell.SetColor();
                    }

                    // Reduce energy by 1.0% per frame, or a minimum of 1 energy point
                    playerEnergy -= Mathf.Max(1, Mathf.FloorToInt(playerEnergy * 0.01f));
                }
            }
        }
    }

    // Method to increase energy when collecting orbs
    public void CollectEnergy(int amount)
    {
        playerEnergy += amount;
        if (playerEnergy > maxEnergy)
        {
            playerEnergy = maxEnergy;  // Ensure the energy doesn't exceed the max limit
        }
    }
}