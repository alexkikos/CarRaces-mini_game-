using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	class Truck : Car
	{

	

		public override void MoveCar()
		{
			base.Speed = rand.Next(3, 9);
			base.MoveCar();
		}

		public Truck(string name, string type_car = "Truck_Car") : base(name, type_car)
		{
			base.Speed = rand.Next(3, 8);		
		}
	}
}
