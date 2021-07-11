using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    Player playerScript;
    public GameObject player;

    public Image yourCurrentCustome;

    public GameObject Custome1PricePanel;
    public GameObject Custome2PricePanel;

    public Text moneyText;
    public Text Custome1Text;
    public Text Custome2Text;

    public GameObject DialogUI;
    public GameObject ShopUI;

    public GameObject PauseMenua;

    void Start(){
        playerScript = player.GetComponent<Player>();
    }
    public void onChangeToDefault(){
        playerScript.choosedCustome = Player.Custome.def;
        yourCurrentCustome.sprite = playerScript.Chars[0];
    }
    public void onCustome1Addition(){
        if(!playerScript.currentCustomes.Contains(Player.Custome.one) && playerScript.money >= 250){
            playerScript.money -= 250;
            playerScript.currentCustomes.Add(Player.Custome.one);
            playerScript.moneyText.text = playerScript.money.ToString();
            Custome1PricePanel.SetActive(false);
            Custome1Text.text = "Press to Change to this Custome";
        }
        else if(!playerScript.currentCustomes.Contains(Player.Custome.one) && playerScript.money < 250){
            moneyText.text = "You don't have enough money to buy this Custome!";
        }
        else{
            playerScript.choosedCustome = Player.Custome.one;
            yourCurrentCustome.sprite = playerScript.Chars[1];
            
        }
    }
    public void onCustome2Addition(){
        if(playerScript.currentCustomes.Contains(Player.Custome.two)){
            playerScript.choosedCustome = Player.Custome.two;
            yourCurrentCustome.sprite = playerScript.Chars[2];
        }
        else if(!playerScript.currentCustomes.Contains(Player.Custome.one) && playerScript.money >= 750){
            playerScript.money -= 750;
            playerScript.currentCustomes.Add(Player.Custome.two);
            playerScript.moneyText.text = playerScript.money.ToString();
            Custome2PricePanel.SetActive(false);
            Custome2Text.text = "Press to Change to this Custome";
        }
        else{
            moneyText.text = "You don't have enough money to buy this Custome!";
        }
    }
    public void onBackPress(){
        DialogUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void onEnterShopPress(){
        DialogUI.SetActive(false);
        ShopUI.SetActive(true);
    }


    public void onResume(){
        PauseMenua.SetActive(false);
        Time.timeScale = 1;
    }

    public void onRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void onExit(){
        Application.Quit();
    }
}
