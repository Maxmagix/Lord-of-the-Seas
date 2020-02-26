using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace Cameras {
    public class translateCamera : MonoBehaviour
    {
        public float speed;
    	public List<Camera> cameras = new List<Camera>();
        public int state;
        public List<GameObject> UIs = new List<GameObject>();
        public Camera in_between_cam; 
        private bool translating;
        private float val = 0;
        private int direction_translation = 0;
        private float CPUElapsedTime;


        // Start is called before the first frame update
        void Start()
        {
            translating = false;
            deactivate();
            setState(state);
        }

        public void translate(int dir) {
            deactivate();
            var startpos = cameras[state].gameObject.transform.position;
            in_between_cam.gameObject.transform.position = startpos;
            var startrot = cameras[state].gameObject.transform.rotation;
            in_between_cam.gameObject.transform.rotation = startrot;
            in_between_cam.gameObject.SetActive(true);
            direction_translation = dir;
            translating = true;
            val = 0;
        }

        private void move(int dir) {
            var actualpos = in_between_cam.gameObject.transform.position;
            var startpos = cameras[state].gameObject.transform.position;
            var endpos = cameras[state + dir].gameObject.transform.position;
            var startrot = cameras[state].gameObject.transform.rotation;
            var endrot = cameras[state + dir].gameObject.transform.rotation;
            var direction = endpos - startpos;
            var angle = new Vector4(endrot.x - startrot.x, endrot.y - startrot.y, endrot.z - startrot.z, endrot.w - startrot.w);
            var scaleddirection = new Vector3(direction.x / (float)(speed), direction.y / (float)(speed), direction.z / (float)(speed));
            var scaledangle = new Vector4(angle.x / (float)(speed), angle.y / (float)(speed), angle.z / (float)(speed), angle.w / (float)(speed));
            if (val > speed) {
                in_between_cam.gameObject.SetActive(false);
                setState(state + dir);
                translating = false;
            }
            in_between_cam.gameObject.transform.position = new Vector3(actualpos.x + scaleddirection.x * CPUElapsedTime,
             actualpos.y + scaleddirection.y * CPUElapsedTime,
              actualpos.z + scaleddirection.z * CPUElapsedTime); 
            var actualrot = in_between_cam.gameObject.transform.rotation;
            Quaternion newrot = new Quaternion(actualrot.x + scaledangle.x * CPUElapsedTime,
             actualrot.y + scaledangle.y * CPUElapsedTime,
              actualrot.z + scaledangle.z * CPUElapsedTime,
               actualrot.w + scaledangle.w * CPUElapsedTime); 
            in_between_cam.gameObject.transform.rotation = newrot;
            val += CPUElapsedTime;
        }

        private void deactivate() {
            foreach (Camera camera in cameras) {
                camera.gameObject.SetActive(false);
            }
            foreach (GameObject ui in UIs) {
                ui.gameObject.SetActive(false);
            }     
        }

        public void setState(int new_state) {
            state = new_state;
            if (state >= cameras.Count)
                state = cameras.Count - 1;
            deactivate();
            cameras[state].gameObject.SetActive(true);
            UIs[state].gameObject.SetActive(true);
        }

        private void Update() {
            CPUElapsedTime = Time.deltaTime;
            if (translating) {
                move(direction_translation);
            }
        }
    }
}