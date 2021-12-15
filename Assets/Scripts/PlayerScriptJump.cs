using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptJump : MonoBehaviour
{
   public static PlayerScriptJump instance;

   private Rigidbody2D _rb;
   private Animator _anim;

   private float forceX, forceY;
   private float treshHoldX = 7f, treshHoldY = 14f;

   private bool setPower, didJump;

   private Slider _slider;
   private float sliderTreshHold = 10f;
   private float sliderBarValue = 0f;
   private void Awake()
   {
      MakeInstance();
      
      Initialize();

      _slider = GameObject.Find("PowerBar").GetComponent<Slider>();
      _slider.minValue = 0f;
      _slider.maxValue = 10f;
      _slider.value = sliderBarValue;
   }
   
   private void MakeInstance()
   {
      if (instance == null)
      {
         instance = this;
      }
   }

   private void Initialize()
   {
      _rb = GetComponent<Rigidbody2D>();
      _anim = GetComponent<Animator>();
   }
   private void Update()
   {
      SetPower();
   }

  

   void SetPower()
   {
      if (setPower)
      {
         forceX += treshHoldX * Time.deltaTime;
         forceY += treshHoldY * Time.deltaTime;

         if (forceX > 6.5f)
         {
            forceX = 6.5f;
         }

         if (forceY > 13.5f)
         {
            forceY = 13.5f;
         }

         sliderBarValue += sliderTreshHold * Time.deltaTime;
         _slider.value = sliderBarValue;
      }
   }

   public void SetPower(bool setPower)
   {
      this.setPower = setPower;

      if (!setPower)
      {
         Jump();
      }
   }

   void Jump()
   {
      _rb.velocity = new Vector2(forceX, forceY);
      
      forceX = forceY = 0f;//обнуляем!
      didJump = true;

      _anim.SetBool("Jump",true);
      sliderBarValue = 0f;
      _slider.value = sliderBarValue;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
     
      if (didJump)
      {
         didJump = false;
         
         _anim.SetBool("Jump",false);
         
         if (other.CompareTag("Platform"))
         {
            Sound.instance.PlaySound(SOUNDFX.JUMP);
            if (GameManger.instance != null)
            {
               GameManger.instance.CreateNewPlatformAndLerp(other.transform.position.x);
               GameManger.instance.IncreaseScore();
            }
         }
      }

      if (other.CompareTag("DeadZone"))
      {
         if (GameOverManger.instance != null)
         {
            GameOverManger.instance.ShowGameOverPanel();
         }
         Destroy(gameObject);
      }
   }
}
