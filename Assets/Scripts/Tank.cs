using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    //Burda �imdi miras (abstrac) falan yapcaz iyi ��renmek laz�m 



    //her class i�in otomatik olarak rigidbody al�cak 
    public Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    //speed k�s�mlar� 
    public float moveSpeed=10;
    public float rootSpeed=240f;

   

    public Transform other;
    public Transform turret;

    public Rigidbody bombPrefab;//bir bomba prefab� ve 
    public Transform bombSpawn;//bomban�n instantiate yap�ca��m�z yer
    [Range(10000f, 30000f)] //bu bize de�er aral��� veriyor ve bu de�er aral��� i�inde gezebiliyoruz 
    public float bombSpeed = 15000f;//o de�er aral��� bombSpeed'imize yar�ycak  

    //tekerlerin d�nme efekti gibi yap�ca��m�z materialin offsetiyle oynamak i�in
    public Material matOfset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();



    protected abstract IEnumerator LookAt(Transform other);
    //turret'in otomatik d��man� g�rmesi ve ate� etmemiz i�in
    //ata class'�m�zda shoot fonksiyonu private'den protected'a �eviriyoruz ki AITank'tanda �a��rabilelim.
    protected void Shoot()
    {
        //local oldu�u i�in var diyicez ve 
        var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        bomb.AddForce(turret.forward * bombSpeed);
    }

    //parametre olarak olu�turdu�umuz moveAxis de�erini verdik ve maintexture'nin offsetini artt�r�yoruz
    protected void createMoveEffect(float moveAxis)
    {
        matOfset.mainTextureOffset += new Vector2(moveAxis, 0);
    }
}
