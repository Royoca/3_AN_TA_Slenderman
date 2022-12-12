using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Slenderman : MonoBehaviour
{
    
  [SerializeField]
  float movementSpeed;
  [SerializeField]
  float pageSpeedMultiplier;
  NavMeshAgent agent;
  GameObject player;
  PlayerPages pages;
  [SerializeField]
  MeshRenderer renderer;
  [SerializeField]
  Camera playerCamera;
  [SerializeField]
  float distance;
  [SerializeField]
  VideoPlayer video;
  public bool runAway;

  
  float auxMovementSpeed;
  float intensity;
  
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    player = GameObject.Find("Player");
    pages = player.GetComponent<PlayerPages>();
    auxMovementSpeed = movementSpeed;
    intensity = 0.0f;
  }
  
  void Update()
  {
    AIUpdate();
    SpeedUpdate();
    StaticUpdate();
    if(intensity >= 1.0f) {
      KillPlayer();
    }
  }

  void StaticUpdate() {
    if(IsTargetVisible(playerCamera, renderer.gameObject) && Vector3.Distance(transform.position, player.transform.position) < distance + distance * 0.2f) {
      intensity += Time.deltaTime * 0.03f * (pages.currentPages + 1);
    } else {
      intensity -= Time.deltaTime * 0.2f * (pages.currentPages + 1);
    }
    intensity = Mathf.Clamp(intensity,0.0f,1.0f);
    video.targetCameraAlpha = intensity;
    video.SetDirectAudioVolume(0,intensity);
  }

  void AIUpdate() {
    if(!runAway) {
      agent.destination = player.transform.position;
      gameObject.transform.LookAt(player.transform);
      if(Vector3.Distance(transform.position, player.transform.position) < distance) {
        if(IsTargetVisible(playerCamera, renderer.gameObject)) {
          agent.speed = 0.0f;
        } else {
          agent.speed = movementSpeed;
        }
      } else {
        agent.speed = movementSpeed;
      }
    } else {
      transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
      Vector3 runTo = transform.position + transform.forward * 20.0f;
      agent.speed = 2.0f;
      agent.destination = runTo;
    }
  }

  void SpeedUpdate() {
    if(pages.currentPages > 0) {
      movementSpeed = auxMovementSpeed * pages.currentPages * pageSpeedMultiplier;
    } else {
      movementSpeed = auxMovementSpeed;
    }
  }

  bool IsTargetVisible(Camera c,GameObject go)
  {
    var planes = GeometryUtility.CalculateFrustumPlanes(c);
    var point = go.transform.position;
    foreach (var plane in planes)
    {
    if (plane.GetDistanceToPoint(point) < 0)
      return false;
    }
    return true;
  }

  void KillPlayer() {
    SceneManager.LoadScene("Menu");
  }
}


