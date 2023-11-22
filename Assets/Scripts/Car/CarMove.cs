using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMove : MonoBehaviour {
    private CarController _controller;
    
    [SerializeField] private Rigidbody _rigidbody;

    public float Speed;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _speedRotation = 1f;

    private Tween Tween;

    private void OnValidate() {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    public void Init(CarController controller) {
        _controller = controller;
        _rigidbody.centerOfMass = transform.position;
    }

    public void FreezeRotation() => _rigidbody.freezeRotation = true;
    public void UnfreezeRotation() => _rigidbody.freezeRotation = false;

    public void StartMove() {
       // Debug.Log("start move");
        _controller.SetCurrentCarState(CarState.MOVING);
        _speed = Speed;
        canMove = true;

    }
    
    public void StopMove() {
        //Debug.Log("stop move");
        _speed = 0f;
        
        Tween = null;
    }

    public void canmovetrue() => canMove = true;
    public void canmoveFalse() => canMove = false;

    public bool canMove;
    private void FixedUpdate() {
        if(canMove)
            _rigidbody.velocity = transform.forward * _speed;
    }

    public void RotateCar(float direction, Turn turn = Turn.DIRECTLY) {
        Tween = _rigidbody.DORotate(new Vector3(0f, transform.eulerAngles.y + 90f * direction, 0f), _speedRotation);
        _controller.SetCurrentTurn(turn);
    }
}