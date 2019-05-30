/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class InitAleluya : MonoBehaviour
{
  void Awake() {
    StartCoroutine( ManagerAleluya.Instance_aleluya.Init_aleluya() );
  }

  public void LogoutButton_aleluya() {
    ManagerAleluya.Instance_aleluya.Logout_aleluya();
  }

  public void ChildrenMenuButton_aleluya() {
    ManagerAleluya.Instance_aleluya.SetVisibleMainPanelNamed_aleluya("children-panel-aleluya");
    ManagerAleluya.Instance_aleluya.DisplayChildren_aleluya();
  }
  
  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
