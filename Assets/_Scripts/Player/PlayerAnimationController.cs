using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerAnimationState _playerState = PlayerAnimationState.None;

    private void Start()
    {
        ChangeState(PlayerAnimationState.Idle);
    }

    public void ChangeState(PlayerAnimationState newState)
    {
        if (_playerState == newState) return;
        _playerState = newState;
        _animator.SetTrigger(_playerState.ToString());
    }
}

public enum PlayerAnimationState {None, Idle, Run, Punch}
