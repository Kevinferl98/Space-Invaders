using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float speed;
    [SerializeField]
    AudioClip sound;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
        if (transform.position.y <= -3.48)
            Destroy(gameObject);
    }

    public virtual void Pick() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pick();
            AudioManager.Instance().PlaySound(sound);
        }
    }
}
