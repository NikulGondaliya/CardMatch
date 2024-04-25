using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public static Gamemanager instance;
    
    public GridManager gridManager;
    public CardManager cardManager;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }








}
