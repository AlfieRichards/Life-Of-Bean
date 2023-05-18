using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreBoardController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = levelManager.score.ToString();
        timeText.text = levelManager.time.ToString();
    }
}
