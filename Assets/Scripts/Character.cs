using UnityEngine;

public class Character : MonoBehaviour, IHealthCange
{
    [SerializeField]
    private int _maxHP = 10;
    [SerializeField]
    private GameObject _hPCanvas;

    private Health _health;
    private Animator _animCtrl;

    private void Awake()
    {
        _health = new Health(_maxHP);
    }
    void Start()
    {
        FlipOpponnents();

        _health.HealthChanged += OnHealthChanged;
        _animCtrl = GetComponent<Animator>();
    }

    private void FlipOpponnents()
    {
        if (transform.position.z > 0)
        {
            transform.Rotate(0f, 180.0f, 0.0f, Space.Self);
            _hPCanvas.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnHealthChanged(int currentHealth, float currentHealthAsPercantage)
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
            action.Health = _health;
            action.PerformAction();
            Destroy(collision.gameObject);
        }
    }

    public Health GetHealth()
    {
        return _health;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}
