using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorakManager : MonoBehaviour
{
    #region Singleton

    public static MorakManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject Morak;
}
