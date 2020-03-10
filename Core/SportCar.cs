using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Core
{
	public sealed class SportCar : Car
	{


		public override void MoveCar()
		{
			base.Speed = rand.Next(10, 16);
			base.MoveCar();			
		}
	
		public SportCar(string name ,string type_car = "Sport_Car") : base(name,type_car)
		{
			base.Speed = rand.Next(10, 15);			
		}		


	}
}
