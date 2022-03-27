using System.Collections.Generic;
using UnityEngine;


/*
   ,   ,
  /////|
 ///// |
|~~~|  | 
|===|  |  
|J  |  |  BY JOSE JASPE <JCODE>
| - |  |  Para: Josue Gaming (comentario de youtube)
|  G| /
|===|/
'---'

metric ruler is the main class that is in charge of building a square-type visual ruler that is built using the mesh.
*/
public class ExampleMultiPlataformScreemPoint : MonoBehaviour
{
      Camera camerainUse;
      RaycastHit RCH;
      /// <summary>
      /// Variable that stores the beam that will be launched from the camera depending on the position
      /// of the touched screen, towards the scene.
      /// </summary>
      Ray _ray;
      
      void Start()
      {
          //Get main camera
          camerainUse = Camera.main;
      }

      
      void Update()
      {
            if(HasCliked()){
                 Vector3 screenPos = getTouchScreen();
                 Vector3 hit = getHitPosition();
                 Debug.DrawLine(camerainUse.transform.position, hit);
            }
      }

      /// <summary>
      /// gets a point on the screen, the result varies from platform to platform.
      ///
      /// esta funcion optiene un vector3 que enrealidad es un vector2 que corresponde a la posicion de la pantalla que se este tocando dependiendo
      /// de la plataforma donde se este ejecutando el codigo.
      ///
      /// </summary>
      /// <returns>
      /// a vector3 with the representation of the position of the click or touch
      /// retorna un vector 3 representado la posicion en pantalla del clic o touch.
      /// </returns>
      Vector3 getTouchScreen(){
          #if UNITY_ANDROID || UNITY_IOS
              if(Input.touchCount > 0){
                  return Input.GetTouch(0).position;
              }
              return Vector3.zero;
          #else
              return Input.mousePosition;
          #endif
      }
      
      


      /// <summary>
      /// it checks if the screen was clicked and it varies depending on the platform.
      ///
      /// esta funcion checa si se preciono o se hizo clic sobre la pantalla dependiendo de la plataforma
      /// </summary>
      /// <returns>
      /// Returns a boolean corresponding to the state of the click
      /// Retorna un booleano dependiendo de si esta hay algo tocando o hacinedo click en la pantalla
      ///</returns>
      bool HasCliked(){
           #if UNITY_ANDROID || UNITY_IOS
              if(Input.touchCount > 0){
                  return true;
              }else{return false;}
          #else
              //return(Input.GetMouseButtonUp(2)); //Middle mouse button
              //return(Input.GetMouseButtonUp(1)); //Right mouse button
              return(Input.GetMouseButtonUp(0)); //Left mouse button
          #endif
      }
      
      
      
      /// <summary>
      /// devuelve el punto de impacto
      /// </summary>
      /// <param name="screemPosition">
      /// Se debe pasar el punto tocado en la pantalla
      /// </param>
      /// <returns>
      /// Retorna un punto en el espacio.
      /// </returns>
      Vector3 getHitPosition(Vector3 screemPosition){
          _ray = camerainUse.ScreenPointToRay(screemPosition);
          if(Physics.Raycast(_ray, out RCH, 100f , LayerCollider)){
              if(magneticPoints){
                  return findMangeticPoint();
              }else{
                  return RCH.point;
              }
          }else{
              return _ray.GetPoint(10);
          }
      }
}
