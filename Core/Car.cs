using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{

	public abstract class Car
	{
		/// <summary>
		/// показывает каждый шаг машины
		/// </summary>
		public event EventHandler MoveShowEvent;
		/// <summary>
		/// событие для определения на финише ли мы
		/// </summary>
		public event EventHandler Onfinish;
		/// <summary>
		/// сброс настроек на 0, первичная иницилизация
		/// </summary>
		public event EventHandler Inicilization;
		/// <summary>
		/// евент на изменение скорости машины
		/// </summary>
		public event EventHandler ChangeSpeed;

		/// <summary>
		/// название машины
		/// </summary>
		protected string name;
		/// <summary>
		/// текущая пройденная дист
		/// </summary>
		protected int distance;
		/// <summary>
		/// скорость на каждом шаге меняется
		/// </summary>
		protected int speed;//устанавливается рандомно в своем диапазоне	
		public static Random rand;
		/// <summary>
		/// место которое заняли после гонки
		/// </summary>
		protected int winner_places;
		protected string type_car;//тип машины, спортивная и тп

		/// <summary>
		/// показываем движение машины
		/// </summary>
		public virtual void ShowCarInfo()
		{

			Console.Write("Name: " + name + "; Type of Car: " + type_car + "; Speed = " + Speed + "; current position: " + distance + "\n");

		}

		/// <summary>
		/// метод для отображения победителей и их места
		/// </summary>
		public virtual void ShowWhenEndPoint()
		{
			if (Winner_places == 1) Console.ForegroundColor = ConsoleColor.Green;
			else if (Winner_places == 2) Console.ForegroundColor = ConsoleColor.Magenta;
			else if (Winner_places == 3) Console.ForegroundColor = ConsoleColor.DarkCyan;
			else Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Name: " + name + "; Type of Car: " + type_car + "; current position: " + distance + " places: " + winner_places + "\n");
			Console.ResetColor();
		}

		/// <summary>
		/// доехали до конца или нет
		/// </summary>
		/// <returns></returns>
		public virtual bool CheckFInished()
		{
			return distance >= 100;
		}

		/// <summary>
		/// uses event inicilization
		/// </summary>
		public virtual void PrepearToStart()
		{
			distance = 0;
			if (Inicilization != null) Inicilization(this, null);
		}

		/// <summary>
		/// use event moveshow
		/// </summary>
		public virtual void MoveCar()
		{
			if (distance < 100)
			{
				distance += Speed;
				if (MoveShowEvent != null) MoveShowEvent(this, null);
				if (ChangeSpeed != null) ChangeSpeed(this, null);
			}
			if (distance >= 100) Onfinish(this, null);
		}

		#region констр и свойства


		public int Distance
		{
			get => distance;
		}
		public string Name { get => name; set => name = value; }
		public static Random Rand { get => rand; set => rand = value; }
		public int Winner_places { get => winner_places; set => winner_places = value; }
		public int Speed { get => speed; set => speed = value; }
		public string Type_car { get => type_car; set => type_car = value; }

		public Car(string name, string type_car)
		{
			if (rand == null) rand = new Random();
			this.name = name;
			this.distance = 100;
			Winner_places = -1;
			MoveShowEvent = null;
			Onfinish = null;
			Inicilization = null;
			ChangeSpeed = null;
			this.type_car = type_car;
		} 
		#endregion


	}
}
