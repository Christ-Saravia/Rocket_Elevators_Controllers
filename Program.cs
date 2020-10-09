/*
DÉFINIR LES BESOINS:
    -Battery
    -Column
    -Elevator
    -CallButtons
    -Display   //Seulement si j'ai le temps
DÉFINIR LES MEMBRES APPROPRIÉS
    >Battery:
        -RequestElevator()
        -AssignElevator()
        -DistanceToTravel()
        -UpdatingMyList()
        -Move()
    >Column
        -CreateFloorList()
        -CreateElevatorList()
    >Elevator:
        -CreateElevator()
    
    >CallButtons:
        -CreateCallButtons()
*/
using System;
using System.Collections.Generic;
using System.Linq;


namespace src
{
    class CallButton
    {
        public string direction;
        public int floor;

        public CallButton(string _direction, int _floor)
        {
            direction = _direction;
            floor = _floor;
        }
    }
    class Elevator
    {
        public int identification;
        public int position = 1;
        public string direction = "UP";
        public List<int> stopList = new List<int>();
        public List<int> floorList = new List<int>();
        public string Door = "CLOSED";
        public string BufferDirection = "UP";
        public List<int> BufferList = new List<int>();


        public Elevator(int identification, int etageAppel, int etageEnd)
        {
            this.identification = identification;
            for (int i = etageAppel; i <= etageEnd; i = i + 1)
            {
                floorList.Add(i);
            }
        }
    }

    /*
    Cette class va me permettre de creer ma liste de floor, elevator, callButton tel que dans mon residentiel en python
    */
    class Column
    {
        public int identification;
        public List<Elevator> elevatorList = new List<Elevator>();
        public List<int> floorList = new List<int>();
        public List<CallButton> callButonList = new List<CallButton>();
        public Column(int identification, int etageAppel, int etageEnd, int numberOfElevators)
        {
            this.identification = identification;
            floorList.Add(1);

            for (int i = etageAppel; i <= etageEnd; i++)
            {
                floorList.Add(i);
            }

            for (int i = 1; i <= numberOfElevators;i++)
            {
                elevatorList.Add(new Elevator(i, etageAppel, etageEnd));
            }

            if (identification == 1)
            {
                for (int i = etageEnd; i <= etageAppel; i++)
                {
                    CallButton myCallButton = new CallButton("UP", i);
                    callButonList.Add(myCallButton);
                }
            }
            else
            {
                for (int i = etageAppel; i <= etageEnd; i++)
                {
                    CallButton myCallButton = new CallButton("DOWN", i);
                    callButonList.Add(myCallButton);
                }
            }
            Console.WriteLine("ColumnID: " + this.identification.ToString() + " supports these floors: " + string.Join(" | ", floorList));
        }
    }
    class Battery
    {
        public List<Column> columnList = new List<Column>();


