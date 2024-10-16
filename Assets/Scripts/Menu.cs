using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TMP_InputField inputPlayerName;


    private void Start()
    {
        bestScoreText.text =  "Best Score : " + GameManager.Instance.BestPlayer.Name 
                                      + " : " + GameManager.Instance.BestPlayer.Score;

        inputPlayerName.text = GameManager.Instance.BestPlayer.Name;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

#else
        Application.Quit();
#endif
    }

}
