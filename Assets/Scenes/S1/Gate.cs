using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    //[SerializeField] GameObject _loadingCanvat;
  //  [SerializeField] Slider _slider;
   // [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        //_loadingCanvat.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Loading.....

            // _loadingCanvat.SetActive(true);
            //StartCoroutine(Loading());
            SceneManager.LoadScene("S1");
        }
    }
    //IEnumerator Loading()
    //{
    //    var value = 0;
    //    _slider.value = value;
    //    while (true)
    //    {
    //        value++;
    //        _slider.value = value;
    //        _textMeshProUGUI.text = value + "%";
    //        yield return new WaitForSeconds(0.1f);
    //        if (value >= 100)
    //        {
    //            break;
    //        }
    //    }
        // chuyển sang sc2
       // SceneManager.LoadScene(1);
    
}

