using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogSelector : MonoBehaviour
{
    [SerializeField] GameObject[] dogsToChooseFrom;
    [SerializeField] TextMeshProUGUI chosenDog;
    [SerializeField] GameObject playerInput;
    private int chosenDogIndex = 99;
    private string playerName = "";

    public void DogSelected (int dogIndex)
    {
        GameObject dog = dogsToChooseFrom[dogIndex];
        for (int i = 0; i < dogsToChooseFrom.Length; i++)
        {
            dogsToChooseFrom[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        dog.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        chosenDog.text = dog.name;
        chosenDogIndex = dogIndex;
    }

    public void StartNew()
    {
        playerName = playerInput.GetComponent<TMP_InputField>().text;
        if (playerName == "")
        {
            Debug.Log("Please enter your name.");
            return;
        }
        if (chosenDogIndex > 2)
        {
            Debug.Log("Please choose a dog.");
            return;
        }
        if (MenuHandler.Instance != null)
        {
            MenuHandler.Instance.playerName = playerName;
            MenuHandler.Instance.dogSelectedIndex = chosenDogIndex;
        }
        SceneManager.LoadScene(1);
    }
}
