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
        //bomba oluþturuldaktan 2 saniye sonra kendini yok edicek hiyerarþiden
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //bomba çarptýðýnda ses efektini çalmak için 
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);//bir kere çalar bu satýr 
            //birden fazla collider olduðunda çaldýðý için if satýrý içine yazdýk bir kere çalýcak çalma gerçekleþirken
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
        //çarpýlan bölgede kaç collider varsa onlarý alýcaz
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);// bu bize yarýçapý 7birim olan alanda colliderlar dizisi verir
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
