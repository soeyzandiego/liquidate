using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset : MonoBehaviour
{

    [SerializeField] public assetTypes type;

    bool selected = false;
    AssetSpawner spawner;

    public enum assetTypes
    {
        Potion,
        Ball,
        Sword,
        Pick,
        Crate,
        Rocket
    }

    void Start()
    {
        spawner = FindObjectOfType<AssetSpawner>();
    }

    void Update()
    {
        if (selected)
        {
            // move asset with cursor
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        Collider2D collider = Physics2D.OverlapBox(transform.position, transform.localScale, 0, LayerMask.GetMask("Investor"));
        if (collider != null)
        {
            Investor investor = collider.GetComponent<Investor>();
            if (investor != null)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    investor.CollectAsset(this);
                    spawner.SubtractAsset();
                    Destroy(gameObject);
                }
            }
            else if (collider.CompareTag("Trash"))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    spawner.SubtractAsset();
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) { selected = true; }
        else { selected = false; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}

