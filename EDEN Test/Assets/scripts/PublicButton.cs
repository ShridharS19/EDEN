using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class makes sure that the button can be checked by just the call of a method, no need to add listeners
public class PublicButton : Button
{
  //Returns true if the button is pressed, false otherwise
  public bool PressedState() {
    return IsPressed();
  }
}
