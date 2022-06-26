using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//playerTank Tankdan t�retilcek o y�zden 
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
            //d�nd�rme ekseni belli olmasayd� e�er 
            //turret.forward ile (other.position - transform.position) bu iki ekseni prosproduct �arpmam�z gerekirdi
            //ve bu sayede 3. bir eksen elde etmemiz gerekiyordu ve 3.eksen etraf�nda bunu yapmam�z gerekiyordu
            turret.Rotate(turret.up, 4);
            yield return null;
        }
        //d�n�p ate� ettiriyorduk normalde bunlar� ba��ms�z olarak yapmaya karar verdik 
        Shoot();
    }

    protected override void Move() 
    {
        //y�n k�sm� i�in inputla yap�caz 
        //burda getAxis smooth bir de�er yani daha yumu�ak bir d�n�� i�in kullan�yoruz 
        //getAxisRow direk 1 ve -1 d�nd�r�yor ara de�erler olmuyor o y�zden sert d�n�� yap�yor
        float moveAxis = Input.GetAxis("Vertical");//ileri geri x ekseninde w s tuslar�m�z i�in
        float rootAxis = Input.GetAxis("Horizontal");//buda  a ve d tu�lar�ndan d�nd�rmek i�in
        //burda tank� ileri do�ru hareket ettiricez o y�zden
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime*moveAxis);
        //burdada tank�n y�n�n� ayarl�ycaz
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * rootAxis * rootSpeed * Time.deltaTime));
        createMoveEffect(moveAxis);// tekerlerin effecti i�in

        //tank i�inde yazd���m�z d��man� hedefliycek turret fonksiyonu i�in
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
            moveDir = touchPoint - transform.position;//gidece�i y�ne i�in t�klanan nokta ve bulundu�u konumun ��kar�m�n� yapt�k

        }
    }
    private void GoToTouchPoint()
    {
        //transform.position += moveDir * moveSpeed;
        transform.position = Vector3.Lerp(transform.position, touchPoint, Time.deltaTime * moveSpeed);
        Quaternion lookRotation = Quaternion.LookRotation(moveDir);//mouse sol t�k�n oldu�u yere d�nerek gitmesi i�in

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }

   
}
