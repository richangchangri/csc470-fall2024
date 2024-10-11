using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Renderer cubeRenderer;

    public bool alive = false;

    public int aliveCount = 0;

    public int xIndex = -1;
    public int yIndex = -1;

    public Color aliveColor;
    public Color deadColor;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetColor();

        GameObject gmObj = GameObject.Find("GameManagerObject");
        gameManager = gmObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    void OnMouseDown()
    {
        alive = !alive;
        SetColor();

        // Count my neighbors!
        int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        Debug.Log("(" + xIndex + "," + yIndex + "): " + neighborCount);
    }
    public void SetColor() {
        // Set color based on alive status
        cubeRenderer.material.color = alive ? aliveColor : deadColor;

        // Change the height of the cell based on alive status
        Vector3 newScale = transform.localScale;
        newScale.y = alive ? 2f : 0.5f; // Taller when alive, shorter when dead
        transform.localScale = newScale;
    }

    public void SetHighlight(bool highlight)
    {
        if (highlight)
        {
            // Change the material or color to indicate the player is standing on this cell
            cubeRenderer.material.color = Color.black; 
        }
        else
        {
            SetColor(); // Revert to the original color based on alive status
        }
    }

}
