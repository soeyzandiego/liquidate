using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInvestor : MonoBehaviour
{
    // handles the decorative investors on the menu page

    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer assetSprite;

    void Start()
    {
        RandomizeSprite();

        GetComponent<Animator>().SetBool("moving", true);

        if (transform.position.x < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        }
        else if (transform.position.x > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void RandomizeSprite()
    {
        int i = Random.Range(0, sprites.Length);
        assetSprite.sprite = sprites[i];
    }

}
