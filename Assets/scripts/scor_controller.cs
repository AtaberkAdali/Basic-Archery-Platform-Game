using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scor_controller : MonoBehaviour
{
    [SerializeField] Text scoreValueText;
    [SerializeField] float coinRotatespeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, coinRotatespeed, 0f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            GameObject.Find("lvl_manager").GetComponent<level_controller>().score_arttir(10);
            Destroy(gameObject);
        }
    }
    
}
