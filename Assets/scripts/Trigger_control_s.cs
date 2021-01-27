using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_control_s : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<oyuncu_kontol>().onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<oyuncu_kontol>().onGround = false;
    }
}
