using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class growPlants : MonoBehaviour {

	public float timeLeft;
	public int timer = 30;
	public bool playerInCol = false;
	private GameObject plant;
	public GameObject Carrot;
	public GameObject Corn;
	public GameObject Potato;
	public GameObject Sprout;
	public GameObject Tomato;
	public bool plotFilled;
	public enum PlotStatus {
		Empty=0, Corn=6, Carrot=8, Potato=8, Tomato=10
	};
	public PlotStatus plantInPlot;

	public int dirtVerticalOffset = 100;
	public Text dirtText;
	public int sellVerticalOffset = 100;
	public Text sellText;

	// Use this for initialization
	void Start () {
		plotFilled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		if (playerInCol == true) {
			if (Input.GetKeyDown("1") && plotFilled == false && FindObjectOfType<playerResources>().cornCounter > 0){
				plant = Instantiate(Corn, transform.position, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Corn;
				plotFilled = true;
			}
			if (Input.GetKeyDown("2") && plotFilled == false && FindObjectOfType<playerResources>().carrotCounter > 0){
				plant = Instantiate(Carrot, transform.position+Vector3.down*.3f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Carrot;
				plotFilled = true;
			}
			if (Input.GetKeyDown("3") && plotFilled == false && FindObjectOfType<playerResources>().tomatoCounter > 0){
				plant = Instantiate(Sprout, transform.position+Vector3.up*.3f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Tomato;
				plotFilled = true;
			}
			if (Input.GetKeyDown("4") && plotFilled == false && FindObjectOfType<playerResources>().potatoCounter > 0){
				plant = Instantiate(Potato, transform.position+Vector3.up*.1f, Quaternion.identity) as GameObject;
				timeLeft = timer;
				plantInPlot = PlotStatus.Potato;
				plotFilled = true;
			}
		}

		if (plotFilled == true && timeLeft <= 0 && playerInCol) {
			Vector3 newPos = sellText.rectTransform.position;
			newPos.y = sellVerticalOffset;
			sellText.rectTransform.position = newPos;

			if (Input.GetKeyDown ("f")) {
				FindObjectOfType<playerResources> ().addMoney ((int)plantInPlot);
				plantInPlot = PlotStatus.Empty;
				plotFilled = false;
				Destroy (plant);
			}
		}
	}

	void OnTriggerEnter(Collider Col){
		if (plotFilled == false) {
			Vector3 newPos = dirtText.rectTransform.position;
			newPos.y = dirtVerticalOffset;
			dirtText.rectTransform.position = newPos;
		}
		playerInCol = true;
	}

	void OnTriggerExit(Collider Col){
		Vector3 newPos = dirtText.rectTransform.position;
		newPos.y = dirtVerticalOffset * -1;
		dirtText.rectTransform.position = newPos;

		Vector3 newPos2 = sellText.rectTransform.position;
		newPos2.y = sellVerticalOffset * -1;
		sellText.rectTransform.position = newPos2;

		playerInCol = false;
	}
		
}