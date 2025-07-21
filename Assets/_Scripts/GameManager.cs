using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private LevelUpSpot _leveUpSpot;
    [SerializeField] private int _playerLevel;
    private int _playerMoney = 0;

    public int PlayerLevel => _playerLevel;
    public int PlayerMoney => _playerMoney;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdatePlayerMoney()
    {
        _moneyText.text = "Money: " + _playerMoney.ToString();
    }

    public void EnemiesCollected(int val)
    {
        _playerMoney += val * 100;
        UpdatePlayerMoney();
    }

    public void PlayerLevelUpCost(int cost)
    {
        _playerMoney -= cost;
        _playerLevel++;
        UpdatePlayerMoney();
    }
}