        public Battery(int numberOfColumns, int num_floors, int basement, int nombreAscenseurColonne)
        {


            double nbFloors = (num_floors - basement);
            int moyEtageColonne = (int)Math.Floor(nbFloors / (numberOfColumns - 1));
            int etageAppel = 2;
            int etageEnd = moyEtageColonne;
            int identificationColonneActuel = 1;
            while (identificationColonneActuel <= numberOfColumns)
            {

                if (identificationColonneActuel == 1)
                {
                    Column column = new Column(1, -basement, -1, nombreAscenseurColonne);
                    columnList.Add(column);
                }

                else if (identificationColonneActuel < numberOfColumns)
                {
                    Column column = new Column(identificationColonneActuel, etageAppel, etageEnd, nombreAscenseurColonne);
                    columnList.Add(column);
                    etageAppel = etageEnd + 1;
                    etageEnd = etageEnd + moyEtageColonne;
                }
                else
                {
                    Column column = new Column(identificationColonneActuel, etageAppel, (int)nbFloors, nombreAscenseurColonne);
                    columnList.Add(column);
                }
                identificationColonneActuel = identificationColonneActuel + 1;
            }
        }
        public void RequestElevator(int floorNumber)
        {

            Column currentColumn = columnList[0];
            foreach (var item in columnList)
            {
                if (floorNumber >= item.floorList[1] && floorNumber <= item.floorList.Last())
                {
                    currentColumn = item;
                    break;
                }
            }

            string direction;
            if (floorNumber < 1)
            {
                direction = "UP";
            }
            else
            {
                direction = "DOWN";
            }
            Console.WriteLine("Serving column:  columnID {0} from floor {1} to floor {2}", currentColumn.identification, currentColumn.floorList[1], currentColumn.floorList.Last());

            int distanceToGo = 1000;
            int distance;
            List<Elevator> priority1 = new List<Elevator>();
            List<Elevator> priority2 = new List<Elevator>();
            List<Elevator> priority3 = new List<Elevator>();
            Elevator BestElevator = currentColumn.elevatorList[0];
            foreach (Elevator myElevator in currentColumn.elevatorList)
            {
                int currentDestination;
                if (myElevator.direction == "IDLE")
                {
                    currentDestination = 0;
                }
                else
                {
                    currentDestination = myElevator.stopList.Last();
                }
                Console.Write("111elID = {0}, elPos = {1}, elDir = {2}, current Destination = {3}", myElevator.identification, myElevator.position, myElevator.direction, currentDestination);
                distance = TravelDistance(myElevator, floorNumber, direction);
                Console.WriteLine(". distanceToGo: {0}", distance);
                if (myElevator.direction == direction)
                {
                    if ((direction == "UP" && myElevator.position <= floorNumber) | (direction == "DOWN" && myElevator.position >= floorNumber))
                    {
                        priority1.Add(myElevator);
                    }
                }
                else if (myElevator.direction == "IDLE")
                {
                    priority2.Add(myElevator);
                }
                else
                {
                    priority3.Add(myElevator);
                }
            }
            if (priority1.Count > 0)
            {
                foreach (Elevator myElevator in priority1)
                {
                    distance = TravelDistance(myElevator, floorNumber, direction);

                    if (distance <= distanceToGo)
                    {
                        distanceToGo = distance;
                        BestElevator = myElevator;
                    }
                }
            }
            else if (priority2.Count > 0)
            {
                foreach (Elevator myElevator in priority2)
                {
                    distance = TravelDistance(myElevator, floorNumber, direction);

                    if (distance <= distanceToGo)
                    {
                        distanceToGo = distance;
                        BestElevator = myElevator;
                    }
                }
            }
            else
            {
                foreach (Elevator myElevator in priority3)
                {
                    distance = TravelDistance(myElevator, floorNumber, direction);

                    if (distance <= distanceToGo)
                    {
                        distanceToGo = distance;
                        BestElevator = myElevator;
                    }
                }
            }

            if (BestElevator.direction == direction | BestElevator.direction == "IDLE")
            {
                if (BestElevator.direction == "DOWN" && BestElevator.position >= floorNumber)
                {
                    Console.Write(" Take column {0} ElevatorID: {1} which is currently at floor {2}. ", currentColumn.identification, BestElevator.identification, BestElevator.position);
                    UpdateList(BestElevator, BestElevator.stopList, floorNumber);
                    UpdateList(BestElevator, BestElevator.stopList, 1);
                    Console.WriteLine(" stopList: [ " + string.Join(" | ", BestElevator.stopList) + " ]");

                    move(BestElevator);
                }
                else if (BestElevator.direction == "UP" && BestElevator.position <= floorNumber)
                {
                    Console.Write(" Take column {0} ElevatorID: {1} which is currently at floor {2}. ", currentColumn.identification, BestElevator.identification, BestElevator.position);
                    UpdateList(BestElevator, BestElevator.stopList, floorNumber);
                    UpdateList(BestElevator, BestElevator.stopList, 1);
                    Console.WriteLine(" stopList: [ " + string.Join(" | ", BestElevator.stopList) + " ]");

                    move(BestElevator);
                }
                else if (BestElevator.direction == "IDLE")
                {
                    Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.identification, BestElevator.position);
                    UpdateList(BestElevator, BestElevator.stopList, floorNumber);
                    UpdateList(BestElevator, BestElevator.stopList, 1);
                    Console.WriteLine(" stopList: [ " + string.Join(" | ", BestElevator.stopList) + " ]");

                    move(BestElevator);
                }
                else
                {
                    Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.identification, BestElevator.position);
                    UpdateList(BestElevator, BestElevator.BufferList, floorNumber);
                    UpdateList(BestElevator, BestElevator.BufferList, 1);
                    Console.Write(" stopList: [ " + string.Join(" | ", BestElevator.stopList) + " ]");
                    Console.WriteLine(" BufferLsit: [ " + string.Join(" | ", BestElevator.BufferList) + " ]");

                    if (floorNumber > 1)
                    {
                        BestElevator.BufferDirection = "DOWN";
                        move(BestElevator);
                    }
                    else
                    {
                        BestElevator.BufferDirection = "UP";
                        move(BestElevator);
                    }
                }
            }
            else
            {
                Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.identification, BestElevator.position);
                UpdateList(BestElevator, BestElevator.BufferList, floorNumber);
                UpdateList(BestElevator, BestElevator.stopList, 1);
                Console.Write(" stopList: [ " + string.Join(" | ", BestElevator.stopList) + " ]");
                Console.WriteLine(" BufferLsit: [ " + string.Join(" | ", BestElevator.BufferList) + " ]");


