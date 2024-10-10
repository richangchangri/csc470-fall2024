using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For displaying the energy on the UI

public class Play : MonoBehaviour
{
    public int playerEnergy = 10; // The player's energy level
    public Text energyText; // Reference to a UI text element to display energy

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update energy display on the UI
        energyText.text = "Energy: " + playerEnergy;

        // Player presses 'E' to toggle a cell if they have enough energy
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, 2f))
            {
                CellScript cell = hit.collider.GetComponent<CellScript>();
                if (cell != null && playerEnergy > 0)
                {
                    // Toggle cell state
                    cell.alive = !cell.alive;
                    cell.SetColor();

                    // Use energy when toggling a cell
                    playerEnergy--;
                }
            }
        }
    }

    // Method to increase energy when collecting orbs
    public void CollectEnergy(int amount)
    {
        playerEnergy += amount;
    }
}
