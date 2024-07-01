using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public TextMeshProUGUI dogComment;
    public Animator anim;
    public string foodGiven;

    public string favoriteFood;
    public string dontLikeFood;


    // POLYMORPHISM
    public virtual void SetDogTaste()
    {
        favoriteFood = "Steak";
        dontLikeFood = "Pear";
    }

    public void CheckForFood(string favorite, string dontlike)
    {
        foodGiven = HungerController.Instance.CheckForFood();
        if (foodGiven != "none" && foodGiven.Length > 0)
        {
            bool eat = false;
            bool fav = false;
            if (HungerController.Instance.HungerLevel >= 5)
            {
                dogComment.text = "Thank you, but I'm not hungry.";
                eat = false;
            }
            else
            {
                if (foodGiven == dontlike)
                {
                    dogComment.text = "Thank you, but I don't like " + foodGiven + ".";
                    eat = false;
                }
                else
                {
                    if (foodGiven == favorite)
                    {
                        dogComment.text = favorite + "! Thank you, that's my favorite!";
                        fav = true;
                    }
                    else
                    {
                        dogComment.text = "Yammie! " + foodGiven + "!";
                    }
                    eat = true;
                }
            }
            StartEating(eat, fav);
        }
    }

    public void StartEating(bool eat, bool fav)
    {
        HungerController.Instance.isEating = true;
        StartCoroutine(EatingFood(eat));
        HungerController.Instance.FoodConsumed(eat, fav);
    }

    IEnumerator EatingFood(bool eat)
    {
        if (eat)
        {
            anim.SetFloat("Speed_f", 0);
            anim.SetBool("Eat_b", true);
        }
        yield return new WaitForSeconds(5);
        anim.SetBool("Eat_b", false);
        dogComment.text = "";
        HungerController.Instance.isEating = false;
    }
}
