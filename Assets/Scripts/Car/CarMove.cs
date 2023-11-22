using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMove : MonoBehaviour {
    private CarController _controller;
    
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _speedCar;
    [SerializeField] private float _speedRotation = 1f;
    
    private float _speed;
    private bool _canMove;

    private Tween Tween;

    private void OnValidate() {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    public void Init(CarController controller) {
        _controller = controller;
    }

    public void FreezeRotation() => _rigidbody.freezeRotation = true;
    public void UnfreezeRotation() => _rigidbody.freezeRotation = false;

    public void StartMove() {
        _controller.SetCurrentCarState(CarState.MOVING);
        _speed = _speedCar;
        _canMove = true;
    }
    
    public void StopMove() {
        _speed = 0f;
        Tween = null;
    }

    public void SetCanMove(bool value) => _canMove = value;
    
    private void FixedUpdate() {
        if(_canMove)
            _rigidbody.velocity = transform.forward * _speed;
    }

    public void RotateCar(float direction, Turn turn = Turn.DIRECTLY) {
        Tween = _rigidbody.DORotate(new Vector3(0f, transform.eulerAngles.y + 90f * direction, 0f), _speedRotation);
        _controller.SetCurrentTurn(turn);
    }
}