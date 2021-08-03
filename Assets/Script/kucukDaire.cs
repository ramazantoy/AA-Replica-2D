using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kucukDaire : MonoBehaviour
{
    LineRenderer cizgi;
    bool durduMu = false;
    Rigidbody2D r;
    Transform anaDaire;
    yonetici yonetici;
    void Start()
    {
        cizgi = GetComponent<LineRenderer>();
        r = GetComponent<Rigidbody2D>();
        anaDaire = GameObject.Find("ana_daire").transform;
        yonetici = GameObject.FindObjectOfType<yonetici>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (durduMu == true)
        {
            cizgi.SetPosition(0, anaDaire.position);
            cizgi.SetPosition(1, gameObject.transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ana_daire")
        {
            r.velocity = Vector2.zero;
            Vector2 yenipoz = transform.position;
            yenipoz.y = (anaDaire.transform.position.y) - ((anaDaire.GetComponent<CircleCollider2D>().bounds.size.y) * 1.5f);
            transform.position = yenipoz;
            transform.SetParent(anaDaire.transform);
            durduMu = true;
            yonetici.dairePozGuncelle();
                
        }
        else if (collision.gameObject.tag == "kucukdaire")
        {
            yonetici.kaybettin();
        }
    }
 
}
