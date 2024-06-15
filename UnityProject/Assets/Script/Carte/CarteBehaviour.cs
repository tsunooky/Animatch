using System;
using System.Collections;
using System.Collections.Generic;
using Destructible2D;
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
    
    public CarteData carteData;
    
    public Text text;

    private bool samecard;
    public static bool alreadylifted;
    public static Image cardImage; 
    private static Canvas cardCanvas;
    protected abstract void Awake();
    
    private void Start()
    {
        player = Instance.playerActif;
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1.75f, 5);
        collider.offset = new Vector2(0, -1);
        collider.isTrigger = true;
        
        spriteRenderer.sprite = carteData.Sprite;
        spriteRenderer.sortingOrder = 11;
        spriteRenderer.color = new Color32(200, 200, 200, 255);
        
        
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
    public void Initialize(PlayerManager player)
    {
        this.player = player;
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
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
    
    private void OnMouseDown()
    {
        if (Instance.playerActif == player)
        {
            Wait();
            if (carte_actuel)
            {
                samecard = true;
                DeselectCard();

            }
            else
            {
                SelectCard();
            }
        }
    }

    private void SelectCard()
    {
        Wait();
        if (samecard)
        {
            Wait();
            LiftCard();
        }
        if (Instance.playerActif == player && player.drops - carteData.drops >= 0 && !player.enAction)
        {
            carte_actuel = true;

            alreadylifted = true;
            Instance.playerActif.enAction = true;
            Instance.playerActif.SetAura(true);
            player.drops -= carteData.drops;
            player.MiseAjourAffichageDrops();
            SpellClickOnCarte();
        }
        
    }

    public void DeselectCard()
    {
        Wait();
        carte_actuel = false;
        Instance.playerActif.SetAura(false);
        Instance.playerActif.enAction = false;
        player.drops += carteData.drops;
        player.MiseAjourAffichageDrops();
        alreadylifted = false;
        var vector3 = transform.localPosition;
        vector3.y = -4f;
        transform.localPosition = vector3;
        spriteRenderer.color = new Color32(200,200,200,255);

        
    }
    private void OnMouseEnter()
    {
        if (Instance.playerActif == player)
        {
            if (!carte_actuel && !alreadylifted)
            {
                Wait();
                LiftCard();
                Wait();
            }
        }
    }
    

    private void OnMouseExit()
    {
        if (Instance.playerActif == player)
        {
            if (!carte_actuel && !alreadylifted && !samecard)
            {
                Wait();
                LowerCard();
                Wait();
            }
            else
            {
                samecard = false;
            }
        }
    }
    
    private void LiftCard()
    {
        var vector3 = transform.localPosition;
        vector3.y = -3f;
        transform.localPosition = vector3;
        spriteRenderer.color = Color.white;
    }
    
    private void LowerCard()
    {
        var vector3 = transform.localPosition;
        vector3.y = -4f;
        transform.localPosition = vector3;
        spriteRenderer.color = new Color32(200, 200, 200, 255);
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
        if (carte_actuel)
        {
            carte_actuel = false;
            samecard = false;
            alreadylifted = false;
            
            var vect = transform.localPosition;
            vect.y = -4f;
            transform.localPosition = vect;
            
            foreach (var gr in player.mainManager)
            {
                var min = gr.transform.localPosition.x;
                if (min > 8f && min < 21f)
                {
                    // Déplacement relatif de la carte actuellement en jeu
                    var vec = gr.transform.localPosition;
                    vec.x += (transform.localPosition.x - vec.x);
                    gr.transform.localPosition = vec;

                    // Déplacement relatif du transform
                    var vector3 = transform.localPosition;
                    vector3.x = 28f ; 
                    transform.localPosition = vector3;

                    foreach (var car in player.mainManager)
                    {
                        if (car.transform.localPosition.x > 10f)
                        {
                            var vec2 = car.transform.localPosition;
                            vec2.x -= 2f;
                            car.transform.localPosition = vec2;
                        }
                    }
                    break;
                }
            }

            foreach (var next_card in player.mainManager)
            {
                var x = next_card.transform.localPosition.x;
                if (x > 8f && x < 21f)
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
