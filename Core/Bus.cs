using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	class Bus:Car
	{


		public override void MoveCar()
		{
			base.Speed = rand.Next(1, 6);
			base.MoveCar();
		}

	
		public Bus(string name, string type_car = "Bus") : base(name, type_car)
		{
			base.Speed = rand.Next(1, 5);	
		}
	}
}
