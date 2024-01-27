using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="")]
public class ItemsManager : ScriptableObject
{
    [Header("General parameters")]
    public string ItemName;
    public float timeOfEffect;

    [Space]
    [Header("Specific parameters")]
    public int powerIncreasaed;
    public int happinesModifier;
    public float speedModifier;
    public float secondsOfImmunity;
    public float slipperyModifier;
}
