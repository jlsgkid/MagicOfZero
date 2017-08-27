using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using System.Collections;

using UnityEngine.UI;

namespace VRStandardAssets.Menu
{
    // This class 'pops' each of the menu items out
    // when the user looks at them.
    public class MenuItemPopout : MonoBehaviour
    {
        [SerializeField] private Transform m_Transform;         // Used to control the movement whatever needs to pop out.
        //[SerializeField] private VRInteractiveItem m_Item;      // The VRInteractiveItem of whatever should pop out.
        [SerializeField] private float m_PopSpeed = 8f;         // The speed at which the item should pop out.
        public float m_PopDistance = 60f;    // The distance the item should pop out.


        private Vector3 m_StartPosition;                        // The position aimed for when the item should not be popped out.
        private Vector3 m_PoppedPosition;                       // The position aimed for when the item should be popped out.
        private Vector3 m_TargetPosition;                       // The current position being aimed for.
		[SerializeField] private Renderer m_Renderer;
		private const string k_SliderMaterialPropertyName = "_SliderValue"; 
		private AsyncOperation op;
		[SerializeField] private Text load;
		private int currLoad = 0;
		private bool isFirstLoad = true;
		public AudioSource ass;

        private void Start ()
        {
            // Store the original position as the one that is not popped out.
            m_StartPosition = m_Transform.position;

            // Calculate the position the item should be when it's popped out.
			m_PoppedPosition = m_Transform.position - m_Transform.forward * m_PopDistance * 2;

			ass = this.GetComponent<AudioSource> ();
        }

		public void GazeIn(){
			m_TargetPosition = m_PoppedPosition;
			m_Transform.position = Vector3.MoveTowards(m_Transform.position, 
				m_TargetPosition, m_PopSpeed * Time.deltaTime);
			//if(isFirstLoad) LoadStage01 ();
			//isFirstLoad = false;
		}

		public void GazeOut(){
			m_TargetPosition = m_StartPosition;
			m_Transform.position = Vector3.MoveTowards(m_Transform.position, 
				m_TargetPosition, m_PopSpeed * Time.deltaTime);
		}

		public void GazeClick1(){
			//SceneManager.LoadScene ("Scene01");
			//Debug.Log("sasasa----");
			if(isFirstLoad) LoadStageF ();
			isFirstLoad = false;
			if(!ass.isPlaying){
				ass.Play ();
			}
		}
		public void GazeClick(){
			//SceneManager.LoadScene ("Scene01");
			if(isFirstLoad) LoadStage01 ();
			isFirstLoad = false;
			if(!ass.isPlaying){
				ass.Play ();
			}
		}

		private void LoadStageF(){
			StartCoroutine (StartLoading("MeshBkrTest"));
		}

		private void LoadStage01(){
			StartCoroutine (StartLoading("Scene03_bake"));
		}
			
		private IEnumerator StartLoading(string sceneName) {
			Debug.Log(sceneName);
			op = SceneManager.LoadSceneAsync(sceneName);
			op.allowSceneActivation = false;  
			yield return op;
		}

		void Update(){

			int loadVale = 0;
			if(op == null){
				return;
			}
			if (op.progress < 0.9f) {
				//load.text = "Loading :" + (int)op.progress * 100;
				loadVale = (int)op.progress * 100;
			} else {
				//load.text = "Loading :100";
				loadVale = 100;
			}

			if(currLoad < loadVale ){
				currLoad++;
			}
			//load.text = "Loading :" + currLoad +"%";
			SetLoadingPercentage (currLoad/100.0f);
			if(currLoad == 100){
				op.allowSceneActivation = true;  
			}
		}

		private void SetLoadingPercentage (float sliderValue)
		{
			// If there is a renderer set the shader's property to the given slider value.
			if(m_Renderer)
				m_Renderer.sharedMaterial.SetFloat (k_SliderMaterialPropertyName, sliderValue);
		}

//        private void Update ()
//        {
//            // Set the target position based on whether the item is being looked at or not.
//            m_TargetPosition = m_Item.IsOver ? m_PoppedPosition : m_StartPosition;
//
//            // Move towards the target position.
//            m_Transform.position = Vector3.MoveTowards(m_Transform.position, m_TargetPosition, m_PopSpeed * Time.deltaTime);
//        }
    }
}