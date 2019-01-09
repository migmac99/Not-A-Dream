using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    [HideInInspector]
    public Animator animator;

    public GameObject player;
    [Space (10)]
    ///////////////////////////////Health///////////////////////////////
    public float playerHealth;
    [Space (5)]
    [Range (0, 20)] public int PassiveRegenValue;
    [Range (0, 5)] public int RegenEvery_x_Seconds;
    [Space (10)]
    public bool damaged;
    public bool invincible = false;
    [Space (20)]
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

        Regen (PassiveRegenValue, true); //This is the passive regen of the player
    }
    // Update is called once per frame
    void Update () {
        Player_Health ();
    }

    // Reusable timer that will execute CODE_HERE after the timer is done --> used in fight timers and such
    // This is creating a CoRoutine which runs independently of the function it is called from
    // StartCoroutine (Countdown (3f, () => {CODE_HERE}));
    IEnumerator Countdown (float seconds, Action onComplete) {
        yield return new WaitForSecondsRealtime (seconds);
        onComplete ();
    }

    public void TakeDamage (float ammount) {
        if ((!invincible) && (playerHealth > 0)) {
            playerHealth -= ammount;
            damaged = true;
        }
    }

    public void Regen (float ammount, bool repeat = false) {
        if ((!PauseMenu.GameIsPaused) && (playerHealth > 0) && (playerHealth < 100)) {
            playerHealth += ammount;
        }
        if (repeat) {
            StartCoroutine (Countdown (RegenEvery_x_Seconds, () => {
                if ((!PauseMenu.GameIsPaused) || (playerHealth > 0) || (playerHealth < 100)) {
                    Regen (ammount, true);
                } else {
                    Regen (0, true);
                }
            }));
        }
    }

    void Player_Health () {
        if (playerHealth > 100) {
            playerHealth = 100;
        }

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