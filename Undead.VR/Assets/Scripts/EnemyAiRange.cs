using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAiRange : MonoBehaviour
{
    [SerializeField] private float _minWalkableDistance;
    [SerializeField] private float _maxWalkableDistance;
    [SerializeField] private float _reachedPointDistance;
    [SerializeField] private GameObject _roamTarget;
    [SerializeField] private float _targetFollowRange;
    [SerializeField] private float _stopTargetFollowingRange;
    [SerializeField] private EnemyAttackRange _enemyAttack;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private AIDestinationSetter _aiDestinationSetter;
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private AudioSource _audio;
    [Header("Audio")]
    [SerializeField] private AudioClip[] _audioAttackClips;

    private Player _player;
    private bool _musicOff;
    private EnemyStates _currentState;
    private Vector3 _roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _currentState = EnemyStates.Roaming;
        _roamPosition = GenerateRoamPosition();
        _audio = GetComponent<AudioSource>();
        _musicOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case EnemyStates.Roaming:
                _roamTarget.transform.position = _roamPosition;
                if (Vector3.Distance(gameObject.transform.position, _roamPosition) <= _reachedPointDistance)
                {
                    _roamPosition = GenerateRoamPosition();
                }
                _enemyAnimator.IsWalking(true);
                _enemyAnimator.IsRunning(false);
                _aiPath.maxSpeed = 3;
                TryFindPlayer();
                break;

            case EnemyStates.Following:
                _aiDestinationSetter.target = _player.transform;

                _enemyAnimator.IsWalking(false);
                _enemyAnimator.IsRunning(false);
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < (_enemyAttack.AttackRange - 2))
                {
                    _aiPath.maxSpeed = 3;
                    _enemyAnimator.IsRunning(true);
                    Vector3 escapeDirection = gameObject.transform.position + (gameObject.transform.position - _player.transform.position).normalized * 2f;
                    GameObject destinationCords = new GameObject();
                    destinationCords.transform.position = escapeDirection;
                    _aiDestinationSetter.target = destinationCords.transform;
                }
                else if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _enemyAttack.AttackRange)
                {
                    _aiPath.maxSpeed = 0;
                    if (_enemyAttack.CanAttack)
                    {
                        transform.LookAt(_player.transform.position);
                        _enemyAttack.TryAttackPlayer();
                        _enemyAnimator.PlayerAttack();
                    }
                }
                else
                {
                    _aiPath.maxSpeed = 3;
                    _enemyAnimator.IsRunning(true);
                }
                break;
        }
    }

    private void SoundAttack()
    {
        AudioClip clipAttack = _audioAttackClips[Random.Range(0, _audioAttackClips.Length)];
        _audio.PlayOneShot(clipAttack);
        _musicOff = false;
    }

    public void ChangeSpeed()
    {
        _aiPath.maxSpeed -= 1;
    }

    private void TryFindPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _targetFollowRange)
        {
            _enemyAnimator.IsWalking(false);
            _enemyAnimator.IsRunning(true);
            _currentState = EnemyStates.Following;
            if (_musicOff)
            {
                SoundAttack();
            }
        }
    }
    private Vector3 GenerateRoamPosition()
    {
        var roamPosition = gameObject.transform.position + GenerateRandomDirection() * GenerateRandomWalkableDistance();
        return roamPosition;
    }

    private float GenerateRandomWalkableDistance()
    {
        var randomDistance = Random.Range(_minWalkableDistance, _maxWalkableDistance);
        return randomDistance;
    }

    private Vector3 GenerateRandomDirection()
    {
        var newDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        return newDirection.normalized;

    }

    private enum EnemyStates
    {
        Roaming,
        Following
    }
}