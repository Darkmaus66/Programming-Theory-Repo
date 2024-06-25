using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] dogs;
    [SerializeField] GameObject[] foods;
    [SerializeField] TextMeshProUGUI foodSelected;


    public string playerName;
    public int dogIndex;
    public string activeFood;
    public bool isFoodSelected;

    Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = MenuHandler.Instance.playerName;
        dogIndex = MenuHandler.Instance.dogSelectedIndex;
        dogs[dogIndex].SetActive(true);
    }

    void Update()
    {
        CheckForFoodClick();
    }

    void CheckForFoodClick()
    {
        GameObject objectClicked;
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                objectClicked = hit.transform.gameObject;
                if (objectClicked.CompareTag("Food"))
                {
                    FoodSelected(objectClicked.name);
                }
                if (objectClicked.CompareTag("Dog") && isFoodSelected)
                {
                    DogSelected();
                }
            }
        }
    }

    public void FoodSelected(string foodName)
    {
        activeFood = foodName;
        foodSelected.text = activeFood;
        isFoodSelected = true;
    }

    public void DogSelected()
    {
        Debug.Log("Thank you for the " + activeFood);
    }
}
