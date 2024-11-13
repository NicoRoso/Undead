using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _minWalkableDistance;
    [SerializeField] private float _maxWalkableDistance;

    [SerializeField] private float _reachedPointDistance;

    [SerializeField] private GameObject _roamTarget;

    [SerializeField] private float _targetFollowRange;

    [SerializeField] private float _stopTargetFollowingRange;

    [SerializeField] private EnemyAttack _enemyAttack;

    [SerializeField] private AIDestinationSetter _aiDestinationSetter;

    [SerializeField] private EnemyAnimator _enemyAnimator;

    [SerializeField] private AIPath _aiPath;

    [SerializeField] private AudioSource _audio;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _audioAttackClips;


    private Player _player;

    private bool _MusicOff;

    private EnemyStates _currentState;
    private Vector3 _roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();

        _currentState = EnemyStates.Roaming;

        _roamPosition = GenerateRoamPosition();

        _audio = GetComponent<AudioSource>();

        _MusicOff = true;
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

                _aiPath.maxSpeed = 3;

                break;

            case EnemyStates.Following:
                _aiDestinationSetter.target = _player.transform;

                if (_MusicOff == true)
                {
                    SoundAttack();
                }

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
                    _enemyAnimator.IsRunning(false);

                    if (_enemyAttack.CanAttack)
                    {
                        _enemyAttack.TryAttackPlayer();
                        _enemyAnimator.PlayerAttack();
                    }
                }

                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) >= _stopTargetFollowingRange)
                {
                    _currentState = EnemyStates.Roaming;
                    _MusicOff = true;
                }

                break;
        }
    }

    public void SoundAttack()
    {
        AudioClip clipAttack = _audioAttackClips[UnityEngine.Random.Range(0, _audioAttackClips.Length)];
        _audio.PlayOneShot(clipAttack);
        _MusicOff = false;
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
