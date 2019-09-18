using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keybindings : MonoBehaviour
{
    
    public Button BmoveLeft;
    public Button BmoveRight;
    public Button BJump;
    public Button BAttack;

  public void BmoveLeftClick()
  {
        BmoveLeft.interactable = false;
        
  }

   
}
