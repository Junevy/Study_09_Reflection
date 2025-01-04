using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Study_09_Reflection {
    internal class Program {
        public static void Main(string[] args) {
            ITank tank = new HeavyTank();

            Type t = tank.GetType();
            object o = Activator.CreateInstance(t);
            MethodInfo mtd_1 = t.GetMethod("Run");
            MethodInfo mtd_2 = t.GetMethod("Fire");
            mtd_1.Invoke(o, null);
            mtd_2.Invoke(o, null);

            Console.WriteLine("===========================");

            var sc = new ServiceCollection();
            sc.AddScoped(typeof(ITank), typeof(HeavyTank));
            var sp = sc.BuildServiceProvider();
            ITank tank_1 = sp.GetService<ITank>();
            tank_1.Run();
            tank_1.Fire();


        }
    }

    interface IVehicle {
        void Run();
    }
    interface ITank : IVehicle, IWeapon{

    }
    interface IWeapon {
        void Fire();
    }

    class Car : IVehicle {
        public void Run() {
            Console.WriteLine("Car is running!");
        }
    }

    class TruckCar : IVehicle {
        public void Run() {
            Console.WriteLine("Truck is running");
        }
    }

    class HeavyTank : ITank {
        public void Run() {
            Console.WriteLine("Heavy Tank is running");
        }

        public void Fire() {
            Console.WriteLine("Boom...");
        }
    }

    class Driver {
        private IVehicle vehicle;
        public Driver(IVehicle vehicle) {
            this.vehicle = vehicle;
        }
        public void drive() {
            vehicle.Run();
        }
    }
}
