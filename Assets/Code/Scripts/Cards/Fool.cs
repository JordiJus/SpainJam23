using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fool", menuName = "Level/Prefabs/Cards")]
public class Fool : ScriptableObject
{
    public int uses = 1;
    public int manaCost = 0;
    public int manaRecovery = 5;
}
