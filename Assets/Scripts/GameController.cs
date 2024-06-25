using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private string playerName;
    private int dogIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerName = MenuHandler.Instance.playerName;
        dogIndex = MenuHandler.Instance.dogSelectedIndex;
        Debug.Log("Hello " +  playerName + ", dog " + dogIndex + " is hungry.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
