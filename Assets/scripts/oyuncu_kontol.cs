using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oyuncu_kontol : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    [SerializeField] float jumpPower;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowSpeed;
    private bool attacked;
    private float currentTimeAttack;
    [SerializeField] float defaultCurrentAttack;
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;
    [SerializeField] AudioClip dieMusic;
    private AudioSource defaultMusic;
    [SerializeField] GameObject winPanel, losePanel;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        attacked = false;
        currentTimeAttack = defaultCurrentAttack;
        arrowNumberText.text = arrowNumber.ToString();
        defaultMusic = GameObject.Find("ses_controller").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        #region player'in yüzünü dönmesi
        mySpeedX = Input.GetAxis("Horizontal");
        myBody.velocity = new Vector2((mySpeedX * speed), myBody.velocity.y);
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion

        #region ateş etme
        if (Input.GetMouseButtonDown(0) && arrowNumber>0)
        {
            if(attacked == false)
            {
                Invoke("Fire", 0.5f);
                myAnimator.SetTrigger("Attack");
                attacked = true;
            }
        }
        if(attacked == true)
        {
            currentTimeAttack -= Time.deltaTime;
        }
        if (currentTimeAttack <= 0)
        {
            currentTimeAttack = defaultCurrentAttack;
            attacked = false;
        }
        #endregion

        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
    }

    private void FixedUpdate()
    {
        #region zıplama
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true;
                myAnimator.SetTrigger("Jump");
            }
            else
            {
                if (canDoubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false;
                }
            }
        }
        #endregion
    }

    void Fire()
    {
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.transform.parent = GameObject.Find("Arrows").transform;
        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0);
        }
        else
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0);
            Vector3 okScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okScale.x, okScale.y, okScale.z);
        }
        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();
    }

    #region ölme aimasyonu
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("diken"))
        {
            GetComponent<time_controller>().enabled = false;
            die();
        }
        else if (collision.gameObject.CompareTag("finish"))
        {
            Destroy(collision.gameObject);
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void die()
    {
        defaultMusic.clip = null;
        defaultMusic.PlayOneShot(dieMusic);
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("Speed", 0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        StartCoroutine(wait(false));
    }
    #endregion

    IEnumerator wait(bool win)
    {
        yield return new WaitForSecondsRealtime(2f);
        if (!win)
        {
            losePanel.SetActive(true);
        }
    }
}
