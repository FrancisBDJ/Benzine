using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositManager : MonoBehaviour
{
    [SerializeField]public float benzineDiposit;

    public float ReadBenzine()
    {
        return benzineDiposit;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        benzineDiposit = Random.Range(1.0f, 5.0f);
    }
    
}
