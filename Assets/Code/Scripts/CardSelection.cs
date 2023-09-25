using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSelection : MonoBehaviour
{   
    int numOfCards = 0;
    private CardsSelected cardsSelected;

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
    
    public AudioSource audioClick;

    List<int> listOfCards = new List<int>();
    int randomCard1;
    int randomCard2;
    

    void Start(){
        
        cardsSelected = FindObjectOfType<CardsSelected>();

        but1.SetActive(true);
        but2.SetActive(true);

        ShowCards();
    }

    public void ShowCards(){
        text1.SetText("");
        text2.SetText("");
        randomCard1 = Random.Range(0, 8);
        if(!listOfCards.Contains<int>(randomCard1)) {
            listOfCards.Add(randomCard1);
        } else {
            while(listOfCards.Contains<int>(randomCard1)){
                randomCard1 = Random.Range(0, 8);
            }
            listOfCards.Add(randomCard1);
        }
        
        randomCard2 = Random.Range(0, 8);
        if(!listOfCards.Contains<int>(randomCard2)){
            listOfCards.Add(randomCard2);
        } else {
            while(listOfCards.Contains<int>(randomCard2)){
                randomCard2 = Random.Range(0, 8);
            }
            listOfCards.Add(randomCard2);
        }
        
        
        

        image1 = CreateImageCard(randomCard1,card1, text1);
        image2 = CreateImageCard(randomCard2,card2, text2);

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

            SceneManager.LoadScene(4);
        }
    }

    public void OnButton1(){
        audioClick.Play();
        but1.SetActive(false);
        but2.SetActive(false);
        StartCoroutine(CardChoice(randomCard1));
    }

    public void OnButton2(){
        audioClick.Play();
        but1.SetActive(false);
        but2.SetActive(false);
        StartCoroutine(CardChoice(randomCard2));
    }

    public GameObject CreateImageCard(int card, Transform cardPlace, TMP_Text text){
        if(card == 0){
            text.SetText("The Fool - Only 1 use\n\nThis card give a random effect. Three powerful positive ones and two negatives.");
            return Instantiate(Fool, cardPlace);
        } else if (card == 1) {
            text.SetText("Magician - Cost: 0 Mana\n\nYou recover all your mana upon use.");
            return Instantiate(Magician, cardPlace);
        } else if (card == 2) {
            text.SetText("Lovers - Cost: 2 Mana\n\nYou recover 4 health with this card.");
            return Instantiate(Lovers, cardPlace);
        } else if (card == 3) {
            text.SetText("Strength - Cost: 3 Mana\n\nYou gain +1 to your damage.");
            return Instantiate(Strength, cardPlace);
        } else if (card == 4) {
            text.SetText("Death - Cost: 5 Mana\n\nYou deal 5 damage to your enemy.");
            return Instantiate(Death, cardPlace);
        } else if (card == 5) {
            text.SetText("The Devil - Cost: 3 Mana\nOnly 1 Use\n\nYou gain +2 to your damage but you cannot defend for the rest of the combat.");
            return Instantiate(Devil, cardPlace);
        } else if (card == 6) {
            text.SetText("The Moon - Cost: 1 Mana/Turn\n\n-1 to incoming damage until you run out of mana. Cannot be deactivated.");
            return Instantiate(Moon, cardPlace);
        } else {
            text.SetText("The Sun - Cost: 4 Mana\n\nYou deal 3 damage and heal 2 of your health.");
            return Instantiate(Sun, cardPlace);
        }
    }
}
