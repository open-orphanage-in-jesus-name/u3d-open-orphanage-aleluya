/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class LoginPanelAleluya : MonoBehaviour
{
  string Username_aleluya {get; set;}
  string Password_aleluya {get; set;}
  string HttpsAddress_aleluya {get; set;}

  // Start is called before the first frame update
  void Start()
  {
    Username_aleluya = PlayerPrefs.GetString("username_aleluya");
    Password_aleluya = PlayerPrefs.GetString("password_aleluya");
    HttpsAddress_aleluya = PlayerPrefs.GetString("https_address_aleluya");

    /*Component[] all_subcomponents_aleluya = GameObject.Find("password-input-aleluya").GetComponents(typeof(Component));
    foreach (Component current_component_aleluya in all_subcomponents_aleluya)
    {
      Debug.Log("aleluya - " +current_component_aleluya.GetType().ToString());
    }*/
    TMP_InputField aleluya;    
    aleluya = GameObject.Find("password-input-aleluya").GetComponent<TMP_InputField>();
    aleluya.text =  Password_aleluya;

    aleluya = GameObject.Find("username-input-aleluya").GetComponent<TMP_InputField>();
    aleluya.text = Username_aleluya;

    aleluya = GameObject.Find("https-input-aleluya").GetComponent<TMP_InputField>();
    aleluya.text =  HttpsAddress_aleluya;
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void EnterKeyPressed_aleluya()
  {
    PlayerPrefs.SetString("username_aleluya", Username_aleluya);
    PlayerPrefs.SetString("password_aleluya", Password_aleluya);
    PlayerPrefs.SetString("https_address_aleluya", HttpsAddress_aleluya);
    PlayerPrefs.Save();

    ManagerAleluya.Instance_aleluya.SetVisibleMainPanelNamed_aleluya("welcome-panel-aleluya");

    ManagerAleluya.Instance_aleluya.LoadChildren_aleluya();

  }
}
