/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InputAleluya : MonoBehaviour
{

  //When we press the Logout button on the right side of the menu, what should happen?
  public void LogoutButton_aleluya() {
    ManagerAleluya.Instance_aleluya.Logout_aleluya();
  }


  //When we press the children menu button on the right, Hallelujah
  public void ChildrenMenuButton_aleluya() {
    ManagerAleluya.Instance_aleluya.SetVisibleMainPanelNamed_aleluya("children-panel-aleluya");
    ManagerAleluya.Instance_aleluya.DisplayChildren_aleluya();
  }

  //When we click "Add new child" in the children list
  public void AddNewChildButton_aleluya() {

  }

  //When we click "Save Child" when a child is being edited
  public void SaveChildButton_aleluya() {

  }


  //When we click "Remove Child" when a child is being edited
  public void RemoveChildButton_aleluya() {

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
