using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float radius = 7f;
    public float explosionForce=500f;
    AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
    private void Start()
    {
        //bomba olu�turuldaktan 2 saniye sonra kendini yok edicek hiyerar�iden
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //bomba �arpt���nda ses efektini �almak i�in 
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);//bir kere �alar bu sat�r 
            //birden fazla collider oldu�unda �ald��� i�in if sat�r� i�ine yazd�k bir kere �al�cak �alma ger�ekle�irken
        }
        
        Health health = collision.gameObject.GetComponent<Health>();
        
        if (health)
        {
                health.TakeDamage(10);
        }
        CreateExplosionEffect();
      
    }

    private void CreateExplosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //�arp�lan b�lgede ka� collider varsa onlar� al�caz
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);// bu bize yar��ap� 7birim olan alanda colliderlar dizisi verir
        foreach(var nearby in nearbyObjects)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
    }
}
