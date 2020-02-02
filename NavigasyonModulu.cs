using UnityEngine;
using UnityEngine.UI;

public class SagesNavigasyonModulu : MonoBehaviour
{
    //-------------------------------------------------------------
    //-------------------------------------------------------------
    public GameObject HandAnchor;
    public GameObject CenterEyeAnchor;
    public int BaslangicSahnesi;
    public int PAktifSahne;
    public bool Loglama;
    //-------------------------------------------------------------
    [System.Serializable]
    public class C_BolgeTanilama
    {
        public Material GecilecekSahne;
        public int Genislik_Baslangic;
        public int Genislik_Bitis;
        public int Yukseklik_Baslangic;
        public int Yukseklik_Bitis;
    }
    //-------------------------------------------------------------
    [System.Serializable]
    public class C_SahneTanimlama
    {
        public Material Sahne;
        public AudioSource SahneMuzigi;
        public C_BolgeTanilama[] BolgeSayisi;
    }
    public C_SahneTanimlama[] Sahneler;
    //-------------------------------------------------------------
    //-------------------------------------------------------------
    void Start(){
        PAktifSahne = BaslangicSahnesi;
        CenterEyeAnchor.GetComponent<Skybox>().material = Sahneler[BaslangicSahnesi].Sahne;
        //-------------------------------------------------------------
    }
    //-------------------------------------------------------------
    public void Tiklandi()
    {
        Debug.Log("Tiklanan: " + base.name);

    }
    //-------------------------------------------------------------
    void Update()
    {
        //-------------------------------------------------------------
        C_SahneTanimlama AktifSahne = Sahneler[PAktifSahne];
        //-------------------------------------------------------------
        if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            //-------------------------------------------------------------
            Debug.Log("Yukseklik: " + HandAnchor.transform.rotation.eulerAngles.x + "Genislik: " + HandAnchor.transform.rotation.eulerAngles.y);
            
            //-------------------------------------------------------------
            for (int i = 0; i < AktifSahne.BolgeSayisi.Length; i += 1){
                //-------------------------------------------------------------
                bool LazerYakalama_Yatay = false;
                bool LazerYakalama_Dikey = false;
                //-------------------------------------------------------------
                if (AktifSahne.BolgeSayisi[i].Genislik_Baslangic < AktifSahne.BolgeSayisi[i].Genislik_Bitis){
                    if ((HandAnchor.transform.rotation.eulerAngles.y > AktifSahne.BolgeSayisi[i].Genislik_Baslangic) && (HandAnchor.transform.rotation.eulerAngles.y < AktifSahne.BolgeSayisi[i].Genislik_Bitis)){LazerYakalama_Yatay = true;}
                }else{
                    if (((HandAnchor.transform.rotation.eulerAngles.y < AktifSahne.BolgeSayisi[i].Genislik_Bitis) && (HandAnchor.transform.rotation.eulerAngles.y >= 0)) || ((HandAnchor.transform.rotation.eulerAngles.y > AktifSahne.BolgeSayisi[i].Genislik_Baslangic) && (HandAnchor.transform.rotation.eulerAngles.y <= 360))){LazerYakalama_Yatay = true;}
                }
                //-------------------------------------------------------------
                if (AktifSahne.BolgeSayisi[i].Yukseklik_Baslangic < AktifSahne.BolgeSayisi[i].Yukseklik_Bitis){
                    if ((HandAnchor.transform.rotation.eulerAngles.x > AktifSahne.BolgeSayisi[i].Yukseklik_Baslangic) && (HandAnchor.transform.rotation.eulerAngles.x < AktifSahne.BolgeSayisi[i].Yukseklik_Bitis)){LazerYakalama_Dikey = true;}
                }else{
                    if (((HandAnchor.transform.rotation.eulerAngles.x < AktifSahne.BolgeSayisi[i].Yukseklik_Bitis) && (HandAnchor.transform.rotation.eulerAngles.x >= 0)) || ((HandAnchor.transform.rotation.eulerAngles.x > AktifSahne.BolgeSayisi[i].Yukseklik_Baslangic) && (HandAnchor.transform.rotation.eulerAngles.x <= 360))){LazerYakalama_Dikey = true;}
                }
                //-------------------------------------------------------------
                if (LazerYakalama_Yatay && LazerYakalama_Dikey){
                    PAktifSahne = SahneIDBul(AktifSahne.BolgeSayisi[i].GecilecekSahne);
                    Debug.Log("Yeni Aktif Sahne: " + PAktifSahne);
                    CenterEyeAnchor.GetComponent<Skybox>().material = AktifSahne.BolgeSayisi[i].GecilecekSahne;
                }
                //-------------------------------------------------------------
            }
            //-------------------------------------------------------------
        }
        //-------------------------------------------------------------

        if(CenterEyeAnchor.GetComponent<Skybox>().material != AktifSahne.Sahne)
        {
            Debug.LogError("Aktif sahne ile data sahne ayni degil");
        }
        //-------------------------------------------------------------
    }
    //-------------------------------------------------------------

    int SahneIDBul(Material Sahne){
        for (int i = 0; i <= Sahneler.Length; i += 1){
            if (Sahneler[i].Sahne == Sahne){
                return i;
            }
        }
        Debug.LogError("HATA: Sahne bulunamadi");
        return -1;
    }
}