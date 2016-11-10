using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreScript : MonoBehaviour {

    [SerializeField]
    private ScrollRect _scrollNameRect;
    [SerializeField]
    private ScrollRect _scrollScoreRect;
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _scoreText;
    private static BinaryFormatter _bf = new BinaryFormatter();
    private static Dictionary<string, int> _highscoreList = new Dictionary<string, int>();
    private static Dictionary<string, int> _sortedList = new Dictionary<string, int>();


    private bool returnAxisInUse = false;

    private void Start()
    {
        try
        {
            LoadList();
        }
        catch
        {
            print("List not found");
        }
        
        DisplayScores();
        Init();
    }

    private void Update()
    {
        checkAxisCommands("Fire1", ref returnAxisInUse);
    }

    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            if (pAxisToggle == false) //if the key is not pressed down
            {
                switch (pAxisName)
                {
                    case "Fire1":
                        selectionAxisCommands();
                        break;
                }
                pAxisToggle = true; //key is now 'down'
            }
        }
        //key has now been lifted up
        if (Input.GetAxisRaw(pAxisName) == 0)
        {
            pAxisToggle = false;
        }
    }

    private void selectionAxisCommands()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void Init()
    {
        string activateReplayButton = PlayerPrefs.GetString("Replay");
        PlayerPrefs.SetInt("score", 0);
    }

    private void DisplayScores()
    {
        _nameText.text = "";
        _scoreText.text = "";
        foreach (KeyValuePair<string,int> kvp in _highscoreList)
        {
            _nameText.text += kvp.Key + "\n";
            _scrollNameRect.verticalNormalizedPosition = 0;
            _scoreText.text += kvp.Value + "\n";
            _scrollScoreRect.verticalNormalizedPosition = 0;
        }
    }

    public static void AddScore(string pName, int pScore)
    {
        try
        {
            LoadList();
        }
        catch
        {
            print("List not found");
        }
        if (_highscoreList.ContainsKey(pName) && pScore > _highscoreList[pName])
        {
            _highscoreList[pName] = pScore;
            SortList();
        }
        else if (_highscoreList.ContainsKey(pName) && pScore < _highscoreList[pName])
        {
            return;
        }
        else if (_highscoreList.Count < 10)
        {
            _highscoreList.Add(pName, pScore);
            SortList();
        }
        else
        {
            _highscoreList.Add(pName, pScore);
            SortList();
            _highscoreList.Remove(_highscoreList.Last().Key);
        }
        SaveList();
    }

    public static bool IsItHighscore(int pScore)
    {
        try
        {
            LoadList();
        }
        catch
        {
            print("not found");
        }
        
        foreach (KeyValuePair<string,int> item in _highscoreList)
        {
            if (pScore > item.Value) return true;
        }
        return false;
    }


    private static void SortList()
    {
        _sortedList.Clear();
        foreach (KeyValuePair<string, int> item in _highscoreList.OrderByDescending(key => key.Value))
        {
            _sortedList.Add(item.Key, item.Value);
        }
        _highscoreList = _sortedList;
    }

    private static void SaveList()
    {
        FileStream stream = new FileStream(Application.persistentDataPath + "/highscore.dat", FileMode.Create);

        try
        {
            _bf.Serialize(stream, _highscoreList);
        }
        catch
        {
            print("Failed");
        }
        stream.Close();
    }

    private static void LoadList()
    {
        FileStream stream = new FileStream(Application.persistentDataPath + "/highscore.dat", FileMode.Open);

        try
        {
            object list = _bf.Deserialize(stream);
            _highscoreList = list as Dictionary<string, int>;
        }
        catch
        {
            print("failed");
            stream.Close();
        }
        finally
        {
            stream.Close();
        }
    }
}
