using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EnemiesStack _stack;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _speed;

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerLevel _playerLevel;
    private ModelController _modelController;
    private PlayerAnimationController _animatorController;

    public EnemiesStack Stack => _stack;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        _animatorController = GetComponent<PlayerAnimationController>();
        _playerLevel = GetComponent<PlayerLevel>();
        _modelController = GetComponent<ModelController>();
    }

    private void Update()
    {
        Vector2 input = _playerInput.actions["move"].ReadValue<Vector2>();
        Vector3 move = new(input.x, 0, input.y);

        if (move.x != 0.0f || move.z != 0.0f) 
        {
            _animatorController.ChangeState(PlayerAnimationState.Run);
            transform.rotation = Quaternion.LookRotation(move);
        }
        
        if(move.x == 0.0f || move.z == 0.0f)
        { 
            _animatorController.ChangeState(PlayerAnimationState.Idle);
        }

        _characterController.Move(move * Time.deltaTime * _speed);
        _cameraPivot.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyController = other.GetComponent<EnemyController>();

        if (enemyController == null) return;

        if(enemyController.Alive) _animatorController.ChangeState(PlayerAnimationState.Punch);
        enemyController.GetPunched();
        StartCoroutine(CatchEnemy(enemyController));
    }

    private IEnumerator CatchEnemy(EnemyController enemyController)
    {
        Debug.Log("enemy " + enemyController);
        yield return new WaitForSeconds(1f);
        _stack.StackEnemy(enemyController);
    }

    public void PlayerLevelUp()
    {
        if(!_playerLevel.LevelUp()) return;

        _modelController.ChangeModel(_playerLevel.CurBracket.Material);
        _stack.ChangeCapacity(_playerLevel.CurBracket.Capacity);
        GameManager.Instance.PlayerLevelUpCost(_playerLevel.CurBracket.Cost);
    }
}
