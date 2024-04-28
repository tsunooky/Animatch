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

    public CarteData carteData;

    public Text text;
    
    protected abstract void Awake();
    
    private void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(1.75f,3);
        spriteRenderer.sprite = carteData.Sprite;
        spriteRenderer.sortingOrder = 1;
        player = Instance.joueur;
        
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
        text.color = new Color32(56, 56, 56, 255);
        text.alignment = TextAnchor.UpperCenter;
        text.fontStyle = FontStyle.Bold; 
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(70, 92); 
    }
    
    
    private void Update()
    {
        text.text = carteData.drops.ToString();
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + 0.89f, transform.position.z);
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
        rectTransform.position = screenPoint;
    }

    private void OnMouseDown()
    {
        if (Instance.playerActif == player && player.drops - carteData.drops >= 0 && !player.enAction)
        { 
            Instance.playerActif.enAction = true;
            Instance.playerActif.SetAura(true);
            Spell();
            player.drops -= carteData.drops;
            player.MiseAjourAffichageDrops();
        }
        else if (player.enAction)
        {
            Instance.playerActif.SetAura(false);
            Instance.playerActif.enAction = false;
            player.drops += carteData.drops;
            player.MiseAjourAffichageDrops();
        }
    }

    private void OnMouseEnter()
    {
        var vector3 = transform.position;
        vector3.y += 1f;
        transform.position = vector3;
    }

    private void OnMouseExit()
    {
        var vector3 = transform.position;
        vector3.y =  -4f;
        transform.position = vector3;
    }

    protected abstract void Spell();

    protected virtual void RemoveSpell()
    {
        Instance.playerActif.enAction = false;
    }
}
