using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class DogBorderCollie : Dog
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
}
