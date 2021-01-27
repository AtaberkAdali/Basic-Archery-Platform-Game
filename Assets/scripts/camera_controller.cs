using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float minX, MaxX;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x,minX,MaxX), transform.position.y, transform.position.z);
    }
}
