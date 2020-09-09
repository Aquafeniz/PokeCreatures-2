using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public static BattleHUD Instance {get; set;}

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;

    void Awake()
    {
        if (Instance == null) Instance = this;        
    }

    public void DisableButtons()
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
    }

    public void EnableButtons()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
    }


}
