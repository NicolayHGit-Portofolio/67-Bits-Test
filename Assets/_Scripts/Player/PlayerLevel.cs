using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private List<PlayerLevelBracket> _playerLevelsBracket;

    private int _currentLevel = 0;

    public int CurrentLevel => _currentLevel;

    public PlayerLevelBracket CurBracket => _playerLevelsBracket[_currentLevel];

    public bool LevelUp()
    {
        int nextLevel = _currentLevel + 1;
        if (_playerLevelsBracket[nextLevel] == null) return false;
        if (GameManager.Instance.PlayerMoney < _playerLevelsBracket[nextLevel].Cost) return false;

        _currentLevel++;
        return true;
    }
}

[Serializable]
public class PlayerLevelBracket
{
    [SerializeField] private Material _mat;
    [SerializeField] private int _carryCapacity;
    [SerializeField] private int _cost;

    public Material Material => _mat;
    public int Capacity => _carryCapacity;
    public int Cost => _cost;
}
