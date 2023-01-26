using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Health _health;
    private Animator _animCtrl;

    private void Awake()
    {
        _health = new Health();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _health.ChangeHealth(-1);
        }
    }

    void Start()
    {
        _animCtrl = GetComponent<Animator>();
    }

    public Health GetHealth()
    {
        return _health;
    }
}
