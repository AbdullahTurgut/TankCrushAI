using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    //burda perspective asl�nda g�r�� duyumuzu temsil edicek

    //a��y� temsil edicek de�i�ken
    float fieldOfView;
    //���n g�nderirken gerekicek de�i�ken uzunluk i�in
    float maxCheckDistance;
    //tabi birde hedef laz�m 
    public Transform other;
    Animator fsm;
    public override void InitializeSense()
    {
        fieldOfView = 60f;
        maxCheckDistance = 20f;
        fsm = GetComponent<Animator>();
    }

    public override void UpdateSense()
    {
        //other objemizle �uanki scripte sahip olucak objemiz aras�ndaki fark vekt�r�n� ald�k
        //y�n i�in
        //B�y�kl��� ile ilgilenmedi�imiz i�in normalized ettik ve art�k y�n� de�i�miycek �ekilde bir 
        //birim vekt�r� oldu
        Vector3 dir = (other.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, dir, Color.white);
        //�imdi a�� hesab�m�z i�in 
        //burdada mant�k �uanki vect�r�m�z�n �n�nden bakma ger�ekle�ece�i i�in forward'� ile
        //dir aras�ndaki a��y� bulduk
        float angle = Vector3.Angle(dir, transform.forward);
        //burda bir transform.forward i�inde bi drawline vericek olursak 
        Debug.DrawRay(transform.position, transform.forward * maxCheckDistance, Color.blue);

        Debug.DrawRay(transform.position, dir * maxCheckDistance, Color.red);
        //burdada buldu�umuz a�� e�erki g�r�� a��m�z�n yar�s�nda k���kse i�eridedir 
        if (angle < fieldOfView / 2f)
        {
            //�izilen ���n i�in bulundu�umuz noktadan kar�� tarafa olucak �ekilde
            Ray ray = new Ray(transform.position, dir);

            
            if (Physics.Raycast(ray,out RaycastHit hitInfo,maxCheckDistance))
            {
                //burdaki if ko�ulumuz asl�nda burda �arp�lan bir obje varsa true d�nd�r�r

                //burdada olu�turdu�umuz ���n� g�sterdik 
                //bu ���n verdi�imiz max uzakl�k boyutu kadar olucak 
                Debug.DrawRay(transform.position, dir * maxCheckDistance, Color.green);
                string name = hitInfo.transform.name;
                if (name.Equals("playerTank"))
                {
                    fsm.SetBool("Visibility", true);
                }
                else
                {
                    fsm.SetBool("Visibility", false);
                }

            }
            else
            {
                fsm.SetBool("Visibility", false);
            }
        }
        
    }
}
