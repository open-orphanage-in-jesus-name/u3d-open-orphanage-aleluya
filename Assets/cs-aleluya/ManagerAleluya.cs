/* For God so loved the World, that He gave His only begotten Son, that all who believe in HIm should not perish, but have everlasting life */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.EventSystems;


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

  public IEnumerator LoadChildImage_aleluya(ChildData_aleluya cd_aleluya)
  {

    string url_aleluya = cd_aleluya.avatar_media_url_aleluya;
    if( url_aleluya.Length < 2) yield break ;
    using (UnityWebRequest webRequest_aleluya = UnityWebRequest.Get(url_aleluya))
    {
      AddAuthentication_aleluya(webRequest_aleluya);
      DownloadHandlerTexture texDl_aleluya = new DownloadHandlerTexture(true);
      webRequest_aleluya.downloadHandler = texDl_aleluya; 
     
      // Request and wait for the desired page.
      yield return webRequest_aleluya.SendWebRequest();

      // If we have an error, log out
      if (webRequest_aleluya.isNetworkError)
      {
          Debug.Log(": Aleluya Error: " + webRequest_aleluya.error);
          //notificationText_aleluya = "✝ Error with supplied site and credentials - " + webRequest_aleluya.error;
          //this.Logout_aleluya();

      }
      else
      {
        Texture2D tex_aleluya = texDl_aleluya.texture;
        GameObject go_aleluya = GameObject.Find("child-row-image-" + cd_aleluya.id + "-aleluya");
        RawImage img_aleluya = go_aleluya.GetComponent<RawImage>();
        img_aleluya.texture = tex_aleluya;

        
        //SetVisibilityCanvasNamed_aleluya( "main-aleluya", true );    
        //SetVisibilityCanvasNamed_aleluya( "login-aleluya", false );
      }
    }
  }


  public class ClickAction_aleluya : MonoBehaviour, IPointerClickHandler
  { 
    public ChildData_aleluya cd_aleluya;

    public void OnPointerClick(PointerEventData eventData)
    {
        ManagerAleluya.Instance_aleluya.notificationText_aleluya = "Hallelujah - click child " + this.cd_aleluya.id;
    }
  }

  public void DisplayChildren_aleluya()
  {
    GameObject childRows_aleluya = GameObject.Find("child-rows-aleluya");
    RectTransform childRowsRect_aleluya = childRows_aleluya.GetComponent<RectTransform>();
    int rownum_aleluya = 0;
    
    foreach(ChildData_aleluya cd_aleluya in allChildren_aleluya.children_aleluya ) {
      GameObject childRow_aleluya = new GameObject("child-row-" + cd_aleluya.id + "-aleluya");
      ClickAction_aleluya ca_aleluya = childRow_aleluya.AddComponent<ClickAction_aleluya>();
      ca_aleluya.cd_aleluya = cd_aleluya;
      
      CanvasRenderer cr_aleluya = childRow_aleluya.AddComponent<CanvasRenderer>();
      childRow_aleluya.transform.SetParent(childRows_aleluya.transform, false);
      childRow_aleluya.AddComponent<RectTransform>();

      //Thank You Lord for http://answers.unity.com/comments/1544746/view.html
      RectTransform rt_aleluya;
      rt_aleluya = cr_aleluya.GetComponent<RectTransform>();
      rt_aleluya.pivot = new Vector2(0.0f,1.0f);
      rt_aleluya.anchorMin = new Vector2(0.0f,1.0f);
      rt_aleluya.anchorMax = new Vector2(1.0f,1.0f);
      rt_aleluya.offsetMin = new Vector2(0, -104 * (rownum_aleluya));
      rt_aleluya.offsetMax = new Vector2(0, - 104 * (rownum_aleluya + 1));
      rownum_aleluya ++;
      //rt_aleluya.sizeDelta = new Vector2(childRowsRect_aleluya.sizeDelta.x,100);

      GameObject imgObj_aleluya = new GameObject("child-row-image-" + cd_aleluya.id + "-aleluya");
      imgObj_aleluya.transform.SetParent(childRow_aleluya.transform, false);
      cr_aleluya = imgObj_aleluya.AddComponent<CanvasRenderer>();
      RawImage i_aleluya = imgObj_aleluya.AddComponent<RawImage>();       
      rt_aleluya = cr_aleluya.GetComponent<RectTransform>();
      rt_aleluya.pivot = new Vector2(0.0f,1.0f);
      rt_aleluya.anchorMin = new Vector2(0.0f,0.0f);
      rt_aleluya.anchorMax = new Vector2(0.0f,0.0f);
      rt_aleluya.offsetMin = new Vector2(0,0);
      //rt_aleluya.offsetMax = new Vector2(-100,-100);
      rt_aleluya.sizeDelta = new Vector2(100,100);     
    

      GameObject txtObj_aleluya = new GameObject("child-row-text-" + cd_aleluya.id + "-aleluya");
      txtObj_aleluya.transform.SetParent(childRow_aleluya.transform, false);
      cr_aleluya = txtObj_aleluya.AddComponent<CanvasRenderer>();
      TMP_Text aleluya = txtObj_aleluya.AddComponent<TextMeshProUGUI>();
      rt_aleluya = cr_aleluya.GetComponent<RectTransform>();
      rt_aleluya.pivot = new Vector2(0.0f,1.0f);
      rt_aleluya.anchorMin = new Vector2(0.0f,0.0f);
      rt_aleluya.anchorMax = new Vector2(1.0f,1.0f);
      rt_aleluya.offsetMin = new Vector2(100,0);
      rt_aleluya.offsetMax = new Vector2(0,00);
      StartCoroutine( LoadChildImage_aleluya( cd_aleluya ));
      //rt_aleluya.sizeDelta = new Vector2(1000,100);
      aleluya.alignment = TextAlignmentOptions.Center; 
      aleluya.text = cd_aleluya.first_names_aleluya + "\n";
    }

  }

}
