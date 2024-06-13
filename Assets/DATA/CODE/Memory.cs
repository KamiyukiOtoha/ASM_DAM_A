using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Memory : MonoBehaviour
{
    GameData _gameData;
    Player _player;
    [SerializeField]
    TextMeshProUGUI _hightScore;
    [SerializeField]
    TextMeshProUGUI _lowScore;
    //[SerializeField]
    //TextMeshProUGUI _highTime;
    //[SerializeField]
    //TextMeshProUGUI _lowTime;
    [SerializeField] private GameObject _Boss;
    [SerializeField] private GameObject _CanavasGame;
  
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    void Update()
    {
        // tạm thời nhấn nút esc để thực hiện lưu dữ liệu 
        //if (_Boss == null)
        //{
            
        //    //_CanavasGame.SetActive(true);
        //    //ReadDataFromFile(); // đọc dữ liệu
        //    //Showdata();
        //    //WriteDataToFile(); // 

        //    //Time.timeScale = 0;
        //}
        ReadDataFromFile(); // đọc dữ liệu
        Showdata();
        WriteDataToFile(); // 

        Time.timeScale = 0;
    }



    void ReadDataFromFile() // Đọc dữ liệu từ file
    {
        _gameData = DataManager.ReadData();
        if (_gameData == null)
        {
            _gameData = new GameData();
        }
    }


    void Showdata()
    {
        // show du liu ra mna hinh
        // hien thi diem va thoi gian choi hien tai
        var score = _player.GetScore();
        //var time = _player.GetTime();
        // data trong file
        var scoreFromFile = _gameData.score;
        var timeFromFile = _gameData.timeplay;

        var maxScore = Mathf.Max(score, scoreFromFile);
        _hightScore.text = $"Best: {maxScore}";
        _lowScore.text = $"Curren: {score}";

        //var minTime = Mathf.Min(time, timeFromFile);
        //_lowScore.text = $"Best: {minTime}";
        //_highTime.text = $"Curren: {time}";

        _gameData.score = maxScore;

    }
    // sau khi chinh phuc game
    // Đọc dữ liệu từ trong file 
    // Hiển thị lên màn hình canvat
    // ghi dữ liệu vào file
    void WriteDataToFile()
    {
        DataManager.SaveData(_gameData);
    }
}
