using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrow_controller : MonoBehaviour
{
    [SerializeField] GameObject effect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.CompareTag("player")))
        {
            if (!(collision.gameObject.CompareTag("coin")))
            {
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.CompareTag("Enemy")) 
        {
            Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            GameObject.Find("lvl_manager").GetComponent<level_controller>().score_arttir(50);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
