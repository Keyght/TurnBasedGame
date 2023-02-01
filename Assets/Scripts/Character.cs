using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
    POISONED,
    DEFENDED
}

public class Character : MonoBehaviour, IHealthCange
{
    public Dictionary<Effect, int> Effects;
    [SerializeField]
    private int _maxHP = 10;
    [SerializeField]
    private GameObject _hPCanvas;
    [SerializeField]
    private Transform _actionPoint;

    private Health _health;
    private Animator _animCtrl;

    private void Awake()
    {
        _health = new Health(_maxHP);
        Effects = new Dictionary<Effect, int>();

        FlipOpponnents();
    }
    void Start()
    {
        _health.HealthChanged += OnHealthChanged;
        _animCtrl = GetComponent<Animator>();
    }

    private void FlipOpponnents()
    {
        if (isEnemy(transform.position))
        {
            transform.Rotate(0f, 180.0f, 0.0f, Space.Self);
            _hPCanvas.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnHealthChanged(int currentHealth, int currentAdditionalHealth, float currentHealthAsPercantage)
    {
        if (currentHealth == 0)
        {
            _animCtrl.SetBool("isDead", true);
            _animCtrl.SetTrigger("Death");
        }
        else
        {
            _animCtrl.SetBool("isDead", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Action action))
        {
            bool performable;
            action.SetTarget(this, out performable);
            if (performable)
            {
                action.PerformAction();
                Destroy(collision.gameObject);
            }
        }
    } 

    public static bool isEnemy(Vector3 currPos)
    {
        return currPos.z > 0 ? true: false;
    }

    public Health GetHealth()
    {
        return _health;
    }

    public Transform GetActionPoint()
    {
        return _actionPoint;
    }

    public Animator GetAnimator()
    {
        return _animCtrl;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}
