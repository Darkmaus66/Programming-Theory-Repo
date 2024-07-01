using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class DogLabrador : Dog
{
    private void Start()
    {
        SetDogTaste();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckForFood(favoriteFood, dontLikeFood);
    }

    // POLYMORPHISM
    public override void SetDogTaste()
    {
        favoriteFood = "Pizza";
        dontLikeFood = "Pear";
    }
}
