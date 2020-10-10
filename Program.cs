using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace Commercial_Controller.cs
{
    
    class CallButton
    {
        public string Direction;
        public int Floor;

        // Construction of a CallButton
        public CallButton(string d, int f)
        {
            this.Direction = d;
            this.Floor = f;
        }
     
    }
  
    class Elevator
    {
        public int ID;
        public int Position = 1;
        public string Direction = "UP";
        public List<int> StopList = new List<int>();
        public List<int> floorList = new List<int>();
        public string Door = "CLOSED";
        public string BufferDirection = "UP";
        public List<int> BufferList = new List<int>();
        
        // Construction of an elevator
        public Elevator(int ID, int fromFloor, int toFloor)
        {
            this.ID = ID;
            for (int i = fromFloor; i <= toFloor; ++i)
            {
                this.floorList.Add(i);
            }
        }
    }

    class Column
    {
        public int ID;
        public List<Elevator> elevatorList = new List<Elevator>();
        public List<int> floorList = new List<int>();
        public List<CallButton> callButonList = new List<CallButton>();
        
        // Construction of a column
        public Column(int ID, int fromFloor, int toFloor, int numberOfElevators)
        {
            this.ID = ID;
            this.floorList.Add(1);
            // Creating Floor List for each Column
            for (int i = fromFloor; i <= toFloor; ++i)
            {
                this.floorList.Add(i);
            }
            // Creating Elevator List for each Column by creating new elevators,
	        // knowing the range of floors for each column (fromFloor - toFloor)
            for (int i = 1; i <= numberOfElevators; ++i)
            {
                this.elevatorList.Add(new Elevator(i, fromFloor, toFloor));
            }
            // Creating CallButtons for Basements (at if clause) and then for floors (at else clause)
	        // knowing the range of floors for each column (fromFloor - toFloor)
            if (ID == 1)
            {
                for (int i = toFloor; i <= fromFloor; ++i)
                {
                    CallButton callButton = new CallButton("UP", i);
                    this.callButonList.Add(callButton);
                }
            }
            else
            {
                for (int i = fromFloor; i <= toFloor; ++i)
                {
                    CallButton callButton = new CallButton("DOWN", i);
                    this.callButonList.Add(callButton);
                }
            }
            String identification;
            switch(this.ID){
                case 1:
                identification = "A";
                Console.WriteLine("ColumnID: " + identification + " supports these floors: " + string.Join(" | ", this.floorList));
                break;
                case 2:
                identification = "B";
                Console.WriteLine("ColumnID: " + identification + " supports these floors: " + string.Join(" | ", this.floorList));
                break;
                case 3:
                identification = "C";
                Console.WriteLine("ColumnID: " + identification + " supports these floors: " + string.Join(" | ", this.floorList));
                break;
                case 4:
                identification = "D";
                Console.WriteLine("ColumnID: " + identification + " supports these floors: " + string.Join(" | ", this.floorList));
                break;
            }
        }
    }
    class Battery
    {
        public List<Column> columnList = new List<Column>();
        
        // Construction of a battery
        public Battery(int numberOfColumns, int totalNbFloors, int nbBasem, int numberOfElevatorPerColumn)
        {
            
            // nbFloors includes all the floors excluding the Ground Floor and the basements
            double nbFloors = totalNbFloors - nbBasem;
            int avgFloorPerColumn = (int)Math.Floor(nbFloors / (numberOfColumns - 1));
            int fromFloor = 2;
            int toFloor = avgFloorPerColumn;
            int currentColumnID = 1;
            while (currentColumnID <= numberOfColumns)
            {
                // instantiation of the 1st column for the basements
                if (currentColumnID == 1)
                {
                    Column column = new Column(1, -nbBasem, - 1, numberOfElevatorPerColumn);
                    this.columnList.Add(column);
                }
                // instantiation of the other columns
                else if(currentColumnID < numberOfColumns)
                {
                    Column column = new Column(currentColumnID, fromFloor, toFloor, numberOfElevatorPerColumn);
                    this.columnList.Add(column);
                    fromFloor = toFloor + 1;
                    toFloor = toFloor + avgFloorPerColumn;
                }
                else
                {
                    Column column = new Column(currentColumnID, fromFloor, (int)nbFloors, numberOfElevatorPerColumn);
                    this.columnList.Add(column);
                }
                currentColumnID = currentColumnID + 1;
            }

        }
        
        public void RequestElevator(int FloorNumber)
        {
            // Finding the proper column
            Column currentColumn = this.columnList[0];
            foreach(var item in this.columnList)
            {
                if ( FloorNumber >= item.floorList[1] && FloorNumber <= item.floorList.Last()) 
                {
                    currentColumn = item;
                    break;
                }
            }

            // determining the USER Direction
            string Direction;
            if (FloorNumber < 1)
            {
                Direction = "UP";
            }
            else 
            {
                Direction = "DOWN";
            }
            Console.WriteLine("Serving column:  columnID {0} from floor {1} to floor {2}", currentColumn.ID, currentColumn.floorList[1], currentColumn.floorList.Last());
            // finding the closest elevator Based on on comparing DistanceToGo AND the following Priority: 
            //      1- the moving elevator which is arriving to the user
            //      2- the IDLE elevator
            //      3- other elevators
            int DistanceToGo = 1000;
            int distance;
            List<Elevator> FirstPriority = new List<Elevator>();
            List<Elevator> SecondPriority = new List<Elevator>();
            List<Elevator> ThirdPriority = new List<Elevator>();
            Elevator BestElevator = currentColumn.elevatorList[0];
            foreach(Elevator elev in currentColumn.elevatorList)
            {   
                int currentDestination;
                if (elev.Direction == "IDLE")
                {
                    currentDestination = 0;
                }
                else
                {
                    currentDestination = elev.StopList.Last();
                }
                Console.Write("Elevator ID = {0}, PositonOfElevator = {1}, DirectionOfElevator = {2}, current Destination = {3}", elev.ID, elev.Position, elev.Direction, currentDestination);
                distance = CalculateDistanceToGo(elev, FloorNumber, Direction);
                Console.WriteLine(". distanceToGo: {0}", distance);  
                if (elev.Direction == Direction)
                {
                    if ((Direction == "UP" && elev.Position <= FloorNumber) | (Direction == "DOWN" && elev.Position >= FloorNumber))
                    {
                        FirstPriority.Add(elev);
                    }
                }
                else if (elev.Direction == "IDLE") 
                {
                    SecondPriority.Add(elev);
                }
                else
                {
                    ThirdPriority.Add(elev);
                }
            }
            if (FirstPriority.Count > 0)
            {
                foreach (Elevator elev in FirstPriority)
                {
                    distance = CalculateDistanceToGo(elev, FloorNumber, Direction);
                             
                    if ( distance <= DistanceToGo)
                    {
                        DistanceToGo = distance;
                        BestElevator = elev;
                    }
                }   
            }
            else if (SecondPriority.Count > 0)
            {
                foreach (Elevator elev in SecondPriority)
                {
                    distance = CalculateDistanceToGo(elev, FloorNumber, Direction);
                            
                    if ( distance <= DistanceToGo)
                    {
                        DistanceToGo = distance;
                        BestElevator = elev;
                    }
                }
            }
            else
            {
                foreach (Elevator elev in ThirdPriority)
                {
                    distance = CalculateDistanceToGo(elev, FloorNumber, Direction);
                     
                    if ( distance <= DistanceToGo)
                    {
                        DistanceToGo = distance;
                        BestElevator = elev;
                    }
                }
            }
            
            // Updating the STOPLIST of the selected elevator
            if (BestElevator.Direction == Direction | BestElevator.Direction == "IDLE"){
                if (BestElevator.Direction == "DOWN" && BestElevator.Position >= FloorNumber){        
                    Console.Write(" Take column {0} ElevatorID: {1} which is currently at floor {2}. ", currentColumn.ID, BestElevator.ID, BestElevator.Position);
                    Thread.Sleep(2000);  
                    UpdateList(BestElevator, BestElevator.StopList, FloorNumber);
                    UpdateList(BestElevator, BestElevator.StopList, 1);
                    Console.WriteLine(" StopList: [ " + string.Join(" | ", BestElevator.StopList) + " ]");
                    
                    move(BestElevator);
                }
                else if (BestElevator.Direction == "UP" && BestElevator.Position <= FloorNumber)
                {
                    Console.Write(" Take column {0} ElevatorID: {1} which is currently at floor {2}. ", currentColumn.ID, BestElevator.ID, BestElevator.Position);
                    Thread.Sleep(2000); 
                    UpdateList(BestElevator, BestElevator.StopList, FloorNumber);
                    UpdateList(BestElevator, BestElevator.StopList, 1);
                    Console.WriteLine(" StopList: [ " + string.Join(" | ", BestElevator.StopList) + " ]"); 
                    
                    move(BestElevator);
                }
                else if (BestElevator.Direction == "IDLE")
                {
                    Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.ID, BestElevator.Position);
                    Thread.Sleep(2000);
                    UpdateList(BestElevator, BestElevator.StopList, FloorNumber);
                    UpdateList(BestElevator, BestElevator.StopList, 1);
                    Console.WriteLine(" StopList: [ " + string.Join(" | ", BestElevator.StopList) + " ]");
                    
                    move(BestElevator);
                }
                else
                {
                    Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.ID, BestElevator.Position);
                    Thread.Sleep(2000);
                    UpdateList(BestElevator, BestElevator.BufferList, FloorNumber);
                    UpdateList(BestElevator, BestElevator.BufferList, 1);
                    Console.Write(" StopList: [ " + string.Join(" | ", BestElevator.StopList) + " ]");
                    Console.WriteLine(" BufferLsit: [ " + string.Join(" | ", BestElevator.BufferList) + " ]");
                    
                    if (FloorNumber > 1)
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
            } else{
                Console.Write(" Take ElevatorID: {0} which is currently at floor {1}. ", BestElevator.ID, BestElevator.Position);
                Thread.Sleep(2000);
                UpdateList(BestElevator, BestElevator.BufferList, FloorNumber);
                UpdateList(BestElevator, BestElevator.StopList, 1);
                Console.Write(" StopList: [ " + string.Join(" | ", BestElevator.StopList) + " ]");
                Console.WriteLine(" BufferLsit: [ " + string.Join(" | ", BestElevator.BufferList) + " ]");

                
                // For Basements
                if (FloorNumber > 1)
                {
                    BestElevator.BufferDirection = "DOWN";
                    move(BestElevator);
                }
                // For other floors
                else
                {
                    BestElevator.BufferDirection = "UP";
                    move(BestElevator);
                }
            }
        }

        public void AssignElevator(int RequestedFloor)
        {
            // Finding the proper column
            Column currentColumn = this.columnList[0];
            foreach(var item in this.columnList)
            {
                
                if ( RequestedFloor >= item.floorList[1] && RequestedFloor <= item.floorList.Last()) 
                {
                    currentColumn = item;
                    break;
                }
            }
           Console.WriteLine("the column ID for floor {0} is {1}", RequestedFloor, currentColumn.ID);
           int DistanceToGo = 1000;
           Elevator BestElevator = currentColumn.elevatorList[0];
           string UserDirection;
           if (RequestedFloor > 1)
           {
               UserDirection = "UP";
           }
           else
           {
               UserDirection = "DOWN";
           }
           // Finding the best elevator based on comparing DistanceToGo
           foreach(Elevator elevator in currentColumn.elevatorList)
           {    
                int currentDestination;
                if (elevator.Direction == "IDLE")
                {
                    currentDestination = 0;
                }
                else
                {
                    currentDestination = elevator.StopList.Last();
                }
                Console.Write("Elevator ID = {0}, PositonOfElevator = {1}, DirectionOfElevator = {2}, current Destination = {3}", elevator.ID, elevator.Position, elevator.Direction, currentDestination);
                
                int distance = CalculateDistanceToGo(elevator, 1, UserDirection);
                Console.WriteLine(". distanceToGo: {0}", distance);  
                         
                if ( distance <= DistanceToGo)
                {
                    DistanceToGo = distance;
                    BestElevator = elevator;
                }
           }
           Console.WriteLine("*** Take the column {0}, elevator {1} *** ", currentColumn.ID, BestElevator.ID);
           if (BestElevator.Position == 1)
           {
                
                UpdateList(BestElevator, BestElevator.StopList, RequestedFloor);    
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
        public int CalculateDistanceToGo(Elevator elevator, int UserPosition, string UserDirection)
        {
            if (elevator.Direction != "IDLE" | elevator.StopList.Count != 0)
            {
                if (elevator.Direction == UserDirection)
                {
                    if (elevator.Direction == "UP" && elevator.Position <= UserPosition) 
                    {
                        return Math.Abs(elevator.Position - UserPosition);
                    }
                    else if (elevator.Direction == "DOWN" && elevator.Position >= UserPosition)
                    {
                        return Math.Abs(elevator.Position - UserPosition);
                    }
                    else
                    {
                        return Math.Abs(elevator.StopList.Last() - elevator.Position) + Math.Abs(elevator.StopList.Last() - UserPosition);    
                    }
                }
                else
                {
                    return Math.Abs(elevator.StopList.Last() - elevator.Position) + Math.Abs(elevator.StopList.Last() - UserPosition);;    
                }
            }
            else 
            {
                return Math.Abs(elevator.Position - UserPosition);
            }
        }
        public void UpdateList (Elevator elevator, List<int> List, int Position)
        {
            bool check = true;
            foreach(int stop in List)
            {
                if (stop == Position)
                {
                    check = false;
                }
            }
            if (check)
            {
                List.Add(Position);
                List.Sort();
            }
        }
        public void move(Elevator elevator)
        {
            while (elevator.StopList.Count > 0)
            {
                if (elevator.StopList[0] > elevator.Position)
                {
                    elevator.Direction = "UP";
                    while (elevator.Position < elevator.StopList[0])
                    {
                        elevator.Position += 1;
                        if (elevator.Position != 0)
                        {
                            Console.WriteLine("Elevator {0} is at floor {1} ", elevator.ID, elevator.Position);
                            Thread.Sleep(2000);
                        }
                        if (elevator.Position == elevator.floorList.Last())
                        {
                            elevator.Direction = "IDLE";
                        }
                    }
                    elevator.Door = "OPENED";
                    for(int s = 5; s > 0; s--){
                        Console.Beep(); 
                    }
                    Console.WriteLine("DOOR IS OPENED");
                    elevator.StopList.RemoveAt(0);
                }
                else 
                {
                    elevator.Direction = "DOWN";
                    while (elevator.Position > elevator.StopList.Last())
                    {
                        elevator.Position -= 1;
                        if (elevator.Position != 0)
                        {
                            Console.WriteLine("Elevator {0} is at floor {1} ", elevator.ID, elevator.Position);
                            Thread.Sleep(2000);
                        }
                        if (elevator.Position == elevator.floorList.First())
                        {
                            elevator.Direction = "IDLE";
                        }
                    }
                    elevator.Door = "OPENED";
                    Console.WriteLine("DOOR OPENING...");
                    for(int t = 5; t >0; t--){
                        Thread.Sleep(1000); 
                        Console.WriteLine(t);
                    }
                    for(int s = 5; s > 0; s--){
                        Console.Beep(); 
                    }
                    Console.WriteLine("DOOR IS OPENED");
                    elevator.StopList.RemoveAt(elevator.StopList.Count -1);
                }
                
                elevator.Door = "CLOSED";
                Console.WriteLine("DOOR IS CLOSING IN: ");
                for(int t = 5; t >0; t--){
                    Thread.Sleep(1000); 
                    Console.WriteLine(t);
                }
                for(int s = 5; s > 0; s--){
                    Console.Beep(); 
                }
                Console.WriteLine("DOOR CLOSED");
                Thread.Sleep(2000); 

                elevator.Direction = "IDLE";
            }
            if (elevator.BufferList.Count > 0)
            {
                elevator.StopList = elevator.BufferList;
                elevator.Direction = elevator.BufferDirection;
                move(elevator);
            }
            else 
            {
                elevator.Direction = "IDLE";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Process.Start("../myLogo.bat");
            Thread.Sleep(2000);   
            
            // Prompt user to enter a scenario [1 to 4].
            Console.WriteLine("\nPlease choose a number between 1 and 4.\nIf you attempt another number it is at your own risk!\nDon't try!\nEnter your scenario:");

            // Now read the scenario entered.
            string valeur = Console.ReadLine();
            int scenario = Convert.ToInt32(valeur);

            // Greet the user with the scenario that was entered.
            Console.WriteLine("Scenario, " + scenario);
            


            if(scenario < 1 || scenario > 4){
                int mystery = scenario;
                mystery = mystery * 10;
                while(mystery > 0){
                    Console.WriteLine("YOU WERE WARNED!");
                    Thread.Sleep(100); 
                    mystery--;
                }
            }
            else{
                Console.WriteLine("Press Enter to terminate...");
                Console.Read();
            }


            switch(scenario) 
            {
            case 1:
            void Scenario1 ()
            {    
                Console.WriteLine(">>>>>>>>>   SCENARIO 1  <<<<<<<<<<");
                
                Battery Battery1 = new Battery(4, 66, 6, 5);

                Battery1.columnList[1].elevatorList[0].Position = 20;
                Battery1.columnList[1].elevatorList[0].Direction = "DOWN";
                Battery1.columnList[1].elevatorList[0].StopList.Add(5);

                Battery1.columnList[1].elevatorList[1].Position = 2;
                Battery1.columnList[1].elevatorList[1].Direction = "UP";
                Battery1.columnList[1].elevatorList[1].StopList.Add(15);

                Battery1.columnList[1].elevatorList[2].Position = 13;
                Battery1.columnList[1].elevatorList[2].Direction = "DOWN";
                Battery1.columnList[1].elevatorList[2].StopList.Add(1);

                Battery1.columnList[1].elevatorList[3].Position = 15;
                Battery1.columnList[1].elevatorList[3].Direction = "DOWN";
                Battery1.columnList[1].elevatorList[3].StopList.Add(2);

                Battery1.columnList[1].elevatorList[4].Position = 6;
                Battery1.columnList[1].elevatorList[4].Direction = "DOWN";
                Battery1.columnList[1].elevatorList[4].StopList.Add(1);
                
                Console.WriteLine("\nUser at floor 1. He goes UP to floor 20\n");
                Thread.Sleep(2000); 
                Console.WriteLine("Elevator 5 from Column 2 is expected");
                
                Battery1.AssignElevator(20);       
            }
            Scenario1(); 
            break;
            case 2:
            void Scenario2 ()
            {
                
                Console.WriteLine(">>>>>>>>>   SCENARIO 2  <<<<<<<<<<");
                
                Battery Battery2 = new Battery(4, 66, 6, 5);
      
                Battery2.columnList[2].elevatorList[0].Position = 1;
                Battery2.columnList[2].elevatorList[0].Direction = "UP";
                Battery2.columnList[2].elevatorList[0].StopList.Add(21);
 
                Battery2.columnList[2].elevatorList[1].Position = 23;
                Battery2.columnList[2].elevatorList[1].Direction = "UP";
                Battery2.columnList[2].elevatorList[1].StopList.Add(28);
 
                Battery2.columnList[2].elevatorList[2].Position = 33;
                Battery2.columnList[2].elevatorList[2].Direction = "DOWN";
                Battery2.columnList[2].elevatorList[2].StopList.Add(1);
    
                Battery2.columnList[2].elevatorList[3].Position = 40;
                Battery2.columnList[2].elevatorList[3].Direction = "DOWN";
                Battery2.columnList[2].elevatorList[3].StopList.Add(24);
        
                Battery2.columnList[2].elevatorList[4].Position = 39;
                Battery2.columnList[2].elevatorList[4].Direction = "DOWN";
                Battery2.columnList[2].elevatorList[4].StopList.Add(1);
               
                Console.WriteLine(" \nUser at floor 1. He goes UP to floor 36\n");
                Thread.Sleep(2000); 
                Console.WriteLine(" Elevator 1 from Column 3 is expected ");
                
                Battery2.AssignElevator(36);   
            }
            Scenario2();
            break;
            case 3:
            void Scenario3 ()
            {
                
                Console.WriteLine(">>>>>>>>>   SCENARIO 3  <<<<<<<<<<");
                
                Battery Battery3 = new Battery(4, 66, 6, 5);
      
                Battery3.columnList[3].elevatorList[0].Position = 58;
                Battery3.columnList[3].elevatorList[0].Direction = "DOWN";
                Battery3.columnList[3].elevatorList[0].StopList.Add(1);
     
                Battery3.columnList[3].elevatorList[1].Position =50;
                Battery3.columnList[3].elevatorList[1].Direction = "UP";
                Battery3.columnList[3].elevatorList[1].StopList.Add(60);
              
                Battery3.columnList[3].elevatorList[2].Position = 46;
                Battery3.columnList[3].elevatorList[2].Direction = "UP";
                Battery3.columnList[3].elevatorList[2].StopList.Add(58);
       
                Battery3.columnList[3].elevatorList[3].Position = 1;
                Battery3.columnList[3].elevatorList[3].Direction = "UP";
                Battery3.columnList[3].elevatorList[3].StopList.Add(54);

                Battery3.columnList[3].elevatorList[4].Position = 60;
                Battery3.columnList[3].elevatorList[4].Direction = "DOWN";
                Battery3.columnList[3].elevatorList[4].StopList.Add(1);
                
                   
                Console.WriteLine("\nUser at floor 54. He goes DOWN to floor 1\n ");
                Thread.Sleep(2000); 
                Console.WriteLine("Elevator 1 from Column 4 is expected");
                
                Battery3.RequestElevator(54);       
            }
            Scenario3();
            break;
            case 4:
            void Scenario4 ()
            {
                
                Console.WriteLine(">>>>>>>>>   SCENARIO 4  <<<<<<<<<<");
                
                Battery Battery4 = new Battery(4, 66, 6, 5);
       
                Battery4.columnList[0].elevatorList[0].Position = -4;
                Battery4.columnList[0].elevatorList[0].Direction = "IDLE";
                
             
                Battery4.columnList[0].elevatorList[1].Position = 1;
                Battery4.columnList[0].elevatorList[1].Direction = "IDLE";
                
             
                Battery4.columnList[0].elevatorList[2].Position = -3;
                Battery4.columnList[0].elevatorList[2].Direction = "DOWN";
                Battery4.columnList[0].elevatorList[2].StopList.Add(-5);
    
                Battery4.columnList[0].elevatorList[3].Position = -6;
                Battery4.columnList[0].elevatorList[3].Direction = "UP";
                Battery4.columnList[0].elevatorList[3].StopList.Add(1);

                Battery4.columnList[0].elevatorList[4].Position = -1;
                Battery4.columnList[0].elevatorList[4].Direction = "DOWN";
                Battery4.columnList[0].elevatorList[4].StopList.Add(-6);
                
                
                Console.WriteLine("\nUser at floor -3. He goes UP to floor 1\n");
                Thread.Sleep(2000); 
                Console.WriteLine(" Elevator 4 from Column 1 is expected");
                
                Battery4.RequestElevator(-3);
            }
            Scenario4();    
            break;
            default:
            Console.Clear();
            Thread.Sleep(3000); 
            System.Diagnostics.Process.Start("../warning.bat");
            Thread.Sleep(3000);
            Console.Clear();
            Thread.Sleep(3000);
            Console.WriteLine("BON WEEK-END DE L'ACTION DE GRACE CORRECTEUR!");  
            break;
            }
        }
    }
}