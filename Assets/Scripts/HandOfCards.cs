﻿using System.Collections.Generic;
using UnityEngine;

public class HandOfCards : MonoBehaviour {
    private LinkedList<Card> cards;
    public Vector3Value gridSize;
    public FloatValue cardSize;
    //4 cards fixed size of hand, to not make it dynamic because it's much more complicated.
    public FloatValue handSize;
    private GameObject[] cardsGO;

    private void OnEnable() {
        var offsetSoItsVisible = .5f;
        cardsGO = new GameObject[(int)handSize.value];
        float cardSizeX = gridSize.value.x / handSize.value;
        for (int i = 0; i < handSize.value; i++) {
            cardsGO[i] = CardsPool.instance.Get();
            var position = cardsGO[i].transform.position;
            var mr = cardsGO[i].GetComponent<MeshRenderer>();
            var bounds = mr.bounds;
            
            var scale = cardsGO[i].transform.localScale;
            scale.x = cardSizeX / bounds.size.x;
            scale.z = cardSize.value / bounds.size.z;
            cardsGO[i].transform.localScale = scale;
            
            position.x = (- gridSize.value.x + cardSizeX) / 2 + i * cardSizeX + i * offsetSoItsVisible;
            position.z = (gridSize.value.z + bounds.size.z * scale.z) / 2;
            position.y = 0;
            cardsGO[i].transform.position = position;
        }
    }

    private void OnDisable() {
        for (int i = 0; i < handSize.value; i++) {
            CardsPool.instance.DestroyObject(cardsGO[i]);
        }
    }

    public void Draw(int amount, Deck deck) {
        cards = new LinkedList<Card>();
        deck.Draw(amount, cards);
        int i = 0;
        foreach (var card in cards) {
            print("my physicall card, number " + i);
            cardsGO[i].GetComponent<CardContainer>().SetCard(card);
            i++;
        }
    }
}