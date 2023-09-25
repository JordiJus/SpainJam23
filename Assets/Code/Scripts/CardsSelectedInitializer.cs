using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSelectedInitializer : MonoBehaviour
{
    public CardsSelected cardsSelected;

    private void Awake()
    {
        if (cardsSelected == null)
        {
            cardsSelected = ScriptableObject.CreateInstance<CardsSelected>();
        }

        DontDestroyOnLoad(gameObject);
    }
}

