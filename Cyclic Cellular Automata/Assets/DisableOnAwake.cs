using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

}
