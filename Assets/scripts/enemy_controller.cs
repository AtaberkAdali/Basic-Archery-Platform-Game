using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    [SerializeField] bool onGround;
    private float width;
    [SerializeField] float speed;
    private Rigidbody2D mybody;
    [SerializeField] LayerMask engel;
    private static int totalEnemyNumber=0;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemyNumber++;
        Debug.Log("Düşman ismi: " + gameObject.name + " oluştu. Toplam düşman sayısı:" + totalEnemyNumber );
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width/2), Vector2.down, 2f, engel);
        if(hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }
        mybody.velocity = new Vector2(transform.right.x * speed, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 playerRealPozition = transform.position + (transform.right * width / 2);
        Gizmos.DrawLine(playerRealPozition, playerRealPozition + new Vector3(0, -2f, 0));
    }
}
