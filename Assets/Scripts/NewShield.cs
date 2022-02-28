using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShield : MonoBehaviour
{
    public SpriteRenderer[] sr;
    private SpriteRenderer mine;
    public GameObject explode;
    private int life;

    private void Start()
    {
        mine = GetComponent<SpriteRenderer>();
        life = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            Instantiate(explode, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            life--;
            if (life <= 0)
                gameObject.SetActive(false);
            else
                mine.sprite = sr[life - 1].GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void restart()
    {
        life = 5;
        mine.sprite = sr[life-1].GetComponent<SpriteRenderer>().sprite;
        gameObject.SetActive(true);
    }
}
