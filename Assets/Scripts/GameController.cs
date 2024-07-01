using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] dogs;
    [SerializeField] GameObject[] foods;
    [SerializeField] float[] foodDefaultScale;
    [SerializeField] GameObject cam;
    [SerializeField] TextMeshProUGUI foodSelected;
    [SerializeField] TextMeshProUGUI dogComment;
    [SerializeField] float hungerInterval;
    private GameObject activeDog;
    private Animator dogAnim;

    public string playerName;
    public int dogIndex;
    public string activeFood;
    public bool isFoodSelected;
    public bool gameEnded;

    Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnded)
        {
            CheckForFoodClick();
        }
    }

    void InitGame()
    {
        playerName = MenuHandler.Instance.playerName;
        dogIndex = MenuHandler.Instance.dogSelectedIndex;
        activeDog = dogs[dogIndex];
        activeDog.SetActive(true);
        dogAnim = activeDog.GetComponent<Animator>();
        dogAnim.SetFloat("Speed_f", 0);
        dogComment.text = "Hi " + playerName + ", I'm hungry.";
        InvokeRepeating("GetHungry", hungerInterval, hungerInterval);
    }

    public void RestartGame()
    {
        Destroy(GameObject.Find("Hunger Controller"));
        SceneManager.LoadScene(0);
    }

    void CheckForFoodClick()
    {
        GameObject objectClicked;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            objectClicked = hit.transform.gameObject;
            if (objectClicked.CompareTag("Food"))
            {
                ResetFood();
                objectClicked.transform.localScale *= 1.2f;
                FoodSelected(objectClicked.name);
            }
            if (objectClicked.CompareTag("Dog") && isFoodSelected && !HungerController.Instance.isEating)
            {
                foodSelected.text = "";
                isFoodSelected = false;
                if (activeFood == "Energy Can")
                {
                    EnergyBoost();
                }
                else
                {
                    ResetFood();
                    HungerController.Instance.FoodReceived(activeFood);
                }
            }
        }
    }

    void ResetFood()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            foods[i].transform.localScale = new Vector3(foodDefaultScale[i], foodDefaultScale[i], foodDefaultScale[i]);
        }
    }

    public void FoodSelected(string foodName)
    {
        activeFood = foodName;
        foodSelected.text = activeFood;
        isFoodSelected = true;
    }

    void GetHungry()
    {
        if (!gameEnded)
        {
            HungerController.Instance.DecreaseLevel();
        }
    }

    void EnergyBoost()
    {
        Rigidbody dogRb;
        dogComment.text = "Oops, that was not a good idea.";
        foodSelected.text = "Game Over";
        dogAnim.SetFloat("Speed_f", 1);
        dogRb = activeDog.GetComponent<Rigidbody>();
        dogRb.velocity = Vector3.forward;
        gameEnded = true;
    }
}
