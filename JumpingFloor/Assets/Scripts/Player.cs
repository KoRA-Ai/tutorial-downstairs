using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float movespeed = 5f;
    GameObject currentFloor;
    [SerializeField] int hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    int floor;
    float scoreTime;
    Animator anim;
    SpriteRenderer render;
    AudioSource deathSound;
    [SerializeField] GameObject btns;
    [SerializeField] GameObject continueBtn;

    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        floor = 0;
        scoreTime = 0f;
        anim = GetComponent<Animator>();
        render=GetComponent<SpriteRenderer>();
        deathSound=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(movespeed*Time.deltaTime, 0, 0);
            //make player can turn left and right(the animation/photo)
            render.flipX = false;
            anim.SetBool("run", true);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-movespeed * Time.deltaTime, 0, 0);
            render.flipX = true;
            anim.SetBool("run", true);

        }
        else
        {
            //the player not run->display idle animation
            anim.SetBool("run", false);
        }
        UpdateScore();

        if (Input.GetKey(KeyCode.Escape)){
            Time.timeScale=0;
            btns.SetActive(true);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "normal")
        {
            if(collision.contacts[0].normal==new Vector2(0f, 1f))
            {
                currentFloor=collision.gameObject;
                ModifyHp(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (collision.gameObject.tag == "nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = collision.gameObject;
                ModifyHp(-2);
                anim.SetTrigger("hurt");
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (collision.gameObject.tag == "ceiling")
        {
            //cancel player/floor collision
            currentFloor.GetComponent<BoxCollider2D>().enabled = false ;
            ModifyHp(-3);
            anim.SetTrigger("hurt");
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deathLine")
        {
            Die();
        }
    }

    void ModifyHp(int num)
    {
        hp += num;

        if (hp > 10)
        {
            hp = 10;
        }
        else if (hp < 0)
        {
            hp = 0; 
            Die();
        }
        //update UI
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        //total hp=10, loop 10 times
        for(int i = 0; i < HpBar.transform.childCount; i++) 
        {
            // if hp=5, i=0, 1st hp child appear
            if (hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            // if hp=5, i=7, 8th hp child not appear
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 2f)
        {
            floor++;
            scoreTime = 0f;
            scoreText.text = "B" + floor.ToString();
        }
    }
    void Die(){
        deathSound.Play();
        Time.timeScale=0f;
        btns.SetActive(true);
        continueBtn.SetActive(false);
    }

    public void replay(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void goToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void continueOnClick(){
        btns.SetActive(false);
        Time.timeScale = 1f;
    }


}
