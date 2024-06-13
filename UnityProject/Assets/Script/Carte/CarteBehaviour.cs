using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using static Script.Manager.AGameManager;
using UnityEngine.UI;
public abstract class CarteBehaviour : MonoBehaviour, Tireur
{
    public SpriteRenderer spriteRenderer;

    public PlayerManager player;
    
    public bool carte_actuel = false;
    
    private static CarteBehaviour ancienne_carte_selec;
    
    public CarteData carteData;
    
    public Text text;
    
    public static bool alreadylifted;
    public static Image cardImage; 
    private static Canvas cardCanvas;
    protected abstract void Awake();
    
    private void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(1.75f, 5);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1);
        spriteRenderer.sprite = carteData.Sprite;
        spriteRenderer.sortingOrder = 11;
        player = Instance.joueur;
        spriteRenderer.color = new Color32(200, 200, 200, 255);
        ancienne_carte_selec = null;
        
        //pour mettre le coût de la carte sur la carte : 
        GameObject canvasObj = new GameObject("CardCanvas");
        canvasObj.transform.SetParent(transform);
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        GameObject textObj = new GameObject("CardText");
        textObj.transform.SetParent(canvasObj.transform);
        text = textObj.AddComponent<Text>();
        text.font = Resources.Load<Font>("Fonts/Splatch");
        text.text = carteData.drops.ToString();
        text.fontSize = 25; 
        text.color = new Color32(50, 50, 50, 255);
        text.alignment = TextAnchor.UpperCenter;
        text.fontStyle = FontStyle.Bold; 
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(70, 92);

        if (cardCanvas == null)
        {
            CreateSharedCanvas();
            LoadCardImage(player.mainManager[6].GetComponents<CarteBehaviour>()[0].carteData.Sprite.name);
        }
        

    }
    private static void CreateSharedCanvas()
    {
        // Create and set up a Canvas for UI elements
        GameObject canvasObj = new GameObject("CardCanvas");
        cardCanvas = canvasObj.AddComponent<Canvas>();
        cardCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler2 = canvasObj.AddComponent<CanvasScaler>();
        scaler2.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler2.referenceResolution = new Vector2(1920, 1080);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create an Image UI element for the card image
        GameObject imageObj = new GameObject("CardImage");
        imageObj.transform.SetParent(canvasObj.transform);
        cardImage = imageObj.AddComponent<Image>();
        RectTransform imageRect = imageObj.GetComponent<RectTransform>();
        imageRect.sizeDelta = new Vector2(90, 150); // Adjust size to fit your card
        imageRect.anchoredPosition = new Vector2(135, -463);
        cardImage.color = Color.gray;
       
    }
    
    private static void LoadCardImage(string imageName)
    {
        string cheminImage = "Sprites/cards/" + imageName;
        Sprite newSprite = Resources.Load<Sprite>(cheminImage);
        if (newSprite != null)
        {
            cardImage.sprite = newSprite;
            cardImage.rectTransform.sizeDelta = new Vector2(90,150); // Adjust size to match image
        }
        else
        {
            Debug.LogError("Image non trouvée au chemin: " + cheminImage);
        }
    }
   
    private void Update()
    {
        if (text != null)
        {
            text.text = carteData.drops.ToString();
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + 0.89f, transform.position.z);
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
            rectTransform.position = screenPoint;
        }
    }
    private void OnMouseDown()
    {
        if (carte_actuel)
        {
            DeselectCard();
            OnMouseExit();
        }
        else
        {
            SelectCard();
        }
    }

    private void SelectCard()
    {
        
        if (Instance.playerActif == player && player.drops - carteData.drops >= 0 && !player.enAction)
        {
            carte_actuel = true;
            ancienne_carte_selec = this;
            alreadylifted = true;
            Instance.playerActif.enAction = true;
            Instance.playerActif.SetAura(true);
            SpellClickOnCarte();
            player.drops -= carteData.drops;
            player.MiseAjourAffichageDrops();
        }
        
    }

    public void DeselectCard()
    {
        carte_actuel = false;
        Instance.playerActif.SetAura(false);
        Instance.playerActif.enAction = false;
        player.drops += carteData.drops;
        player.MiseAjourAffichageDrops();
        alreadylifted = false;
        var vector3 = transform.position;
        vector3.y -= 4f;
        transform.position = vector3;
        spriteRenderer.color = new Color32(200,200,200,255);

        if (ancienne_carte_selec == this)
        {
            ancienne_carte_selec = null;
        }
    }
    /*
    private void OnMouseDown()
    {
        if (ancienne_carte_selec != null && ancienne_carte_selec != this)
        {
            ancienne_carte_selec.carte_actuel = false;
        }

        if (ancienne_carte_selec == this)
        {
            DeselectCard();
        }
        
        if (Instance.playerActif == player && player.drops - carteData.drops >= 0 && !player.enAction)
        {
            var vector3 = transform.position;
            vector3.y =  -4f;
            transform.position = vector3;
            spriteRenderer.color = new Color32(200,200,200,255);
            SelectCard();
        }
        else if (player.enAction && player.drops != 0)
        {
            DeselectCard();
        }
    }

    private void SelectCard()
    {
        carte_actuel = true;
        var vector3 = transform.position;
        vector3.y += 1f;
        transform.position = vector3;
        spriteRenderer.color = Color.white;
        
        alreadylifted = true;
        Instance.playerActif.enAction = true;
        Instance.playerActif.SetAura(true);
        SpellClickOnCarte();
        player.drops -= carteData.drops;
        player.MiseAjourAffichageDrops();
        ancienne_carte_selec = this;
        
    }

    public void DeselectCard()
    {
        carte_actuel = false;
        Instance.playerActif.SetAura(false);
        Instance.playerActif.enAction = false;
        player.drops += carteData.drops;
        player.MiseAjourAffichageDrops();
        alreadylifted = false;
        OnMouseExit();
        if (ancienne_carte_selec == this)
        {
            ancienne_carte_selec = null;
        }
    }
*/
    private void OnMouseEnter()
    {
        if (!carte_actuel && !alreadylifted )
        {
            var vector3 = transform.position;
            vector3.y += 1f;
            transform.position = vector3;
            spriteRenderer.color = Color.white;
            
        }
    }

    private void OnMouseExit()
    {
        if (!carte_actuel && !alreadylifted)
        { 
            var vector3 = transform.position;
            vector3.y =  -4f;
            transform.position = vector3;
            spriteRenderer.color = new Color32(200,200,200,255);
        }
    }
    
    protected abstract void SpellClickOnCarte();
    public abstract void SpellAfterClick();

    public abstract void SpellAfterShoot(Vector2 start,Vector2 current);

    protected virtual void RemoveSpell()
    {
        Instance.playerActif.enAction = false;
    }
    
    public void PiocherMain()
    { 
        if ( carte_actuel)
        {
            foreach (var gr in player.mainManager)
            {
                var min = gr.transform.position.x;
                if (min > 8 && min < 21)
                {

                    var vec = gr.transform.position;
                    vec.x = transform.position.x;
                    gr.transform.position = vec;

                    var vector3 = transform.position;
                    vector3.x = 28;
                    transform.position = vector3;
                    foreach (var car in player.mainManager)
                    {
                        if (car.transform.position.x > 10)
                        {
                            var vec2 = car.transform.position;
                            vec2.x -= 2;
                            car.transform.position = vec2;
                        }
                    }
                    
                }
            }
            foreach (var next_card in player.mainManager)
            {
                var x = next_card.transform.position.x;
                if (x > 8 && x < 21)
                {
                    LoadCardImage(next_card.GetComponents<CarteBehaviour>()[0].carteData.Sprite.name);
                    break;
                }
            }
        }
    }


    public void ClassiqueShoot(Vector2 startPosition, Vector2 currentMousePos)
    {
        var projectileData = carteData.projectileData;
        projectileData.Lanceur = gameObject;
        GameObject bullet = Instantiate(projectileData.Projectile, startPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        bulletBehaviour.Set((startPosition, currentMousePos), projectileData);
    }

    

     protected void FinAction()
    {
        GameManager.Instance.playerActif.enAction = false;
        CarteBehaviour.alreadylifted = false;
        alreadylifted = false;
        if (GameManager.Instance.playerActif.drops == 0)
        {
            GameManager.Instance.FinDuTour();
        }
    }
    
   

}
