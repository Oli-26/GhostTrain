﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


 public class PurchaseUICard : UIElement
 {
     private PurchaseType _type;
     private Price _price;

     public TextMesh Title;
     public TextMesh DisplayedPrice;
     public SpriteRenderer Icon;
     public PurchaseButton PurchaseButton;

     public void Initialise(PurchaseType type, string title, Price price, Sprite icon, KeyCode shortcut)
     {
         _type = type;
         _price = price;
         
         Title.text = title + " [" + shortcut + "]";
         DisplayedPrice.text = price.ToString();
         Icon.sprite = icon;
         
         PurchaseButton.Type = type;
         PurchaseButton.Shortcut = shortcut;
         
     }
     
 }
 
 public enum PurchaseType
 {
     Grabber,
     Refiner,
     Extension,
     StorageExtension,
     LivingExtension,
     ResearchExtension,
     CropPlot,
     Worker
 }
