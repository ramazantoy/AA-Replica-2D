using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donme : MonoBehaviour
{
    [SerializeField]// arayüzde görünün fakat diğer scriptler tarafından görünmez
    float donme_hizi = 1;

    void Update()
    {
        transform.Rotate(0, 0, donme_hizi * Time.deltaTime);
        
    }
}
