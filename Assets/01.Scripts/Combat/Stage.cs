using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public Transform startPosTrm;
    public float clearTime;
     
    private void OnValidate()
    {
        enemyList = GetComponentsInChildren<Enemy>().ToList();
    }
}
