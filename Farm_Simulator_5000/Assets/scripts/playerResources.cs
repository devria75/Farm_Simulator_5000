using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerResources : MonoBehaviour {

	//start with $15
	public static int money = 15;
	public Text textElement;

	public GameObject StoreObj;

	//intergers for how many of each seed player has
	public int carrotCounter = 0;
	public int cornCounter = 0;
	public int potatoCounter = 0;
	public int tomatoCounter = 0;

	//text elements
	public Text carrotText;
	public Text cornText;
	public Text potatoText;
	public Text tomatoText;
	public Text storeMoneyText;

	// Use this for initialization
	void Start () {
		//instantiate text upon load with how many items each has
		textElement.text = money.ToString();
		carrotText.text = carrotCounter.ToString ();
		cornText.text = cornCounter.ToString ();
		potatoText.text = potatoCounter.ToString ();
		tomatoText.text = tomatoCounter.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		//update how many of each element you have
		textElement.text = money.ToString();
		carrotText.text = "Carrot seeds: " + carrotCounter.ToString ();
		cornText.text = "Corn seeds: " + cornCounter.ToString ();
		potatoText.text = "Potato seeds: " + potatoCounter.ToString ();
		tomatoText.text = "Tomato seeds: " + tomatoCounter.ToString ();
		storeMoneyText.text = "$" + money.ToString ();

		//if I is pressed open the store
		if(Input.GetKeyDown(KeyCode.I))
		{
			OpenStore();
		}
	}

	//Add money to player's money
	public void addMoney(int Money){
		money += Money;
		textElement.text = money.ToString();
	}

	//subtract money from player's money
	public void subtractMoney(int Money){
		money -= Money;
		textElement.text = money.ToString();
	}

	//Open store
	public void OpenStore()
	{
		StoreObj.SetActive (!StoreObj.activeSelf);

		if (Cursor.lockState == CursorLockMode.None) {
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if(Cursor.lockState == CursorLockMode.Locked) {
			Cursor.lockState = CursorLockMode.None;
		}




			
	}

	//what happens when each element is bought
	public void BuyCarrot()
	{
		if (money < 5) {
			Debug.Log ("Can't buy that, not enough money");
		} else 
		{
			money = money - 5;
			carrotCounter++;
		}
	}

	public void BuyCorn()
	{
		if (money < 3) {
			Debug.Log ("Can't buy that, not enough money");
		} else 
		{
			money = money - 3;
			cornCounter++;
		}
	}

	public void BuyPotato()
	{
		if (money < 4) {
			Debug.Log ("Can't buy that, not enough money");
		} else 
		{
			money = money - 4;
			potatoCounter++;
		}
	}

	public void BuyTomato()
	{
		if (money < 7) {
			Debug.Log ("Can't buy that, not enough money");
		} else 
		{
			money = money - 7;
			tomatoCounter++;
		}
	}

}
