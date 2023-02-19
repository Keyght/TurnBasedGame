using System.Collections.Generic;
using System;
using Actions;
using Health;
using UnityEngine;

/// <summary>
/// Класс для описания поведения персонажа
/// </summary>
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour, IHealthCange
{
    public event Action<bool> OnDeath;
    public bool IsDead => _isDead;
    public Dictionary<Effect, int> Effects;
    [SerializeField] private int _maxHp = 10;
    [SerializeField] private GameObject _hPCanvas;
    [SerializeField] private Transform _actionPoint;

    private Health.Health _health;
    private Animator _animCtrl;
    private bool _isDead;
    private static readonly int _dead = Animator.StringToHash("isDead");
    private static readonly int _death = Animator.StringToHash("Death");

    public Health.Health Health => _health;
    public Animator AnimCtrl => _animCtrl;
    public Transform ActionPoint => _actionPoint;

    private void Awake()
    {
        _health = new Health.Health(_maxHp);
        Effects = new Dictionary<Effect, int>();

        FlipOpponnents();
    }

    private void Start()
    {
        _isDead = false;
        _health.HealthChanged += OnHealthChanged;
        _animCtrl = GetComponent<Animator>();
    }

    private void FlipOpponnents()
    {
        if (!IsEnemy(transform.position)) return;
        transform.Rotate(0f, 180.0f, 0.0f, Space.Self);
        _hPCanvas.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnHealthChanged(int currentHealth, int currentAdditionalHealth, float currentHealthAsPercantage)
    {
        if (currentHealth == 0)
        {
            _isDead = true;
            _animCtrl.SetBool(_dead, true);
            _animCtrl.SetTrigger(_death);
            OnDeath?.Invoke(_isDead);
        }
        else
        {
            _isDead = false;
            _animCtrl.SetBool(_dead, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var actionObject = collision.gameObject;
        if (actionObject.TryGetComponent(out BaseAction action))
        {
            PerformingAction(action, actionObject);
        }
    }

    public void PerformingAction(BaseAction baseAction, GameObject actionObject)
    {
        baseAction.SetTarget(this, out var performable);
        if (!performable) return;
        baseAction.PerformAction();
        Destroy(actionObject);
    }

    public static bool IsEnemy(Vector3 currPos)
    {
        return currPos.z > 0;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}