using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float speed = 10;       // velocità di movimento del proiettile
    public GameObject expbullet;    // effetto che simula la distruzione del proiettile
    SpriteRenderer spriter;        

    // Start is called before the first frame update
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        if (transform.position.y >= 3.30)       // se supera una certa altezza diventa rosso 
        {
            spriter.color = Color.red;
        }
        if (transform.position.y >= 4.07)       // esplode (per simulare che abbia colpito un muro) e viene distrutto
        {
            Instantiate(expbullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            collision.gameObject.GetComponent<Alien>().Kill();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("DMI"))
        {
            collision.gameObject.GetComponent<DMI>().Damage();
            Destroy(gameObject);
        }
    }
}
