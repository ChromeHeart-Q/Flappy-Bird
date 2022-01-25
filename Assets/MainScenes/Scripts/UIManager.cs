using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Text txtScore;
    private int score;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        score = 0;
        txtScore = transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        txtScore.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore()
    {
        score++;
        txtScore.text = score.ToString();
    }

    public void MinusScore()
    {
        score--;
        txtScore.text = score.ToString();
    }
}
