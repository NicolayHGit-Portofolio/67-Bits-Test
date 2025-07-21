using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesStack : MonoBehaviour
{
    [SerializeField] private float _stackHeight = 1.0f;
    [SerializeField] private int _carryCapacity = 1;

    private float _followSpeed = 5f;
    private float _stackOffsetY = 1.2f;

    private List<EnemyController> _stack = new();

    void Update()
    {
        for (int i = 0; i < _stack.Count; i++)
        {
            Transform target = (i == 0) ? transform : _stack[i - 1].transform;
            Vector3 desiredPos = target.position + Vector3.up * _stackOffsetY;

            _stack[i].transform.position = Vector3.Lerp(_stack[i].transform.position, desiredPos, Time.deltaTime * _followSpeed);

            Quaternion desiredRot = Quaternion.LookRotation(target.forward);
            _stack[i].transform.rotation = Quaternion.Slerp(_stack[i].transform.rotation, desiredRot, Time.deltaTime * _followSpeed);
        }
    }

    private IEnumerator MoveBodiesFromStack(Transform target, float interval = 0.5f)
    {
        List<EnemyController> tempStack = _stack;
        foreach (var enemy in tempStack)
        {
            StartCoroutine(enemy.MoveEnemyToLocation(target, Vector3.zero, 0.6f, 1.5f, true));
            yield return new WaitForSeconds(interval);
        }
        yield return null;
    }

    public void ChangeCapacity(int capacity)
    {
        _carryCapacity = capacity;
    }
    
    public void StackEnemy(EnemyController enemy)
    {
        if (enemy == null) return;
        if(_stack.Count + 1 > _carryCapacity) return;

        enemy.EnableKinematic();

        Transform last = _stack.Count == 0 ? transform : _stack.Last().transform;

        Vector3 targetPosMod = Vector3.up * _stackHeight;

        _stack.Add(enemy);
        StartCoroutine(enemy.MoveEnemyToLocation(last, targetPosMod, 0.6f, 1.5f));
    }

    public int CollectEnemies(Transform collector)
    {
        if(_stack == null) return 0;

        int count = _stack.Count;

        StartCoroutine(MoveBodiesFromStack(collector));

        _stack = new();

        return count;
    }
}
