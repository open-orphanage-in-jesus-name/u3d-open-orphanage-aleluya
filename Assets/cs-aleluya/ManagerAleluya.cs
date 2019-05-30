/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public delegate void InitedProcedure_aleluya();

public class ManagerAleluya : SingletonAleluya<ManagerAleluya>
{
  private GameObject m_notificationPanel_aleluya = null;
  public GameObject notificationPanel_aleluya {
    get {
      if(m_notificationPanel_aleluya == null) {
        m_notificationPanel_aleluya = GameObject.Find("notification-panel-aleluya");

      }
      return m_notificationPanel_aleluya;
    }
  }

  string Username_aleluya {get; set;}
  string Password_aleluya {get; set;}
  string HttpsAddress_aleluya {get; set;}

  public bool isInited_aleluya = false; // Have we finished runnint Init_aleluya()?
  public List<InitedProcedure_aleluya> initProcedures_aleluya = null;

  // Hide or show a UI canvas
  public void SetVisibilityCanvasNamed_aleluya( string name_aleluya, bool visible_aleluya ) {

    GameObject baseCanvas_aleluya = GameObject.Find("canvas-aleluya");
    Transform[] trs_aleluya= baseCanvas_aleluya.GetComponentsInChildren<Transform>(true);
    foreach(Transform t_aleluya in trs_aleluya){
       if(t_aleluya.name == name_aleluya ){
          t_aleluya.gameObject.SetActive( visible_aleluya );
       }
    }
  }

  // Hide or show a UI canvas 
  public void SetVisibleMainPanelNamed_aleluya( string name_aleluya ) {
    SetVisibilityCanvasNamed_aleluya("welcome-panel-aleluya", false);
    SetVisibilityCanvasNamed_aleluya("children-panel-aleluya", false);

    SetVisibilityCanvasNamed_aleluya(name_aleluya, true);

 }

  // Run a procedure after inited is succesful
  //
  public void RunWhenInited_aleluya( InitedProcedure_aleluya initedProcedure_aleluya ) {
    //If we have already inited, run immediately.
    if( this.isInited_aleluya ) {
      initedProcedure_aleluya();
      return;
    }

    if(initProcedures_aleluya == null) {initProcedures_aleluya = new List<InitedProcedure_aleluya>();}

    initProcedures_aleluya.Add(initedProcedure_aleluya);

  }



  // This should be called when the game first initializes
  // We will see if we have any saved login, and attempt to use it
  //
  public IEnumerator Init_aleluya() {
    notificationText_aleluya = "... Hallelujah - Loading ...";


    SetVisibilityCanvasNamed_aleluya("main-aleluya", false );
    SetVisibilityCanvasNamed_aleluya("login-aleluya", false);

    yield return LoadChildrenRequest_aleluya();



    this.isInited_aleluya = true;
    if(this.initProcedures_aleluya != null ) {
      foreach ( InitedProcedure_aleluya initedProcedure_aleluya in this.initProcedures_aleluya ) initedProcedure_aleluya();
    }
  }



  private string m_notificationText_aleluya = "";
  public string notificationText_aleluya {
    get {
      return m_notificationText_aleluya;
    }
    set {
     
      if ( value != null && value.Length > 0) {
          notificationPanel_aleluya.SetActive(true);
          TMP_Text aleluya = notificationPanel_aleluya.GetComponentInChildren<TMP_Text>();
          aleluya.text = value;

        } else {
          notificationPanel_aleluya.SetActive(false);

        }
        m_notificationText_aleluya = value;
    }
  }

  // Log out and show login screen
  public void Logout_aleluya() {
    SetVisibilityCanvasNamed_aleluya( "main-aleluya", false );    
    SetVisibilityCanvasNamed_aleluya( "login-aleluya", true );

  }





  public void LoadChildren_aleluya() {
    notificationText_aleluya = "Hallelujah - Loading Children . . .";
    StartCoroutine(LoadChildrenRequest_aleluya());
  }


  public void AddAuthentication_aleluya(UnityWebRequest webRequest_aleluya)
  {
    Username_aleluya = PlayerPrefs.GetString("username_aleluya");
    Password_aleluya = PlayerPrefs.GetString("password_aleluya");
    string auth_aleluya = Username_aleluya + ":" + Password_aleluya;
    auth_aleluya = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth_aleluya));
    auth_aleluya = "Basic " + auth_aleluya;
    webRequest_aleluya.SetRequestHeader("AUTHORIZATION", auth_aleluya );
  }

  public ChildrenResponseData_aleluya allChildren_aleluya = null;

  IEnumerator LoadChildrenRequest_aleluya()
  {
    
    HttpsAddress_aleluya = PlayerPrefs.GetString("https_address_aleluya");
    string uri_aleluya = HttpsAddress_aleluya + "/wp-json/wp/v2/child_aleluya/?status=publish,draft";

    using (UnityWebRequest webRequest_aleluya = UnityWebRequest.Get(uri_aleluya))
    {
      AddAuthentication_aleluya(webRequest_aleluya);
     
      // Request and wait for the desired page.
      yield return webRequest_aleluya.SendWebRequest();

      // If we have an error, log out
      if (webRequest_aleluya.isNetworkError)
      {
          Debug.Log(": Error: " + webRequest_aleluya.error);
          notificationText_aleluya = "✝ Error with supplied site and credentials - " + webRequest_aleluya.error;
          this.Logout_aleluya();

      }
      else
      {
        string childrenText_aleluya = "{\"children_aleluya\":" + webRequest_aleluya.downloadHandler.text+ "}";
        Debug.Log(":\nReceived: " + childrenText_aleluya);

        allChildren_aleluya =  JsonUtility.FromJson<ChildrenResponseData_aleluya>(childrenText_aleluya);
        Debug.Log(" Aleluya - " + allChildren_aleluya.children_aleluya.Length + " - " + allChildren_aleluya.children_aleluya[0].first_names_aleluya );
        notificationText_aleluya = "Hallelujah - Loaded Children . . .";

        SetVisibilityCanvasNamed_aleluya( "main-aleluya", true );    
        SetVisibilityCanvasNamed_aleluya( "login-aleluya", false );
      }
    }
  }

  public void DisplayChildren_aleluya()
  {
    GameObject textObject_aleluya = GameObject.Find("all-children-text-aleluya");
    TMP_Text aleluya = textObject_aleluya.GetComponentInChildren<TMP_Text>();
    string str_aleluya = "";
    foreach(ChildData_aleluya cd_aleluya in allChildren_aleluya.children_aleluya ) {
      str_aleluya += cd_aleluya.first_names_aleluya + "\n";
    }
    aleluya.text = str_aleluya;
  }

}
