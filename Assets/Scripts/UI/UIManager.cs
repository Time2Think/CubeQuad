using System;
using System.Collections.Generic;
using Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class UIManager : MonoBehaviour
   {
      public event Action StartGame;
      public event Action RestartGame;
      public event Action NextLevel;

      [SerializeField] 
      private TutorialPanel _tutorialPanel;
      [SerializeField] 
      private StartPanel _startGamePanel;
      [SerializeField] 
      private WinPanel _winPanel;
      [SerializeField] 
      private LosePanel _losePanel;
      
      [SerializeField]
      private Button _startGameButton;
      [SerializeField]
      private List<Button> _restartGameButtons;
      [SerializeField]
      private Button _startNextLevelButton;
      [SerializeField]
      private TextMeshProUGUI _presentlevel;

      private  Dictionary<PanelType, UIPanel> panelsDictionary = new Dictionary<PanelType, UIPanel>();
      
      private void Awake()
      {
         _startGameButton.onClick.AddListener(StartLevel);
         _startNextLevelButton.onClick.AddListener(StartNextLevel);
         foreach (var restartButton in _restartGameButtons)
         {
            restartButton.onClick.AddListener(RestartLevel);
         }
      }

      public void InitUI()
      {
         RegisterAllPanels();
      }
      
      public void InitStartPanel(int presentLevel)
      {
         ShowPanel(presentLevel == 0 ? PanelType.TutorialPanel : PanelType.StartPanel);

         _presentlevel.text = $"Level: {presentLevel}";
      }


      public void WinGame()
      {
         ShowPanel(PanelType.WinPanel);
      }
      
      public void LoseGame()
      {
         ShowPanel(PanelType.LosePanel);
      }
      
      private void OnDestroy()
      {
         _startGameButton.onClick.RemoveListener(StartLevel);
         _startNextLevelButton.onClick.RemoveListener(StartNextLevel);
         foreach (var restartButton in _restartGameButtons)
         {
            restartButton.onClick.RemoveListener(RestartLevel);
         }
         UnregisterPanel();
      }
      
      private void RegisterAllPanels()
      {
         RegisterPanel(_tutorialPanel);
         RegisterPanel(_startGamePanel);
         RegisterPanel(_winPanel);
         RegisterPanel(_losePanel);
      }

      private void RegisterPanel(UIPanel panel)
      {
         if (panel != null )
         {
            panelsDictionary[panel.TypePanel] = panel;
            if (panel is TutorialPanel tutorialPanel)
            {
               tutorialPanel.CompleteTutorial += CloseTutorial;
            }
         }
      }
      
      private void UnregisterPanel()
      {
         foreach (var panel in panelsDictionary.Values)
         {
            if (panel is TutorialPanel tutorialPanel)
            {
               tutorialPanel.CompleteTutorial -= CloseTutorial;
            }
         }
         panelsDictionary.Clear();
      }

      private void CloseTutorial()
      {
         ChangePanel(_startGamePanel);
      }
      
      private void ChangePanel(UIPanel panel)
      {
         ShowPanel(panel.TypePanel);
      }
      
      private void ShowPanel(PanelType type)
      {
         foreach (var panel in panelsDictionary.Values)
         {
            if (panel.TypePanel != type)
            {
               panel.gameObject.SetActive(false);
            }
         }

         if (panelsDictionary.TryGetValue(type, out var panelToShow))
         {
            panelToShow.gameObject.SetActive(true);
         }
         else
         {
            Debug.LogError($"Panel of type {type} not found in the dictionary.");
         }
      }

      private void HidePanel(PanelType type)
      {
         if (panelsDictionary.TryGetValue(type, out var panel))
         {
            panel.gameObject.SetActive(false);
         }
         else
         {
            Debug.LogError($"Panel of type {type} not found in the dictionary.");
         }
      }
      
      private void StartLevel()
      {
         HidePanel(PanelType.StartPanel);
         StartGame?.Invoke();
      }

      private void RestartLevel()
      {
         RestartGame?.Invoke();
      }
      
      private void StartNextLevel()
      {
         NextLevel?.Invoke();
      }
      
   }
}
