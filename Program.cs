using System;

namespace src
{
    class Program
    {
        static void Column(int num_elevator)
        {
            static void createElevatorList(int elevatorNumber)
            {
                int[] elevatorIdList;
                int n = elevatorNumber;
                elevatorIdList = new int[n];

                for (int i = 0; i < n; i++)
                {
                    elevatorIdList[i]= i + 1;
                }
                foreach(var item in elevatorIdList)
                {
                    Console.WriteLine(item.ToString());
                }

            }
            createElevatorList(num_elevator);

        }
        static void Elevator(int num_floor, int num_elevator, int requestedFloor)
        {
            static void requestElevator(int floorNumber)
            {
                Console.WriteLine(floorNumber);
            }
            static void AssignElevator(int requestedFloor)
            {
                Console.WriteLine(requestedFloor);
            }
            requestElevator(num_floor);
            AssignElevator(requestedFloor);sl

        }
        static void Doors(int num_floor, int num_elevator)
        {
            Console.WriteLine("Doors just got collected:" + num_elevator +"-"+ num_floor);
        }
        static void Button(int num_floor, int num_elevator)
        {
            Console.WriteLine("Button just got collected:" + num_elevator +"-"+ num_floor);
        }
        static void Display(int num_floor, int num_elevator)
        {
            Console.WriteLine("Display just got collected:" + num_elevator +"-"+ num_floor);
        }
        static void Battery(int num_floor, int num_elevator)
        {
            Console.WriteLine("Battery just got collected:" + num_elevator +"-"+ num_floor);
        }




        static void Main(string[] args)
        {
            int num_floor = 10;
            int num_elevator = 10;
            int requestedFloor = 60;

            Column(num_elevator);
            Elevator(num_floor,num_elevator, requestedFloor);
            Doors(num_floor, num_elevator);
            Button(num_floor, num_elevator);
            Display(num_floor, num_elevator);
            Battery(num_floor, num_elevator);


        }
    }
}
