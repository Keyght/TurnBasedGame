using UnityEngine;

namespace Actions
{
    public enum Flag
    {
        Attacking,
        Healing,
        Defending
    }

    /// <summary>
    /// Базовый класс для описания действий
    /// </summary>
    public abstract class BaseAction : MonoBehaviour
    {
        [SerializeField]
        private Flag _flag;
        [SerializeField]
        private GameObject _linePrefab;
        [SerializeField]
        private float _dragSensivity = 1.2f;

        protected Character Target;
        private LineRenderer _line;
        private Camera _mainCamera;
        private Vector3 _mouseOffset;
        private float _mouseZCoord;
        private Vector3 _firstPos;
        private Renderer _meshRenderer;
        private Transform _childOther;
        private bool _isDraggable;
        protected bool IsSelfTargeted = false;
        protected bool IsEnemyTargeted = false;

        public Flag Flag => _flag;
        protected bool IsAttacking => Flag == Flag.Attacking;

        public abstract void PerformAction();

        private void Awake()
        {
            _mainCamera = Camera.main;
            _meshRenderer = GetComponent<Renderer>();
            _childOther = transform.GetChild(0);
        }

        protected void Start()
        {
            if (!Character.IsEnemy(transform.position)) _isDraggable = true;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _mouseZCoord;

            return _mainCamera.ScreenToWorldPoint(mousePoint);
        }

        private void OnMouseDown()
        {
            if (!_isDraggable) return;

            Cursor.visible = false;
            _line = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();

            var position = transform.position;
            _mouseZCoord = _mainCamera.WorldToScreenPoint(position).z;
            _mouseOffset = position - GetMouseWorldPosition();

            _firstPos = position;
            _line.SetPosition(0, _firstPos);

            _meshRenderer.enabled = false;
            _childOther.gameObject.SetActive(false);
        }

        private void OnMouseDrag()
        {
            if (!_isDraggable) return;

            var newPos = (GetMouseWorldPosition() + _mouseOffset) * _dragSensivity;
            var cacheTransform = transform;
            cacheTransform.position = new Vector3(newPos.x, _firstPos.y, newPos.z);

            _line.SetPosition(1, cacheTransform.position);
        }

        private void OnMouseUp()
        {
            if (!_isDraggable) return;

            gameObject.AddComponent<Rigidbody>();
            Destroy(_line.gameObject);
            Cursor.visible = true;
        }

        public void SetTarget(Character target, out bool performable)
        {
            if (IsSelfTargeted)
            {
                if (target.gameObject.Equals(transform.parent.gameObject))
                {
                    Target = target;
                    performable = true;
                }
                else
                {
                    ResetAction();
                    performable = false;
                }
            }
            else if (!_isDraggable)
            {
                Target = target;
                performable = true;
            }
            else if (IsEnemyTargeted)
            {
                if (Character.IsEnemy(target.transform.position))
                {
                    Target = target;
                    performable = true;
                }
                else
                {
                    ResetAction();
                    performable = false;
                }
            }
            else
            {
                performable = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                ResetAction();
            }
        }

        private void ResetAction()
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            var cacheTransform = transform;
            cacheTransform.position = _firstPos;
            cacheTransform.rotation = Quaternion.identity;
            _meshRenderer.enabled = true;
            _childOther.gameObject.SetActive(true);
        }
    }
}