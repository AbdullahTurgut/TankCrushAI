using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//burda Yapay zekam�za Nav Mesh Agent componenti verdik o y�zden �u k�t�phane
using UnityEngine.AI;


//AITank Tankdan t�retilcek o y�zden 
public class AITank : Tank
{
    //nav mesh agent bile�enini ald�k
    public NavMeshAgent agent { get { return GetComponent<NavMeshAgent>(); } }

    public Animator fsm { get { return GetComponent<Animator>(); } }
    //public Transform target;
    //waypointler i�in bir dizi 
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

    //move k�sm� update'de olu�turuluyor
    protected override void Move()
    {
        //fsm anim controllerde olu�turdu�umuz float distance i�in 
        // yapayzeka tank� ile player tank aras�ndaki distance(uzakl���) ald�k ve bunu g�nderdik
        float distance = Vector3.Distance(transform.position,other.position);
        fsm.SetFloat("Distance", distance);

        //waypointler aras�ndaki mesafeyi hesaplamak i�in
        float distanceFromWayPoint = Vector3.Distance(transform.position, wayPointPositions[index]);
        fsm.SetFloat("distanceFromCurrentWaypoint", distanceFromWayPoint);
    }

    //behaviourslar i�in 
    //buraya bir gecikme tan�ml�ycaz
    float delayed;
    internal void Shooting()
    {
        //Bu �ekilde her saniyede bir ate� ettirebiliriz yapay zekam�za
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

    //turret'in otomatik d��man� g�rmesi ve ate� etmemiz i�in
    protected override IEnumerator LookAt(Transform other)
    {
        while (Vector3.Angle(turret.forward, (other.position - transform.position)) > 5f)
        {
            //d�nd�rme ekseni belli olmasayd� e�er 
            //turret.forward ile (other.position - transform.position) bu iki ekseni prosproduct �arpmam�z gerekirdi
            //ve bu sayede 3. bir eksen elde etmemiz gerekiyordu ve 3.eksen etraf�nda bunu yapmam�z gerekiyordu
            turret.Rotate(turret.up, 4);
            yield return null;
        }
        //d�n�p ate� ettiriyorduk normalde bunlar� ba��ms�z olarak yapmaya karar verdik 
        // Shoot();
    }
   

    internal void Chase()
    {
        //Takip etme k�sm�n� buraya al�yoruz 
        //Burda setDestination ile yapay zekam�za var�calak olan hedefi yani playertank� vericez
        agent.SetDestination(other.position);
       
    }
}
