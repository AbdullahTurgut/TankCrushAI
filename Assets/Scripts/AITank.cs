using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//burda Yapay zekamýza Nav Mesh Agent componenti verdik o yüzden þu kütüphane
using UnityEngine.AI;


//AITank Tankdan türetilcek o yüzden 
public class AITank : Tank
{
    //nav mesh agent bileþenini aldýk
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }

    public Animator fsm { get { return GetComponent<Animator>(); } }
    //public Transform target;
    //waypointler için bir dizi 
    public Transform[] wayPoints;
    Vector3[] wayPointPositions;
    int index;

    private void Start()
    {
        wayPointPositions = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPointPositions[i] = wayPoints[i].position;
        }
    }
    internal void FindNewWaypoint()
    {
        switch (index)
        {
            case 0:
                index = 1;
                break;
            case 1:
                index = 0;
                break;
        }
        agent.SetDestination(wayPointPositions[index]);
    }

    //move kýsmý update'de oluþturuluyor
    protected override void Move()
    {
        //fsm anim controllerde oluþturduðumuz float distance için 
        // yapayzeka tanký ile player tank arasýndaki distance(uzaklýðý) aldýk ve bunu gönderdik
        float distance = Vector3.Distance(transform.position,other.position);
        fsm.SetFloat("Distance", distance);

        //waypointler arasýndaki mesafeyi hesaplamak için
        float distanceFromWayPoint = Vector3.Distance(transform.position, wayPointPositions[index]);
        fsm.SetFloat("distanceFromCurrentWaypoint", distanceFromWayPoint);
    }

    //behaviourslar için 
    //buraya bir gecikme tanýmlýycaz
    float delayed;
    internal void Shooting()
    {
        //Bu þekilde her saniyede bir ateþ ettirebiliriz yapay zekamýza
        if((delayed += Time.deltaTime) > 1f)
        {
            Shoot();
            delayed = 0;
        }
        
    }

    internal void Patrol()
    {
        //LookAt(other);
        agent.SetDestination(wayPointPositions[index]);
    }

    //turret'in otomatik düþmaný görmesi ve ateþ etmemiz için
    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 5f)
        {
            //döndürme ekseni belli olmasaydý eðer 
            //turret.forward ile (other.position - transform.position) bu iki ekseni prosproduct çarpmamýz gerekirdi
            //ve bu sayede 3. bir eksen elde etmemiz gerekiyordu ve 3.eksen etrafýnda bunu yapmamýz gerekiyordu
            turret.Rotate(turret.up, 4);
            yield return null;
        }
        //dönüp ateþ ettiriyorduk normalde bunlarý baðýmsýz olarak yapmaya karar verdik 
        // Shoot();
    }
   

    internal void Chase()
    {
        //Takip etme kýsmýný buraya alýyoruz 
        //Burda setDestination ile yapay zekamýza varýcalak olan hedefi yani playertanký vericez
        agent.SetDestination(other.position);
       
    }
}
