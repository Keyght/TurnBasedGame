using UnityEngine;

public class Character : MonoBehaviour, IHealthCange
{
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

        FlipOpponnents();
    }
    void Start()
    {
        _health.HealthChanged += OnHealthChanged;
        _animCtrl = GetComponent<Animator>();
    }

    private void FlipOpponnents()
    {
        if (isEnemy(transform))
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
            action.Health = _health;
            action.PerformAction();
            Destroy(collision.gameObject);
        }
    }

    public static bool isEnemy(Transform curTransform)
    {
        return curTransform.position.z > 0 ? true: false;
    }

    public Health GetHealth()
    {
        return _health;
    }

    public Transform GetActionPoint()
    {
        return _actionPoint;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}
