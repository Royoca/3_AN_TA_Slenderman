using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOncoliision : MonoBehaviour
{

    BoxCollider BX_Collider;

    MeshRenderer Mesh;

    // Start is called before the first frame update
    void Start()
    {
        BX_Collider = GetComponent<BoxCollider>();
        Mesh = GetComponent<MeshRenderer>();
        BX_Collider.enabled = true;
        Mesh.enabled = false;
        ActivateLeafs(false);
        Debug.Log("Maquina");
    }

    void ActivateLeafs(bool active)
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Choco");
        if (other.gameObject.tag == "Activator")
        {
            Mesh.enabled = true;
            ActivateLeafs(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("naur Choco");
        if (other.gameObject.tag == "Activator"){
            Mesh.enabled = false;
            ActivateLeafs(false);
        }
    }
}
