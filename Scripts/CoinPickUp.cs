using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int coinScore = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FindObjectOfType<GameSession>().AddToScore(coinScore);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);          
            Destroy(gameObject);
        }
    }

}
