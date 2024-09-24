using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timedownSc : MonoBehaviour
{
    public TMP_Text countdownText;
    float countdownTime = 10f;      
    bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            countdownTime -= Time.deltaTime;

            // Update the countdown text to display remaining time
            countdownText.text = "Time: " + Mathf.Ceil(countdownTime).ToString();

            if (countdownTime <= 0)
            {
                isGameOver = true;
                countdownText.text = "You Lost"; // Display "You Lost" when time is up
                Destroy(gameObject);             // Destroy the plane
            }
        }

    }
}
