using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time_controller : MonoBehaviour
{
    [SerializeField] Text timeValue;
    [SerializeField] float time;
    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        timeValue.text = time.ToString();
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            time -= Time.deltaTime;
            timeValue.text = ((int)time).ToString();
        }
        if (time < 0)
        {
            time = 60;
            GetComponent<oyuncu_kontol>().die();
            gameActive = false;
        }
    }
}
