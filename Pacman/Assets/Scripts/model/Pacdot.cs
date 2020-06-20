﻿using UnityEngine;

public class Pacdot : MonoBehaviour
{
    public bool isSuperPacdot = false;
    public bool isMidPacdot = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (isSuperPacdot)
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                GameManager.Instance.OnEatSuperPacdot();
                Destroy(gameObject);
            }

            else if (isMidPacdot)
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                GameManager.Instance.OnEatMidPacdot();
                Destroy(gameObject);
            }

            else
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (isSuperPacdot)
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                GameManager.Instance.OnEatSuperPacdot();
                Destroy(gameObject);
            }

            else if (isMidPacdot)
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                GameManager.Instance.OnEatMidPacdot();
                Destroy(gameObject);
            }

            else
            {
                GameManager.Instance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
