using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	class PassengerCar:Car
	{



		public override void MoveCar()
		{
			base.Speed = rand.Next(8, 13);
			base.MoveCar();
		}

	
		public PassengerCar(string name,string type_car = "Passenger_Car") : base(name,type_car)
		{
			base.Speed = rand.Next(8, 12);
			
		}

	}
}
