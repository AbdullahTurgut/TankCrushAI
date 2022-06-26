using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    //burda perspective aslýnda görüþ duyumuzu temsil edicek

    //açýyý temsil edicek deðiþken
    float fieldOfView;
    //ýþýn gönderirken gerekicek deðiþken uzunluk için
    float maxCheckDistance;
    //tabi birde hedef lazým 
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
        //other objemizle þuanki scripte sahip olucak objemiz arasýndaki fark vektörünü aldýk
        //yön için
        //Büyüklüðü ile ilgilenmediðimiz için normalized ettik ve artýk yönü deðiþmiycek þekilde bir 
        //birim vektörü oldu
        Vector3 dir = (other.transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, dir, Color.white);
        //þimdi açý hesabýmýz için 
        //burdada mantýk þuanki vectörümüzün önünden bakma gerçekleþeceði için forward'ý ile
        //dir arasýndaki açýyý bulduk
        float angle = Vector3.Angle(dir, transform.forward);
        //burda bir transform.forward içinde bi drawline vericek olursak 
        Debug.DrawRay(transform.position, transform.forward * maxCheckDistance, Color.blue);

        Debug.DrawRay(transform.position, dir * maxCheckDistance, Color.red);
        //burdada bulduðumuz açý eðerki görüþ açýmýzýn yarýsýnda küçükse içeridedir 
        if (angle < fieldOfView / 2f)
        {
            //çizilen ýþýn için bulunduðumuz noktadan karþý tarafa olucak þekilde
            Ray ray = new Ray(transform.position, dir);

            
            if (Physics.Raycast(ray,out RaycastHit hitInfo,maxCheckDistance))
            {
                //burdaki if koþulumuz aslýnda burda çarpýlan bir obje varsa true döndürür

                //burdada oluþturduðumuz ýþýný gösterdik 
                //bu ýþýn verdiðimiz max uzaklýk boyutu kadar olucak 
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