                if (floorNumber > 1)
                {
                    BestElevator.BufferDirection = "DOWN";
                    move(BestElevator);
                }

                else
                {
                    BestElevator.BufferDirection = "UP";
                    move(BestElevator);
                }
            }
        }

        /*
            Ma methode servira pour les demandes faites au 1er etage
        */
        public void AssignElevator(int RequestedFloor)
        {

            Column currentColumn = columnList[0];
            foreach (var item in columnList)
            {

                if (RequestedFloor >= item.floorList[1] && RequestedFloor <= item.floorList.Last())
                {
                    currentColumn = item;
                    break;
                }
            }
            Console.WriteLine("the column identification for floor {0} is {1}", RequestedFloor, currentColumn.identification);
            int distanceToGo = 1000;
            Elevator BestElevator = currentColumn.elevatorList[0];
            string userDirection;
            if (RequestedFloor > 1)
            {
                userDirection = "UP";
            }
            else
            {
                userDirection = "DOWN";
            }

            foreach (Elevator elevator in currentColumn.elevatorList)
            {
                int currentDestination;
                if (elevator.direction == "IDLE")
                {
                    currentDestination = 0;
                }
                else
                {
                    currentDestination = elevator.stopList.Last();
                }
                Console.Write("111elID = {0}, elPos = {1}, elDir = {2}, current Destination = {3}", elevator.identification, elevator.position, elevator.direction, currentDestination);

                int distance = TravelDistance(elevator, 1, userDirection);
                Console.WriteLine(". distanceToGo: {0}", distance);

                if (distance <= distanceToGo)
                {
                    distanceToGo = distance;
                    BestElevator = elevator;
                }
            }
            Console.WriteLine("*** Take the column {0}, elevator {1} *** ", currentColumn.identification, BestElevator.identification);
            if (BestElevator.position == 1)
            {

                UpdateList(BestElevator, BestElevator.stopList, RequestedFloor);
            }
            else
            {

                UpdateList(BestElevator, BestElevator.BufferList, RequestedFloor);

                if (RequestedFloor > 1)
                {
                    BestElevator.BufferDirection = "DOWN";
                }

                else
                {
                    BestElevator.BufferDirection = "UP";
                }
            }
            move(BestElevator);


        }

        /*
            Ma methode permettra d'evaluer le nombre d'etage a parcourir
            pour atteindre la destination
        */
        public int TravelDistance(Elevator elevator, int UserPosition, string userDirection)
        {
            if (elevator.direction != "IDLE" | elevator.stopList.Count != 0)
            {
                if (elevator.direction == userDirection)
                {
                    if (elevator.direction == "UP" && elevator.position <= UserPosition)
                    {
                        return Math.Abs(elevator.position - UserPosition);
                    }
                    else if (elevator.direction == "DOWN" && elevator.position >= UserPosition)
                    {
                        return Math.Abs(elevator.position - UserPosition);
                    }
                    else
                    {
                        return Math.Abs(elevator.stopList.Last() - elevator.position) + Math.Abs(elevator.stopList.Last() - UserPosition);
                    }
                }
                else
                {
                    return Math.Abs(elevator.stopList.Last() - elevator.position) + Math.Abs(elevator.stopList.Last() - UserPosition); ;
                }
            }
            else
            {
                return Math.Abs(elevator.position - UserPosition);
            }
        }

        /*
            Ma methode permettra de faire un suivi de l'interaction des listes.
        */
        public void UpdateList(Elevator elevator, List<int> List, int position)
        {
            bool check = true;
            foreach (int stop in List)
            {
                if (stop == position)
                {
                    check = false;
                }
            }
            if (check)
            {
                List.Add(position);
                List.Sort();
            }
        }

        /*
            Ici un peu comme mon code de residentiel en python, je vais faire bouger les ascenseurs selon la direction 
        */
        public void move(Elevator elevator)
        {
            while (elevator.stopList.Count > 0)
            {
                if (elevator.stopList[0] > elevator.position)
                {
                    elevator.direction = "UP";
                    while (elevator.position < elevator.stopList[0])
                    {
                        elevator.position += 1;
                        if (elevator.position != 0)
                        {
                            Console.WriteLine("Elevator {0} is at floor {1} ", elevator.identification, elevator.position);
                        }
                        if (elevator.position == elevator.floorList.Last())
                        {
                            elevator.direction = "IDLE";
                        }
                    }
                    elevator.Door = "OPENED";
                    Console.WriteLine("Door is open");
                    elevator.stopList.RemoveAt(0);
                }
                else
                {
                    elevator.direction = "DOWN";
                    while (elevator.position > elevator.stopList.Last())
                    {
                        elevator.position -= 1;
                        if (elevator.position != 0)
                        {
                            Console.WriteLine("Elevator {0} is at floor {1} ", elevator.identification, elevator.position);
                        }
                        if (elevator.position == elevator.floorList.First())
                        {
                            elevator.direction = "IDLE";
                        }
                    }
                    elevator.Door = "OPENED";
                    Console.WriteLine("Door opening...");
                    Console.WriteLine("<DOOR OPEN>");
                    elevator.stopList.RemoveAt(elevator.stopList.Count - 1);
                }

                elevator.Door = "CLOSED";
                for(int i = 5; i > 0; --i){
                    Console.WriteLine("Door is closing in " + i + "...");
                }
                
                Console.WriteLine("<DOOR CLOSED>");
                elevator.direction = "IDLE";
            }
            if (elevator.BufferList.Count > 0)
            {
                elevator.stopList = elevator.BufferList;
                elevator.direction = elevator.BufferDirection;
                move(elevator);
            }
            else
            {
                elevator.direction = "IDLE";
            }
        }
    }

    
        enum CommercialBuilding
        {
            NumberOfFloor = 66,      
            Basement = 6,           
            Columns = 4,            
            Cages = 5,                      
        }

        enum SectionEtageCouvertA
        {
            L,
            B1,
            B2,
            B3,
            B4,
            B5,
            B6,

        }
        enum SectionEtageCouvertB
        {
            L,
            L2,
            L3,
            L4,
            L5,
            L6,
            L7,
            L8,
            L9,
            L10,
            L11,
            L12,
            L13,
            L14,
            L15,
            L16,
            L17,
            L18,
            L19,
            L20,
        }

        enum SectionEtageCouvertC
        {
            L,
            L21,
            L22,
            L23,
            L24,
            L25,
            L26,
            L27,
            L28,
            L29,
            L30,
            L31,
            L32,
            L33,
            L34,
            L35,
            L36,
            L37,
            L38,
            L39,
            L40
        }
        enum SectionEtageCouvertD
        {
            L,
            L41,
            L42,
            L43,
            L44,
            L45,
            L46,
            L47,
            L48,
            L49,
            L50,
            L51,
            L52,
            L53,
            L54,
            L55,
            L56,
            L57,
            L58,
            L59,
            L60,
        }

        enum ScenarioA
        {

        }

        enum ScenarioB
        {

        }

        enum ScenarioC
        {

        }

        enum ScenarioD
        {

        }
    class Program
    {
        static void Main(string[] args)
        {
            int myNumOfFloor = (int)CommercialBuilding.NumberOfFloor;
            int myBasement = (int)CommercialBuilding.Basement;
            int myColumns = (int)CommercialBuilding.Columns;
            int myCages =(int)CommercialBuilding.Cages;

            // Prompt user to enter a name.
            Console.WriteLine("Enter your scenario, please:");

            // Now read the name entered.
            string scenario = Console.ReadLine();

            // Greet the user with the name that was entered.
            Console.WriteLine("Scenario, " + scenario);

            // Wait for user to acknowledge the results.

            Console.WriteLine("Press Enter to terminate...");
            Console.Read();
            Console.WriteLine(myNumOfFloor);
            Console.WriteLine(myBasement);
            Console.WriteLine(myColumns);
            Console.WriteLine(myCages);
            Battery myBattery = new Battery(myColumns,myNumOfFloor,myBasement,myCages);
        }
    }
}
