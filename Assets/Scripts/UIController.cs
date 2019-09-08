using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public PlayerController playerController;
    public EnemySpawner enemySpawner;

    public Text lifeValue;
    public Text scoreValue;
    public Text playerLevelValue;
    public Text enemyLevelValue;

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (lifeValue)
            lifeValue.text = playerController.Life.ToString();
        if (scoreValue)
            scoreValue.text = Score.GetScore().ToString();
        if (playerLevelValue)
            playerLevelValue.text = playerController.Level.ToString();
        if (enemyLevelValue)
            enemyLevelValue.text = enemySpawner.Level.ToString();

    }
}
