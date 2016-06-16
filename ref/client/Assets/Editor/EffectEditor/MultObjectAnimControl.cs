using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class MultObjectAnimControl : EditorWindow
{
   
   bool m_bPlaying = false;
   bool m_bNeedUpdate = false;
   float m_fTime = 0.0f;
   float m_fMaxTime = 0.0f;
   uint  m_uAnimCount =0;

   public class DelayObject
   {
          public float m_fDelayTime;
          public GameObject m_GameObject;
    }
    List<DelayObject> m_ListDelayObject = new List<DelayObject> ();


    void OnEnable()
   {
          m_bNeedUpdate = true;
    }

    void OnDestroy()
    {
     
     
     }

     void Update()
    {  
           if (m_bPlaying)
           {
                m_fTime += Time.fixedDeltaTime;
                if (m_fTime>m_fMaxTime)
                {
                       m_fTime = 0.0f;
                 }
            }
            bool bErase = true;
            while (bErase)
            {
                bErase = false;
                foreach(DelayObject obj in m_ListDelayObject)
                { 
                       if (obj.m_GameObject==null)
                       {
                           bErase = true ;
                           m_ListDelayObject.Remove(obj);
                           break;
                        }
                  }

             }
            if (m_bPlaying|| m_bNeedUpdate)
            {
                   m_fMaxTime = 0.0f;
                   m_uAnimCount = 0;
                   GameObject[ ] obj = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
                   foreach (GameObject gameObj in obj)
                   {
                            Animation anim = gameObj.GetComponent<Animation>();
                            ParticleSystem particle = gameObj.GetComponent<ParticleSystem>();
                            float fDelay =0.0f;
                            foreach (DelayObject exist in m_ListDelayObject)
                            {
                                     if(exist.m_GameObject == gameObj)
                                     {
                                           fDelay =exist.m_fDelayTime;
                                           break;
                                      }
                             }
                            float fTime = m_fTime - fDelay;
                            if (fTime<0)
                            {
                                    fTime = 0.0f;
                             }
                            if (anim != null && anim.clip !=null)
                            {
                                   ++m_uAnimCount;
                                   if(fTime<anim.clip.length)
                                   { 
                                          anim.clip.SampleAnimation(gameObj,fTime);
                                   }
                                   else
                                   {
                                           anim.clip.SampleAnimation(gameObj,anim.clip.length);
                                    }
                                   if (m_fMaxTime < anim.clip.length+fDelay)
                                    { 
                                           m_fMaxTime = anim.clip.length+fDelay;
                                      
                                     }
                             }
                             if(particle!=null)
                             {
                                         particle.Simulate(fTime);
                              }
                     }
                     m_bNeedUpdate = false;
                     Repaint();
                 }
      }



     void OnGUI()
     {
               GUILayout.Label("动画"+ m_uAnimCount +"个,时间（秒）:"+ m_fTime +"/"+ m_fMaxTime);
               float fTime = GUILayout.HorizontalSlider(m_fTime,0.0f,m_fMaxTime);
               string strTime = fTime.ToString();
               if(!strTime.Contains("."))
               {
                      strTime+=".0";
                }
                strTime = GUILayout.TextField(strTime);
               float.TryParse(strTime, out fTime);
               if(Mathf.Abs(fTime-m_fTime)>0.001f)
               {
                      m_bPlaying = false;
                      m_fTime = fTime;
                      m_bNeedUpdate = true;
                }
                if(m_bPlaying)
                {
                      if(GUILayout.Button("停止"))
                       {
                         m_bPlaying = false;
                        }  
                    
                 }
                      
                 else
                 {
                       if(GUILayout.Button("播放"))   
                       {    
                        m_bPlaying = true;
                        }
                   }
                 if (GUILayout.Button("选中物体延迟到当前时间播放"))
                 {
                    UnityEngine.Object[] selectobj = Selection.objects;
                    foreach (UnityEngine.Object obj in selectobj)
                    {
                          if(obj is GameObject) 
                          { 
                              GameObject gameObject = (GameObject)obj;
                              bool bFind = false;
                              foreach (DelayObject exist in m_ListDelayObject)  
                              {
                                    if(exist.m_GameObject==gameObject) 
                                    {
                                            exist.m_fDelayTime = m_fTime;
                                            bFind = true;
                                            break;
                                     }
                               }
                               if(!bFind)
                               {
                                       DelayObject delay =new DelayObject();
                                       delay.m_GameObject = gameObject;
                                       delay.m_fDelayTime = m_fTime;
                                       m_ListDelayObject.Add(delay);
                                }
                                m_bNeedUpdate =true;
                     }
                }
           }
     }
}

                             
        




















