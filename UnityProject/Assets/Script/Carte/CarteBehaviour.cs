using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using static Script.Manager.GameManager;
using UnityEngine.UI;
public abstract class CarteBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 

    public PlayerManager player;
    
    private bool carte_actuel = false;
    
    private static CarteBehaviour ancienne_carte_selec;
    
    public CarteData carteData;
    
    public Text text;
    
    protected abstract void Awake();
    
    private void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(1.75f, 5);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1);
        spriteRenderer.sprite = carteData.Sprite;
        spriteRenderer.sortingOrder = 11;
        player = Instance.joueur;
        spriteRenderer.color = new Color32(200,200,200,255);
        
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
        if (ancienne_carte_selec != null && ancienne_carte_selec != this)
        {
            ancienne_carte_selec.carte_actuel = false;
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
        
        Instance.playerActif.enAction = true;
        Instance.playerActif.SetAura(true);
        Spell();
        player.drops -= carteData.drops;
        player.MiseAjourAffichageDrops();
        ancienne_carte_selec = this;
    
    }

    private void DeselectCard()
    {
        carte_actuel = false;
        Instance.playerActif.SetAura(false);
        Instance.playerActif.enAction = false;
        player.drops += carteData.drops;
        player.MiseAjourAffichageDrops();
        OnMouseExit();
        if (ancienne_carte_selec == this)
        {
            ancienne_carte_selec = null;
        }
    }

    private void OnMouseEnter()
    {
        if (!carte_actuel)
        {
            var vector3 = transform.position;
            vector3.y += 1f;
            transform.position = vector3;
            spriteRenderer.color = Color.white;
        }
    }

    private void OnMouseExit()
    {
        if (!carte_actuel)
        { 
            var vector3 = transform.position;
            vector3.y =  -4f;
            transform.position = vector3;
            spriteRenderer.color = new Color32(200,200,200,255);
        }
    }
    
    protected abstract void Spell();

    protected virtual void RemoveSpell()
    {
        Instance.playerActif.enAction = false;
    }
}
