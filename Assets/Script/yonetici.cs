using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class yonetici : MonoBehaviour
{
    public GameObject kucukDaire;
    int toplamDaireSayisi;
    bool restartDurumu = false;
   
    List<Rigidbody2D> tumDaireler = new List<Rigidbody2D>();
    float daireHiz = 20;
    float ilkDaireY_poz = -3;
    [SerializeField]
    TextMeshProUGUI level_txt;
    [SerializeField]
    TextMeshProUGUI sonraki_txt;
    [SerializeField]
    TextMeshProUGUI restart_txt;
    [SerializeField]
    TextMeshProUGUI restart_exit;
    int level;
    [SerializeField]
    Animator a;
    void Start()
    {

        //PlayerPrefs.DeleteAll();

        // CancelInvoke("yeniBolum");
        restartDurumu = false;
        levelKontrol();
        ilkDaireleriEkle();
    }

   
  
    void levelKontrol()
    {
        if (PlayerPrefs.HasKey("level"))//level diye kayıtlı bir değişken var ise
        {
            level = PlayerPrefs.GetInt("level");
        }
        else// daha önce oyunu oynamış ise
        {
            level = 1;
            PlayerPrefs.SetInt("level",level);

        }
        level_txt.text = "" + level;

    }
    void ilkDaireleriEkle()
    {
        toplamDaireSayisi = level * 2;
        for(int i = 0; i < toplamDaireSayisi; i++)
        {
            GameObject yeniDaire = Instantiate(kucukDaire);
            if (i == 0)
            {
                yeniDaire.transform.position = new Vector2(0, ilkDaireY_poz);
            }
            else
            {
                yeniDaire.transform.position = new Vector2(0, tumDaireler[i - 1].transform.position.y - (tumDaireler[i - 1].GetComponent<CircleCollider2D>().bounds.size.y)*1.5f);
            }
            yeniDaire.GetComponentInChildren<TextMeshProUGUI>().text = ""+(toplamDaireSayisi - i);
            tumDaireler.Add(yeniDaire.GetComponent<Rigidbody2D>());
        }
    }
  public  void dairePozGuncelle()
    {
        for(int i = 0; i < tumDaireler.Count; i++)
        {
            if (i == 0)
            {
                tumDaireler[i].transform.position = new Vector2(0, ilkDaireY_poz);
            }
            else
            {
               tumDaireler[i].transform.position = new Vector2(0, tumDaireler[i - 1].transform.position.y - (tumDaireler[i - 1].GetComponent<CircleCollider2D>().bounds.size.y)*1.5f);
            }
        }
    }
    void kazandin()
    {
        a.Play("kazandin_anim");
        sonraki_txt.gameObject.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            int simdikiLevel = PlayerPrefs.GetInt("level");
            simdikiLevel++;
            PlayerPrefs.SetInt("level", simdikiLevel);
            Invoke("yeniBolum", 0.5f);

        }
 
    }
   public void kaybettin()
    {
        a.Play("kaybettin_anim");
        restart_txt.gameObject.SetActive(true);
        restart_exit.gameObject.SetActive(true);
        restartDurumu = true;
    }
    void yeniBolum()
    {
        sonraki_txt.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && tumDaireler.Count > 0 && restartDurumu==false)
        {
            tumDaireler[0].velocity = Vector2.up * daireHiz;
            tumDaireler.RemoveAt(0);//baştan silme
        }
        else if(restartDurumu==true && Input.GetMouseButtonDown(0))
        {
           // Debug.Log("sa");
            yeniBolum();
            restart_txt.gameObject.SetActive(false);
            restartDurumu = false;
            SceneManager.LoadScene(0);
        }
        else if(restartDurumu==true && Input.GetKeyDown(KeyCode.Escape))
        {
            restart_txt.gameObject.SetActive(false);
            restart_exit.gameObject.SetActive(false);
            restartDurumu = false;
            //Debug.Log("Quiting");
            Application.Quit();
        }
        else if (tumDaireler.Count <= 0)
        {
            kazandin();
        }
    }

}
