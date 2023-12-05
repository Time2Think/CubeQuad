using System.Collections.Generic;
using Dreamteck.Splines;
using GamePlay;
using UI;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField] 
    private SplineFollower _splineFollower;
    [SerializeField]
    private List<SplineComputer> _allLevels;

    private SplineComputer _activeLevel;
    
    private int _presentLevel;
    private PlayerController _playerController;
    private ProgressManager _progressManager;
   
    
    [Inject]
    private void Construct(ProgressManager progressManager)
    {
        _progressManager = progressManager;
    }

    private void Awake()
    {
        _playerController = _splineFollower.GetComponentInChildren<PlayerController>();
        _presentLevel = _progressManager.LoadProgress();
        _uiManager.InitUI();
        InitLevel();
        Subscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
    
    private void Subscribe()
    {
        _uiManager.RestartGame += RestartLevel;
        _uiManager.StartGame += StartLevel;
        _uiManager.NextLevel += StartNextLevel;
        _playerController.Win += LevelWin;
        _playerController.Die += LevelLose;
    }

    private void UnSubscribe()
    {
        _uiManager.RestartGame -= RestartLevel;
        _uiManager.StartGame -= StartLevel;
        _uiManager.NextLevel -= StartNextLevel;
        _playerController.Win -= LevelWin;
        _playerController.Die -= LevelLose;
    }

    private void InitLevel()
    {
        _uiManager.InitStartPanel(_presentLevel);
        if (_activeLevel != null)
        {
            Destroy(_activeLevel.gameObject);
        }
        _activeLevel = Instantiate(_allLevels[_presentLevel], transform.position, Quaternion.identity);
        _playerController.InitPlayer(_activeLevel);
        _splineFollower.SetDistance(20f);
    }
    
    private void StartLevel()
    {
        _playerController.StartRun();
    }

    private void RestartLevel()
    {
        InitLevel();
    }

    private void StartNextLevel()
    {
        _presentLevel++;
        if (_presentLevel >= _allLevels.Count)
        {
            _presentLevel = 0;
        }
        _progressManager.SaveProgress(_presentLevel);
        InitLevel();
    }
    private void LevelWin()
    {
        _uiManager.WinGame();
    }
    
    private void LevelLose()
    {
        _uiManager.LoseGame();
    }
}
