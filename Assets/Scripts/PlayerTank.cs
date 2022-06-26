using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//playerTank Tankdan türetilcek o yüzden 
public class PlayerTank : Tank
{
    Vector3 touchPoint=Vector3.zero;
    Camera main;
    Vector3 moveDir;
    private void Start()
    {
        main = Camera.main;
    }
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
        Shoot();
    }

    protected override void Move() 
    {
        //yön kýsmý için inputla yapýcaz 
        //burda getAxis smooth bir deðer yani daha yumuþak bir dönüþ için kullanýyoruz 
        //getAxisRow direk 1 ve -1 döndürüyor ara deðerler olmuyor o yüzden sert dönüþ yapýyor
        float moveAxis = Input.GetAxis("Vertical");//ileri geri x ekseninde w s tuslarýmýz için
        float rootAxis = Input.GetAxis("Horizontal");//buda  a ve d tuþlarýndan döndürmek için
        //burda tanký ileri doðru hareket ettiricez o yüzden
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime*moveAxis);
        //burdada tankýn yönünü ayarlýycaz
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * rootAxis * rootSpeed * Time.deltaTime));
        createMoveEffect(moveAxis);// tekerlerin effecti için

        //tank içinde yazdýðýmýz düþmaný hedefliycek turret fonksiyonu için
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LookAt(other)); 
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetTouchPoint();
            GoToTouchPoint();
        }
           
       
    }

    private void SetTouchPoint()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            touchPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            moveDir = touchPoint - transform.position;//gideceði yöne için týklanan nokta ve bulunduðu konumun çýkarýmýný yaptýk

        }
    }
    private void GoToTouchPoint()
    {
        //transform.position += moveDir * moveSpeed;
        transform.position = Vector3.Lerp(transform.position, touchPoint, Time.deltaTime * moveSpeed);
        Quaternion lookRotation = Quaternion.LookRotation(moveDir);//mouse sol týkýn olduðu yere dönerek gitmesi için

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }

   
}
