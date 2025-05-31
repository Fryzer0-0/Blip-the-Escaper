using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private CoinCounter _coinCounter;

    private void Start()
    {
        _coinCounter = FindObjectOfType<CoinCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _coinCounter.Coin++;
            gameObject.SetActive(false);
        }
    }
}
