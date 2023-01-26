using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthCange
{
    void OnHealthChanged(int currentHealth, float currentHealthAsPercantage);
}
