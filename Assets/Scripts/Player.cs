using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float money = 1000;
    public BoxCollider2D smallVer;
    public BoxCollider2D bigVer;
    public UnityEngine.UI.Text moneyText;
    public enum Custome {def, one, two};
    public Custome choosedCustome;
    public List<Custome> currentCustomes = new List<Custome>();
    public float speed = 150.0f;
    public GameObject Inside;
    public GameObject Outside;
    public BoxCollider2D OutsideCollider;

    public RuntimeAnimatorController[] PlayerControllers;

    public Sprite[] Chars;

    float vertical;
    float horizontal;
    bool inShopKeeperCollider;
    bool isPaused = false;
    public GameObject PauseMenu;
    Animator playerAnimator;
    Rigidbody2D playerRB;
    public GameObject DialogUI;
    public GameObject ShopUI;
    SpriteRenderer playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = money.ToString();
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        OutsideCollider = Outside.GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        currentCustomes.Add(Custome.def);
        choosedCustome = (Custome.def);
    }

    // Update is called once per frame
    void Update()
    {
        if(choosedCustome.Equals(Custome.def)){
            this.transform.localScale = new Vector3(0.45f,0.45f,this.transform.localScale.z);
            playerSprite.sprite = Chars[0];
            playerAnimator.runtimeAnimatorController = PlayerControllers[0];
            smallVer.enabled = true;
            bigVer.enabled = false;
        }
        else if(choosedCustome.Equals(Custome.one)){
            playerSprite.sprite = Chars[1];
            playerAnimator.runtimeAnimatorController = PlayerControllers[1];
            this.transform.localScale = new Vector3(2.5f,2.5f,this.transform.localScale.z);
            smallVer.enabled = false;
            bigVer.enabled = true;
        }
        else if(choosedCustome.Equals(Custome.two)){
            playerSprite.sprite = Chars[2];
            playerAnimator.runtimeAnimatorController = PlayerControllers[2];
            this.transform.localScale = new Vector3(2.5f,2.5f,this.transform.localScale.z);
            smallVer.enabled = false;
            bigVer.enabled = true;
        }


        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.None)){
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", false);
        }
        else if(vertical > 0){
            playerAnimator.SetBool("Up", true);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", false);
        }
        else if(vertical < 0){
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", true);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", false);
        }
        else if(horizontal > 0){
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Right", true);
            playerAnimator.SetBool("Left", false);
        }
        else if(horizontal < 0){
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", true);
        }
        else{
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", false);
        }


        if(Input.GetKeyUp(KeyCode.Escape)){
            if(DialogUI.active || ShopUI.active){
                DialogUI.SetActive(false);
                ShopUI.SetActive(false);
                Time.timeScale = 1;
            }
            else{
                if(isPaused){
                    PauseMenu.SetActive(false);
                    isPaused = false;
                    Time.timeScale = 1;
                }
                else{
                    PauseMenu.SetActive(true);
                    isPaused = true;
                    Time.timeScale = 0;
                }
            }

        }

        if(Input.GetKey(KeyCode.Return) && inShopKeeperCollider){
            DialogUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnAnimatorMove(){
        playerRB.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Door collider")){
            if(!Outside.active){
                Outside.SetActive(true);
                OutsideCollider.enabled = true;
                Inside.SetActive(false);
            }
            else{
                Inside.SetActive(true);
                Outside.SetActive(false);
                OutsideCollider.enabled = false;
            }
        }
        else if (other.tag == "ShopKeeper"){
            inShopKeeperCollider = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "ShopKeeper"){
            inShopKeeperCollider = false;
        }
    }
}
