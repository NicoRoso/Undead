using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float _minWalkableDistance;
    [SerializeField] private float _maxWalkableDistance;

    [SerializeField] private float _reachedPointDistance;

    [SerializeField] private GameObject _roamTarget;

    [SerializeField] private float _targetFollowRange;

    [SerializeField] private float _stopTargetFollowingRange;

    [SerializeField] private BossAttack _enemyAttack;


    [SerializeField] private AIDestinationSetter _aiDestinationSetter;

    [SerializeField] private BossAnimator _enemyAnimator;

    [SerializeField] private BossHp _enemyHp;

    [SerializeField] private AIPath _aiPath;

    private Player _player;

    private EnemyStates _currentState;
    private Vector3 _roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();

        _currentState = EnemyStates.Roaming;

        _roamPosition = GenerateRoamPosition();

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

                _aiDestinationSetter.target = _roamTarget.transform;

                TryFindPlayer();
                _enemyAnimator.IsWalking(true);
                _enemyAnimator.IsRunning(false);

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= 5f)
                {
                    _aiPath.maxSpeed = 0;
                    _enemyAnimator.IsRunning(false);
                }
                else
                {
                    _aiPath.maxSpeed = 5;
                    _enemyAnimator.IsRunning(true);
                }

                break;

            case EnemyStates.Following:
                _aiDestinationSetter.target = _player.transform;

                _enemyAnimator.IsWalking(false);
                _enemyAnimator.IsRunning(true);

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= 4f)
                {
                    _aiPath.maxSpeed = 0;
                    _enemyAnimator.IsRunning(false);
                }
                else
                {
                    _aiPath.maxSpeed = 4;
                    _enemyAnimator.IsRunning(true);
                }

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _enemyAttack.AttackRange)
                {
                    _enemyAnimator.IsWalking(false);
                    _enemyAnimator.IsRunning(true);

                    if (_enemyAttack.CanAttack)
                    {
                        _enemyAnimator.IsWalking(false);
                        _enemyAnimator.IsRunning(false);
                        _enemyAttack.TryAttackPlayer();
                        _enemyAnimator.PlayerAttack();
                    }

                    if (_enemyAttack.CanAttackMagic && _enemyAttack._secondPhase)
                    {
                        _enemyAttack.SecondPhaseAttack();
                    }
                }

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) >= _stopTargetFollowingRange)
                {
                    _currentState = EnemyStates.Roaming;
                }

                break;
        }
    }

    public void ChangeSpeed()
    {
        _aiPath.maxSpeed -= 1;
    }

    private void TryFindPlayer()
    {
        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _targetFollowRange)
        {
            _currentState = EnemyStates.Following;
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

    public enum EnemyStates
    {
        Roaming,
        Following
    }
}
