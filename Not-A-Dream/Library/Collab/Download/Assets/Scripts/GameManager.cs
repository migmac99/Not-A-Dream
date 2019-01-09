using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Animator animator;
    public GameObject player;

    ///////////////////////////////Health///////////////////////////////
    public float playerHealth;
    public bool damaged;

    public GameObject Health60;
    public GameObject Health30;
    public GameObject Health15;

    Image Health60SR;
    Image Health30SR;
    Image Health15SR;
    Color currentColor;

    void Awake () {
        Health60SR = Health60.GetComponent<Image> ();
        Health30SR = Health30.GetComponent<Image> ();
        Health15SR = Health15.GetComponent<Image> ();

        animator = player.GetComponent<Animator> ();
    }
    // Update is called once per frame
    void Update () {
        Player_Health ();
    }

    public void TakeDamage (float ammount) {
        playerHealth -= ammount;
        damaged = true;
    }

    void Player_Health () {
        if (damaged) {
            animator.SetBool ("isPlayerReceivingDamage", true);
        } else {
            animator.SetBool ("isPlayerReceivingDamage", false);
        }
        damaged = false;
        ////////////////////////////////////////////////////////
        if (playerHealth <= 60) {
            currentColor = Health60SR.color;
            Health60SR.enabled = true;
            Health60SR.color = currentColor;
        }
        if (playerHealth <= 30) {
            currentColor = Health30SR.color;
            Health30SR.enabled = true;
        }
        if (playerHealth <= 15) {
            currentColor = Health15SR.color;
            Health15SR.enabled = true;
        }
        if (playerHealth <= 0) {
            animator.SetBool ("isPlayerAlive", false);
        } else {
            animator.SetBool ("isPlayerAlive", true);
        }
        ////////////////////////////////////////////////////////
        if (playerHealth >= 60) {
            currentColor = Health60SR.color;
            Health60SR.enabled = false;
            Health60SR.color = currentColor;
        }
        if (playerHealth >= 30) {
            currentColor = Health30SR.color;
            Health30SR.enabled = false;
        }
        if (playerHealth >= 15) {
            currentColor = Health15SR.color;
            Health15SR.enabled = false;
        }
    }
}