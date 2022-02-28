using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 7;
    public GameObject expbullet;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;    // sposta il proiettile verso il basso
        if (transform.position.y <= -3.347) // se arriva vicino il bordo in basso instanzia il proiettile distrutto e si distrugge
        {
            Instantiate(expbullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
