using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSelection : MonoBehaviour
{   
    int numOfCards = 0;
    public CardsSelected cardsSelected;

    public Transform card1;
    public Transform card2;

    public GameObject invisibleButton;

    public GameObject but1;
    public GameObject but2;

    GameObject image1;
    GameObject image2;

    public GameObject Fool;
    public GameObject Magician;
    public GameObject Lovers;
    public GameObject Strength;
    public GameObject Death;
    public GameObject Devil;
    public GameObject Moon;
    public GameObject Sun;

    public TMP_Text text1;
    public TMP_Text text2;

    List<int> listOfCards = new List<int>();
    int randomCard1;
    int randomCard2;
    

    void Start(){
        but1.SetActive(true);
        but2.SetActive(true);

        ShowCards();
    }

    public void ShowCards(){
        text1.SetText("");
        text2.SetText("");
        randomCard1 = Random.Range(0, 8);
        while(listOfCards.Contains<int>(randomCard1)){
                randomCard1 = Random.Range(0, 8);
        }
        randomCard2 = Random.Range(0, 8);
        while(listOfCards.Contains<int>(randomCard2)){
                randomCard2 = Random.Range(0, 8);
        }
        listOfCards.Add(randomCard1);
        listOfCards.Add(randomCard2);

        image1 = CreateImageCard(randomCard1,card1);
        image2 = CreateImageCard(randomCard2,card2);

    }

    IEnumerator CardChoice(int cardSelected){
        if (numOfCards == 0) {
            cardsSelected.card1 = (CardsTypes)cardSelected;
            numOfCards++;
            Destroy(image1);
            Destroy(image2);

            yield return new WaitForSeconds(1f);

            but1.SetActive(true);
            but2.SetActive(true);

            ShowCards();
        } else if (numOfCards == 1) {
            cardsSelected.card2 = (CardsTypes)cardSelected;
            numOfCards++;
            Destroy(image1);
            Destroy(image2);

            yield return new WaitForSeconds(1f);

            but1.SetActive(true);
            but2.SetActive(true);

            ShowCards();
        } else if (numOfCards == 2) {
            cardsSelected.card3 = (CardsTypes)cardSelected;

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(5);
        }
    }

    public void OnButton1(){
        Debug.Log("Click");
        but1.SetActive(false);
        but2.SetActive(false);
        StartCoroutine(CardChoice(randomCard1));
    }

    public void OnButton2(){
        Debug.Log("Click");
        but1.SetActive(false);
        but2.SetActive(false);
        StartCoroutine(CardChoice(randomCard2));
    }

    public GameObject CreateImageCard(int card, Transform cardPlace){
        if(card == 0){
            text1.SetText("The Fool - Cost: 0 Mana\nThis card give a random effect. Four positive and one negative.");
            return Instantiate(Fool, cardPlace);
        } else if (card == 1) {
            return Instantiate(Magician, cardPlace);
        } else if (card == 2) {
            return Instantiate(Lovers, cardPlace);
        } else if (card == 3) {
            return Instantiate(Strength, cardPlace);
        } else if (card == 4) {
            return Instantiate(Death, cardPlace);
        } else if (card == 5) {
            return Instantiate(Devil, cardPlace);
        } else if (card == 6) {
            return Instantiate(Moon, cardPlace);
        } else {
            return Instantiate(Sun, cardPlace);
        }
    }
}
