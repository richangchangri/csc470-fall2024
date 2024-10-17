using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // For displaying the energy on the UI

public class PlayerController : MonoBehaviour
{
    public int playerEnergy = 10;         // Player starts with 10 energy (percentage)
    public int maxEnergy = 100;            // Maximum energy player can have
    public float energyRegenRate = 2f;     // How fast energy regenerates (in seconds)
    public float energyRegenAmount = 1f;   // Amount of energy to regenerate each interval
    public Text energyText;                // UI text to display energy percentage
    public TextMeshProUGUI gameOverText;   // TMP text to display Game Over message
    public bool isGameOver = false;        // Track game state

    private float energyRegenTimer;        // Timer to track energy regeneration

    void Start()
    {
        energyRegenTimer = energyRegenRate;   // Initialize the energy regeneration timer
       
        gameOverText.gameObject.SetActive(false);  // Hide the Game Over text initially
    }

    void Update()
    {
        if (isGameOver)
            return;  // If the game is over, stop updating logic

        // Update the energy display on the UI
        if (energyText != null)
            energyText.text = "Energy: " + playerEnergy + "%(click to change the cell)";

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
        if (Input.GetMouseButton(0) && playerEnergy > 0)  //  Only interact if energy is greater than 0
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f))
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))  // Check if the player collided with an obstacle
        {
            GameOver();  // Call Game Over function
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);  // Show the Game Over text
        gameOverText.text = "Game Over";          // Update the text content
                                                  // Optionally, stop other game elements here (e.g., disable player movement)
    }


}