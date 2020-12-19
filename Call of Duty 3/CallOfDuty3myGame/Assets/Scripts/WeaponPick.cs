using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 10f;
    GameObject currentWeapon;
    GameObject wp;

    bool canGrab;

    public GameObject m1Garant;

    private void Update()
    {
        CheckWeapons();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentWeapon != null)
                    Drop();

                Desactivate();

                m1Garant.SetActive(true);
            }
        }

        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Drop();
        }
    }

    private void CheckWeapons()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "CanGrab")
            {
                Debug.Log("I can grab it!");
                canGrab = true;
                wp = hit.transform.gameObject;

            }
        }
        else
            canGrab = false;
    }

    private void Desactivate()
    {
        currentWeapon = wp;
        currentWeapon.SetActive(false);
    }

    private void Drop()
    {
    }
}
