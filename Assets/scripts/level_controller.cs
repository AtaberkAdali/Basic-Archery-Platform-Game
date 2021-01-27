using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level_controller : MonoBehaviour
{
    [SerializeField] Text scoreValueText;

    private void Start()
    {
        scoreValueText = GameObject.Find("score_value").GetComponent<Text>();
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PreviousLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void score_arttir(int a)
    {
        int scoreValue = int.Parse(scoreValueText.text);
        scoreValue += a;
        scoreValueText.text = scoreValue.ToString();
    }
}
