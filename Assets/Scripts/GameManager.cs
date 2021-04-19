using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform RedChip;
	public Transform BlueChip;

	        //0 - клетка пуста
	        //1 - красная фишка
	        //2 - синяя фишка
	        //3 - красная выигрышная фишка
	        //4 - синяя выигрышная фишка
	        int[,] gameBoard = new int[6, 7]
	        {
	           {0,0,0,0,0,0,0},
	           {0,0,0,0,0,0,0},
	           {0,0,0,0,0,0,0},
	           {0,0,0,0,0,0,0},
	           {0,0,0,0,0,0,0},
	           {0,0,0,0,0,0,0}
	        };

	//Определяет есть ли победитель
	int GameWin()
	{
		int winerIndex = 0;
		string str = "";

		//Проверка победитилей по горизонтали
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 7; x++)
				str += gameBoard [y, x];
			str += ";";                
				}

		//Проверка победитилей по вертикали
		for (int x = 0; x < 7; x++){		
		for (int y = 0; y < 6; y++)
				str += gameBoard [y, x];
			str += ";";                
		}

		//Проверка победитилей по диагонали вправо
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 7; x++)	{
				for (int z = 0; z < 6; z++) {
					if(-1 < x+z && x+z < 7 && -1< y+z && y+z < 6)
						str+=gameBoard [y+z, x+z];
				}
				str += ";";
			}				
			str += ";";       
		}

		//Проверка победитилей по диагонали влево
		for (int y = 0; y < 6; y++) {
			//строка
			for (int x = 0; x < 7; x++)	{
				for (int z = 0; z < 6; z++) {
					if(-1 < x-z && x-z < 7 && -1< y-z && y-z < 6)
						str+=gameBoard [y-z, x-z];
				}
				str += ";";
			}				
			str += ";";
		}

		if(str.IndexOf ("1111")!=-1)
				winerIndex = 1;
			else if(str.IndexOf ("2222")!=-1)
				winerIndex = 2;

		return winerIndex;
	}	       

	//Возвращает номер текущего хода
	int ViewCurStepGame()
	{
		int numCurrrentStep = 0;
		string tboxRow = "";
		for (int x = 0; x < 6; x++) {
			tboxRow = "";
			for (int y = 0; y < 7; y++)
			{
			if (gameBoard [x, y] != 0) {
				numCurrrentStep++;
			}
			tboxRow += gameBoard [x, y] + " ";
			}
		}
		return numCurrrentStep;
	}

	int GetRowPositionForChip(int cell)
	{
		int row = -1;
		for (int j = 0; j < 6; j++) 
			if (gameBoard [5 - j, cell] == 0)
				if(row == -1)
					row = 5 - j;
		if (row != -1)
			gameBoard[row,cell] = (ViewCurStepGame() % 2 == 0) ? 1 : 2;

		return row;
	}

	Vector3 GetPositionForChip(int cell)
	{
		Vector3 position = Vector3.zero;
		int row = GetRowPositionForChip (cell);
		if (row != -1) 
			position = new Vector3 (-4.2f+cell*1.4f,3.7f-row*1.5f, 0);
		return position;
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Ход красных!");
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(
			new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			-Camera.main.transform.position.z)
			);
		float posX = mousePos.x + 4f;
		
		float fl = 8f/6f;
		if(posX <0)
			posX = 0;
		else if(posX>8)
			posX = 8;
		else			
			if((posX)%fl>fl/2f)				
				posX = (float)((int)(posX/fl+1)*fl);
		else posX = (float)((int)(posX/fl)*fl);
		
		posX =posX-4f;

			if (Input.GetButtonDown("Fire1")) {

			mousePos = GetPositionForChip((int)((posX+4.5f)/fl));
			if(mousePos != Vector3.zero)
			{
			if(ViewCurStepGame() % 2 != 0)
			Instantiate(RedChip, mousePos, Quaternion.identity);
			else
				Instantiate(BlueChip, mousePos, Quaternion.identity);
			}
			if(GameWin() == 1)
			Debug.Log("Победили красные!!!");
			else if(GameWin() == 2) Debug.Log("Победили синие!!!");
			else if(ViewCurStepGame() % 2 == 0)
				Debug.Log("Ход красных!");
			else Debug.Log("Ход синих!");
				}
	}
}