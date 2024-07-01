using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HungerController : MonoBehaviour
{
    public static HungerController Instance;
    [SerializeField] Image[] hungerScale;

    // ENCAPSULATION
    private int m_HungerLevel;
    public int HungerLevel
    {
        get { return m_HungerLevel; }
        set { if ((value >= 0) && (value < hungerScale.Length + 1)) { m_HungerLevel = value; } }
    }

    private int levelMin = 0;
    private int levelMax = 5;
    private int levelInit = 0;

    public string foodGiven;
    public bool isEating;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        HungerLevel = levelInit;
        UpdateScale();
    }

    public void IncreaseLevel()
    {
        if (HungerLevel < levelMax)
        {
            HungerLevel++;

            // ABSTRACTION
            UpdateScale();
        }
    }

    public void DecreaseLevel()
    {
        if (HungerLevel > levelMin)
        {
            HungerLevel--;
            // ABSTRACTION
            UpdateScale();
        }
    }

    // ABSTRACTION
    void UpdateScale()
    {
        for (int i = 0; i < HungerLevel; i++)
        {
            hungerScale[i].color = Color.green;
        }
        for (int i = HungerLevel; i < levelMax; i++)
        {
            hungerScale[i].color = Color.red;
        }
    }

    public void FoodReceived(string activeFood)
    {
        foodGiven = activeFood;
    }

    public string CheckForFood()
    {
        if (foodGiven != null)
        {
            string foodReceived = foodGiven;
            foodGiven = null;
            return foodReceived;
        }
        return "none";
    }

    public void FoodConsumed(bool eaten, bool isFav)
    {
        foodGiven = null;
        if (eaten) { IncreaseLevel(); }
        if (isFav) { IncreaseLevel(); }
    }

}
