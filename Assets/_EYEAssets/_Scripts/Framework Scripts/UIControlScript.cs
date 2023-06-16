using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControlScript : MonoBehaviour
{
    private AudioManager _audioManager;

    //Player Score
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private TMP_Text _endGameMessage;
    
    [SerializeField] private TMP_Text _scoreAmount;
    private int _currentScore;

    //Enemy Score
    [SerializeField]
    private TMP_Text _enemyScoreAmount;
    private int _currentEnemyScore;
    private int _enemyGameOverPoint;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        if (_audioManager == null)
            Debug.Log("AudioManager = NULL");
        StartCoroutine(ClearMessageText());
    }
    
    public void UpdateScore(int score)
    {
        _currentScore = _currentScore + score;
        //Debug.Log("Updating Score " + _currentScore);
        _scoreAmount.text = _currentScore.ToString();
        UpdateScoreMessage(_currentScore);
    }

    private void UpdateScoreMessage(int currentscore)
    {
        string message = " ";
        switch(currentscore)
        {
            case 1:
                message = "Good Start";
                    break; 
            case 2:
                message = "Not Bad";
                    break; 
            case 3: 
                message = "Getting Better";
                    break; 
            case 4: 
                message = "You're On Your Way";
                    break; 
            case 5: 
                message = "Halfway There";
                    break; 
            case 6:
                message = "Better Than Average";
                    break; 
            case 7: 
                message = "Shooting Star";
                    break; 
            case 8: 
                message = "Dynamite";
                    break; 
            case 9:
                message = "Crack Shot";
                    break; 
            case 10: 
                message = "Perfect Score";
                _audioManager.PlayAudioClip(4);
                _endGameMessage.text = "You Win!";
                    break;
            default: 
                break;
        }
        _messageText.text = message; 
    }

    public void UpdateEnemyScore(int enemyScore)
    {
        _currentEnemyScore= _currentEnemyScore + enemyScore;
        _enemyScoreAmount.text= _currentEnemyScore.ToString();
        if (_currentEnemyScore >= (_enemyGameOverPoint / 2))
            _endGameMessage.text = "GAME OVER!";        
    }

    public void WhatIsEnemyMax(int enemyCount)
    {
        _enemyGameOverPoint = enemyCount;
    }

    IEnumerator ClearMessageText()
    {        
        yield return new WaitForSeconds(3);
        _messageText.text = "";
    }
}
