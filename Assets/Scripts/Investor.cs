using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investor : MonoBehaviour
{

    [SerializeField] Asset.assetTypes assetOwned;
    //[SerializeField] [Range(1, 5)] int amount = 1;
    [SerializeField] GameObject bubble;
    [SerializeField] float speed = 2f;

    [Header("Audio")]
    [SerializeField] AudioClip correct;
    [SerializeField] AudioClip incorrect;

    Rigidbody2D rb;
    Animator anim;
    HealthTracker health;
    AudioSource audioSrc;
    bool left;
    bool collected = false;
    bool inPosition = false;
    Vector3 pos;
    float timeWaiting = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = FindObjectOfType<HealthTracker>();
        audioSrc = GetComponent<AudioSource>();
        SetupBubble();
        if (transform.position.x < 0) // if on the left side of the screen
        {
            left = true;
        }
        else if (transform.position.x > 0)
        {
            left = false;
            GetComponent<SpriteRenderer>().flipX = true; // if on right side, flip sprite  
        }

        // choose random x pos to move to
        float xPos = Random.Range(-5f, 5f);
        pos = new Vector2(xPos, transform.position.y);
    }

    void Update()
    {
        anim.SetBool("moving",
            Mathf.Abs(rb.velocity.x) > Mathf.Epsilon ||
            Mathf.Abs(rb.velocity.y) > Mathf.Epsilon);
        MoveTowardsCenter();

        if (inPosition)
        {
            // start impatience timer
            RunTimer();
        }
    }

    void SetupBubble()
    {
        bubble.SetActive(true);
    }

    public void CollectAsset(Asset asset)
    {
        if (asset.type == assetOwned)
        {
            collected = true;
            FindObjectOfType<ScoreTracker>().Add();
            bubble.SetActive(false);
            rb.velocity = new Vector2(0, -speed);
            audioSrc.PlayOneShot(correct);
        }
        else
        {
            Leave();
        }
    }

    void MoveTowardsCenter()
    {
        // if asset was already collected before getting into position, cancel
        if (collected) { return; }

        if (Vector2.Distance(transform.position, pos) > 0.5f)
        {
            if (left) { rb.velocity = new Vector2(speed, 0); }
            else if (!left) { rb.velocity = new Vector2(-speed, 0); }
        }
        else
        {
            rb.velocity = Vector2.zero;
            inPosition = true;
        }
    }

    void RunTimer()
    {
        if (collected) { return; }
        timeWaiting += Time.deltaTime;

        if (timeWaiting > 2 && timeWaiting < 6)
        {
            // TODO first impatience
        }
        else if (timeWaiting >= 6 && timeWaiting < 12)
        {
            // TODO second impatience
        }
        else if (timeWaiting >= 12)
        {
            Leave();
        }

    }

    private void Leave()
    {
        collected = true;
        rb.velocity = new Vector2(0, -speed);
        health.Damage();
        bubble.SetActive(false);
        audioSrc.PlayOneShot(incorrect);
        Destroy(GetComponent<Collider2D>());
    }
}
