using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    //Distancia para detectar a arma
    public float distance = 10f;

    //Destruir o objeto no chão depois de pego
    GameObject droppedWeapon;

    bool canGrab;
    string arma = "";

    //Armas para serem ativadas ou desativadas
    public GameObject m1Garant;
    public GameObject Thompson;

    private void Update()
    {
        CheckWeapons();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                ActivateGun();

            }
        }
    }

    private void CheckWeapons()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "CanGrab")
            {
                canGrab = true;

                droppedWeapon = hit.transform.gameObject;

                arma = hit.transform.name;
                Debug.Log("Arma: " + arma);

            }
        }
        else
            canGrab = false;
    }

    private void ActivateGun()
    {

        if(arma == "M1Garand")
        {
            m1Garant.SetActive(true);
            Thompson.SetActive(false);
        }
        else if(arma == "Thompson")
        {
            m1Garant.SetActive(false);
            Thompson.SetActive(true);
        }

        if(droppedWeapon != null)
        Destroy(droppedWeapon);
    }
}
