using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For displaying the energy on the UI

public class PlayerController : MonoBehaviour
{
    public int playerEnergy = 10;           // Player starts with 10 energy
    public int maxEnergy = 20;              // Maximum energy the player can have
    public float energyRegenRate = 2f;      // How fast energy regenerates (in seconds)
    public Text energyText;                 // UI text to display energy

    private float energyTimer;              // Timer to track energy regeneration

    void Start()
    {
        energyTimer = energyRegenRate;      // Initialize the energy timer
    }

    void Update()
    {
        // Update the energy display on the UI
        if (energyText != null)
            energyText.text = "Energy: " + playerEnergy;

        // Automatically grow energy over time
        energyTimer -= Time.deltaTime;
        if (energyTimer <= 0f && playerEnergy < maxEnergy)
        {
            playerEnergy++;                 // Increase energy by 1
            energyTimer = energyRegenRate;  // Reset the timer
        }

        // Player presses 'E' to toggle a cell if they have enough energy
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, 2f))
            {
                CellScript cell = hit.collider.GetComponent<CellScript>();
                if (cell != null && playerEnergy > 0)
                {
                    // Toggle the cell's state and decrease energy
                    cell.alive = !cell.alive;
                    cell.SetColor();
                    playerEnergy--;
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
            playerEnergy = maxEnergy;  // Make sure the energy doesn't exceed the max limit
        }
    }
}
