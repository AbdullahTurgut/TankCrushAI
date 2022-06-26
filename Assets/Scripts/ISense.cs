using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bu bir class olmadýðý için monobehavior'dan çekmedik birþey
public interface ISense 
{
    //ve yazýcaðýmýz fonksiyonlar concrad olduðu için body'e sahip olamazlar

    //her hangi bir class bu arayüzü uygularsa aþaðýdaki fonksiyonlarý doldurmak zorunda
    void InitializeSense();
    void UpdateSense();

}
