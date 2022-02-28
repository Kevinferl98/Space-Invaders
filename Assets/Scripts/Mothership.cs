using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    public int scoreValue;
    public AudioClip sfx;
    [SerializeField]
    GameObject killed;

    private const float MAX_LEFT = -7.52f;
    private float speed = 3;

    private void Start()
    {
        AudioManager.Instance().PlayMS(sfx);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;

        if (transform.position.x <= MAX_LEFT)
        {
            AudioManager.Instance().StopMS();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            UI.Instance().UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            AudioManager.Instance().StopMS();
            Instantiate(killed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
