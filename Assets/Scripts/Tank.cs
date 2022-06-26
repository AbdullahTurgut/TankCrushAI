using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    //Burda þimdi miras (abstrac) falan yapcaz iyi öðrenmek lazým 



    //her class için otomatik olarak rigidbody alýcak 
    public Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    //speed kýsýmlarý 
    public float moveSpeed=10;
    public float rootSpeed=240f;

   

    public Transform other;
    public Transform turret;

    public Rigidbody bombPrefab;//bir bomba prefabý ve 
    public Transform bombSpawn;//bombanýn instantiate yapýcaðýmýz yer
    [Range(10000f, 30000f)] //bu bize deðer aralýðý veriyor ve bu deðer aralýðý içinde gezebiliyoruz 
    public float bombSpeed = 15000f;//o deðer aralýðý bombSpeed'imize yarýycak  

    //tekerlerin dönme efekti gibi yapýcaðýmýz materialin offsetiyle oynamak için
    public Material matOfset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();



    protected abstract IEnumerator LookAt(Transform other);
    //turret'in otomatik düþmaný görmesi ve ateþ etmemiz için
    //ata class'ýmýzda shoot fonksiyonu private'den protected'a çeviriyoruz ki AITank'tanda çaðýrabilelim.
    protected void Shoot()
    {
        //local olduðu için var diyicez ve 
        var bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        bomb.AddForce(turret.forward * bombSpeed);
    }

    //parametre olarak oluþturduðumuz moveAxis deðerini verdik ve maintexture'nin offsetini arttýrýyoruz
    protected void createMoveEffect(float moveAxis)
    {
        matOfset.mainTextureOffset += new Vector2(moveAxis, 0);
    }
}
