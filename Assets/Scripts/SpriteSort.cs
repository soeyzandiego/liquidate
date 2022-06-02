using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSort : MonoBehaviour
{
    // script automates sorting layers based on the y position
    [SerializeField] float yOffset = 0;

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int orderNum = Mathf.RoundToInt((transform.position.y + yOffset) * -100);
        renderer.sortingOrder = orderNum;
    }
}
