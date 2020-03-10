using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class Races
	{
		#region обьявление делегатов + их переменных

		delegate void MoveAllCars();
		delegate void ShowCarsInfo();
		delegate void ShowCarsInfoEnd();
		delegate void PrepairToStartAll();
		delegate bool IsfinishedCar();
		/// <summary>
		/// делегат движения всех машин
		/// </summary>
		MoveAllCars moveAllCars;
		/// <summary>
		/// делегат отображения данных о машинах
		/// </summary>
		ShowCarsInfo showCarsInfo;
		/// <summary>
		/// делегат собирает все методы финальных стадий машин
		/// </summary>
		ShowCarsInfoEnd showCarsInfoEnd;
		/// <summary>
		/// делегат сброс параметров машин на 0
		/// </summary>
		PrepairToStartAll prepairToStartAll;
		/// <summary>
		/// делеаг собирает методы проверки на финише или нет
		/// </summary>
		IsfinishedCar isfinishedCar;
		#endregion

		List<Car> car;
		int winner;


		void Prepair()
		{
			foreach (var item in car)
			{
				///формирую массив методов в делегат
				moveAllCars += item.MoveCar;
				showCarsInfo += item.ShowCarInfo;
				showCarsInfoEnd += item.ShowWhenEndPoint;
				prepairToStartAll += item.PrepearToStart;
				isfinishedCar += item.CheckFInished;
				
				///подписки на начало игры, движение машины, и финиш и изменение скорости
				item.Inicilization += Item_Inicilization;
				item.MoveShowEvent += Item_MoveShowEvent;
				item.Onfinish += Item_Onfinish;
				item.ChangeSpeed += Item_ChangeSpeed;
			}
		}



		void ResetDeligate()
		{
			moveAllCars = null;
			showCarsInfo = null;
			prepairToStartAll = null;
			isfinishedCar = null;
			showCarsInfoEnd = null;
			winner = 1;
			GC.Collect();
		}

		/// <summary>
		/// машина изменила скорость, ивент это покажет
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Item_ChangeSpeed(object sender, EventArgs e)
		{
			if (sender is Car)
			{
				Car a = sender as Car;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("This care changed speed: " + a.Speed);
				Console.ResetColor();
			}
		}


		/// <summary>
		/// когда срабатывает событие финиша, присваиваю машине место и вывожу ее из гонки
		/// при этом из массива делегатов финиша машина удаляется
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Item_Onfinish(object sender, EventArgs e)
		{
			if (sender is Car)
			{
				Car a = sender as Car;
				a.Winner_places = winner;
				Console.Write("This care is finished! ");
				a.ShowWhenEndPoint();
				winner++;
				isfinishedCar -= a.CheckFInished;//удаляю финиш машину из массива делигатов
				moveAllCars -= a.MoveCar;
				sender = a;
			}
		}


		/// <summary>
		/// евент на изменение текущей позиции машины
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Item_MoveShowEvent(object sender, EventArgs e)
		{
			if (sender is Car)
			{
				Car a = sender as Car;
				if (!a.CheckFInished()) Console.WriteLine("Car: " + a.Name + "; type of car: " + a.Type_car + "; have new position: " + a.Distance);
			}
		}

		private void Item_Inicilization(object sender, EventArgs e)
		{
			if (sender is Car)
			{
				Car a = sender as Car;
				Console.Write("Iam ready!");
				a.ShowCarInfo();
			}
		}

		void StartGame()
		{
			ResetDeligate();

			Prepair();
			Console.WriteLine("=================Prepair Label================================");
			prepairToStartAll();
			Console.WriteLine("====================End Label=================================");
			Console.Write("pusk key for continue: ");
			Console.ReadLine();
			Console.Clear();
			while (isfinishedCar != null)
			{
				Console.WriteLine("===================Races Label================================");
				moveAllCars();
				Console.WriteLine("====================End Label=================================");
				Console.Write("pusk key for continue: ");
				Console.ReadLine();
			}
			Console.WriteLine("=====================RACES END================================");
			showCarsInfoEnd();
			Console.WriteLine("====================GAME END==================================");
			Console.Write("pusk key for continue: ");
			Console.ReadLine();
			UserMenu();

		}

		/// <summary>
		/// меню для пользователя
		/// </summary>
		public void UserMenu()
		{
			Console.WriteLine("1. Start game");
			Console.WriteLine("2. Exit");
			int.TryParse(Console.ReadLine(), out int result);
			switch (result)
			{
				case 1:
					Console.Clear();
					Console.Write("Enter total numbers of car: ");
					if (car.Count > 0) car.Clear();
					if (int.TryParse(Console.ReadLine(), out int res))
					{
						if (res < 2)
						{
							car.Add(new SportCar("Fustig"));//создал первую машину спортмашиной
							car.Add(new SportCar("Fustig 2"));//создал первую машину спортмашиной	
						}
						else
						{
							car.Add(new SportCar("Fustig"));//создал первую машину спортмашиной
							for (int i = 0; i < res-1; i++)
							{
								int r = Car.rand.Next(1, 5);
								switch (r)
								{
									case 1: car.Add(new SportCar("Mustang " + (i).ToString())); break;
									case 2: car.Add(new Bus("AutoBus " + (i).ToString())); break;
									case 3: car.Add(new Truck("Trucker " + (i).ToString())); break;
									case 4: car.Add(new PassengerCar("PassCar " + (i).ToString())); break;
									default:
										break;
								}
							}
						}
						StartGame();
					}
					else
					{
						Console.WriteLine("wrong input");
						UserMenu();
					}
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// констуктор по умолчанию, запрос количества машин в меню
		/// </summary>
		public Races()
		{
			car = new List<Car>();
			moveAllCars = null;
			showCarsInfo = null;
			prepairToStartAll = null;
			isfinishedCar = null;
			showCarsInfoEnd = null;
			winner = 1;
		}

	}


}

