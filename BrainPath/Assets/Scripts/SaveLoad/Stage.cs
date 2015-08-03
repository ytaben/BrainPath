using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public string name;
    public Sprite pircure; //Picture to display 
    public LevelManager[] levels; //Levels that belong to this stage

    public Text nameText;
    public Image imageObject;
}
