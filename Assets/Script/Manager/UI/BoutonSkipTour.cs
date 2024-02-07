using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
public class BoutonSkipTour : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.FinfDuTour();
    }
}
